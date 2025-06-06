using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPC02.Models
{
    [Table("WASTES")]

    public partial class WASTES
    {
        [Display(Name = "聯單編號")]
        [Required(ErrorMessage = "必填")]
        [Key]

        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }
        [Display(Name = "清運日期")]
        [Column(TypeName = "datetime2")]
        public DateTime? REMOVAL_DATE { get; set; }

        public string FormattedRemovalDate
        {
            get
            {
                return REMOVAL_DATE?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "清除者代碼")]
        [StringLength(200)]
        public string CLEANER_CODE { get; set; }
        [Display(Name = "清除者名稱")]
        [StringLength(200)]
        public string PURGE_NAME { get; set; }
        [Display(Name = "運載車號")]
        [StringLength(200)]
        public string NUMBER_CARRIER { get; set; }
        [Display(Name = "中間處理方式")]
        [StringLength(200)]
        public string TREATMENT { get; set; }
        [Display(Name = "最終處置者代碼")]
        [StringLength(200)]
        public string FINAL_DISPOSER_CODE { get; set; }
        [Display(Name = "最終處置者名稱")]
        [StringLength(200)]
        public string FINAL_DISPOSER_NAME { get; set; }
        [Display(Name = "最終處置方式")]
        [StringLength(200)]
        public string FINAL_DISPOSER { get; set; }
        [Display(Name = "廢棄物代碼")]
        [StringLength(200)]
        public string SCRAP_CODE { get; set; }
        [Display(Name = "廢棄物名稱")]
        [StringLength(200)]
        public string SCRAP_NAME { get; set; }
        [Display(Name = "申報重量(公噸)")]
        public decimal? DECLARED_WEIGHT { get; set; }
        [Display(Name = "運輸公里")]
        public decimal? KILOMETERS { get; set; }
        [Display(Name = "活動數據")]
        public decimal? ACTIVITY_DATA { get; set; }
        [Display(Name = "排放係數")]
        public decimal? CARBON_EMISSION_FACTOR { get; set; }
        [Display(Name = "二氧化碳當量")]
        public decimal? CARBON_DIOXIDE { get; set; }

        public DateTime? data { get; set; }

        public string CreatedBy { get; set; }

        [Display(Name = "秤重照片:上傳(最大2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }


        [Display(Name = "秤重照片:上傳(最大2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "傳票號碼")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }
    }
}
//HomeController_20231226紀錄登打時間
namespace prjWastes6.Models.Extensions
{

    public partial class WASTES
    {
        public DateTime? CreatedTime { get; set; }
    }
}