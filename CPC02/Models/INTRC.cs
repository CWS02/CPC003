using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CPC02.Models
{
    [Table("INTRC")]
    public class INTRC
    {
        /// <summary>
        /// 隨機碼（主鍵）。
        /// </summary>
        [Key]
        public string INT000 { get; set; }

        /// <summary>
        /// 報價時間。
        /// </summary>
        public string INT001 { get; set; }

        /// <summary>
        /// 報價明細（JSON 格式）。
        /// </summary>
        public string INT002 { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [AllowHtml]
        public string INT003 { get; set; }
        /// <summary>
        /// 交期
        /// </summary>
        public string INT004 { get; set; }
        /// <summary>
        /// 是否稅額
        /// </summary>
        public bool? INT005 { get; set; }
        /// <summary>
        /// 檔案網址
        /// </summary>
        [StringLength(150)]
        public string INT006 { get; set; }
        /// <summary>
        /// 備註2
        /// </summary>
        [AllowHtml]
        public string INT007 { get; set; }
        /// <summary>
        /// 業務人員 ID。
        /// </summary>
        public int Mid { get; set; }

        /// <summary>
        /// INTRA-INT000 關聯欄位。
        /// </summary>
        public string INT999 { get; set; }

        /// <summary>
        /// 客戶端 IP 位址。
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 訂單狀態 0=追蹤中 1=已結案 2=正式訂單
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 建立時間，預設為當前時間。
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// JSON 結構
    /// </summary>
    public class QuotationDetail
    {
        /// <summary>
        /// 邊距 1。
        /// </summary>
        public string Margin1 { get; set; }

        /// <summary>
        /// 邊距 2。
        /// </summary>
        public string Margin2 { get; set; }

        /// <summary>
        /// 單位。
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 數量。
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 單價。
        /// </summary>
        public string UnitPrice { get; set; }
    }
}