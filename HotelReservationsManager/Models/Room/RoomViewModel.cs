using DataLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Room
{
    public class RoomViewModel :BaseViewModel
    {
        [Required]
        [Display(Name = "Number")]
        public int Number { get; set; }

        [Required]
        [Range(1, 4)]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Required]
        [Display(Name = "Room type")]
        public RoomTypeEnum Type { get; set; }


        [Display(Name = "Occupied")]
        public bool IsFree { get; set; }

        [Required]
        [Display(Name = "Bed price for adult")]
        public decimal BedPriceForAdult { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Моля въведете позитивно число!")]
        [Display(Name = "Bed price for kids")]
        public decimal BedPriceForKid { get; set; }
        
    }
}
