
using DocumentFormat.OpenXml.Drawing.Charts;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models;
using TienThinhCandy.Models.DB;

namespace TienThinhCandy.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
            IEnumerable<Models.DB.Order> items = _db.Orders.Where(x => x.Status != 1).OrderByDescending(x => x.CreatedDate).ToList();
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
            
        public ActionResult Handle(int? page, string SearchText)
        {
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Models.DB.Order> items = _db.Orders.Where(x => x.Status != 3 && x.Status != 2 && x.Status != 4).OrderByDescending(x => x.CreatedDate).ToList();
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Code.Contains(SearchText) || x.CustomerName.Contains(SearchText) || x.Phone.Contains(SearchText));
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.Page = pageNumber;
            ViewBag.PageSize = pageSize;
            TempData["Count"] = items.Count();
            return View(items.ToPagedList(pageNumber, pageSize)); 
        }


        [HttpPost]
        public ActionResult Update(int id)
        {
            var item = _db.Orders.Find(id);
            if (item != null)
            {
                _db.Orders.Attach(item);
                item.Status = 2;
                _db.Entry(item).Property(x => x.Status).IsModified = true;
                _db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "UnSuccess", Success = false });
        }
       
        public ActionResult GetListProduct(int orderId)
        {
            var productsList = (from o in _db.Orders
                                join od in _db.OrderDetails on o.Id equals od.OrderId
                                join p in _db.Products on od.ProductId equals p.Id
                                where o.Id == orderId
                                select new InvoiceViewModel
                                {
                                    CustomerName = o.CustomerName,
                                    Address = o.Address,
                                    OrderCode = o.Code,
                                    Phone = o.Phone,
                                    OrderId = o.Id,
                                    OrderDate = o.CreatedDate,
                                    ProductName = p.Title,
                                    Quantity = od.Quantity,
                                    UnitPrice = p.Price,
                                    TotalPrice = od.Quantity * p.Price
                                }).ToList();

            // Kiểm tra danh sách sản phẩm trước khi trả về
            if (productsList == null || !productsList.Any())
            {
                return Content("Không tìm thấy sản phẩm nào!");
            }

            return View("GetListProduct",productsList);
           
        }

        public ActionResult ExportPdf(int orderId)
        {
            var productsList = (from o in _db.Orders
                                join od in _db.OrderDetails on o.Id equals od.OrderId
                                join p in _db.Products on od.ProductId equals p.Id
                                where o.Id == orderId
                                select new InvoiceViewModel
                                {
                                    CustomerName = o.CustomerName,
                                    Address = o.Address,
                                    OrderCode = o.Code,
                                    Phone = o.Phone,
                                    OrderId = o.Id,
                                    OrderDate = o.CreatedDate,
                                    ProductName = p.Title,
                                    Quantity = od.Quantity,
                                    UnitPrice = p.Price,
                                    TotalPrice = od.Quantity * p.Price
                                }).ToList();
            string fullHtml = RenderViewToString("~/Areas/Admin/Views/Order/GetListProduct.cshtml", productsList); // Render toàn bộ View

            byte[] pdfBytes = GeneratePdf(fullHtml);
            return File(pdfBytes, "application/pdf", "HoaDon"+productsList.First().OrderCode +".pdf");
        }

        private string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
                if (viewResult.View == null)
                {
                    throw new InvalidOperationException($"Không tìm thấy View: {viewName}");
                }

                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.ToString();
            }
        }
        private byte[] GeneratePdf(string htmlContent)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                doc.Open();
                var htmlParser = XMLWorkerHelper.GetInstance();
                htmlParser.ParseXHtml(writer, doc, new StringReader("<html><body>" + htmlContent + "</body></html>"));

                string imagePath = Server.MapPath("~/Content/Client/img/Tienthinh.jpg");
                Image logo = Image.GetInstance(imagePath);
                logo.ScaleAbsolute(120f, 120f); // Điều chỉnh kích thước ảnh
                logo.SetAbsolutePosition(40f, 710f); // Đặt vị trí ảnh trên PDF
                doc.Add(logo);
           

                doc.Close();
                // Kiểm tra nếu HTML có dữ liệu
                if (string.IsNullOrWhiteSpace(htmlContent))
                {
                    htmlContent = "<p>Không có nội dung để xuất PDF</p>";
                }

                // Chuyển đổi HTML thành PDF
               

                doc.Close();
                return memoryStream.ToArray();
            }
        }


    }

}