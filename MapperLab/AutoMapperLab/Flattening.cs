using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AutoMapperLab.Models.Flattening;
using Xunit;

namespace AutoMapperLab
{
    /// <summary>
    /// 展平
    /// http://docs.automapper.org/en/stable/Flattening.html
    /// </summary>
    public class Flattening
    {
        public void Test()
        {
            // Complex model

            var customer = new Customer
            {
                Name = "George Costanza"
            };
            var order = new Order
            {
                Customer = customer
            };
            var bosco = new Product
            {
                Name = "Bosco",
                Price = 4.99m
            };
            order.AddOrderLineItem(bosco, 15);

            // Configure AutoMapper

            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDto>());
            var executionPlan = configuration.BuildExecutionPlan(typeof(Order), typeof(OrderDto));

            // Perform mapping
            var mapper = new Mapper(configuration);

            OrderDto dto = mapper.Map<Order, OrderDto>(order);

            Assert.Equal("George Costanza", dto.CustomerName);
            Assert.Equal(74.85m, dto.Total);
        }
    }
}
