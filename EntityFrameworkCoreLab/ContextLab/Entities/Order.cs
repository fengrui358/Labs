namespace ContextLab.Entities
{
    public class Order
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ShippingAddress ShippingAddress { get; set; }

        public OrderStatus? Status { get; set; }
        public DetailedOrder DetailedOrder { get; set; }

    }

    public class ShippingAddress
    {
        public string Street { get; set; }

        public string City { get; set; }
    }

    public enum OrderStatus
    {
        Start,

        Processing,

        Finished
    }
}
