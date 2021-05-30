using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bassi.Utilities
{
    public static class Extencions
    {
        /// <summary>
        /// Remueve del string una lista de caracteres
        /// </summary>
        /// <param name="value"></param>
        /// <param name="charList"></param>
        /// <returns></returns>
        public static string ClearChars(this string value, List<char> charList)
        {
            StringBuilder stringBuilder = new StringBuilder(value.Length);

            value.Where(w => !charList.Contains(w)).ToList().ForEach(x => stringBuilder.Append(x.ToString()));

            return stringBuilder.ToString();

        }

        /// <summary>
        /// Remueve de un string un caracter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="charToDelete"></param>
        /// <returns></returns>
        public static string ClearChars(this string value, char charToDelete)
        {
            return value.ClearChars(new List<char>(new char[] { charToDelete }));
        }
    }
}
