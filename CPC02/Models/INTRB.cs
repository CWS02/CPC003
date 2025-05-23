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
        /// 圖片上傳
        /// </summary>
        public string INT012 { get; set; }
        /// <summary>
        /// 是否有旗子
        /// </summary>
        public bool? INT013 { get; set; }
        /// <summary>
        /// 旗標
        /// </summary>
        public bool? INT014 { get; set; }
        /// <summary>
        /// 負責業務(for展覽)
        /// </summary>
        public string INT015 { get; set; }
        /// <summary>
        /// 填表人(for展覽)
        /// </summary>
        public string INT016 { get; set; }
        /// <summary>
        /// 客戶姓名(for展覽)
        /// </summary>
        public string INT017 { get; set; }
        /// <summary>
        /// Email(for展覽)
        /// </summary>
        public string INT018 { get; set; }
        /// <summary>
        /// 電話(for展覽)
        /// </summary>
        public string INT019 { get; set; }
        /// <summary>
        /// 網址(for展覽)
        /// </summary>
        public string INT020 { get; set; }
        /// <summary>
        /// 地址(for展覽)
        /// </summary>
        public string INT021 { get; set; }
        /// <summary>
        /// 興趣產品(for展覽)
        /// </summary>
        public string INT022 { get; set; }
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
        /// -1=刪除0=未審核,1=已審核 
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
        /// <summary>
        /// 最新訪談時間
        /// </summary>
        [NotMapped]
        public DateTime? LastDate { get; set; }
    }
}