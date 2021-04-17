﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager.Models.Reservations
{
    public class ReservationsEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AccomodationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaveDate { get; set; }

        [Required]
        public bool BreakfastIncluded { get; set; }

        [Required]
        public bool AllInclusive { get; set; }

        public decimal Cost { get; set; }

        public string Message { get; set; }

    }
}