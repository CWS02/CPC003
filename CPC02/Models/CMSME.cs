using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("CMSME")]
    public class CMSME
    {
        [Key]
        [Column(TypeName = "nchar")]
        [StringLength(10)]
        public string ME001 { get; set; }

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

        [StringLength(40)]
        public string ME002 { get; set; }

        [StringLength(255)]
        public string ME003 { get; set; }

        [StringLength(20)]
        public string ME004 { get; set; }

        [StringLength(10)]
        public string ME005 { get; set; }

        public decimal? ME006 { get; set; }

        public decimal? ME007 { get; set; }

        [StringLength(1)]
        public string ME008 { get; set; }

        [StringLength(30)]
        public string ME009 { get; set; }

        [StringLength(60)]
        public string ME010 { get; set; }

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

        public decimal? UDF06 { get; set; }

        public decimal? UDF07 { get; set; }

        public decimal? UDF08 { get; set; }

        public decimal? UDF09 { get; set; }

        public decimal? UDF10 { get; set; }
    }
}