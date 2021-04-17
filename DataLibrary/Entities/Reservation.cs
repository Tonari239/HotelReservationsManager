using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLibrary.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime LeaveDate { get; set; }

        public bool BreakfastIncluded { get; set; }

        public bool AllInclusive { get; set; }

        public decimal Cost { get; set; } //TODO: Calculate


        //Navigation properties
        public virtual Room Room { get; set; } // booked room
        public int RoomId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<Client> Clients { get; set; }

        public Reservation()
        {
            Clients = new HashSet<Client>();
        }


    }
}
