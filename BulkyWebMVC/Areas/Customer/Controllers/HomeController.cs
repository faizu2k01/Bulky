using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWebMVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }



        public async Task<IActionResult> Index()
        {

            var products = await _unitOfWork.Product.GetAll(includeProperties: "Category");

            return View(products);
        }



        public async Task<IActionResult> Detail(int id)
        {

            var product = await _unitOfWork.Product.Get(x => x.Id == id, includeProperties: "Category");

            if(product == null)
            {
                RedirectToAction("Index");
            }


            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}