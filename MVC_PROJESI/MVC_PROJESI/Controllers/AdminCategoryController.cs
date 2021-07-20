using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_PROJESI.Controllers
{
    public class AdminCategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        // GET: Admin
        public ActionResult Index()
        {
            var CategoryValues = cm.GetCatList();

            return View(CategoryValues);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            CategoryValidation validationRules = new CategoryValidation();
            ValidationResult validationResult = validationRules.Validate(category);
            if (validationResult.IsValid)
            {
                cm.CategoryAdd(category);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult DeleteCategory(int id)
        {
            var CategoryValue = cm.GetByID(id);
            cm.CategoryDelete(CategoryValue);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var CategoryValue = cm.GetByID(id);
            return View(CategoryValue);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            CategoryValidation validationRules = new CategoryValidation();
            ValidationResult validationResult = validationRules.Validate(category);
            if (validationResult.IsValid)
            {
                cm.CategoryUpdate(category);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}