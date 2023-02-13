using System;

namespace Sat.Recruitment.Services
{
    public class PercentageByCondition
    {
        public decimal Percentage { get; set; }
        public Func<decimal, bool> Condition { get; set; }
    }
}