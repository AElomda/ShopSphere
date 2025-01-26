﻿using Microsoft.AspNetCore.Mvc;
using ElectroSphere.DataAccess;
using ElectroSphere.Entities.Models;
using ElectroSphere.Entities.Repositories;



namespace ElectroSphere.Web.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var categories = _unitOfWork.Category.GetAll();
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
                //_context.Categories.Add(category);
                _unitOfWork.Category.Add(category);
                //_context.SaveChanges();
                _unitOfWork.Complete();     
                TempData["Create"] = "Data Has Created Succsesfully";
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
            var categoryIndb = _unitOfWork.Category.GetFirstorDefault(x => x.Id == id);
            return View(categoryIndb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //_context.Categories.Update(category);
                _unitOfWork.Category.Update(category);
                // _context.SaveChanges();
                _unitOfWork.Complete();
                TempData["Update"] = "Data Has Updated Succsesfully";
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
            var categoryIndb = _unitOfWork.Category.GetFirstorDefault(x => x.Id == id);
            return View(categoryIndb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var categoryIndb = _unitOfWork.Category.GetFirstorDefault(x => x.Id == id);
            if (categoryIndb == null)
            {
                NotFound();
            }
            //_context.Categories.Remove(categoryIndb);
            _unitOfWork.Category.Remove(categoryIndb);
            //_context.SaveChanges();
            _unitOfWork.Complete();
            TempData["Delete"] = "Data Has Deleted Succsesfully";
            return RedirectToAction("Index");
        }
    }
}
