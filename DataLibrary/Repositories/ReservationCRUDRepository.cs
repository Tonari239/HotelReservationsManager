using DataLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Repositories
{
    public class ReservationCRUDRepository : CRUDRepository<Reservation>
    {
        public ReservationCRUDRepository(HotelDbContext Context) : base(Context, Context.Reservations)
        {

        }
    }
}

