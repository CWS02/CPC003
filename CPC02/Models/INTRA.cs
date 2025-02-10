using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CPC02.Models
{
    [Table("INTRA")]
    public class INTRA
    {
        /// <summary>
        /// 流水號
        /// </summary>
        [Key]
        [StringLength(36)]
        public string INT000 { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [StringLength(50)]
        public string INT001 { get; set; }

        /// <summary>
        /// 型態別
        /// </summary>
        [StringLength(2)]
        public string INT002 { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(250)]
        public string INT003 { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        [StringLength(100)]
        public string INT004 { get; set; }

        /// <summary>
        /// 傳真
        /// </summary>
        [StringLength(100)]
        public string INT005 { get; set; }

        /// <summary>
        /// 國家
        /// </summary>
        [StringLength(50)]
        public string INT006 { get; set; }
        /// <summary>
        /// 區域
        /// </summary>
        [StringLength(100)]
        public string INT007 { get; set; }

        /// <summary>
        /// 接洽人1
        /// </summary>
        [StringLength(50)]
        public string INT008 { get; set; }

        /// <summary>
        /// 職稱1
        /// </summary>
        [StringLength(50)]
        public string INT009 { get; set; }

        /// <summary>
        /// 分機1
        /// </summary>
        [StringLength(100)]
        public string INT010 { get; set; }

        /// <summary>
        /// 接洽人2
        /// </summary>
        [StringLength(50)]
        public string INT011 { get; set; }

        /// <summary>
        /// 職稱2
        /// </summary>
        [StringLength(50)]
        public string INT012 { get; set; }

        /// <summary>
        /// 分機2
        /// </summary>
        [StringLength(100)]
        public string INT013 { get; set; }

        /// <summary>
        /// 接洽人3
        /// </summary>
        [StringLength(50)]
        public string INT014 { get; set; }

        /// <summary>
        /// 職稱3
        /// </summary>
        [StringLength(50)]
        public string INT015 { get; set; }

        /// <summary>
        /// 分機3
        /// </summary>
        [StringLength(100)]
        public string INT016 { get; set; }

        /// <summary>
        /// 業務範圍
        /// </summary>
        public string INT017 { get; set; }
        /// <summary>
        /// 信用額度
        /// </summary>
        public string INT018 { get; set; }

        /// <summary>
        /// 成立時間
        /// </summary>
        public string INT019 { get; set; }

        /// <summary>
        /// 公司網站
        /// </summary>
        public string INT020 { get; set; }

        /// <summary>
        /// 公司營業額
        /// </summary>
        public string INT021 { get; set; }

        /// <summary>
        /// 年營業額
        /// </summary>
        public string INT022 { get; set; }

        /// <summary>
        /// 員工人數
        /// </summary>
        public string INT023 { get; set; }

        /// <summary>
        /// 統編
        /// </summary>
        public string INT024 { get; set; }

        /// <summary>
        /// 公司負責人
        /// </summary>
        public string INT025 { get; set; }
        /// <summary>
        /// 付款條件
        /// </summary>
        public string INT026 { get; set; }
        /// <summary>
        /// 客戶Email
        /// </summary>
        public string INT027 { get; set; }
        /// <summary>
        /// CPC相關產品現況（儲存為 JSON 字串）
        /// </summary>
        public string INT028 { get; set; }
        /// <summary>
        /// 部門
        /// 1=國內 2=國外
        /// </summary>
        public int? INT029 { get; set; }
        /// <summary>
        /// 名片上傳
        /// </summary>
        public string INT030 { get; set; }
        /// <summary>
        /// 客戶狀態/來源 (與INTRD的INT000)關聯
        /// </summary>
        public string INTRD { get; set; }
        /// <summary>
        /// 會員ID
        /// </summary>
        public int? Mid { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }

        /// <summary>
        /// 狀態 0=啟用 1=刪除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 建立時間 (自動帶入)
        /// </summary>
        [NotMapped]
        public DateTime CreateTime { get; set; }
        [JsonIgnore]
        public virtual ICollection<INTRB> INTRBs { get; set; }
        [JsonIgnore]
        public virtual ICollection<INTRC> INTRCs { get; set; }

        [NotMapped]
        public DateTime? LastDate { get; set; }
        [NotMapped]
        public DateTime? QuoteLastDate { get; set; }
    }
}