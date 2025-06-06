using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPC02.Models
{
    [Table("ELECTRICITY_BILL")]
    public partial class ELECTRICITY_BILL
    {
        [Display(Name = "通知單號碼")]
        [Required(ErrorMessage = "必填")]
        [Key]
        [StringLength(200)]
        public string DOCUMENT_ID { get; set; }




        [Display(Name = "廠區")]
        [StringLength(200)]
        public string FACTORY { get; set; }
        [Display(Name = "電號")]
        [StringLength(200)]
        public string ELECTRICITY_SIGNAL { get; set; }
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

        [Display(Name = "尖峰用電度數")]
        public decimal? PEAK_ELECTRICITY { get; set; }

        public string FormattedPEAK_ELECTRICITY
        {
            get
            {
                return PEAK_ELECTRICITY?.ToString("#,##0");
                //return PEAK_ELECTRICITY?.ToString("0");
            }
        }

        [Display(Name = "半尖峰用電度數")]
        public decimal? HALF_SPIKE_POWER { get; set; }

        public string FormattedHALF_SPIKE_POWER
        {
            get
            {
                return HALF_SPIKE_POWER?.ToString("#,##0");
                //return HALF_SPIKE_POWER?.ToString("0");
            }
        }

        [Display(Name = "週六半尖峰用電度數")]
        public decimal? SATURDAY_HALF_PEAK { get; set; }

        public string FormattedSATURDAY_HALF_PEAK
        {
            get
            {
                return SATURDAY_HALF_PEAK?.ToString("#,##0");
                //return SATURDAY_HALF_PEAK?.ToString("0");
            }
        }

        [Display(Name = "離峰用電度數")]
        public decimal? OFF_PEAK_ELECTRICITY { get; set; }
        public string FormattedOFF_PEAK_ELECTRICITY
        {
            get
            {
                return OFF_PEAK_ELECTRICITY?.ToString("#,##0");
                //return OFF_PEAK_ELECTRICITY?.ToString("0");
            }
        }

        [Display(Name = "總用電數")]
        public decimal? TOTAL_ELECTRICITY { get; set; }
        public string FormattedTOTAL_ELECTRICITY
        {
            get
            {
                return TOTAL_ELECTRICITY?.ToString("#,##0");
                //return TOTAL_ELECTRICITY?.ToString("0");
            }
        }
        [Display(Name = "總電費(含稅)")]
        public decimal? TOTAL_BILL_TAX { get; set; }

        public string FormattedTOTAL_BILL_TAX
        {
            get
            {
                //return TOTAL_BILL_TAX?.ToString("#,##0");
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

        [Display(Name = "電費繳費通知單:上傳(最大2MB)")]
        [MaxLength]
        public byte[] PDF_FILE { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }


        [Display(Name = "電費繳費通知單:上傳(最大2MB)")]
        public byte[] PDF_CONTENT { get; set; }

        [Display(Name = "傳票號碼")]
        [StringLength(200)]
        public string VOUCHER_NUMBER { get; set; }

    }
}