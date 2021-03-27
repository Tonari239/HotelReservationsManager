using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string CivilNumber { get; set; } // ЕГН

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime EmploymentDate { get; set; }
        
        public DateTime? LeavingDate { get; set; }

        public bool IsActive { get; set; }

        //Navigation Properties

        public virtual ICollection<Reservation> Reservations { get; set; }

        public User()
        {
            this.IsActive = true;
            this.LeavingDate = null;
            this.Reservations = new List<Reservation>();
        }
    }
}
