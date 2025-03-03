using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CPC02.Models
{
    /// <summary>
    /// INTRE 資料表的實體模型
    /// </summary>
     [Table("INTRE")]
    public class INTRE
    {
        /// <summary>
        /// 唯一識別碼 (Primary Key)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string INT000 { get; set; }

        /// <summary>
        /// 追蹤日期
        /// </summary>
        public string INT001 { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string INT002 { get; set; }

        /// <summary>
        /// 聯絡方式
        /// </summary>
        public int INT003 { get; set; }

        /// <summary>
        /// 後續步驟
        /// </summary>
        public string INT004 { get; set; }

        /// <summary>
        /// IP 位址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 狀態 (0=啟用, 1=刪除)
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 建立時間 (預設當前時間)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 會員ID
        /// </summary>
        public int Mid { get; set; }

        /// <summary>
        /// 與 INTRB 表的關聯欄位
        /// </summary>
        public string INT999 { get; set; }
    }
}