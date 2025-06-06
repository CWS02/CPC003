using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("SGS_ParameterSetting")]
    public class SGS_ParameterSetting
    {
        /// <summary>
        /// 唯一識別碼，自動產生。
        /// </summary>
        [Key]
        public Guid PAR000 { get; set; }

        /// <summary>
        /// 與 SGS_Parameter 資料表的關聯識別碼。
        /// </summary>
        public Guid? PAR001 { get; set; }

        /// <summary>
        /// 名稱（顯示名稱或識別標籤）。
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PAR002 { get; set; }

        /// <summary>
        /// 類別，例如：水費、電費等。
        /// </summary>
        [Required]
        [StringLength(20)]
        public string PAR003 { get; set; }
        /// <summary>
        /// 類別細項
        /// </summary>
        [Required]
        public string PAR004 { get; set; }

        /// <summary>
        /// 建立時間。
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新時間。
        /// </summary>
        [Required]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 建立或更新的使用者 IP 位址。
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }
    }
}