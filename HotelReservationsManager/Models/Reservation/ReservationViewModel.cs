using HotelReservationsManager.Models.Client;
using HotelReservationsManager.Models.Room;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelReservationsManager.Models
{
    public class ReservationViewModel :BaseViewModel
    {
        [Required]
        [Display(Name="Резервирана стая")]
        public RoomViewModel BookedRoomViewModel { get; set; }

        [Required]
        [Display(Name = "Потребител, създал резервация")]
        public UserViewModel User { get; set; } // User who created reservation

        [Required]
        [Display(Name = "Настанени клиенти")]
        public IEnumerable<ClientViewModel> ClientsViewModels { get; set; }

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
    }
}
