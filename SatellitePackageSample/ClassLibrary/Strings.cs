using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// A class that exposes strings, one always English and another localizable.
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// Gets a string that will always be rendered in English.
        /// </summary>
        public static string AlwaysEnglish
        {
            get
            {
                return Resources.AlwaysEnglish;
            }
        }

        /// <summary>
        /// Gets a string that is localizable but defaults to English.
        /// </summary>
        public static string Localizable
        {
            get
            {
                return Resources.Localizable;
            }
        }
    }
}
