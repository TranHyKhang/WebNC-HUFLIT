using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TH05.Models;

namespace TH05.Controllers
{
    public class ProductsController : Controller
    {
        TH05Entities databse = new TH05Entities();

        // GET: Categories
        public ActionResult Index(string searchValue)
        {
            if (searchValue == null)
            {
                return View(databse.Products.OrderByDescending(x => x.NamePro));
            }
            else
            {
                return View(databse.Products.OrderByDescending(x => x.NamePro).Where(x => x.Category == searchValue));

            }
        }

        public ActionResult Create()
        {
            var categoryList = databse.Categories.ToList();
            ViewBag.CategoryList = categoryList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Product cate)
        {
            try
            {
                databse.Products.Add(cate);
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
            return View(databse.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }

        public ActionResult Delete(int id)
        {
            return View(databse.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, Product cate)
        {
            try
            {
                cate = databse.Products.Where(s => s.ProductID == id).FirstOrDefault();
                databse.Products.Remove(cate);
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
            var categoryList = databse.Categories.ToList();
            ViewBag.CategoryList = categoryList;

            return View(databse.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, Product cate)
        {
            databse.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            databse.SaveChanges();
            return RedirectToAction("Index");
        }

        //search option
        public ActionResult SearchOption(double min = double.MinValue, double max = double.MaxValue)
        {
            var list = databse.Products.Where(p => (double)p.Price >= min && (double)p.Price <= max).ToList();
            return View(list);
        }
    }
}