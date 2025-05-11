using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models.DB;
using TienThinhCandy.Models;

namespace TienThinhCandy.Controllers
{
    public class WishListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: WishList
        public ActionResult Index(int? page)
        {
            IEnumerable<WishList> item = db.WishLists.Where(x => x.UserName == User.Identity.Name).ToList();
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pageSize);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(item);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostWishList(int productId)
        {
            if (Request.IsAuthenticated == false)
            {
                return Json(new { Success = false, Message = "Bạn chưa đăng nhập" });
            }
            var checkItem = db.WishLists.FirstOrDefault(x => x.ProductId == productId && x.UserName == User.Identity.Name);
            if (checkItem != null)
            {
                return Json(new { Success = false, Message = "Sản phẩm đã có trong danh sách yêu thích" });
            }
            var items = new WishList();
            items.ProductId = productId;
            items.UserName = User.Identity.Name;
            items.CreatedDate = DateTime.Now;
            db.WishLists.Add(items);
            db.SaveChanges();
            return Json(new { Success = true });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.WishLists.Find(id);
            if (item != null)
            {
                db.WishLists.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}