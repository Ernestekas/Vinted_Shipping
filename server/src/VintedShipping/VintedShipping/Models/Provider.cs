using System.Collections.Generic;

namespace VintedShipping.Models
{
    public class Provider
    {
        public string Code { get; set; }
        public List<Package> Packages { get; set; } = new List<Package>();
    }
}
