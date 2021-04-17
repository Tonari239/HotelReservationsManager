
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLibrary.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public int Capacity { get; set; }

        public RoomTypeEnum Type { get; set; }

        public bool IsFree { get; set; }



        public decimal BedPriceForAdult { get; set; }

        public decimal BedPriceForKid { get; set; }

        public int Number { get; set; }

        //Navigation Properties
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }

    }
}
