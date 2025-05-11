using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TienThinhCandy.Models.DB
{ 
    [Table("tb_Order")]
    public class Order : CommonAbstract
    {
        public Order()
        {
            this.OrderDetail = new HashSet<OrderDetail>();
           
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public int TypePayment { get; set; }
        public string CustomerId {  get; set; }
        
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        
    }
}