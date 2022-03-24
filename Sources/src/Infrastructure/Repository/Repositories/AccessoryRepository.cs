﻿using Domain.Interfaces;
using Domain.Models;

namespace Repository.Repositories
{
    public class AccessoryRepository : GenericRepository<Accessory>, IAccessoryRepository
    {
        public AccessoryRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
