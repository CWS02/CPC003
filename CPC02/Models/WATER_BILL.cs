using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPC02.Models
{
    [Table("WATER_BILL")]
    public partial class WATER_BILL
    {
        [Display(Name = "單據號碼")]
        [Required(ErrorMessage = "必填")]
        [Key]
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }

        [Display(Name = "廠區")]
        [StringLength(200)]
        public string FACTORY { get; set; }
        [Display(Name = "水號")]
        [StringLength(200)]
        public string WATER_SIGNAL { get; set; }

        [Display(Name = "計費期間起")]
        [Column(TypeName = "datetime2")]
        public DateTime? FROM_BILLING_PERIOD { get; set; }
        public string FormattedFROM_BILLING_PERIOD
        {
            get
            {
                return FROM_BILLING_PERIOD?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "計費期間迄")]
        [Column(TypeName = "datetime2")]
        public DateTime? UNTIL_BILLING_PERIOD { get; set; }
        public string FormattedUNTIL_BILLING_PERIOD
        {
            get
            {
                return UNTIL_BILLING_PERIOD?.ToString("yyyy-MM-dd");
            }
        }

        [Display(Name = "用水種別")]
        [StringLength(200)]
        public string TYPE { get; set; }

        [Display(Name = "水表表號")]
        [StringLength(200)]
        public string METER_NUMBER { get; set; }


        [Display(Name = "水表口徑")]
        public int METER_DIAMETER { get; set; }


        [Display(Name = "本期指針數")]
        public decimal? NUMBER_POINTERS { get; set; }

        public string FormattedNUMBER_POINTERS
        {
            get
            {
                return NUMBER_POINTERS?.ToString("#,##0");
                //return NUMBER_POINTERS?.ToString("0");
            }
        }

        [Display(Name = "總用度數")]
        public decimal? TOTAL_WATER { get; set; }
        public string FormattedTOTAL_WATER
        {
            get
            {
                return TOTAL_WATER?.ToString("#,##0");
                //return TOTAL_WATER?.ToString("0");
            }
        }

        [Display(Name = "總水費(含稅)")]
        public decimal? TOTAL_BILL_TAX { get; set; }

        public string FormattedTOTAL_BILL_TAX
        {
            get
            {
                return TOTAL_BILL_TAX?.ToString("C0");
                //顯示成金額格式
            }
        }

        [Display(Name = "本期碳排量")]
        public decimal? CARBON_PERIOD { get; set; }

        public string FormattedCARBON_PERIOD
        {
            get
            {
                return CARBON_PERIOD?.ToString("#,##0");
                //return CARBON_PERIOD?.ToString("0");
            }
        }

        [Display(Name = "碳排係數")]
        public decimal? CARBON_EMISSION_FACTOR { get; set; }
        [Display(Name = "二氧化碳當量")]
        public decimal? CARBON_DIOXIDE { get; set; }
        [Display(Name = "建立時間")]
        public DateTime? data { get; set; }
        [Display(Name = "建立者")]
        public string CreatedBy { get; set; }

        [Display(Name = "水費電子通知單:上傳(最大2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [Display(Name = "水費電子通知單:上傳(最大2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "傳票號碼")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }
    }
}