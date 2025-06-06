using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    public partial class CAT_THREE_EMPLOYEE_COMMUTING
    {
        [Display(Name = "員工編號")]
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string USER_ID { get; set; }
        [Display(Name = "姓名")]
        [StringLength(50)]
        public string USER_NAME { get; set; }

        [StringLength(50)]
        public string FIRST_WORK_DATE { get; set; }



        [StringLength(50)]
        public string LAST_WORK_DATE { get; set; }



        [StringLength(100)]
        public string USER_ADDRESS { get; set; }
        [Display(Name = "課別")]
        [StringLength(50)]
        public string DEPARTMENT_NAME { get; set; }
        [Display(Name = "職稱")]
        [StringLength(50)]
        public string TITLE_NAME { get; set; }
        [Display(Name = "出勤日")]
        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string WORK_DATE { get; set; }

        [NotMapped]
        public DateTime WorkDate

        {
            get { return DateTime.Parse(WORK_DATE); }
        }

        [Display(Name = "班別")]
        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string SHIFT_NAME { get; set; }
        [Display(Name = "上班時間")]
        [StringLength(50)]
        public string CLOCK_IN { get; set; }
        [Display(Name = "下班時間")]
        [StringLength(50)]
        public string CLOCK_OUT { get; set; }

        [StringLength(50)]
        public string WORKING_HOURS { get; set; }

        [StringLength(20)]
        public string UNIT { get; set; }


    }
}