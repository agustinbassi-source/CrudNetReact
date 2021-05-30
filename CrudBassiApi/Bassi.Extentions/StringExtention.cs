using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Extentions
{
    public static class StringExtention
    {
        public static bool IsValidFormatDate(this String value, string format)
        {
            string separator = FormatDateSeparator(format);

            if (separator == String.Empty)
            {
                return false;
            }

            List<string> lFormat = new List<string>(format.Split(separator));
            List<string> date = new List<string>(value.Split(separator));

            if (lFormat.Count != date.Count)
            {
                //no es valido
                return false;
            }

            int year = SectionFormat(date, lFormat, "yyyy");
            int month = SectionFormat(date, lFormat, "MM");
            int day = SectionFormat(date, lFormat, "dd");

            if (year == 0 || month == 0 || day == 0)
            {
                return false;
            }

            return year > 1900 && IsValidMonth(month) && day > 0 && day <= CantDiasMes(month, year);
        }

        public static DateTime ToDateTime(this string value, string format)
        {

            string separator = FormatDateSeparator(format);
            DateTime dateTimeEmpty = new DateTime();

            if (separator == String.Empty)
            {
                return dateTimeEmpty;
            }

            List<string> lFormat = new List<string>(format.Split(separator));
            List<string> date = new List<string>(value.Split(separator));

            if (lFormat.Count != date.Count)
            {
                return dateTimeEmpty;
            }

            int year = SectionFormat(date, lFormat, "yyyy");
            int month = SectionFormat(date, lFormat, "MM");
            int day = SectionFormat(date, lFormat, "dd");
            
            if(year == 0 || month == 0 || day == 0 || !(year > 1900 && IsValidMonth(month) && day > 0 && day <= CantDiasMes(month, year)))
            {
                return dateTimeEmpty;
            }

            return new DateTime(year, month, day);
        }

        static string FormatDateSeparator(string format)
        {
            if (format.Contains("-"))
            { 
                return "-"; 
            }
            else if (format.Contains("/"))
            { 
                return "/"; 
            }
            else
            { 
                return ""; 
            }
        }

        static int CantDiasMes(int month, int year)
        {
            List<int> monthWith31 = new List<int>(new int[] { 1, 3, 5, 7, 8, 10, 12 });
            int days = 30;

            if (monthWith31.Contains(month))
            {
                days = 31;
            }

            if (month == 2)
            {
                days = IsBisiesto(year) ? 29 : 28;
            }

            return days;
        }

        static bool IsBisiesto(int year) => year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);

        static bool IsValidMonth(int month) => month > 0 && month < 13;

        static int SectionFormat(List<string> value, List<string> format, string sectionFormat)
        {
            int indexOf = format.IndexOf(sectionFormat);
            if (indexOf < 0 || !int.TryParse(value[indexOf], out int data))
            {
                return 0;
            }

            return data;
        }
    }
}
