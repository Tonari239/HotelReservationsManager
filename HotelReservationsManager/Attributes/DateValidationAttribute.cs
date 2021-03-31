using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Attributes
{
    public class DateValidationAttribute : RangeAttribute
    {
        private static string minimumYear = "1/1/2010";
        private static string ErrorMessage = $"Please enter a date between {minimumYear} and {DateTime.Now.ToString()}";
        public override string FormatErrorMessage(string name)
        {
            return ErrorMessage;
        }
        public DateValidationAttribute() :base(typeof(DateTime), minimumYear, DateTime.Now.ToString())
        {

        }
    }
}
