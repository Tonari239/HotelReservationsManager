﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Client
{
    public class ClientsViewModel :BaseViewModel
    {
        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [MaxLength(10, ErrorMessage = "Моля въведете валиден телефонен номер!")]
        [Required]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Моля въведете валиден имейл адрес!")]
        [Required]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        public IEnumerable<ReservationsViewModel> ReservationsViewModels { get; set; }

        [Display(Name = "Възрастен")]
        public bool IsAdult { get; set; }


        public ClientsViewModel()
        {
            ReservationsViewModels = new HashSet<ReservationsViewModel>().AsEnumerable<ReservationsViewModel>();
        }
    }
}