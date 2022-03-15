using System;
using System.Collections.Generic;
using VintedShipping.Models;

namespace VintedShopping.UnitTests.ExpectedResults
{
    public static class ExpectedTransactions
    {
        public static List<Transaction> GetDefaultAllInvalidTransactions()
        {
            return new List<Transaction>()
            {
                new Transaction()
                {
                    Valid = false,
                    FailedTransaction = "Labas Ignored"
                },
                new Transaction()
                {
                    Valid = false,
                    FailedTransaction = "Krabas Ignored"
                },
                new Transaction()
                {
                    Valid = false,
                    FailedTransaction = "Rabarbarai su cukrum Ignored"
                }
            };
        }

        public static List<Transaction> GetExpectedTransactions()
        {
            return new List<Transaction>()
            {
                new Transaction()
                {
                    Date = new DateTime(2015,2,1),
                    SizeLetter = "S",
                    CarrierCode = "MR",
                    ShipmentPrice = 1.50M,
                    Discount = 0.50M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,2),
                    SizeLetter = "S",
                    CarrierCode = "MR",
                    ShipmentPrice = 1.50M,
                    Discount = 0.50M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,3),
                    SizeLetter = "L",
                    CarrierCode = "LP",
                    ShipmentPrice = 6.90M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,5),
                    SizeLetter = "S",
                    CarrierCode = "LP",
                    ShipmentPrice = 1.50M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,6),
                    SizeLetter = "S",
                    CarrierCode = "MR",
                    ShipmentPrice = 1.50M,
                    Discount = 0.50M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,6),
                    SizeLetter = "L",
                    CarrierCode = "LP",
                    ShipmentPrice = 6.90M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,7),
                    SizeLetter = "L",
                    CarrierCode = "MR",
                    ShipmentPrice = 4.00M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,8),
                    SizeLetter = "M",
                    CarrierCode = "MR",
                    ShipmentPrice = 3.00M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,9),
                    SizeLetter = "L",
                    CarrierCode = "LP",
                    ShipmentPrice = 0.00M,
                    Discount = 6.90M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,10),
                    SizeLetter = "L",
                    CarrierCode = "LP",
                    ShipmentPrice = 6.90M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,10),
                    SizeLetter = "S",
                    CarrierCode = "MR",
                    ShipmentPrice = 1.50M,
                    Discount = 0.50M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,10),
                    SizeLetter = "S",
                    CarrierCode = "MR",
                    ShipmentPrice = 1.50M,
                    Discount = 0.50M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,11),
                    SizeLetter = "L",
                    CarrierCode = "LP",
                    ShipmentPrice = 6.90M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,12),
                    SizeLetter = "M",
                    CarrierCode = "MR",
                    ShipmentPrice = 3.00M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,13),
                    SizeLetter = "M",
                    CarrierCode = "LP",
                    ShipmentPrice = 4.90M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,15),
                    SizeLetter = "S",
                    CarrierCode = "MR",
                    ShipmentPrice = 1.50M,
                    Discount = 0.50M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,17),
                    SizeLetter = "L",
                    CarrierCode = "LP",
                    ShipmentPrice = 6.90M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,17),
                    SizeLetter = "S",
                    CarrierCode = "MR",
                    ShipmentPrice = 1.90M,
                    Discount = 0.10M,
                    Valid = true
                },
                new Transaction()
                {
                    Date = new DateTime(2015,2,24),
                    SizeLetter = "L",
                    CarrierCode = "LP",
                    ShipmentPrice = 6.90M,
                    Discount = 0.00M,
                    Valid = true
                },
                new Transaction()
                {
                    Valid = false,
                    FailedTransaction = "2015-02-29 CUSPS Ignored"
                },
                new Transaction()
                {
                    Date = new DateTime(2015,3,1),
                    SizeLetter = "S",
                    CarrierCode = "MR",
                    ShipmentPrice = 1.50M,
                    Discount = 0.50M,
                    Valid = true
                },
            };
        }
    }
}
