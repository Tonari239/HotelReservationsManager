using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models
{
    public class BaseViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
    }
}
