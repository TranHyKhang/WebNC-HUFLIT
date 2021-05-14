using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TH05.Models;

namespace TH05.Controllers
{
    public class CategoriesController : Controller
    {

        TH05Entities databse = new TH05Entities();

        // GET: Categories
        public ActionResult Index(string searchValue)
        {
            if(searchValue == null)
            {
                return View(databse.Categories.ToList());
            }
            return View(databse.Categories.Where(s => s.NameCate.Contains(searchValue)).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                databse.Categories.Add(cate);
                databse.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Create fail");
            }
        }

        public ActionResult Details(int id)
        {
            return View(databse.Categories.Where(s => s.Id == id).FirstOrDefault());
        }

        public ActionResult Delete(int id)
        {
            return View(databse.Categories.Where(s => s.Id == id).FirstOrDefault());
        }

        [HttpPost] 
        public ActionResult Delete(int id, Category cate)
        {
            try
            {
                cate = databse.Categories.Where(s => s.Id == id).FirstOrDefault();
                databse.Categories.Remove(cate);
                databse.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Delete fail");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(databse.Categories.Where(s => s.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            databse.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            databse.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult CategoryPartial()
        {
            var cateList = databse.Categories.ToList();
            return PartialView(cateList);
        }
    }
}