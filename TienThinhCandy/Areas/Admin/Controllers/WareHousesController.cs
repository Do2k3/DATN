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
    public class WareHousesController : Controller
    {
        // GET: Admin/WareHouses
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Home


        //public ActionResult Index(string SearchText, int? page)
        //{
        //    var query = from o in db.Orders
        //                join od in db.OrderDetails on o.Id equals od.OrderId
        //                join p in db.Products on od.ProductId equals p.Id
        //                select new
        //                {
        //                    Id = p.Id,
        //                    Title = p.Title,
        //                    Quantity = od.Quantity,
        //                    Price = od.Price,
        //                };
        //    var rs = query.GroupBy(x => new { x.Id, x.Title })
        //          .Select(x => new
        //          {
        //              Id = x.Key.Id,
        //              Title = x.Key.Title,
        //              Quantity = x.Sum(y => y.Quantity),
        //          });
        //    var items = db.Products.ToList();
        //    var CountW = db.WareHouse.Count();
        //    var CountP = db.Products.Count();
        //    var Warehouse = db.WareHouse.ToList();
        //    foreach (var item in items)
        //    {
        //        bool check = Warehouse.Any(x => x.Title == item.Title);
        //        if (!check)
        //        {
        //            WareHouse Wh = new WareHouse();
        //            Wh.Title = item.Title;
        //            Wh.ProductCategoryId = item.ProductCategoryId;
        //            Wh.Image = item.Image;
        //            Wh.Price = item.Price;
        //            Wh.ProductCode = item.ProductCode;
        //            Wh.PriceSale = item.PriceSale;
        //            Wh.OriginalPrice = item.OriginalPrice;
        //            Wh.Quantity = item.Quantity;

        //            int totalPurchaseQuantity = rs.Where(i => i.Title == Wh.Title)
        //                      .Select(i => i.Quantity)
        //                      .DefaultIfEmpty(0)
        //                      .Sum();
        //            Wh.PurchaseQuantity = totalPurchaseQuantity;
        //            Wh.RemainingQuantity = Wh.Quantity - totalPurchaseQuantity;

        //            db.WareHouse.Add(Wh);
        //            db.SaveChanges();
        //            Warehouse = db.WareHouse.ToList();
        //        }
        //    }

        //    var pageSize = 10;
        //    if (page == null)
        //    {
        //        page = 1;
        //    }
        //    IEnumerable<WareHouse> Whs = db.WareHouse.OrderByDescending(x => x.Id);
        //    if (!string.IsNullOrEmpty(SearchText))
        //    {
        //        Whs = Whs.Where(x => x.Title.Contains(SearchText));
        //    }
        //    var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
        //    Whs = Whs.ToPagedList(pageIndex, pageSize);
        //    ViewBag.Page = page;
        //    ViewBag.PageSize = pageSize;
        //    return View(Whs);
        //}
    }
}