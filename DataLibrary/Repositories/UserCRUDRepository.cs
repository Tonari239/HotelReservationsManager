using DataLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Repositories
{
    public class UserCRUDRepository : CRUDRepository<User>
    {
        public UserCRUDRepository(HotelDbContext db) :base(db,db.Users)
        {

        }
    }
}
