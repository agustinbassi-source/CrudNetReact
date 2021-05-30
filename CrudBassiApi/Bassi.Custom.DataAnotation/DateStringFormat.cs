using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bassi.Extentions;

namespace Bassi.Custom.DataAnotation
{
    /// <summary>
    /// Especifica si el string es valido para el formato fecha
    /// </summary>
    public class DateStringFormat : ValidationAttribute
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format">yyyy-MM-dd</param>
        public DateStringFormat(string format)
        {
            Format = format;
        }

        public string Format { get; set; }
        public string Separador { get; set; } = "-";

        public string GetErrorMessage(string value) => $"Incorrect date format: {value}, the following format yyyy-MM-dd is expected";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (!((string)value).IsValidFormatDate(Format))
                {
                    return new ValidationResult(GetErrorMessage((string)value));
                }
            }
            catch
            {
                return new ValidationResult(GetErrorMessage((string)value));
            }
            return ValidationResult.Success;

        }

        private bool IsValidMonth(int month) => month > 0 && month < 13;

        private int CantDiasMes(int month, int year)
        {
            List<int> monthWith31 = new List<int>(new int[] { 1, 3, 5, 7, 8, 10, 12 });
            int days = 30;

            if (monthWith31.Contains(month))
            {
                days = 31;
            }
            
            if(month == 2)
            {
                days = IsBisiesto(year) ? 29 : 28;
            }

            return days;
        }

        private bool IsBisiesto(int year) => year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);

        private bool IsValidFormatDate(string value)
        {

            List<string> format = new List<string>(Format.Split(Separador));
            List<string> date = new List<string>(value.Split(Separador));


            if(format.Count != date.Count)
            {
                //no es valido
                return false;
            }

            int indexOf = format.IndexOf("yyyy");

            if (indexOf < 0 || !int.TryParse(date[indexOf], out int year))
            {
                return false;
            }

            indexOf = format.IndexOf("MM");
            if (indexOf < 0 || !int.TryParse(date[indexOf], out int month))
            {
                return false;
            }

            indexOf = format.IndexOf("dd");
            if (indexOf < 0 || !int.TryParse(date[indexOf], out int day))
            {
                return false;
            }

            return year > 1900 && IsValidMonth(month) && day > 0 && day <= CantDiasMes(month, year);

        }
    }
}
