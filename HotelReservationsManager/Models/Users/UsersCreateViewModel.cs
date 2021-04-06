using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationsManager.Models.Users
{
    public class UsersCreateViewModel
    {
        [Required]
        public string Username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Required]
        public string FirstName { get; set; }


        [Required]
        public string SecondName { get; set; }


        [Required]
        public string LastName { get; set; }


        [Required]
        public string CivilNumber { get; set; }


        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


    }
}
