using System;
using System.Collections.Generic;
using System.Text;

namespace Compori.StringExtensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this String instance)
        {
#if NET35
            return String.IsNullOrEmpty(instance) || instance.Trim().Length == 0;
#else
            return String.IsNullOrWhiteSpace(instance);
#endif
        }
    }
}
