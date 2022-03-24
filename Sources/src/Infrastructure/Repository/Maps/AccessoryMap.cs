﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Maps
{
    public class AccessoryMap
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accessory>().HasKey(p => p.Id);
        }
    }
}