using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models;
using TienThinhCandy.Models.DB;

namespace TienThinhCandy.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 8;
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
        public ActionResult ProductCategory(string alias, int id)
        {
            var items = _dbContext.Products.ToList();
            if (id > 0)
            {
                items = items.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = _dbContext.ProductCategories.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;
            }

            ViewBag.CateId = id;
            return View(items);
        }

        public ActionResult Detail(string alias, int? id)
        {
            var item = _dbContext.Products.Find(id);
            if(item != null)
            {
                _dbContext.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                _dbContext.Entry(item).Property(x => x.ViewCount).IsModified = true;
                _dbContext.SaveChanges();
            }
            
            return View(item);
        }

        public ActionResult Partial_ItemsByCateId()
        {
            var items = _dbContext.Products.Where(x => x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSales()
        {
            var items = _dbContext.Products.Where(x => x.IsSale && x.IsActive).Take(12).ToList();
            return PartialView(items);
        }


    }
}