using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLibrary.Entities
{
    public class ClientReservation
    {
        public ClientReservation()
        {

        }

        [Key, Column(Order = 0)]
        public int ClientId { get; set; }

        [Key, Column(Order = 1)]
        public int ReservationId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
