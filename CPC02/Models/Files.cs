using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("Files")]

    public class Files
    {
        /// <summary>
        /// 識別碼，唯一的會員ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public Guid SourceID { get; set; }

        /// <summary>
        /// 檔案名稱
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 路徑
        /// </summary>
        public string ServerPath { get; set; }

        /// <summary>
        /// 檔案大小
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 副檔名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// DB來源
        /// </summary>
        public string SourceDB { get; set; }        
        /// <summary>
        /// DB來源
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }    
    }
}