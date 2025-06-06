using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("WD40A")]
    public class WD40A
    {
        /// <summary>
        /// 唯一碼
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "唯一碼")]
        public string WD000 { get; set; }

        /// <summary>
        /// 採購單
        /// </summary>
        [Display(Name = "採購單")]
        public string WD001 { get; set; }

        /// <summary>
        /// 供應廠
        /// </summary>
        [Display(Name = "供應廠")]
        public string WD002 { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Display(Name = "日期")]
        public string WD003 { get; set; }

        /// <summary>
        /// 品號
        /// </summary>
        [Display(Name = "品號")]
        public string WD004 { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        [Display(Name = "品名")]
        public string WD005 { get; set; }

        /// <summary>
        /// 已交量
        /// </summary>
        [Display(Name = "已交量")]
        [DataType(DataType.Currency)]
        public decimal WD006 { get; set; }

        /// <summary>
        /// 交期
        /// </summary>
        [Display(Name = "交期")]
        public string WD007 { get; set; }

        /// <summary>
        /// 內容量(克/罐)
        /// </summary>
        [Display(Name = "內容量(克/罐)")]
        public decimal WD008 { get; set; }

        /// <summary>
        /// 重量(克)
        /// </summary>
        [Display(Name = "重量(克)")]
        public decimal WD009 { get; set; }

        /// <summary>
        /// 二氧化碳占比%
        /// </summary>
        [Display(Name = "二氧化碳占比%")]
        public decimal WD010 { get; set; }

        /// <summary>
        /// 二氧化碳重量(克)
        /// </summary>
        [Display(Name = "二氧化碳重量(克)")]
        public decimal WD011 { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        [Display(Name = "IP")]
        public string IP { get; set; }

        /// <summary>
        /// 狀態 0=未審核,1=刪除
        /// </summary>
        [Display(Name = "狀態")]
        public int Status { get; set; }

        /// <summary>
        /// 建立時間(自動帶入)
        /// </summary>
        [Display(Name = "建立時間")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
    }
}