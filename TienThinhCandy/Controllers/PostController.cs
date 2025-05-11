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
    public class PostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Post
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Posts> item = db.Posts.OrderByDescending(x => x.CreatedDate).ToList();
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            item = item.ToPagedList(pageIndex, pageSize);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View(item);
        }
        public ActionResult Detail(int id)
        {
            var item = db.Posts.Find(id);
            return View(item);
        }
        public ActionResult Partial_Post_Home()
        {
            var items = db.Posts.Take(3).ToList();
            return PartialView(items);
        }
    }
}