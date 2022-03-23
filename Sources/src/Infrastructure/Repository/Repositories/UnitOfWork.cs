using Domain.Interfaces;

namespace Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository ProductRepository { get; }

        public IWorkshopRepository WorkshopRepository { get; }

        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository, IWorkshopRepository workshopRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            WorkshopRepository = workshopRepository;
        }

        public int Completed()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}