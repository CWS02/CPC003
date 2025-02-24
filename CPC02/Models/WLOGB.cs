using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("WLOGB")]
    public class WLOGB
    {
        /// <summary>
        /// 唯一識別碼
        /// </summary>
        [Key]
        public Guid LOG000 { get; set; }

        /// <summary>
        /// 專案名稱。
        /// </summary>
        public string LOG001 { get; set; }

        /// <summary>
        /// 部門代號。
        /// </summary>
        public string LOG002 { get; set; }

        /// <summary>
        /// IP 地址。
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 會員 ID，與 CMSMV-MV001 相關聯。
        /// </summary>
        public string Mid { get; set; }

        /// <summary>
        /// 狀態欄位 (0=正常, -1=刪除)。
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        ///  專案目標
        /// </summary>
        public string LOG003 { get; set; }
        /// <summary>
        ///  專案內容
        /// </summary>
        public string LOG004 { get; set; }
        /// <summary>
        /// 專案日期
        /// </summary>
        public DateTime? LOG005 { get; set; }


        /// <summary>
        /// 建立時間
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreaTime { get; set; }
        /// <summary>
        /// 部門
        /// </summary>
        [NotMapped]
        public string MPosition { get; set; }
    }
}