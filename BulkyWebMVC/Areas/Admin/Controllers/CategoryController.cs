
using Bulky.DataAccess;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _unitOfWork.Category.GetAll();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Name and Display order should not be same");
            }
            if (!ModelState.IsValid) return View(model);
            var saved = await _unitOfWork.Category.AddAsync(model);

            if (saved)
            {
                TempData["success"] = "Category is Added Successfully.";

                return RedirectToAction("Index");
            }

            return View(model);

        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var category = await _unitOfWork.Category.Get(x => x.Id == id);
            if (category == null) return NotFound();

            return View(category);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                var result = _unitOfWork.Category.Update(model);

                if (result.Result)
                {
                    TempData["success"] = "Category is Edited Successfully.";

                    return RedirectToAction("Index");
                }

            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Category.Get(x => x.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteMethod(int id)
        {
            var category = await _unitOfWork.Category.Get(x => x.Id == id);
            if (category == null) return NotFound();
            await _unitOfWork.Category.Remove(category);
            TempData["success"] = "Category is Deleted Successfully.";
            return RedirectToAction("Index");
        }
    }
}
