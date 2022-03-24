﻿using Domain.Interfaces;
using Domain.Models;

namespace Repository.Repositories
{
    public class FiringRepository : GenericRepository<Firing>, IFiringRepository
    {
        public FiringRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
