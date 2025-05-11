using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TienThinhCandy.Models;
using TienThinhCandy.Models.DB;

namespace TienThinhCandy.Models.DB
{
    [Table("tb_Posts")]
    public class Posts : CommonAbstract
    {
        
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được để trống tên bài viết")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public int CategoryId { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public int ViewCount { get; set; }
        public string Image { get; set; }
        [StringLength(250)]
        public string SeoTitle { get; set; }
        [StringLength(250)]
        public string SeoDescription { get; set; }
        [StringLength(250)]
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public virtual Category Category { get; set; }
    }
}