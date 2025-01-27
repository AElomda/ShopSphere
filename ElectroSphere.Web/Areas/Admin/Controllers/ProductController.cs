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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
        public IActionResult Create(ProductVM productVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var upload = Path.Combine(RootPath, @"Images\Product");
                    var ext = Path.GetExtension(file.FileName);
                    using (var filestream = new FileStream(Path.Combine(upload, filename + ext), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    productVM.Product.Img = @"Images\Products\" + filename + ext;
                }
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Complete();
                TempData["Create"] = "Data Has Created Succsesfully";
                return RedirectToAction("Index");
            }
            return View(productVM.Product);
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
