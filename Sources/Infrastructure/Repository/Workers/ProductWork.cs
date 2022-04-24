using Domain.Interfaces;
using Domain.InterfacesWorker;

namespace Repository.Workers
{
    public class ProductWork : IProductWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository ProductRepository { get; }
        public IMaterialRepository MaterialRepository { get; }
        public IFiringRepository FiringRepository { get; }
        public IAccessoryRepository AccessoryRepository { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Db Context</param>
        /// <param name="productRepository">Product Repository</param>
        /// <param name="materialRepository">Material Repository</param>
        /// <param name="firingRepository">Firing Repository</param>
        /// <param name="accessoryRepository">Accessory Repository</param>
        public ProductWork(ApplicationDbContext context, IProductRepository productRepository,
                           IMaterialRepository materialRepository, IFiringRepository firingRepository,
                           IAccessoryRepository accessoryRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            MaterialRepository = materialRepository;
            FiringRepository = firingRepository;
            AccessoryRepository = accessoryRepository;
        }

        public int Completed()
        {
            return _context.SaveChanges();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Clear();
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