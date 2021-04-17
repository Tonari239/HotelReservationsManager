<<<<<<< Updated upstream
﻿using DataLibrary.Enumeration;
using System;
=======
﻿using System;
using System.Collections.Generic;
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

<<<<<<< Updated upstream
        private bool isFree;

        public bool IsFree
        {
            get { return isFree; }
            set 
            {
               isFree= DateTime.Compare(Reservation.LeaveDate, DateTime.Now) <= 0 ? true : false;
            }
        }
=======
>>>>>>> Stashed changes
=======
        public bool IsFree { get; set; }
>>>>>>> Stashed changes

        public bool IsFree { get; set; }

        public decimal BedPriceForAdult { get; set; }

        public decimal BedPriceForKid { get; set; }

        public int Number { get; set; }

        //Navigation Properties
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }

    }
}
