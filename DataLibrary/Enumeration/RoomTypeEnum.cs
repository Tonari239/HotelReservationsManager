using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Enumeration
{
    public enum RoomTypeEnum
    {
        [Display(Name = "Две отделни легла")]
        TwoSingleBed = 0,
        Apartment = 1,
        [Display(Name = "Двойно легло")]
        DoubleBed = 2,
        Penthouse = 3,
        Maisonette = 4
    }
}
