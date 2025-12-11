using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC02.Models
{
    [Table("Gas_Parameter")]
    public class Gas_Parameter
    {
        /// <summary>
        /// 氣體參數記錄的唯一識別碼 (Primary Key)。
        /// 對應 SQL 欄位 GP000 (UNIQUEIDENTIFIER)。
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GP000 { get; set; }

        /// <summary>
        /// 排放源類別 (如：汽油, 柴油, 電力等)。
        /// 對應 SQL 欄位 GP001 (NVARCHAR(10))。
        /// </summary>
        public string GP001 { get; set; } = string.Empty;

        /// <summary>
        /// 係數生效的年份。
        /// 對應 SQL 欄位 GP002 (INT)。
        /// </summary>
        public int GP002 { get; set; }

        /// <summary>
        /// 溫室氣體名稱 (如：CO₂, CH₄, N₂O)。
        /// 對應 SQL 欄位 GP003 (NVARCHAR(10))。
        /// </summary>
        public string GP003 { get; set; } = string.Empty;

        /// <summary>
        /// 氣體排放係數 1 (Coefficient)，通常為 kg CO₂/單位燃料。
        /// 對應 SQL 欄位 GP004 (DECIMAL(10, 10))。
        /// </summary>
        public decimal? GP004 { get; set; }

        /// <summary>
        /// 氣體排放係數 2 (Factor 或 GWP 值)。
        /// 對應 SQL 欄位 GP005 (DECIMAL(10, 10))。
        /// </summary>
        public decimal? GP005 { get; set; }

        /// <summary>
        /// 紀錄建立時間。
        /// 對應 SQL 欄位 CreateTime (DATETIME)。
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 紀錄狀態 (0: 啟用, 1: 停用/刪除)。
        /// 對應 SQL 欄位 Status (INT)。
        /// </summary>
        public int Status { get; set; } = 0;
    }
}