using System;
using System.Collections.Generic;

#nullable disable

namespace ScaffoldLab
{
    public partial class Order
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string ShippingAddressCity { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public int? Status { get; set; }
        public DateTime? Version { get; set; }
    }
}
