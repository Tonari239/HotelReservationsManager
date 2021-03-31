using HotelReservationsManager.Models.Client;
using HotelReservationsManager.Models.Room;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelReservationsManager.Models
{
    public class ReservationsViewModel :BaseViewModel
    {

        public virtual RoomsViewModel Room { get; set; }
        public int CurrentReservationClientCount { get; set; }

        [Required]
        [Display(Name="Резервирана стая")]
        public RoomsViewModel BookedRoomViewModel { get; set; }

        [Required]
        [Display(Name = "Потребител, създал резервация")]
        public UsersViewModel User { get; set; } // User who created reservation

        [Required]
        [Display(Name = "Настанени клиенти")]
        public IEnumerable<ClientsViewModel> ClientsViewModels { get; set; }

        [Required]
        [Display(Name = "Дата на настаняване")]
        public DateTime AccommodationDate { get; set; }

        [Required]
        [Display(Name = "Дата на напускане")]
        public DateTime LeaveDate { get; set; }

        
        [Display(Name = "Включена закуска")]
        public bool BreakfastIncluded { get; set; }

        
        [Display(Name = "All-inclusive")]
        public bool AllInclusive { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Cost { get; set; } //TODO: Calculate ; imam ideq, no neka q obsudim po-kam kraq

        [Required]
        public int ClientId { get; set; }

        public virtual IEnumerable<SelectListItem> AvailableClients { get; set; }

        public virtual ICollection<ClientsViewModel> SignedInClients { get; set; }
    }
}
