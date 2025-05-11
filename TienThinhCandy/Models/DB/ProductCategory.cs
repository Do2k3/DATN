using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TienThinhCandy.Models.DB
{
    [Table("tb_ProductCategory")]
    public class ProductCategory : CommonAbstract
    {
        public ProductCategory()
        {
            this.Product = new HashSet<Product>();
            this.WareHouse = new HashSet<WareHouse>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = ("Không được để trống "))]
        [StringLength(250)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        public ICollection<Product> Product { get; set; }
        public ICollection<WareHouse> WareHouse { get; set; }
    }
}