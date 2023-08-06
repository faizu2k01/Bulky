using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : RepositroyImp<Product>, IProductRepository
    {  
        public readonly DataContext _dataContext;
    
        public ProductRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<bool> Update(Product product)
        {
             _dataContext.Products.Update(product);

            return await _dataContext.SaveChangesAsync()>0;
        }
    }
}
