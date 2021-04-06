using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Clients
{
    public class ClientsDetailViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsAdult { get; set; }

        public ICollection<ReservationsViewModel> PastReservations { get; set; }

        public ICollection<ReservationsViewModel> UpcomingReservations { get; set; }
    }
}
