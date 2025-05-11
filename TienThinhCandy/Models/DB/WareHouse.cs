using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TienThinhCandy.Models.DB
{
    [Table("tb_WareHouse")]
	public class WareHouse
	{
        public WareHouse()
        {
            this.ProductImage = new HashSet<ProductImage>();
            
        }
        [Key]
        
        public int Id { get; set; }
        [Required(ErrorMessage = ("Không được để trống tên sản phẩm"))]
        [StringLength(250)]
        public string Title { get; set; }
        public string ProductCode { get; set; }
        public int ProductCategoryId { get; set; }
        public string Image { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public int Quantity { get; set; }
        public int PurchaseQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
    }
}