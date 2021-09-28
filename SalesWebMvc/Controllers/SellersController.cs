using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;

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

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var seller = _sellerServices.GetForId(id.Value);
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel() {Seller = seller, Departments = departments};           

            return View(viewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var obj = _sellerServices.GetForId(id.Value);
            if (obj == null) return NotFound();
            return View(obj);
        }
        public IActionResult Details(int id)
        {
            var seller = _sellerServices.GetForId(id);   
            return View(seller);
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
        public IActionResult Edit(int id, Seller seller)
        {
            if (seller.Id != id) return BadRequest();

            try
            {
                _sellerServices.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbConcurrencyException)
            {
                return BadRequest();
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}