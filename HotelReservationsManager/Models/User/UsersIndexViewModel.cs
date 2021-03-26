using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models
{
    public class UsersIndexViewModel
    {

        public IEnumerable<UserViewModel> UserViewModels { get; set; }
        public UsersIndexViewModel()
        {
            UserViewModels = new HashSet<UserViewModel>().AsEnumerable<UserViewModel>();
        }
    }
}
