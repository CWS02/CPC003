using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC02.Models
{
    [Table("INTRC")]
    public class INTRC
    {
        /// <summary>
        /// �H���X�]�D��^�C
        /// </summary>
        [Key]
        public string INT000 { get; set; }

        /// <summary>
        /// �����ɶ��C
        /// </summary>
        public string INT001 { get; set; }

        /// <summary>
        /// �������ӡ]JSON �榡�^�C
        /// </summary>
        public string INT002 { get; set; }

        /// <summary>
        /// �Ƶ�
        /// </summary>
        public string INT003 { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        public string INT004 { get; set; }
        /// <summary>
        /// �O�_�|�B
        /// </summary>
        public bool? INT005 { get; set; }
        /// <summary>
        /// �ɮ׺��}
        /// </summary>
        [StringLength(150)]
        public string INT006 { get; set; }
        /// <summary>
        /// �~�ȤH�� ID�C
        /// </summary>
        public int Mid { get; set; }

        /// <summary>
        /// INTRA-INT000 ���p���C
        /// </summary>
        public string INT999 { get; set; }

        /// <summary>
        /// �Ȥ�� IP ��}�C
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// ���A�G0 = ���f�֡A1 = �R���C
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// �إ߮ɶ��A�w�]����e�ɶ��C
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// JSON ���c
    /// </summary>
    public class QuotationDetail
    {
        /// <summary>
        /// ��Z 1�C
        /// </summary>
        public string Margin1 { get; set; }

        /// <summary>
        /// ��Z 2�C
        /// </summary>
        public string Margin2 { get; set; }

        /// <summary>
        /// ���C
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// �ƶq�C
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// ����C
        /// </summary>
        public string UnitPrice { get; set; }
    }
}