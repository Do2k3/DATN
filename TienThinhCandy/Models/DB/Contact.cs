using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TienThinhCandy.Models.DB
{
    [Table("tb_Contact")]
    public class Contact : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = ("Không được để trống trường này "))]
        public string Name { get; set; }
        public string Website { get; set; }
        [Required(ErrorMessage = ("Không được để trống trường này"))]

        public string Link { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
    }
}