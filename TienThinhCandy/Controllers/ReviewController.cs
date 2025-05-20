using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models.DB;
using TienThinhCandy.Models;
using DocumentFormat.OpenXml.Drawing;

namespace TienThinhCandy.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Review(int productId)
        {
            ViewBag.ProductId = productId;
            var item = new ReviewProduct();
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);
                if (user != null)
                {
                    item.Email = user.Email;
                    item.FullName = user.FullName;
                    item.UserName = user.UserName;
                }
                return PartialView(item);
            }

            return PartialView();
        }

        public ActionResult Load_Review(int productId, int? page)
        {
            var item = db.Reviews.Where(x => x.ProductId == productId).OrderByDescending(x => x.Id).ToList();
            ViewBag.Count = item.Count();
            return PartialView(item);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostReview(ReviewProduct req)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails
                        on o.Id equals od.OrderId
                        select new
                        {
                            CreatedDate = o.CreatedDate,
                            ProductId = od.ProductId,
                            UserName = o.UserName,
                            Status = o.Status
                        };
            if (ModelState.IsValid)
            {
                int i = 0;
                foreach (var item in query)
                {
                    if(item.ProductId == req.ProductId && item.Status == 3)
                    {
                        i = 1;
                        break;
                    }
                }
                if(i == 1)
                {
                    req.CreatedDate = DateTime.Now;
                    db.Reviews.Add(req);
                    db.SaveChanges();
                    return Json(new { Success = true });
                }
            }
            return Json(new { Success = false });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}