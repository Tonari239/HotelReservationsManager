using HotelReservationsManager.Models.Filters;
using HotelReservationsManager.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Client
{
    public class ClientsIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public ClientsFilterViewModel Filter { get; set; }

        public ICollection<ClientsViewModel> Items { get; set; }

        public IEnumerable<ClientsViewModel> ClientViewModels { get; set; }

        public ClientsIndexViewModel()
        {
            ClientViewModels = new HashSet<ClientsViewModel>().AsEnumerable<ClientsViewModel>();
        }
    }
}
