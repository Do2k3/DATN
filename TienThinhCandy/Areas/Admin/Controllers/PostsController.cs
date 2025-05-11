using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models;
using TienThinhCandy.Models.DB;
using PagedList;

namespace TienThinhCandy.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        // GET: Admin/Posts
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Posts> item = _dbContext.Posts.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                item = item.Where(x => x.Alias.Contains(SearchText) || x.Title.Contains(SearchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pageSize);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View(item);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Posts model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 2;
                model.ModifiedDate = DateTime.Now;
                model.Alias = TienThinhCandy.Models.Common.Filter.FilterChar(model.Title);
                _dbContext.Posts.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

        }
        public ActionResult Edit(int id)
        {
            var item = _dbContext.Posts.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Posts model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = TienThinhCandy.Models.Common.Filter.FilterChar(model.Title);
                _dbContext.Posts.Attach(model);
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Posts.Find(id);
            if (item != null)
            {
                _dbContext.Posts.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbContext.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }

        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _dbContext.Posts.Find(Convert.ToInt32(item));
                        _dbContext.Posts.Remove(obj);
                        _dbContext.SaveChanges();
                    }
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}