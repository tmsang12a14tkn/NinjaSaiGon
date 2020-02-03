using Moq;
using NinjaSaiGon.Data.Interfaces;
using NinjaSaiGon.Data.Models;
using NinjaSaiGon.Data.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace NinjaSaigon.Test
{
    public class OrdersTests: TestBase
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        public OrdersTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
        }
        [Fact]
        public void CreateOrder_Test()
        {
            //_mockOrderRepository
            _mockOrderRepository.Setup(d => d.Create(new Order
            {
                AddressLine = "06-08 PDC",
                FirstName = "Tien",
                LastName = "Nguyen",
                MiddleName = "Trung",
               PhoneNumber = "0988232716",
               OrderDetails = new List<OrderDetail>
               {
                   new OrderDetail
                   {
                       DrinkId = 8,
                       DrinkSize = "Size M",
                       DrinkSizeId = 12,
                       IceOption = "20%",
                       Price = 20000,
                       Amount = 20000,
                       Quantity = 1
                   }
               }
            })).Verifiable();


        }
    }
}
