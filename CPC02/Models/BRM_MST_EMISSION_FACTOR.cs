using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    public class BRM_MST_EMISSION_FACTOR
    {
        [StringLength(10)]
        public string EF_CODE { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string EF_NAME { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal EF_VALUE { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string EF_YEAR { get; set; }

        [StringLength(50)]
        public string GHG_CATEGORY { get; set; }

        [StringLength(200)]
        public string DESCRIPTIONS { get; set; }

        [StringLength(10)]
        public string AID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ADT { get; set; }

        [StringLength(10)]
        public string USEID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? USEDT { get; set; }
    }
}