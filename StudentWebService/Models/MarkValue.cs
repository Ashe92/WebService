using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using StudentWebService.Helpers;

namespace StudentWebService.Models
{
    public static class MarkValue
    {
        private static readonly Dictionary<MarkValuesEnum, decimal>  Marks = new Dictionary<MarkValuesEnum, decimal>()
        {
            {MarkValuesEnum.Two,2m},
            {MarkValuesEnum.TwoHalf,2.5m},
            {MarkValuesEnum.Three, 3m},
            {MarkValuesEnum.ThreeHalf, 3.5m},
            {MarkValuesEnum.Four, 4m},
            {MarkValuesEnum.FourHalf, 4.5m},
            {MarkValuesEnum.Five, 5m}
        };

        public static MarkValuesEnum GetValue(decimal value)
        {
            return Marks.FirstOrDefault(item => item.Value == value).Key;
        }

        public static decimal GetValue(MarkValuesEnum valueEnum)
        {
            return Marks[valueEnum];
        }
    }
}