using DocumentFormat.OpenXml.Wordprocessing;
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
   //HomeController
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index( string SearchText )
        {

            IEnumerable<Product> item = db.Products.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                item = item.Where(x => x.Alias.Contains(SearchText) || x.Title.Contains(SearchText));
            }
           
            return View(item);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Refresh()
        {
            var item = new ThongKeModel();

            ViewBag.Visitors_online = HttpContext.Application["visitors_online"];
            var hn = HttpContext.Application["HomNay"];
            item.HomNay = HttpContext.Application["HomNay"].ToString();
            item.HomQua = HttpContext.Application["HomQua"].ToString();
            item.TuanNay = HttpContext.Application["TuanNay"].ToString();
            item.TuanTruoc = HttpContext.Application["TuanTruoc"].ToString();
            item.ThangNay = HttpContext.Application["ThangNay"].ToString();
            item.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
            item.TatCa = HttpContext.Application["TatCa"].ToString();
            return PartialView(item);
        }

        public ActionResult Partial_Subscribe()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(Subscribe req)
        {
            if (ModelState.IsValid)
            {
                db.Subscribes.Add(new Subscribe
                {
                    Email = req.Email,
                    CreatedDate = DateTime.Now,
                });
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return View("Partial_Subscribe", req);
        }
    }
}