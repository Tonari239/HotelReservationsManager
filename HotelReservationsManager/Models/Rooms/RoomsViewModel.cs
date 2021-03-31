using DataLibrary.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Room
{
    public class RoomsViewModel :BaseViewModel
    {
        [Required]
        [Display(Name = "Номер")]
        public int Number { get; set; }

        [Required]
        [Range(1,4)]
        [Display(Name ="Капацитет")]
        public int Capacity { get; set; }

        [Required]
        [Display(Name = "Тип стая")]
        public RoomTypeEnum Type { get; set; }

        
        [Display(Name = "Заета")]
        public bool IsFree { get; set; }

        [Required]
        [Display(Name = "Цена на легло за възрастен")]
        public decimal BedPriceForAdult { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Моля въведете позитивно число!")]
        [Display(Name = "Цена на легло за дете")]
        public decimal BedPriceForKid { get; set; }


    }
}
