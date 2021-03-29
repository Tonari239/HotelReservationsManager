using DataLibrary.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Filters
{
    public class RoomsFilterViewModel
    {
        public int? Capacity { get; set; }

        public RoomTypeEnum? Type { get; set; }

        public bool? IsFree { get; set; }
    }
}
