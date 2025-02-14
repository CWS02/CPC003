using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("COPTC")]
    public class COPTC
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string TC001 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(11)]
        public string TC002 { get; set; }

        public string TC027 { get; set; }  
    }
}