using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("ColdCoal")]
    public partial class ColdCoal
    {
        [Display(Name = "編號")]
        [StringLength(30)]
        public string CC001 { get; set; }

        [Display(Name = "機台名稱")]
        [StringLength(255)]
        public string CC002 { get; set; }

        [Display(Name = "部門代號/保管人")]
        [StringLength(20)]
        public string CC003 { get; set; }
        [Display(Name = "廠區位置")]
        [StringLength(20)]
        public string CC004 { get; set; }
        [Display(Name = "冷卻機台名稱")]
        [StringLength(50)]
        public string CC005 { get; set; }
        [Display(Name = "冷卻機台編號")]
        [StringLength(30)]
        public string CC006 { get; set; }
        [Display(Name = "冷媒類型")]
        [StringLength(30)]
        public string CC007 { get; set; }
        [Display(Name = "原始填充量(公斤)")]
        [Column(TypeName = "numeric")]
        public decimal? CC008 { get; set; }
        [Display(Name = "相片連結")]
        [StringLength(40)]
        public string CC009 { get; set; }

        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal CC000 { get; set; }

        [Display(Name = "廠區")]
        [StringLength(20)]
        public string CC010 { get; set; }
        [Display(Name = "採用之年逸散率（%）")]
        [StringLength(20)]
        public string CC011 { get; set; }
        [Display(Name = "逸散量(公噸)")]
        public decimal? CC012 { get; set; }
    }
}