using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Data
{
    public class UnitValueDto : IDto
    {
        public decimal Value { get; set; }
        public string UIState { get; set; }
        public string Time { get; set; }
        public string Adjust { get; set; }
        public string Total { get; set; }
        public string Unit { get; set; }
        public decimal[] Increments { get; set; }
        public bool AllowIncrementStep { get; set; }
    }
}
