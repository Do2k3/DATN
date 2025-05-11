using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models;

namespace TienThinhCandy.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = _dbContext.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("MenuTop", items);
        }
        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.CateId = id;
            }
            var items = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuLeft", items);
        }
        public ActionResult MenuProductCategory()
        {
            var items = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuProductCategory", items);
        }
        public ActionResult MenuArrivals()
        {
            var items = _dbContext.ProductCategories.ToList();
            return PartialView("_MenuArrivals", items);
        }
        public ActionResult MenuRight()
        {
            var items = _dbContext.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("MenuRight", items);
        }
        public ActionResult MenuContact()
        {
            var item = _dbContext.Contacts.OrderBy(x => x.Id).ToList();
            return PartialView("MenuContact", item);
        }
    }
}