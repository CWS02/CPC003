using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("PCMTG")]
    public partial class PCMTG
    {
        [StringLength(20)]
        public string COMPANY { get; set; }

        [StringLength(10)]
        public string CREATOR { get; set; }

        [StringLength(10)]
        public string USR_GROUP { get; set; }

        [StringLength(8)]
        public string CREATE_DATE { get; set; }

        [StringLength(10)]
        public string MODIFIER { get; set; }

        [StringLength(8)]
        public string MODI_DATE { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? FLAG { get; set; }

        [StringLength(20)]
        public string CREATE_TIME { get; set; }

        [StringLength(50)]
        public string CREATE_AP { get; set; }

        [StringLength(50)]
        public string CREATE_PRID { get; set; }

        [StringLength(20)]
        public string MODI_TIME { get; set; }

        [StringLength(50)]
        public string MODI_AP { get; set; }

        [StringLength(50)]
        public string MODI_PRID { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string TG001 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(11)]
        public string TG002 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string TG003 { get; set; }

        [StringLength(1)]
        public string TG004 { get; set; }

        [StringLength(1)]
        public string TG005 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG006 { get; set; }

        [StringLength(4)]
        public string TG007 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG008 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG009 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG010 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG011 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG012 { get; set; }

        [StringLength(8)]
        public string TG013 { get; set; }

        [StringLength(6)]
        public string TG014 { get; set; }

        [StringLength(20)]
        public string TG015 { get; set; }

        [StringLength(50)]
        public string TG016 { get; set; }

        [StringLength(1)]
        public string TG017 { get; set; }

        [StringLength(1)]
        public string TG018 { get; set; }

        [StringLength(4)]
        public string TG019 { get; set; }

        [StringLength(11)]
        public string TG020 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG021 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG022 { get; set; }

        [StringLength(20)]
        public string TG023 { get; set; }

        [StringLength(255)]
        public string TG024 { get; set; }

        [StringLength(1)]
        public string TG025 { get; set; }

        [StringLength(6)]
        public string TG026 { get; set; }

        [StringLength(20)]
        public string TG027 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG028 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG029 { get; set; }

        [StringLength(1)]
        public string TG030 { get; set; }

        [StringLength(30)]
        public string TG031 { get; set; }

        [StringLength(60)]
        public string TG032 { get; set; }

        [StringLength(3)]
        public string TG033 { get; set; }

        [StringLength(20)]
        public string TG034 { get; set; }

        [StringLength(20)]
        public string TG035 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG036 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG037 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TG038 { get; set; }

        [StringLength(50)]
        public string TG039 { get; set; }

        [StringLength(20)]
        public string TG040 { get; set; }

        [StringLength(30)]
        public string TG041 { get; set; }

        [StringLength(255)]
        public string TG042 { get; set; }

        [StringLength(255)]
        public string TG043 { get; set; }

        [StringLength(255)]
        public string TG044 { get; set; }

        [StringLength(255)]
        public string TG045 { get; set; }

        [StringLength(10)]
        public string TG046 { get; set; }

        [StringLength(40)]
        public string TG047 { get; set; }

        [StringLength(120)]
        public string TG048 { get; set; }

        [StringLength(80)]
        public string TG049 { get; set; }

        [StringLength(10)]
        public string TG050 { get; set; }

        [StringLength(255)]
        public string UDF01 { get; set; }

        [StringLength(255)]
        public string UDF02 { get; set; }

        [StringLength(255)]
        public string UDF03 { get; set; }

        [StringLength(255)]
        public string UDF04 { get; set; }

        [StringLength(255)]
        public string UDF05 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF06 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF07 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF08 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF09 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? UDF10 { get; set; }
    }
}