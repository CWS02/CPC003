using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC02.Models
{
    [Table("INTRB")]
    public class INTRB
    {
        /// <summary>
        /// 隨機碼 (主鍵)
        /// </summary>
        [Key]
        [StringLength(36)]
        public string INT000 { get; set; }

        /// <summary>
        /// 主題
        /// </summary>
        [Required]
        [StringLength(250)]
        public string INT001 { get; set; }

        /// <summary>
        /// 訪談記錄別
        /// 1=到訪, 2=電話, 3=通信
        /// </summary>
        public int INT002 { get; set; }

        /// <summary>
        /// 檔案網址
        /// </summary>
        [StringLength(150)]
        public string INT003 { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        public string INT004 { get; set; }
        /// <summary>
        /// 訪談時間
        /// </summary>
        public string INT005 { get; set; }
        /// <summary>
        /// 主管回覆欄
        /// </summary>
        public string INT006 { get; set; }
        /// <summary>
        /// 專案名稱
        /// </summary>
        public string INT007 { get; set; }
        /// <summary>
        /// 拜訪目的
        /// </summary>
        public string INT008 { get; set; }
        /// <summary>
        /// 後續步驟
        /// </summary>
        public string INT009 { get; set; }
        /// <summary>
        /// 部門
        /// 1=國內 2=國外
        /// 暫時沒用到
        /// </summary>
        public int? INT010 { get; set; }
        /// <summary>
        /// 子公司
        /// </summary>
        public string INT011 { get; set; }
        /// <summary>
        /// 會員ID
        /// </summary>
        public int? Mid { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [ForeignKey("INTRA")]
        [StringLength(36)]
        public string INT999 { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }

        /// <summary>
        /// 狀態
        /// 0=未審核,1=已審核 2=刪除
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 建立時間 (自動帶入)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 重要程度
        /// 等級 1高5低
        /// </summary>
        public int Level { get; set; }

        public virtual INTRA INTRA { get; set; }
        public virtual Member Member { get; set; }
    }
}