using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        public int CivilNumber { get; set; } // ЕГН

        public int PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime EmploymentDate { get; set; }

        public bool IsActive { get; set; }
        
        public DateTime LeavingDate { get; set; }

        //Navigation Properties
        
        public virtual ICollection<Reservation> Reservations { get; set; }
        public User()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}
