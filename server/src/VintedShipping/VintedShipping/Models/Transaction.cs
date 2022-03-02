using System;

namespace VintedShipping.Models
{
    public class Transaction
    {
        public string RawTransaction { get; set; }
        public DateTime Date { get; set; }
        public string SizeLetter { get; set; }
        public string CarrierCode { get; set; }
        public decimal ShipmentPrice { get; set; }
        public decimal Discount { get; set; }
        public bool Valid { get; set; }
    }
}
