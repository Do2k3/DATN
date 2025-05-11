using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models;
using TienThinhCandy.Models.DB;

namespace TienThinhCandy.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var items = _db.Categories;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now; 
                model.ModifiedDate = DateTime.Now;
                model.Alias = TienThinhCandy.Models.Common.Filter.FilterChar(model.Title);
                _db.Categories.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var item = _db.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Attach(model);
                model.ModifiedDate = DateTime.Now;

                model.Alias = TienThinhCandy.Models.Common.Filter.FilterChar(model.Title);
                _db.Entry(model).Property(x => x.Title).IsModified = true;
                _db.Entry(model).Property(x => x.Description).IsModified = true;
                _db.Entry(model).Property(x => x.Alias).IsModified = true;
                _db.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                _db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                _db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                _db.Entry(model).Property(x => x.Position).IsModified = true;
                _db.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                _db.Entry(model).Property(x => x.ModifiedBy).IsModified = true;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _db.Categories.Find(id);
            if (item != null)
            {
                _db.Categories.Remove(item);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}