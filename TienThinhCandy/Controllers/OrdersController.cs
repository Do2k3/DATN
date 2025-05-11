using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TienThinhCandy.Models;
using TienThinhCandy.Models.DB;

namespace TienThinhCandy.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Orders
        public ActionResult Index(int? page)
        {
       
            IEnumerable<Order> items = db.Orders.Where(x => x.UserName == User.Identity.Name).OrderByDescending(x =>x.CreatedDate).ToList();
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View(items);
        }
        
        public ActionResult WaitShipping()
        {
           var items = db.Orders.Where( x =>x.Status == 1 && x.UserName == User.Identity.Name).OrderByDescending(x => x.CreatedDate).ToList();
            return View(items);
        }

        public ActionResult Shipped()
        {
            var items = db.Orders.Where(x => x.Status == 2 && x.UserName == User.Identity.Name).OrderByDescending(x => x.CreatedDate).ToList();

            return View(items);
        }
        public ActionResult Compelete()
        {
            var items = db.Orders.Where(x => x.Status == 3 && x.UserName == User.Identity.Name).OrderByDescending(x => x.CreatedDate).ToList();

            return View(items);
        }

        public ActionResult ViewDetail(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }
        
        public ActionResult Partial_DetailOrder(int id)
        {
            var item = db.OrderDetails.Where(x => x.OrderId == id);
            return PartialView(item);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}