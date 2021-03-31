using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataLibrary.Enumeration;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationsManager.Models.Rooms
{
    public class RoomsEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int Capacity { get; set; }

        public bool IsFree { get; set; }

        [Required]
        public decimal PriceAdult { get; set; }

        [Required]
        public decimal PriceChild { get; set; }

        [Required]
        public RoomTypeEnum RoomType { get; set; }

        public string Message { get; set; }
    }
}
