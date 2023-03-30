using Domain.Interfaces;
using Domain.InterfacesWorker;

namespace Repository.Workers
{
    internal class ApiWorker : IApiWorker
    {
        private readonly ApplicationDbContext _context;
        public IImageInstructionRepository ImageInstructionRepository { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Db Context</param>
        /// <param name="imageInstructionRepository">Image Repository</param>
        public ApiWorker(ApplicationDbContext context, IImageInstructionRepository imageInstructionRepository)
        {
            _context = context;
            ImageInstructionRepository = imageInstructionRepository;
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