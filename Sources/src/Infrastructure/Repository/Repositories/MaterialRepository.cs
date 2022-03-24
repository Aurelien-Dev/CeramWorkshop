﻿using Domain.Interfaces;
using Domain.Models;

namespace Repository.Repositories
{
    public class MaterialRepository : GenericRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}