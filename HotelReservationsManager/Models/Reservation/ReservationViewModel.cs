using HotelReservationsManager.Models.Client;
using HotelReservationsManager.Models.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Reservation
{
    public class ReservationViewModel :BaseViewModel
    {
        public decimal Cost { get; set; }
        public bool BreakfastIncluded { get; set; }
        public bool AllInclusive { get; set; }
        public DateTime LeaveDate { get; set; }
        public DateTime AccommodationDate { get; set; }
        public IQueryable<ClientViewModel> ClientsViewModels { get; set; }
        public int UserId { get; set; }
        public RoomViewModel RoomViewModel { get; set; }

        public ReservationViewModel()
        {
            ClientsViewModels = new HashSet<ClientViewModel>().AsQueryable<ClientViewModel>();
        }
        // TODO: add neshtata strahil

    }
}
