using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("WLOGA")]
    public class WLOGA
    {
        /// <summary>
        /// 唯一識別碼
        /// </summary>
        [Key]
        public Guid LOG000 { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime LOG001 { get; set; }

        /// <summary>
        /// 工作內容
        /// </summary>
        public string LOG002 { get; set; }

        /// <summary>
        /// 工作問題
        /// </summary>
        public string LOG003 { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string LOG004 { get; set; }

        /// <summary>
        /// 明日工作計畫
        /// </summary>
        public string LOG005 { get; set; }

        /// <summary>
        /// 共同完成人員
        /// </summary>
        public string LOG006 { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool LOG007 { get; set; }
        /// <summary>
        /// 部門
        /// </summary>
        public string LOG008 {get; set; }
        /// <summary>
        /// 權限
        /// </summary>
        public int? LOG009 { get; set; }
        /// <summary>
        /// 與CMSMV-MV001關聯
        /// </summary>
        public string Mid { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreaTime { get; set; }

        [NotMapped]
        public string MName { get; set; }

        [NotMapped]
        public string MPosition { get; set; }
    }
}