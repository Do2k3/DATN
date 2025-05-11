using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using TienThinhCandy.Models.DB;

namespace TienThinhCandy.Models.DB
{
    [Table("tb_Category")]
    public class Category:CommonAbstract
	{
        public Category()
        {
            this.Posts = new HashSet<Posts>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = ("Không được để trống tên danh mục"))]
        [StringLength(250)]
        public string Title { get; set; }
        public string Alias { get; set; }

        //public string TypeCode { get; set; }
        //public string Link { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(250)]
        public string SeoTitle { get; set; }
        [StringLength(250)]
        public string SeoDescription { get; set; }
        [StringLength(250)]
        public string SeoKeywords { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Posts> Posts { get; set; }
    }
}
