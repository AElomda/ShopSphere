using Microsoft.AspNetCore.Mvc;
using ElectroSphere.DataAccess;
using ElectroSphere.Entities.Models;
using ElectroSphere.Entities.Repositories;
using ElectroSphere.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace ElectroSphere.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //_context.Categories.Add(producr);
                _unitOfWork.Product.Add(product);
                //_context.SaveChanges();
                _unitOfWork.Complete();
                TempData["Create"] = "Data Has Created Succsesfully";
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var producrIndb = _unitOfWork.Product.GetFirstorDefault(x => x.Id == id);
            return View(producrIndb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                //_context.Categories.Update(producr);
                _unitOfWork.Product.Update(product);
                // _context.SaveChanges();
                _unitOfWork.Complete();
                TempData["Update"] = "Data Has Updated Succsesfully";
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var producrIndb = _unitOfWork.Product.GetFirstorDefault(x => x.Id == id);
            return View(producrIndb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var producrIndb = _unitOfWork.Product.GetFirstorDefault(x => x.Id == id);
            if (producrIndb == null)
            {
                NotFound();
            }
            //_context.Categories.Remove(producrIndb);
            _unitOfWork.Product.Remove(producrIndb);
            //_context.SaveChanges();
            _unitOfWork.Complete();
            TempData["Delete"] = "Data Has Deleted Succsesfully";
            return RedirectToAction("Index");
        }
    }
}
