using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;

        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _dataContext.Set<Category>().ToListAsync();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if(model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name and Display order should not be same");
            }
            if (!ModelState.IsValid) return View(model);
            await _dataContext.Categories.AddAsync(model);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Category is Added Successfully.";

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound();

            return View(category);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _dataContext.Categories.Update(model);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Category is Edited Successfully.";

                return RedirectToAction("Index");
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteMethod(int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound();
             _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Category is Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}
