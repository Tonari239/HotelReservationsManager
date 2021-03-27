using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HotelReservationsManager.Attributes;

namespace HotelReservationsManager.Models
{
    public class UsersViewModel :BaseViewModel
    {


        [Required]
        [Display(Name ="Потребителско име")]
         public string Username { get; set; }

        public string Password { get; set; }

        [Required]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Презиме")]
        public string SecondName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }


        [MaxLength(10,ErrorMessage ="Моля въведете валидно ЕГН")]
        [Required]
        [Display(Name = "ЕГН")]
        public string CivilNumber { get; set; } // ЕГН


        [MaxLength(10,ErrorMessage ="Моля въведете валиден телефонен номер!")]
        [Required]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }


        [EmailAddress(ErrorMessage ="Моля въведете валиден имейл адрес!")]
        [Required]
        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Required]
        [DateValidation] // Custom Date validation, located in Attributes folder
        [Display(Name = "Дата на назначаване")]
        public DateTime EmploymentDate { get; set; }

        [DateValidation]
        [Display(Name = "Дата на освобождаване")]
        public DateTime? LeavingDate { get; set; }

        [Display(Name = "Активен")]
        public bool IsActive { get; set; }

        //Navigation Properties

        public virtual IQueryable<ReservationsViewModel> Reservations { get; set; }
    }
}
