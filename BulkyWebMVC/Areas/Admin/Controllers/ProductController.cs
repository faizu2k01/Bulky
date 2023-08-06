
using Bulky.DataAccess;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace BulkyWebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {

            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductViewModel model = new()
            {
                Product = new Product(),
                CategoryList =  _unitOfWork.Category.GetAll().Result.Select(x => new SelectListItem { Text = x.Name,Value = x.Id.ToString()})
            };

            if(id == null || id == 0)
            {
            return View(model);
            }
            else
            {
                model.Product = await _unitOfWork.Product.Get(x => x.Id == id);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductViewModel model,IFormFile? file)
        {

            model.CategoryList = _unitOfWork.Category.GetAll().Result.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            if (!ModelState.IsValid)
            {

                return View(model);
            }
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            if (model.Product.Id != 0 && file != null)
            {
                if (!string.IsNullOrEmpty(model.Product.ImageURl))
                {
                    var oldPath = Path.Combine(wwwRootPath, model.Product.ImageURl.Trim('\\'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                
            }
            if (file != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var properPath = Path.Combine(wwwRootPath, @"images\product\");
                using(var fileStream = new FileStream(Path.Combine(properPath,fileName),FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                model.Product.ImageURl = @"\images\product\"+fileName;
            }

            
            
            if(model.Product.Id == 0)
            {
            var saved = await _unitOfWork.Product.AddAsync(model.Product);
                TempData["success"] = "Product is Added Successfully.";
                if (!saved) return View(model);
                return RedirectToAction("Index");
            }
            else
            {
                var result = _unitOfWork.Product.Update(model.Product);
                if (!result.Result) return View(model);
                return RedirectToAction("Index");
            }

            

        }



    

       

     



        #region API calls
        [HttpGet]
        //Because of conventional routing it reads the action name
        public async Task<IActionResult> GetAll()
        {
            var list = await _unitOfWork.Product.GetAll(includeProperties: "Category");

            return Json(new { data=list });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var item = await _unitOfWork.Product.Get(x => x.Id == id);
            if(item == null)
            {
                return Json(new { success = false, message = "Error found" });
            }else
            {
                if (!string.IsNullOrEmpty(item.ImageURl))
                {
                    var wwwRootPath = _webHostEnvironment.WebRootPath;
                    var oldPath = Path.Combine(wwwRootPath, item.ImageURl.Trim('\\'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

              var resu =  await _unitOfWork.Product.Remove(item);
               if(resu) return Json(new { success = true, message = "Removed" });
                return Json(new { success = false, message = "Issue while removing" });
            }

        }
        #endregion
    }
}
