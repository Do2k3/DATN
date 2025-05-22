using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TienThinhCandy.Models
{
	public class InvoiceViewModel
	{
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}