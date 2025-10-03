using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CPC02.Models
{
    [Table("SGS_Parameter")]
    public class SGS_Parameter
    {
        [Key]
        [MaxLength(40)]
        public string PAR000 { get; set; } // 隨機碼

        [MaxLength(60)]
        public string PAR001 { get; set; } // 項目

        [MaxLength(30)]
        public string PAR002 { get; set; } // 係數

        [MaxLength(20)]
        public string PAR003 { get; set; } // 單位

        [MaxLength(6)]
        public string PAR004 { get; set; } // 公告年份

        [MaxLength(20)]
        public string PAR005 { get; set; } // IP

        [MaxLength(20)]
        public string PAR006 { get; set; } // 時間
        public short PAR007 { get; set; } // 是否啟用

    }

    public class SGS_Search
    {
        public string category { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public string factory { get; set; }
        public string item { get; set; }
        public string waterdiameter { get; set; }
        public string methods { get; set; }
        public string code { get; set; }
        public string content { get; set; }
        public string year { get; set; }
    }
    public class ElectricitySummaryViewModel
    {
        public string Factory { get; set; }
        public decimal? SumPeakElectricity { get; set; }
        public decimal? SumHalfSpikePower { get; set; }
        public decimal? SumSaturdayHalfPeak { get; set; }
        public decimal? SumOffPeakElectricity { get; set; }
        public decimal? SumTotalElectricity { get; set; }
        public decimal? SumTotalBillTax { get; set; }
        public decimal? SumCarbonPeriod { get; set; }
    }
    public class WD40ViewModel
    {
        public decimal? SumWD011 { get; set; }
    }
    public class ColdCoalViewModel
    {
        public string Code { get; set; }
        public decimal? SumCC012 { get; set; }
        public int GWP { get; set; }
    }

    public class WaterSummaryViewModel
    {
        public string Factory { get; set; }
        public string Waterdiameter { get; set; }
        public decimal? SumNUMBERPOINTERS { get; set; }
        public decimal? SumTOTALWATER { get; set; }
        public decimal? SumTOTALBILLTAX { get; set; }
        public decimal? SumCARBONPERIOD { get; set; }
    }

    public class WasteSummaryViewModel
    {
        public string methods { get; set; }
        public string code { get; set; }
        public decimal? SumDECLAREDWEIGHT { get; set; }
        public decimal? TotalWasteCost { get; set; }
        public decimal? SumCKILOMETERS { get; set; }
        public decimal? SumACTIVITYDATA { get; set; }
        public decimal? SumCARBONEMISSIONFACTOR { get; set; }
        public decimal? SumCCARBONDIOXIDE { get; set; }
    }
    public class WastetransportationViewModel
    {
        public string methods { get; set; }
        public string code { get; set; }
        public decimal? SumDECLAREDWEIGHT { get; set; }
        public decimal? TotalWasteCost { get; set; }
        public decimal? SumCKILOMETERS { get; set; }
        public decimal? SumACTIVITYDATA { get; set; }
        public decimal? SumCARBONEMISSIONFACTOR { get; set; }
        public decimal? SumCCARBONDIOXIDE { get; set; }
    }
    public class FireExtinViewModel
    {
        public string Factory { get; set; }
        public string content { get; set; }
        public decimal? SumFE002 { get; set; }
        public decimal? Sumton { get; set; }
        public decimal? Sumkg { get; set; }
    }

    public class CommutingViewModel
    {
        public string TRANSPORTATION { get; set; }
        public decimal? TotalDoubleKM { get; set; }
        public decimal? EF_VALUE { get; set; }

    }
    public class TrafficViewModel
    {
        public string Type { get; set; }
        public decimal TotalEmission { get; set; }
    }
    public class OfficialcarViewModel
    {
        public string Type { get; set; }
        public decimal TotalEmission { get; set; }
    }
    public class GasCoefficient
    {
        public string GasName { get; set; }
        public decimal Coefficient { get; set; }
        public decimal Factor { get; set; } 
    }

    public class Elec_UPViewModel
    {
        public string Factory { get; set; }
        public decimal? SumTotalElectricity { get; set; }
    }
    public class Gas_UPViewModel
    {
        public string Type { get; set; }
        public decimal TotalEmission { get; set; }
    }
}