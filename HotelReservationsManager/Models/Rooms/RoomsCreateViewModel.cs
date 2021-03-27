using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataLibrary.Enumeration;

namespace HotelReservationsManager.Models.Rooms
{
    public class RoomsCreateViewModel
    {
        [Required]
        public int Number { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public decimal PriceAdult { get; set; }

        [Required]
        public decimal PriceChild { get; set; }

        [Required]
        public RoomTypeEnum RoomType { get; set; }

        public string Message { get; set; }
    }
}
