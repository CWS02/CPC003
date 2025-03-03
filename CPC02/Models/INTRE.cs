using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace CPC02.Models
{
    /// <summary>
    /// INTRE ��ƪ�����ҫ�
    /// </summary>
     [Table("INTRE")]
    public class INTRE
    {
        /// <summary>
        /// �ߤ@�ѧO�X (Primary Key)
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string INT000 { get; set; }

        /// <summary>
        /// �l�ܤ��
        /// </summary>
        public string INT001 { get; set; }

        /// <summary>
        /// ���e
        /// </summary>
        public string INT002 { get; set; }

        /// <summary>
        /// �p���覡
        /// </summary>
        public int INT003 { get; set; }

        /// <summary>
        /// ����B�J
        /// </summary>
        public string INT004 { get; set; }

        /// <summary>
        /// IP ��}
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// ���A (0=�ҥ�, 1=�R��)
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// �إ߮ɶ� (�w�]��e�ɶ�)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// �|��ID
        /// </summary>
        public int Mid { get; set; }

        /// <summary>
        /// �P INTRB �����p���
        /// </summary>
        public string INT999 { get; set; }
    }
}