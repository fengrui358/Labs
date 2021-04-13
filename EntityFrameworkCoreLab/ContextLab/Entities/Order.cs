using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContextLab.Entities
{
    public class Order
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ShippingAddress ShippingAddress { get; set; }
    }

    public class ShippingAddress
    {
        public string Street { get; set; }

        public string City { get; set; }
    }
}
