using IceCream.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCream.Services
{
    public class ProfileServiceImpl : ProfileService
    {
        public DatabaseContext db;
        public ProfileServiceImpl(DatabaseContext _db)
        {
            db = _db;
        }


     
    }
}
