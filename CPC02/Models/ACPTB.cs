using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC02.Models
{
    [Table("ACPTB")]
    public partial class ACPTB
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
        public string TB001 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(11)]
        public string TB002 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string TB003 { get; set; }

        [StringLength(1)]
        public string TB004 { get; set; }

        [StringLength(4)]
        public string TB005 { get; set; }

        [StringLength(11)]
        public string TB006 { get; set; }

        [StringLength(4)]
        public string TB007 { get; set; }

        [StringLength(8)]
        public string TB008 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TB009 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TB010 { get; set; }

        [StringLength(255)]
        public string TB011 { get; set; }

        [StringLength(1)]
        public string TB012 { get; set; }

        [StringLength(20)]
        public string TB013 { get; set; }

        [StringLength(10)]
        public string TB014 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TB015 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TB016 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TB017 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TB018 { get; set; }

        [StringLength(20)]
        public string TB019 { get; set; }

        [StringLength(4)]
        public string TB020 { get; set; }

        [StringLength(6)]
        public string TB021 { get; set; }

        [StringLength(20)]
        public string TB022 { get; set; }

        [StringLength(10)]
        public string TB023 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TB024 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TB025 { get; set; }

        [StringLength(1)]
        public string TB026 { get; set; }

        [StringLength(30)]
        public string TB027 { get; set; }

        [StringLength(60)]
        public string TB028 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TB029 { get; set; }

        [StringLength(20)]
        public string TB030 { get; set; }

        [StringLength(20)]
        public string TB031 { get; set; }

        [StringLength(255)]
        public string TB032 { get; set; }

        [StringLength(255)]
        public string TB033 { get; set; }

        [StringLength(255)]
        public string TB034 { get; set; }

        [StringLength(255)]
        public string TB035 { get; set; }

        [StringLength(255)]
        public string TB036 { get; set; }

        [StringLength(3)]
        public string TB037 { get; set; }

        [StringLength(1)]
        public string TB038 { get; set; }

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