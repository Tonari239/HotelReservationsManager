using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLibrary.Entities
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsAdult { get; set; }

        //Navigation Properties
        
        public virtual Reservation Reservation { get; set; }
    }
}
