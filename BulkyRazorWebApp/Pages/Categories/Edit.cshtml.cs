using BulkyRazorWebApp.DataBase;
using BulkyRazorWebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BulkyRazorWebApp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly DataContext _dataContext;
        public Category category { get; set; }
        public EditModel(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
             category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            }
            else
            {
            category = new Category();

            }

            

        }

        public async Task<IActionResult> OnPost()
        {
            _dataContext.Categories.Update(category);
            var updated =  await _dataContext.SaveChangesAsync()>0;

            if (updated) return RedirectToPage("Index");
            return Page();
        }
    }
}
