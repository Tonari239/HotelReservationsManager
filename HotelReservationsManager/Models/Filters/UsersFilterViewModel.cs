using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelReservationsManager.Models;

namespace HotelReservationsManager.Models.Filters
{
    public class UsersFilterViewModel
    {
        public string Username { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Презиме")]
        public string SecondName { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }


        [DataType(DataType.Text)]
        public string Email { get; set; }

    }
}
