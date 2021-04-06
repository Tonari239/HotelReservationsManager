using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationsManager.Models.Users
{
    public class UsersEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string Password { get; set; }


        [Required]
        public string FirstName { get; set; }


        [Required]
        public string SecondName { get; set; }


        [Required]
        public string LastName { get; set; }


        [Required]
        public string CivilNumber { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        public bool IsActive { get; set; }

        public DateTime? LeavingDate { get; set; }

    }
}
