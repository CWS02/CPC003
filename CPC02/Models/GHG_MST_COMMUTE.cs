using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("GHG_MST_COMMUTE")]
    public partial class GHG_MST_COMMUTE
    {
        [Column(TypeName = "datetime2")]
        public DateTime USEDT { get; set; }

        [Display(Name = "員工編號")]

        [Key]
        [Required]
        [StringLength(200)]
        public string USER_ID { get; set; }


        [Display(Name = "姓名")]
        [Required]
        [StringLength(200)]
        public string USER_NAME { get; set; }

        [Display(Name = "交通工具")]
        [Required]
        [StringLength(200)]
        public string TRANSPORTATION { get; set; }

        [Display(Name = "單程公里數")]
        [Required]

        public decimal? ONEWAY_KM { get; set; }

        [Display(Name = "來回公里數")]
        //public decimal? DOUBLE_KM { get; set; }
        public decimal? DOUBLE_KM
        {
            get { return ONEWAY_KM * 2; }
            set { ONEWAY_KM = value / 2; }
        }

        [Display(Name = "建立時間")]
        public DateTime? data { get; set; }
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }
    }
}