using Domain.Interfaces;
using Domain.InterfacesWorker;

namespace Repository.Workers
{
    public class ProductWorker : WorkerBase, IProductWorker
    {
        public IProductRepository ProductRepository { get; }
        public IMaterialRepository MaterialRepository { get; }
        public IFiringRepository FiringRepository { get; }
        public IImageInstructionRepository ImageInstructionRepository { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Db Context</param>
        /// <param name="productRepository">Product Repository</param>
        /// <param name="materialRepository">Material Repository</param>
        /// <param name="firingRepository">Firing Repository</param>
        /// <param name="imageInstructionRepository">Image instruction Repository</param>
        public ProductWorker(ApplicationDbContext context, IProductRepository productRepository, IMaterialRepository materialRepository,
            IFiringRepository firingRepository, IImageInstructionRepository imageInstructionRepository)
            : base(context)
        {
            ProductRepository = productRepository;
            MaterialRepository = materialRepository;
            FiringRepository = firingRepository;
            ImageInstructionRepository = imageInstructionRepository;
        }

        public void Close()
        {
            ProductRepository.CancelEFCore();
        }
    }
}