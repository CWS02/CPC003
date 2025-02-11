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
        /// �۰ʥͦ� GUID�A�@���D��
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid INT000 { get; set; }

        /// <summary>
        /// �W�� (����)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string INT001 { get; set; }

        /// <summary>
        /// ��a (����)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string INT002 { get; set; }

        /// <summary>
        /// �϶� (�i�� NULL)
        /// </summary>
        [StringLength(100)]
        public string INT003 { get; set; }

        /// <summary>
        /// 0=��� 1=�����
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// �إ߮ɶ��A�w�]����e�ɶ�
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? Sort { get; set; }
    }
}