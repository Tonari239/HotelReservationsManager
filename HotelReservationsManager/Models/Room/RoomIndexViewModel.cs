using HotelReservationsManager.Models.Filters;
using HotelReservationsManager.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Room
{
    public class RoomIndexViewModel
    {
        public PagerViewModel Pager { get; set; }
        public RoomsFilterViewModel Filter { get; set; }

        public IQueryable<RoomViewModel> Items { get; set; }
        public RoomIndexViewModel()
        {
            Items = new HashSet<RoomViewModel>().AsQueryable<RoomViewModel>();
        }
    }
}
