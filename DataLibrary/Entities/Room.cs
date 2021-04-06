using DataLibrary.Enumeration;
using System;
using System.ComponentModel.DataAnnotations;

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
               isFree= DateTime.Compare(Reservation.LeaveDate, DateTime.Now) <= 0 ? true : false;
            }
        }


        public decimal BedPriceForAdult { get; set; }

        public decimal BedPriceForKid { get; set; }

        public int Number { get; set; }

        //Navigation Properties
        public virtual Reservation Reservation { get; set; }

    }
}
