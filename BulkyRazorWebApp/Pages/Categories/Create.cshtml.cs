using BulkyRazorWebApp.DataBase;
using BulkyRazorWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazorWebApp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly DataContext _dataContext;
        public Category category { get; set; }
        public CreateModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void OnGet()
        {
        }

        public async  Task<IActionResult>  OnPost()
        {
            await _dataContext.Categories.AddAsync(category);
            await _dataContext.SaveChangesAsync();

            return RedirectToPage("Index");
            
        }
    }
}
