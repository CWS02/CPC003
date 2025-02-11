using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CPC02.Models
{
    [Table("INTRD")]
    public class INTRD
    {
        /// <summary>
        /// 自動生成 GUID，作為主鍵
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid INT000 { get; set; }

        /// <summary>
        /// 名稱 (必填)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string INT001 { get; set; }

        /// <summary>
        /// 國家 (必填)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string INT002 { get; set; }

        /// <summary>
        /// 區間 (可為 NULL)
        /// </summary>
        [StringLength(100)]
        public string INT003 { get; set; }

        /// <summary>
        /// 0=顯示 1=不顯示
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 建立時間，預設為當前時間
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Sort { get; set; }
    }
}