using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationsManager.Models.Clients
{
    public class ClientsEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsAdult { get; set; }
    }
}
