using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsAdult { get; set; }
        
        public virtual Reservation Reservation { get; set; }
    }
}
