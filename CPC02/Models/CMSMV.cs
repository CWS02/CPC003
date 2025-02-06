using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("CMSMV")]
    public class CMSMV
    {
        [Key]
        [Column("MV001")]
        [StringLength(10)]
        public string MV001 { get; set; }

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

        [StringLength(30)]
        public string MV002 { get; set; }

        [StringLength(10)]
        public string MV003 { get; set; }

        [StringLength(10)]
        public string MV004 { get; set; }

        [StringLength(8)]
        public string MV005 { get; set; }

        [StringLength(6)]
        public string MV006 { get; set; }

        [StringLength(1)]
        public string MV007 { get; set; }

        [StringLength(8)]
        public string MV008 { get; set; }

        [StringLength(20)]
        public string MV009 { get; set; }


        [StringLength(8)]
        public string MV022 { get; set; }

        [StringLength(8)]
        public string MV219 { get; set; }
        public decimal? MV220 { get; set; }
    }
}