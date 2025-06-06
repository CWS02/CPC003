using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("FireExtin")]
    public partial class FireExtin
    {
        [Display(Name = "滅火器名稱")]
        [StringLength(50)]
        public string FE001 { get; set; }


        [Display(Name = "數量")]
        [Column(TypeName = "numeric")]
        public decimal? FE002 { get; set; }

        [Display(Name = "位置")]
        [StringLength(10)]
        public string FE003 { get; set; }

        [Display(Name = "廠區位置")]
        [StringLength(50)]
        public string FE004 { get; set; }

        [Display(Name = "內含物")]
        [StringLength(50)]
        public string FE005 { get; set; }

        [Display(Name = "原始填充量(kg/瓶)")]
        [Column(TypeName = "numeric")]
        public decimal? FE006 { get; set; }

        [Display(Name = "管理部門")]
        [StringLength(10)]
        public string FE007 { get; set; }


        [StringLength(10)]
        public string FE008 { get; set; }

        [Display(Name = "相片連結")]
        [StringLength(30)]
        public string FE009 { get; set; }

        [Key]
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal FE000 { get; set; }

        [Display(Name = "廠區")]
        [StringLength(50)]
        public string FE010 { get; set; }
    }
}