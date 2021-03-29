using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using HotelReservationsManager.Models;
using HotelReservationsManager.Models.Shared;
using HotelReservationsManager.Models.Filters;

namespace HotelReservationsManager.Models
{
    public class UsersIndexViewModel
    {
        public PagerViewModel Pager { get; set; }

        public UsersFilterViewModel Filter { get; set; }

        public ICollection<UsersViewModel> Items { get; set; }

        public IEnumerable<UsersViewModel> UserViewModels { get; set; }
        public UsersIndexViewModel()
        {
            UserViewModels = new HashSet<UsersViewModel>().AsEnumerable<UsersViewModel>();
        }
    }
}
