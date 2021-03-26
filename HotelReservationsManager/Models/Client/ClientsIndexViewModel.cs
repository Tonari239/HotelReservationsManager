using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Client
{
    public class ClientsIndexViewModel
    {
        public IEnumerable<ClientViewModel> ClientViewModels { get; set; }

        public ClientsIndexViewModel()
        {
            ClientViewModels = new HashSet<ClientViewModel>().AsEnumerable<ClientViewModel>();
        }
    }
}
