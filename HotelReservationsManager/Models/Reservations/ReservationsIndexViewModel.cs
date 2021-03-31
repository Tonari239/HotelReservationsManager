using HotelReservationsManager.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Reservation
{
    public class ReservationsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ICollection<ReservationsViewModel> Items { get; set; }

        public IEnumerable<ReservationsViewModel> ReservationViewModels { get; set; }

        ReservationsIndexViewModel()
        {
            ReservationViewModels = new HashSet<ReservationsViewModel>().AsEnumerable<ReservationsViewModel>();
        }
    }
}
