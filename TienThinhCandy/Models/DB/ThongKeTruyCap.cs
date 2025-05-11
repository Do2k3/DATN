using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TienThinhCandy.Models.DB
{
	[Table("ThongKeTruyCaps")]
	public class ThongKeTruyCap
	{

		[Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public DateTime ThoiGian { get; set; }
		public long SoTruyCap { get; set; }
	}
}