using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelReservationsManager.Models.Filters
{
    public class ClientsFilterViewModel
    {
        [DataType(DataType.Text)]
        public string FirstName { get; set; }


        [DataType(DataType.Text)]
        public string LastName { get; set; }
    }
}
