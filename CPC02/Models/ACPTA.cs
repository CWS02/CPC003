using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC02.Models
{
    [Table("ACPTA")]
    public partial class ACPTA
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
        public string TA001 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(11)]
        public string TA002 { get; set; }

        [StringLength(8)]
        public string TA003 { get; set; }

        [StringLength(10)]
        public string TA004 { get; set; }

        [StringLength(6)]
        public string TA005 { get; set; }

        [StringLength(20)]
        public string TA006 { get; set; }

        [StringLength(4)]
        public string TA008 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA009 { get; set; }

        [StringLength(1)]
        public string TA010 { get; set; }

        [StringLength(1)]
        public string TA011 { get; set; }

        [StringLength(1)]
        public string TA012 { get; set; }

        [StringLength(1)]
        public string TA013 { get; set; }

        [StringLength(50)]
        public string TA014 { get; set; }

        [StringLength(8)]
        public string TA015 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA016 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA017 { get; set; }

        [StringLength(1)]
        public string TA018 { get; set; }

        [StringLength(8)]
        public string TA019 { get; set; }

        [StringLength(8)]
        public string TA020 { get; set; }

        [StringLength(255)]
        public string TA021 { get; set; }

        [StringLength(4)]
        public string TA022 { get; set; }

        [StringLength(11)]
        public string TA023 { get; set; }

        [StringLength(1)]
        public string TA024 { get; set; }

        [StringLength(1)]
        public string TA025 { get; set; }

        [StringLength(1)]
        public string TA026 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA027 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA028 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA029 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA030 { get; set; }

        [StringLength(1)]
        public string TA031 { get; set; }

        [StringLength(6)]
        public string TA032 { get; set; }

        [StringLength(1)]
        public string TA033 { get; set; }

        [StringLength(8)]
        public string TA034 { get; set; }

        [StringLength(10)]
        public string TA035 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA036 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA037 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA038 { get; set; }

        [StringLength(6)]
        public string TA039 { get; set; }

        [StringLength(8)]
        public string TA040 { get; set; }

        [StringLength(8)]
        public string TA041 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA042 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA043 { get; set; }

        [StringLength(1)]
        public string TA044 { get; set; }

        [StringLength(2)]
        public string TA045 { get; set; }

        [StringLength(1)]
        public string TA046 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA047 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA048 { get; set; }

        [StringLength(10)]
        public string TA049 { get; set; }

        [StringLength(1)]
        public string TA050 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA051 { get; set; }

        [StringLength(4)]
        public string TA052 { get; set; }

        [StringLength(8)]
        public string TA053 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA054 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA055 { get; set; }

        [StringLength(1)]
        public string TA056 { get; set; }

        [StringLength(30)]
        public string TA057 { get; set; }

        [StringLength(60)]
        public string TA058 { get; set; }

        [StringLength(3)]
        public string TA059 { get; set; }

        [StringLength(20)]
        public string TA060 { get; set; }

        [StringLength(1)]
        public string TA061 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA062 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA063 { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TA064 { get; set; }

        [StringLength(50)]
        public string TA065 { get; set; }

        [StringLength(11)]
        public string TA066 { get; set; }

        [StringLength(4)]
        public string TA067 { get; set; }

        [StringLength(4)]
        public string TA068 { get; set; }

        [StringLength(11)]
        public string TA069 { get; set; }

        [StringLength(1)]
        public string TA070 { get; set; }

        [StringLength(255)]
        public string TA071 { get; set; }

        [StringLength(255)]
        public string TA072 { get; set; }

        [StringLength(255)]
        public string TA073 { get; set; }

        [StringLength(255)]
        public string TA074 { get; set; }

        [StringLength(6)]
        public string TA075 { get; set; }

        [StringLength(20)]
        public string TA076 { get; set; }

        [StringLength(30)]
        public string TA077 { get; set; }

        [StringLength(1)]
        public string TA078 { get; set; }

        [StringLength(1)]
        public string TA079 { get; set; }

        [StringLength(6)]
        public string TA080 { get; set; }

        [StringLength(1)]
        public string TA081 { get; set; }

        [StringLength(8)]
        public string TA082 { get; set; }

        [StringLength(8)]
        public string TA083 { get; set; }

        [StringLength(255)]
        public string TA084 { get; set; }

        [StringLength(1)]
        public string TA085 { get; set; }

        [StringLength(1)]
        public string TA086 { get; set; }

        [StringLength(30)]
        public string TA087 { get; set; }

        [StringLength(60)]
        public string TA088 { get; set; }

        [StringLength(60)]
        public string TA089 { get; set; }

        [StringLength(1)]
        public string TA090 { get; set; }

        [StringLength(1)]
        public string TA091 { get; set; }

        [StringLength(50)]
        public string TA092 { get; set; }

        [StringLength(1)]
        public string TA093 { get; set; }

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