using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerServices _sellerServices;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerServices sellerServices, DepartmentService departmentService)
        {
            _sellerServices = sellerServices;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var lista = _sellerServices.FindAll();
            return View(lista);
        }
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel() {Departments = departments };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var seller = _sellerServices.GetForId(id);
            var departments = _departmentService.FindAll();

            var viewModel = new SellerFormViewModel() {Seller = seller, Departments = departments};
            

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerServices.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SellerFormViewModel sellerViewModel)
        {          
            _sellerServices.Insert(sellerViewModel.Seller);
            return RedirectToAction(nameof(Index));
        }
    }
}