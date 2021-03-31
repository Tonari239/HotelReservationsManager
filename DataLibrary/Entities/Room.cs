using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLibrary.Entities
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }

        public int Capacity { get; set; }

        public string Type { get; set; }

        public bool IsFree { get; set; }

        public decimal BedPriceForAdult { get; set; }

        public decimal BedPriceForKid { get; set; }

        public int Number { get; set; }

        //Navigation Properties
        public virtual Reservation Reservation { get; set; }

    }
}
