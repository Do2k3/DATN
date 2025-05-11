using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models;
using TienThinhCandy.Models.DB;

namespace TienThinhCandy.Areas.Admin.Controllers
{
  
        // GET: Admin/Order
        public class OrderController : Controller
        {
            private ApplicationDbContext _db = new ApplicationDbContext();
            // GET: Admin/Order
            public ActionResult Index(int? page, string SearchText)
            {

                if (page == null)
                {
                    page = 1;
                }
                IEnumerable<Order> items = _db.Orders.OrderByDescending(x => x.CreatedDate).ToList();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    items = items.Where(x => x.Code.Contains(SearchText) || x.CustomerName.Contains(SearchText) || x.Phone.Contains(SearchText));
                }
                var pageNumber = page ?? 1;
                var pageSize = 10;
                ViewBag.Page = pageNumber;
                ViewBag.PageSize = pageSize;
                return View(items.ToPagedList(pageNumber, pageSize));
            }

            public ActionResult View(int id)
            {
                
                var item = _db.Orders.Find(id);
                return View(item);
            }

            public ActionResult Partial_OrderDetail(int id)
            {
                var item = _db.OrderDetails.Where(x => x.OrderId == id);
                return PartialView(item);
            }
            [HttpPost]
            public ActionResult UpdateTT(int id, int tt)
            {
                var item = _db.Orders.Find(id);
                if (item != null)
                {
                    _db.Orders.Attach(item);
                    item.Status = tt;       
                    _db.Entry(item).Property(x => x.Status).IsModified = true;
                    _db.SaveChanges();
                    return Json(new { message = "Success", Success = true });
                }
                return Json(new { message = "UnSuccess", Success = false });
            }
        }
    
}