using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Room
{
    public class RoomsIndexViewModel
    {
        public IEnumerable<RoomViewModel> RoomViewModels { get; set; }
        public RoomsIndexViewModel()
        {
            RoomViewModels = new HashSet<RoomViewModel>().AsEnumerable<RoomViewModel>();
        }
    }
}
