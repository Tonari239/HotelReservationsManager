
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

        private bool isFree;

        public bool IsFree
        {
            get { return isFree; }
            set
            {
                //not sure about this ;
               isFree= DateTime.Compare(Reservation.LeaveDate, DateTime.Now) <= 0 ? true : false; isFree = value; 
            }
        }



        public decimal BedPriceForAdult { get; set; }

        public decimal BedPriceForKid { get; set; }

        public int Number { get; set; }

        //Navigation Properties
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }

    }
}
