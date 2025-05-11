using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TienThinhCandy.Models.DB
{
    [Table("tb_ChatbotData")]
    public class ChatbotData
	{
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(300)]
        public string Keyword { get; set; }
        [StringLength(500)]
        public string SampleQuestion { get; set; }
        [StringLength(3000)]
        public string SampleAnswer { get; set; }
        public bool IsAIGenerated { get; set; }
        [StringLength(3000)]
        public string UserQuestion { get; set; }
        [StringLength(3000)]
        public string GPTAnswer { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}