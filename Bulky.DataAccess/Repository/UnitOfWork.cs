using Bulky.DataAccess.Repository.IRepository;


namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
        }

        public async Task<bool> SaveAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }
    }
}
