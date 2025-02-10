using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CPC02.Models
{
    [Table("INTRA")]
    public class INTRA
    {
        /// <summary>
        /// �y����
        /// </summary>
        [Key]
        [StringLength(36)]
        public string INT000 { get; set; }
        /// <summary>
        /// �Ȥ�W��
        /// </summary>
        [StringLength(50)]
        public string INT001 { get; set; }

        /// <summary>
        /// ���A�O
        /// </summary>
        [StringLength(2)]
        public string INT002 { get; set; }

        /// <summary>
        /// �a�}
        /// </summary>
        [StringLength(250)]
        public string INT003 { get; set; }

        /// <summary>
        /// �q��
        /// </summary>
        [StringLength(100)]
        public string INT004 { get; set; }

        /// <summary>
        /// �ǯu
        /// </summary>
        [StringLength(100)]
        public string INT005 { get; set; }

        /// <summary>
        /// ��a
        /// </summary>
        [StringLength(50)]
        public string INT006 { get; set; }
        /// <summary>
        /// �ϰ�
        /// </summary>
        [StringLength(100)]
        public string INT007 { get; set; }

        /// <summary>
        /// �����H1
        /// </summary>
        [StringLength(50)]
        public string INT008 { get; set; }

        /// <summary>
        /// ¾��1
        /// </summary>
        [StringLength(50)]
        public string INT009 { get; set; }

        /// <summary>
        /// ����1
        /// </summary>
        [StringLength(100)]
        public string INT010 { get; set; }

        /// <summary>
        /// �����H2
        /// </summary>
        [StringLength(50)]
        public string INT011 { get; set; }

        /// <summary>
        /// ¾��2
        /// </summary>
        [StringLength(50)]
        public string INT012 { get; set; }

        /// <summary>
        /// ����2
        /// </summary>
        [StringLength(100)]
        public string INT013 { get; set; }

        /// <summary>
        /// �����H3
        /// </summary>
        [StringLength(50)]
        public string INT014 { get; set; }

        /// <summary>
        /// ¾��3
        /// </summary>
        [StringLength(50)]
        public string INT015 { get; set; }

        /// <summary>
        /// ����3
        /// </summary>
        [StringLength(100)]
        public string INT016 { get; set; }

        /// <summary>
        /// �~�Ƚd��
        /// </summary>
        public string INT017 { get; set; }
        /// <summary>
        /// �H���B��
        /// </summary>
        public string INT018 { get; set; }

        /// <summary>
        /// ���߮ɶ�
        /// </summary>
        public string INT019 { get; set; }

        /// <summary>
        /// ���q����
        /// </summary>
        public string INT020 { get; set; }

        /// <summary>
        /// ���q��~�B
        /// </summary>
        public string INT021 { get; set; }

        /// <summary>
        /// �~��~�B
        /// </summary>
        public string INT022 { get; set; }

        /// <summary>
        /// ���u�H��
        /// </summary>
        public string INT023 { get; set; }

        /// <summary>
        /// �νs
        /// </summary>
        public string INT024 { get; set; }

        /// <summary>
        /// ���q�t�d�H
        /// </summary>
        public string INT025 { get; set; }
        /// <summary>
        /// �I�ڱ���
        /// </summary>
        public string INT026 { get; set; }
        /// <summary>
        /// �Ȥ�Email
        /// </summary>
        public string INT027 { get; set; }
        /// <summary>
        /// CPC�������~�{�p�]�x�s�� JSON �r��^
        /// </summary>
        public string INT028 { get; set; }
        /// <summary>
        /// ����
        /// 1=�ꤺ 2=��~
        /// </summary>
        public int? INT029 { get; set; }
        /// <summary>
        /// �W���W��
        /// </summary>
        public string INT030 { get; set; }
        /// <summary>
        /// �Ȥ᪬�A/�ӷ� (�PINTRD��INT000)���p
        /// </summary>
        public string INTRD { get; set; }
        /// <summary>
        /// �|��ID
        /// </summary>
        public int? Mid { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        [StringLength(20)]
        public string IP { get; set; }

        /// <summary>
        /// ���A 0=�ҥ� 1=�R��
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// �إ߮ɶ� (�۰ʱa�J)
        /// </summary>
        [NotMapped]
        public DateTime CreateTime { get; set; }
        [JsonIgnore]
        public virtual ICollection<INTRB> INTRBs { get; set; }
        [JsonIgnore]
        public virtual ICollection<INTRC> INTRCs { get; set; }

        [NotMapped]
        public DateTime? LastDate { get; set; }
        [NotMapped]
        public DateTime? QuoteLastDate { get; set; }
    }
}