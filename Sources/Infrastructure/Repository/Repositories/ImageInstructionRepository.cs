using Domain.Interfaces;
using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ImageInstructionRepository : GenericRepository<ImageInstruction, int>, IImageInstructionRepository
    {
        public ImageInstructionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<ImageInstruction>> GetAllNonExported() => await _context.ImageInstruction.Where(i => i.FileLocation == Location.Server).ToListAsync();

        public async Task<ImageInstruction?> GetFavoritImageByProduct(int idProduct)
        {
            bool hasFavorite = _context.ImageInstruction.Any(i => i.IdProduct == idProduct && i.IsFavoriteImage);

            if (hasFavorite)
                return await _context.ImageInstruction.FirstAsync(i => i.IdProduct == idProduct && i.IsFavoriteImage);

            return await _context.ImageInstruction.FirstOrDefaultAsync(i => i.IdProduct == idProduct);
        }

        public async Task SetNewFavorite(bool isFavorite, int id, int idProduct)
        {
            ImageInstruction newFavorite = await _context.ImageInstruction.FirstOrDefaultAsync(i => i.IdProduct == idProduct && i.Id == id);

            if (!isFavorite)
            {
                newFavorite.IsFavoriteImage = false;
                _context.Update(newFavorite);
                _context.SaveChanges();
                return;
            }

            ImageInstruction image = await _context.ImageInstruction.FirstOrDefaultAsync(i => i.IdProduct == idProduct && i.IsFavoriteImage);


            if (image != null)
            {
                image.IsFavoriteImage = false;
                _context.Update(image);
            }

            if (newFavorite != null)
            {
                newFavorite.IsFavoriteImage = true;
                _context.Update(newFavorite);
            }

            _context.SaveChanges();
        }
    }
}