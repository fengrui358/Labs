using AutoMapper;
using AutoMapperLab.Models.Flattening;
using Xunit;

namespace AutoMapperLab
{
    class ReverseMappingAndUnflattening
    {
        public void Test()
        {
            var customer = new Customer
            {
                Name = "Bob"
            };

            var order = new Order
            {
                Customer = customer,
            };

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>().ReverseMap()));

            var orderDto = mapper.Map<Order, OrderDto>(order);

            orderDto.CustomerName = "Joe";

            mapper.Map(orderDto, order);

            Assert.Equal("Joe", order.Customer.Name);
        }
    }
}
