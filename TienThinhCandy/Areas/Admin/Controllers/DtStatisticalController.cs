using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models;

namespace TienThinhCandy.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DtStatisticalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/DtStatistical
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public ActionResult DetailStatistical(string fromDate, string toDate)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails on o.Id equals od.OrderId
                        join p in db.Products on od.ProductId equals p.Id
                        select new
                        {
                            Id = p.Id,
                            Title = p.Title,
                            CreatedDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = p.OriginalPrice
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(d => d.CreatedDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(d => d.CreatedDate == endDate);
            }
            var rs = query.GroupBy(x => new {x.Id, x.Title, Date = DbFunctions.TruncateTime(x.CreatedDate) })
                  .Select(x => new
                  {
                      Id = x.Key.Id,
                      Title = x.Key.Title,
                      Date = x.Key.Date.Value,
                      SL = x.Sum(y => y.Quantity),
                      TienVon = x.Sum(y => y.Quantity * y.OriginalPrice),
                      DoanhThu = x.Sum(y => y.Quantity * y.Price),
                      LoiNhuan = x.Sum(y => y.Quantity * y.Price) - x.Sum(y => y.Quantity * y.OriginalPrice)
                  });
            return Json(new { Data = rs }, JsonRequestBehavior.AllowGet);
        }
    }
}