using Domain.Interfaces;
using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ImageInstructionRepository : GenericRepository<ImageInstruction, int>, IImageInstructionRepository
    {
        public ImageInstructionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ImageInstruction>> GetAllNonExported() => await Context.ImageInstruction
            .Where(i => i.FileLocation == Location.Server)
            .ToListAsync(ComponentDisposed);

        public async Task<ImageInstruction?> GetFavoritImageByProduct(int idProduct)
        {
            bool hasFavorite = await Context.ImageInstruction.AnyAsync(i => i.IdProduct == idProduct && i.IsFavoriteImage, ComponentDisposed);

            if (hasFavorite)
                return await Context.ImageInstruction.FirstAsync(i => i.IdProduct == idProduct && i.IsFavoriteImage, ComponentDisposed);

            return await Context.ImageInstruction.FirstOrDefaultAsync(i => i.IdProduct == idProduct, ComponentDisposed);
        }

        public async Task SetNewFavorite(bool isFavorite, int id, int idProduct)
        {
            ImageInstruction newFavorite = await Context.ImageInstruction.FirstOrDefaultAsync(i => i.IdProduct == idProduct && i.Id == id, ComponentDisposed);

            if (!isFavorite)
            {
                newFavorite.IsFavoriteImage = false;
                Context.Update(newFavorite);
                await Context.SaveChangesAsync();
                return;
            }

            ImageInstruction image = await Context.ImageInstruction.FirstOrDefaultAsync(i => i.IdProduct == idProduct && i.IsFavoriteImage, ComponentDisposed);

            if (image != null)
            {
                image.IsFavoriteImage = false;
                Context.Update(image);
            }

            if (newFavorite != null)
            {
                newFavorite.IsFavoriteImage = true;
                Context.Update(newFavorite);
            }

            await Context.SaveChangesAsync();
        }
    }
}