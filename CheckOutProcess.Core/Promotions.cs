using System;
using System.Collections.Generic;
using System.Text;

namespace CheckOutProcess.Core
{
    public class Promotions
    {
        public int PromoId { get; set; }
        public string PromoType { get; set; }
        public string PromoDetails { get; set; }
        public int PromoPrice { get; set; }
        public int PromoQty { get; set; }
        public char PrimarySKUId { get; set; }
        public char SecondarySKUId { get; set; }
    }
}