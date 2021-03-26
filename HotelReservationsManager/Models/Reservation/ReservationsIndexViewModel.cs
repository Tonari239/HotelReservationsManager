using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Reservation
{
    public class ReservationsIndexViewModel
    {
        public IEnumerable<ReservationViewModel> ReservationViewModels { get; set; }

        ReservationsIndexViewModel()
        {
            ReservationViewModels = new HashSet<ReservationViewModel>().AsEnumerable<ReservationViewModel>();
        }
    }
}
