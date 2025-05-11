using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models.DB;
using TienThinhCandy.Models;

namespace TienThinhCandy.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Product> item = _dbContext.Products.OrderByDescending(x => x.Id);
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
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rdDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rdDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true
                            });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = TienThinhCandy.Models.Common.Filter.FilterChar(model.Title);
                }
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                _dbContext.Products.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return Json(new { success = true, isSale = item.IsSale });
            }
            return Json(new { success = false });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            var item = _dbContext.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = TienThinhCandy.Models.Common.Filter.FilterChar(model.Title);
                _dbContext.Products.Attach(model);
                _dbContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_dbContext.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item != null)
            {
                _dbContext.Products.Remove(item);
                _dbContext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _dbContext.Products.Find(Convert.ToInt32(item));
                        _dbContext.Products.Remove(obj);
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