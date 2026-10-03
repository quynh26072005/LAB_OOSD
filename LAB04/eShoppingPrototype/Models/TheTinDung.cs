using System;

namespace eShoppingPrototype.Models
{
    public class TheTinDung
    {
        public string LoaiThe { get; set; }
        public string SoThe { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string TenChuThe { get; set; }
        public string CSV { get; set; }

        public string GetLast4Digits()
        {
            if (string.IsNullOrEmpty(SoThe) || SoThe.Length < 4)
                return "0000";
            return SoThe.Substring(SoThe.Length - 4);
        }
    }
}
