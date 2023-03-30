using Domain.Interfaces;
using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Repository.Repositories
{
    public class ImageInstructionRepository : GenericRepository<ImageInstruction, int>, IImageInstructionRepository
    {
        public ImageInstructionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<ImageInstruction>> GetAllNonExported() => await _context.ImageInstruction.Where(i => i.FileLocation == Location.Server).ToListAsync();
    }
}