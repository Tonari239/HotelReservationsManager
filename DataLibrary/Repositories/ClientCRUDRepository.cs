using DataLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Repositories
{
    public class ClientCRUDRepository :CRUDRepository<Client>
    {
        public ClientCRUDRepository(HotelDbContext Context):base(Context,Context.Clients)
        {

        }
    }
}
