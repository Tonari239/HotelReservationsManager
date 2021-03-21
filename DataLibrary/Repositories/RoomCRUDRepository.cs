using DataLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Repositories
{
    public class RoomCRUDRepository :CRUDRepository<Room>
    {
        public RoomCRUDRepository(HotelDbContext Context):base(Context,Context.Rooms)
        {

        }
    }
}
