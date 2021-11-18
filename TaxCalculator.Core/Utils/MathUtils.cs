using System;

namespace TaxCalculator.Core.Utils
{
    public static class MathUtils
    {
        public static double RoundToZero(double value, int? digits = 2)
        {
            return Math.Round(value, digits.Value, MidpointRounding.ToZero);
        }
    }
}
