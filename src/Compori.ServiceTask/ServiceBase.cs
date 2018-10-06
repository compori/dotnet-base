using System;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.ServiceTask
{
    /// <summary>
    /// Class ServiceBase.
    /// </summary>
    public abstract class ServiceBase : IDisposable
    {
        /// <summary>
        /// The log facility
        /// </summary>
        protected NLog.ILogger Log => NLog.LogManager.GetLogger(this.GetType().FullName);

        /// <summary>
        /// The lock object
        /// </summary>
        private readonly object _lockObject = new object();

        /// <summary>
        /// The main task
        /// </summary>
        protected Task task;

        /// <summary>
        /// The cancel execution token source
        /// </summary>
        private CancellationTokenSource _cancelExecution = null;

        /// <summary>
        /// The cancel delay repetition token source
        /// </summary>
        private CancellationTokenSource _cancelDelay = null;

        /// <summary>
        /// Gets the delay cancellation token source.
        /// </summary>
        /// <returns>CancellationTokenSource.</returns>
        private CancellationTokenSource _GetDelayCancellationTokenSource()
        {
            lock (this._lockObject)
            {
                if (this._cancelDelay == null)
                {
                    this._cancelDelay = new CancellationTokenSource();
                }
                return this._cancelDelay;
            }
        }

        /// <summary>
        /// Resets the delay cancellation token source.
        /// </summary>
        private void _ResetDelayCancellationTokenSource()
        {
            lock (this._lockObject)
            {
                //
                // No cancellation source created.
                // 
                if (this._cancelDelay == null)
                {
                    return;
                }

                //
                // Cancellation source is not triggered yet.
                //
                if (!this._cancelDelay.IsCancellationRequested)
                {
                    return;
                }

                //
                // Reset cancellation source
                //
                this._cancelDelay.Dispose();
                this._cancelDelay = null;
            }
        }

        /// <summary>
        /// The settings
        /// </summary>
        private ServiceSettings _settings;

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public ServiceSettings Settings { get => _settings.Clone(); }

        /// <summary>
        /// Gets or sets the name of this service.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                var name = this._settings.Name;
                if (name == null)
                {
                    return "N/A";
                }
                return name.Clone() as string;
            }
        }

        /// <summary>
        /// The repetitions
        /// </summary>
        private long _executions = 0;

        /// <summary>
        /// Gets the executions.
        /// </summary>
        /// <value>The executions.</value>
        public long Executions { get => this._executions; }

        /// <summary>
        /// The a error counter
        /// </summary>
        private int _errors = 0;

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public long Errors { get => this._errors; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        protected ServiceBase(ServiceSettings settings)
        {
            Guard.AssertArgumentIsNotNull(settings, nameof(settings));

            this._cancelExecution = new CancellationTokenSource();
            this._settings = settings.Clone();
        }

        /// <summary>
        /// Executes the service task with a specified cancelation token.
        /// </summary>
        /// <param name="token">The token.</param>
        protected abstract void Execute(CancellationToken token);

        /// <summary>
        /// Waits the next execution.
        /// </summary>
        /// <param name="executionToken">The execution token.</param>
        /// <param name="delayToken">The delay token.</param>
        /// <param name="delay">The delay.</param>
        private void _DelayNextExecution(CancellationToken executionToken, CancellationToken delayToken, TimeSpan delay)
        {
            if (delayToken.IsCancellationRequested)
            {
                this._ResetDelayCancellationTokenSource();
                return;
            }

            Log.Debug("Next execution will be delayed for {0}.", delay.ToString());

            try
            {
                var token = CancellationTokenSource.CreateLinkedTokenSource(executionToken, delayToken).Token;
                Task.Delay(delay, token).Wait(token);
            }

            catch (OperationCanceledException)
            {
                //
                // Only delay should be canceld
                //
                if (delayToken.IsCancellationRequested)
                {
                    this._ResetDelayCancellationTokenSource();
                    return;
                }

                //
                // otherwise rethrow exception (cancel whole execution)
                //
                throw;
            }
        }

        /// <summary>
        /// Processes the repetititon.
        /// </summary>
        /// <param name="executionToken">The execution token.</param>
        private bool _ProcessRepetititon(CancellationToken executionToken)
        {
            //
            // No repetition
            //
            if (!this._settings.IsRepeatable)
            {
                return false;
            }

            //
            // Check maximum if maximum repetitions is reached
            //
            if (this._settings.MaximumExecutions > 0 && this._settings.MaximumExecutions <= this._executions)
            {
                Log.Debug("Maximum executions of {0} reached.", this._settings.MaximumExecutions);

                return false;
            }

            //
            // Check if delay until the next repetition execution is set.
            //
            if (this._settings.DelayNextExecution.Ticks > 0)
            {
                this._DelayNextExecution(executionToken, _GetDelayCancellationTokenSource().Token, this._settings.DelayNextExecution);
            }

            return true;
        }

        /// <summary>
        /// Executes the main task with a the cancel token.
        /// </summary>
        /// <param name="executionToken">The token.</param>
        private void _Execute(CancellationToken executionToken)
        {
            try
            {
                bool executeAgain = false;

                do
                {
                    bool errorOccurs = false;
                    bool cancelOccurs = false;

                    try
                    {
                        Interlocked.Increment(ref this._executions);

                        Log.Debug("Start {0}. execution.", this._executions);

                        this.Execute(executionToken);

                        Log.Debug("Finished {0}. execution.", this._executions);

                        //
                        // Retrieve if the execution should be repeated
                        //
                        executeAgain = _ProcessRepetititon(executionToken);
                    }

                    catch (AggregateException ex)
                    {
                        foreach (var inner in ex.InnerExceptions)
                        {
                            if (inner is OperationCanceledException canceledException)
                            {
                                cancelOccurs = true;
                                Log.Debug(canceledException, "Cancel operation exception thrown.");
                                continue;
                            }

                            Log.Fatal(inner);
                            errorOccurs = true;
                        }
                    }

                    catch (OperationCanceledException ex)
                    {
                        cancelOccurs = true;
                        Log.Debug(ex, "Cancel operation exception thrown.");
                    }

                    catch (Exception ex)
                    {
                        Log.Fatal(ex);
                        errorOccurs = true;
                    }

                    if (errorOccurs)
                    {
                        Interlocked.Increment(ref this._errors);

                        if (this._settings.RetryOnError)
                        {
                            executeAgain = true;

                            if (this._settings.DelayError.Ticks > 0)
                            {
                                Log.Debug("Next execution will be delayed due to an error.");

                                this._DelayNextExecution(executionToken, this._GetDelayCancellationTokenSource().Token, this._settings.DelayError);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    //
                    // Cancel is requested
                    //
                    if (cancelOccurs)
                    {
                        break;
                    }

                } while (executeAgain);

                Log.Debug("Exit execution.");
            }

            catch (OperationCanceledException canceledException)
            {
                Log.Debug(canceledException, "Cancel operation exception thrown.");
            }

            catch (Exception ex)
            {
                Log.Fatal(ex);
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            lock (this._lockObject)
            {
                var name = this.Name;

                if (this.task == null)
                {
                    Log.Debug("Starting the service '{0}'.", name);

                    this._errors = 0;
                    this._executions = 0;


                    //
                    // Create a new main task with a cancel token
                    //
                    this._cancelExecution = new CancellationTokenSource();
                    var token = this._cancelExecution.Token;

                    this.task = new Task(() =>
                    {
                        Log.Trace("Start executing service task: Task.CurrentId={0} Thread.CurrentThread.ManagedThreadId={1}.", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                        this._Execute(token);
                        Log.Trace("Finished service executing task: Task.CurrentId={0} Thread.CurrentThread.ManagedThreadId={1}.", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                    }, token);

                    this.task.Start();

                    Log.Debug("The service '{0}' started.", name);

                    return;
                }

                //
                // If task is finished restart it.
                //
                if (this.task.IsCompleted)
                {
                    this.task = null;
                    this.Start();
                    return;
                }

                //
                // Task already running
                // 
                Log.Debug("The service '{0}' already running.", name);


                //
                // But if execution is repeatable, cancel the delay repetition.
                //
                if (this._settings.IsRepeatable && this._settings.DelayNextExecution.Ticks > 0)
                {
                    Log.Debug("Cancel delaying next execution on service '{0}'.", name);
                    this._GetDelayCancellationTokenSource().Cancel();
                }
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            lock (this._lockObject)
            {
                var name = this.Name;

                if (this.task != null)
                {
                    Log.Debug("Stopping the service '{0}'.", name);

                    //
                    // Cancel task, wait until execution finished and clean up.
                    // 
                    this._cancelExecution.Cancel();
                    this.task.Wait();
                    this.task = null;

                    // 
                    // Clean up cancel execution token source
                    // 
                    this._cancelExecution.Dispose();
                    this._cancelExecution = null;

                    //
                    // and cancel delay token source
                    //
                    this._ResetDelayCancellationTokenSource();

                    Log.Debug("The service '{0}' stopped.", name);
                    return;
                }

                Log.Debug("The service '{0}' already stopped.", name);
            }
        }

        /// <summary>
        /// Called when Dispose.
        /// </summary>
        protected void OnDispose()
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        #region IDisposable Support
        /// <summary>
        /// Dient zur Erkennung redundanter Aufrufe.
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.
                this.OnDispose();

                lock (this._lockObject)
                {

                    if (this.task != null)
                    {
                        this.Stop();
                    }
                }

                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~AbstractService() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
