using HotelReservationsManager.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Reservation
{
    public class ReservationIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public IQueryable<ReservationViewModel> Items { get; set; }
        public ReservationIndexViewModel()
        {
            Items = new HashSet<ReservationViewModel>().AsQueryable<ReservationViewModel>();
        }
    }
}
