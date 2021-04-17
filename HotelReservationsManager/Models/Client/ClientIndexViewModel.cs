using HotelReservationsManager.Models.Filters;
using HotelReservationsManager.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models.Client
{
    public class ClientIndexViewModel
    {
        public PagerViewModel Pager { get; set; }
        public ClientsFilterViewModel Filter { get; set; }

        public IQueryable<ClientViewModel> Items { get; set; }
        public ClientIndexViewModel()
        {
            Items = new HashSet<ClientViewModel>().AsQueryable<ClientViewModel>();
        }
    }
}
