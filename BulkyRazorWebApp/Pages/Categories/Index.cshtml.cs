using BulkyRazorWebApp.DataBase;
using BulkyRazorWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazorWebApp.Page
{
    public class IndexModel : PageModel
    {
        public readonly DataContext _dataContext ;
        public List<Category> _categories = new List<Category>();
        public IndexModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public void OnGet()
        {
            _categories = _dataContext.Categories.ToList();

        }
    }
}
