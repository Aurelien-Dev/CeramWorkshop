﻿using Domain.Interfaces;
using Domain.Models.MainDomain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ImageInstructionRepository : GenericRepository<ImageInstruction, int>, IImageInstructionRepository
    {
        public ImageInstructionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ImageInstruction>> GetAllNonExported(CancellationToken cancellationToken = default) => await Context.ImageInstruction
            .Where(i => i.FileLocation == Location.Server)
            .ToListAsync(cancellationToken);

        public async Task<ImageInstruction?> GetFavoritImageByProduct(int idProduct, CancellationToken cancellationToken = default)
        {
            bool hasFavorite = await Context.ImageInstruction.AnyAsync(i => i.IdProduct == idProduct && i.IsFavoriteImage, cancellationToken);

            if (hasFavorite)
                return await Context.ImageInstruction.FirstAsync(i => i.IdProduct == idProduct && i.IsFavoriteImage, cancellationToken);

            return await Context.ImageInstruction.FirstOrDefaultAsync(i => i.IdProduct == idProduct, cancellationToken);
        }

        public async Task SetNewFavorite(bool isFavorite, int id, int idProduct, CancellationToken cancellationToken = default)
        {
            ImageInstruction newFavorite = await Context.ImageInstruction.FirstOrDefaultAsync(i => i.IdProduct == idProduct && i.Id == id, cancellationToken);

            if (!isFavorite)
            {
                newFavorite.IsFavoriteImage = false;
                Context.Update(newFavorite);
                await Context.SaveChangesAsync(cancellationToken);
                return;
            }

            ImageInstruction image = await Context.ImageInstruction.FirstOrDefaultAsync(i => i.IdProduct == idProduct && i.IsFavoriteImage, cancellationToken);

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

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}