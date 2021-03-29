﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataLibrary.Enumeration;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelReservationsManager.Models;

namespace HotelReservationsManager.Models.Reservations
{
    public class ReservationsCreateViewModel
    {

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AccomodationDate { get; set; } = DateTime.UtcNow;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaveDate { get; set; } = DateTime.UtcNow;


        public bool IsBreakfastIncluded { get; set; }
        public bool IsAllInclusive { get; set; }
        public decimal OverallBill { get; set; }



        public int RoomId { get; set; }
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }

        public string Message { get; set; }

    }
}
