using ElectroSphere.Web.Data;
using ElectroSphere.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectroSphere.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null | id == 0)
            {
                NotFound();
            }
            var categoryIndb = _context.Categories.Find(id);
            return View(categoryIndb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var categoryIndb = _context.Categories.Find(id);
            return View(categoryIndb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var categoryIndb = _context.Categories.Find(id);
            if(categoryIndb == null)
            {
                NotFound();
            }
            _context.Categories.Remove(categoryIndb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
