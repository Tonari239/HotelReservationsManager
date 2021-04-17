using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Room
{
    public class RoomIndexViewModel
    {
        public IQueryable<RoomViewModel> Items { get; set; }
        public RoomIndexViewModel()
        {
            Items = new HashSet<RoomViewModel>().AsQueryable<RoomViewModel>();
        }
    }
}
