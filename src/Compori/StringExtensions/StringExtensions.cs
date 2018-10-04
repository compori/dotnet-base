using System;
using System.Collections.Generic;
using System.Text;

namespace Compori.StringExtensions
{
    public static class StringExtensions
    {

#if NET35
        public static bool IsNullOrWhiteSpace(this String instance)
        {
            return String.IsNullOrEmpty(instance) || instance.Trim().Length == 0;
        }
#endif

    }
}
