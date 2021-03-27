using HotelReservationsManager.Models.Filters;
using HotelReservationsManager.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Room
{
    public class RoomsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public RoomsFilterViewModel Filter { get; set; }

        public ICollection<RoomsViewModel> Items { get; set; }

        public IEnumerable<RoomsViewModel> RoomViewModels { get; set; }
        public RoomsIndexViewModel()
        {
            RoomViewModels = new HashSet<RoomsViewModel>().AsEnumerable<RoomsViewModel>();
        }
    }
}
