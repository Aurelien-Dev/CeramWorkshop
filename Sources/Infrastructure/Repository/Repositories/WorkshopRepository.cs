﻿using Domain.Interfaces;
using Domain.Models.WorkshopDomaine;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class WorkshopRepository : GenericRepository<Workshop, int>, IWorkshopRepository
    {
        public WorkshopRepository(ApplicationDbContext context) : base(context) { }

        public async  Task<(Workshop?, bool)> GetWorkshopInformationForLogin(string email, CancellationToken cancellationToken = default)
        {
            Workshop workshop = await Context.Workshops.FirstOrDefaultAsync(w => w.Email == email, cancellationToken);
            bool mailExist = workshop != null && email == workshop.Email;

            return (workshop, mailExist);
        }

        public async Task<bool> CheckIfEmailExists(string email, CancellationToken cancellationToken = default)
        {
            return await Context.Workshops.AnyAsync(w => w.Email == email, cancellationToken);
        }
    }
}