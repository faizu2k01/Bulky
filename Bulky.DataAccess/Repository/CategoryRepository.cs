using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;



namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository : RepositroyImp<Category>, ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

       
        public async Task<bool> Update(Category model)
        {
            _dataContext.Categories.Update(model);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
