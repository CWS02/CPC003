using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC02.Models
{
    /// <summary>
    /// 柴油機檢查資料表
    /// </summary>
    [Table("Diesel")]
    public class Diesel
    {
        /// <summary>
        /// 唯一識別碼 (GUID)
        /// </summary>
        [Key]
        public Guid DI000 { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int DI001 { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int DI002 { get; set; }

        /// <summary>
        /// 外觀檢查
        /// </summary>
        public string DI003 { get; set; }

        /// <summary>
        /// 面盤檢查
        /// </summary>
        public string DI004 { get; set; }

        /// <summary>
        /// 電池電壓檢查
        /// </summary>
        public string DI005 { get; set; }

        /// <summary>
        /// 本次運轉時間
        /// </summary>
        public string DI006 { get; set; }

        /// <summary>
        /// 油箱儲存量
        /// </summary>
        public string DI007 { get; set; }

        /// <summary>
        /// 啟動狀況
        /// </summary>
        public decimal DI008 { get; set; }

        /// <summary>
        /// 檢查表 PDF 檔案名稱
        /// </summary>
        public string DI009 { get; set; }

        /// <summary>
        /// 狀態（預設 0）
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}