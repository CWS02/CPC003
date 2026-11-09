using System;
using System.Collections.Generic;

namespace CPC02.Models
{
    public sealed class EsgConfigurationException : Exception
    {
        public EsgConfigurationException(string message) : base(message) { }
    }

    public sealed class EsgCalculationPageViewModel
    {
        public bool HasSearched { get; set; }
        public SGS_Search Search { get; set; }
        public IEnumerable<object> Results { get; set; } = new List<object>();
        public List<EsgCoefficientOption> CoefficientOptions { get; set; } = new List<EsgCoefficientOption>();
        public List<EsgCoefficientSetting> Parameters { get; set; } = new List<EsgCoefficientSetting>();
    }

    public sealed class EsgCoefficientOption
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
    }

    public sealed class EsgCoefficientSetting
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Category2 { get; set; }
        public int Year { get; set; }
    }
}
