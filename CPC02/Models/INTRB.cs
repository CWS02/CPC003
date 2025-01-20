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
        /// �H���X (�D��)
        /// </summary>
        [Key]
        [StringLength(36)]
        public string INT000 { get; set; }

        /// <summary>
        /// �D�D
        /// </summary>
        [Required]
        [StringLength(250)]
        public string INT001 { get; set; }

        /// <summary>
        /// �X�ͰO���O
        /// 1=��X, 2=�q��, 3=�q�H
        /// </summary>
        public int INT002 { get; set; }

        /// <summary>
        /// �ɮ׺��}
        /// </summary>
        [StringLength(150)]
        public string INT003 { get; set; }

        /// <summary>
        /// ���e
        /// </summary>
        public string INT004 { get; set; }
        /// <summary>
        /// �X�ͮɶ�
        /// </summary>
        public string INT005 { get; set; }
        /// <summary>
        /// �D�ަ^����
        /// </summary>
        public string INT006 { get; set; }
        /// <summary>
        /// �M�צW��
        /// </summary>
        public string INT007 { get; set; }
        /// <summary>
        /// ���X�ت�
        /// </summary>
        public string INT008 { get; set; }
        /// <summary>
        /// ����B�J
        /// </summary>
        public string INT009 { get; set; }
        /// <summary>
        /// ����
        /// 1=�ꤺ 2=��~
        /// �ȮɨS�Ψ�
        /// </summary>
        public int? INT010 { get; set; }
        /// <summary>
        /// �l���q
        /// </summary>
        public string INT011 { get; set; }
        /// <summary>
        /// �|��ID
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
        /// ���A
        /// 0=���f��,1=�w�f�� 2=�R��
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// �إ߮ɶ� (�۰ʱa�J)
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ���n�{��
        /// ���� 1��5�C
        /// </summary>
        public int Level { get; set; }

        public virtual INTRA INTRA { get; set; }
        public virtual Member Member { get; set; }
    }
}