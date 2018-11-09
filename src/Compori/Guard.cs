using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Compori
{
    using StringExtensions;

    /// <summary>
    /// Die Klasse Guard bietet Methoden um Parameterwerte zu prüfen.
    /// Der eigentliche Programmcode wird dadurch sauberer und leichter wartbar.
    /// </summary>
    sealed public class Guard
    {

        /// <summary>
        /// Prevents a default instance of the <see cref="Guard"/> class from being created.
        /// </summary>
        private Guard() { }

        /// <summary>
        /// Sichert zu, dass der Parameterwert nicht null ist.
        /// Wird die Zusicherung verletzt wird eine ArgumentNullException geworfen.
        /// </summary>
        /// <param name="value">Der Wert des zu prüfenden Parameters.</param>
        /// <param name="argument">Der Name des zu prüfenden Parameters.</param>
        /// <param name="message">Eine Meldung, die als Ausnahmemeldung übergeben wird.</param>
        /// <exception cref="System.ArgumentNullException">Der Name des Parameters ist null - argument.</exception>
        [DebuggerHidden()]
        public static void AssertArgumentIsNotNull(Object value, String argument, String message)
        {
            // Prüfe den Parametername
            if (argument.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(argument));
            }

            if (value == null)
            {
                if (message == null)
                {
                    throw new ArgumentNullException(argument);
                }
                throw new ArgumentNullException(argument, message);
            }
        }

        /// <summary>
        /// Sichert zu, dass der Parameterwert nicht null ist.
        /// Wird die Zusicherung verletzt wird eine ArgumentNullException geworfen.
        /// </summary>
        /// <param name="value">Der Wert des zu prüfenden Parameters.</param>
        /// <param name="argument">Der Name des zu prüfenden Parameters.</param>
        /// <exception cref="System.ArgumentNullException">Der Name des Parameters ist null - argument.</exception>
        [DebuggerHidden()]
        public static void AssertArgumentIsNotNull(Object value, String argument)
        {
            Guard.AssertArgumentIsNotNull(value, argument, null);
        }

        /// <summary>
        /// Sichert zu, dass der Parameterwert nicht null oder leer ist bzw. nur Whitespaces enthält.
        /// Wird die Zusicherung verletzt wird eine ArgumentNullException oder ArgumentException geworfen.
        /// </summary>
        /// <param name="value">Der Wert des zu prüfenden Parameters.</param>
        /// <param name="argument">Der Name des zu prüfenden Parameters.</param>
        /// <param name="message">Eine Meldung, die als Ausnahmemeldung übergeben wird.</param>
        /// <exception cref="System.ArgumentNullException">Der Name des Parameters ist null - argument.</exception>
        /// <exception cref="System.ArgumentNullException">Der Wert des Parameters ist null oder leer - value.</exception>
        [DebuggerHidden()]
        public static void AssertArgumentIsNotNullOrWhiteSpace(String value, String argument, String message)
        {
            // Teste den Parameter "argument"
            if (argument.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(argument));
            }

            // Teste den Parameterwert für "argument"
            if (value.IsNullOrWhiteSpace())
            {
                if (message == null)
                {
                    throw new ArgumentNullException(argument);
                }
                throw new ArgumentNullException(argument, message);
            }
        }

        /// <summary>
        /// Sichert zu, dass der Parameterwert nicht null oder leer ist bzw. nur Whitespaces enthält.
        /// Wird die Zusicherung verletzt wird eine ArgumentNullException oder ArgumentException geworfen.
        /// </summary>
        /// <param name="value">Der Wert des zu prüfenden Parameters.</param>
        /// <param name="argument">Der Name des zu prüfenden Parameters.</param>
        /// <exception cref="System.ArgumentNullException">Der Name des Parameters ist null - argument.</exception>
        /// <exception cref="System.ArgumentNullException">Der Wert des Parameters ist null oder leer - value.</exception>
        [DebuggerHidden()]
        public static void AssertArgumentIsNotNullOrWhiteSpace(String value, String argument)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(value, argument, null);
        }

        /// <summary>
        /// Sichert zu, dass der Parameterwert innerhalb eines Gültigkeitsbereiches liegt, der über eine Funktion geprüft werden kann.
        /// Wird die Zusicherung verletzt wird eine ArgumentOutOfRangeException geworfen.
        /// </summary>
        /// <typeparam name="T">Generische Typ des Wertes</typeparam>
        /// <param name="value">Der Wert des zu prüfenden Parameters.</param>
        /// <param name="argument">Der Name des zu prüfenden Parameters.</param>
        /// <param name="check">Die aufzurufende Prüffunktion.</param>
        /// <param name="message">Eine Meldung, die als Ausnahmemeldung übergeben wird.</param>
        /// <exception cref="System.ArgumentNullException">Der Name des Parameters ist null - argument.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        [DebuggerHidden()]
        public static void AssertArgumentIsInRange<T>(T value, String argument, Func<T, Boolean> check, String message)
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(argument, nameof(argument));
            Guard.AssertArgumentIsNotNull(check, nameof(check));

            // prüfe die Check Funktion
            if (!check(value))
            {
                if (message == null)
                {
                    throw new ArgumentOutOfRangeException(argument, value, "Specified argument was out of the range of valid values.");
                }

                throw new ArgumentOutOfRangeException(argument, value, message);
            }
        }

        /// <summary>
        /// Sichert zu, dass der Parameterwert innerhalb eines Gültigkeitsbereiches liegt, der über eine Funktion geprüft werden kann.
        /// Wird die Zusicherung verletzt wird eine <see cref="ArgumentOutOfRangeException"/> geworfen.
        /// </summary>
        /// <typeparam name="T">Generische Typ des Wertes</typeparam>
        /// <param name="value">Der Wert des zu prüfenden Parameters.</param>
        /// <param name="argument">Der Name des zu prüfenden Parameters.</param>
        /// <param name="check">Die aufzurufende Prüffunktion.</param>
        /// <exception cref="System.ArgumentNullException">Der Name des Parameters ist null - argument.</exception>
        /// <exception cref="System.ArgumentException">Der Name des Parameters ist leer oder enthält nur Whitespaces - argument.</exception>
        /// <exception cref="System.ArgumentNullException">Die Paremeterprüffunktion darf nicht null sein - check.</exception>
        [DebuggerHidden()]
        public static void AssertArgumentIsInRange<T>(T value, String argument, Func<T, Boolean> check)
        {
            Guard.AssertArgumentIsInRange<T>(value, argument, check, null);
        }

    }
}
