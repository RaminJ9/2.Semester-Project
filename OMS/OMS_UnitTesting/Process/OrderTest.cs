using OMS.Access;
using OMS.DataTypes;
using NUnit.Framework;
using OMS.Access;
using OMS.DataTypes;
using System;
using System.Collections.Generic;
namespace OMS_UnitTesting.Process;

// Creater: Rasmus Rothausen
[TestFixture]
[TestOf(typeof(Order_Data))]
public class Order_HandlerTest
{
    [TestFixture]
    public class OrderHandlerTests
    {
        private Order_Handler orderHandler;
        private Stock_Handler stockHandler;
        private Customer_Handler customerHandler;
        private const string connString = "Host=localhost;Port=5432;Database=oms_test;Username=postgres;Password=1234";

        [SetUp]
        public void Setup()
        {
            customerHandler = new Customer_Handler(connString); // assume constructor exists
            orderHandler = new Order_Handler(connString, ref customerHandler);
        }

        [Test]
        public void InsertProductValidProductReturnsProductId()
        {
            // Arrange
            int productId = 1;
            string productName = "TestProduct";
            decimal price = 99.99m;
            int stock = 100;

            // Act
            int product_id = stockHandler.Create_Product_Database(productId, productName, stock);

            // Assert
            Assert.That(productId, Is.GreaterThan(0));
        }

        [Test]
        public void InsertShippingOptionValidDataReturnsId()
        {
            // Arrange
            string deliveryOption = "Express";
            double price = 15.50D;
            string company = "FastShip";

            // Act
            int id = orderHandler.Create_ShippingOption_Database(deliveryOption, price, company);

            // Assert
            Assert.That(id, Is.GreaterThan(0));
        }

        [Test]
        public void AddOrderToDatabaseValidOrderReturnsOrderId()
        {
            // Arrange
            var customer = new Customer(0, "Test", "User", "test@example.com", 12345678, new Address("Shipping Street", new ZIPCode(5000, "Denmark", "Odense") ));
            var items = new List<Item>
            {
                new Item(0, "Name", 49.99, 2)
            };

            var shipping = new Shipping(0, new Shipping_Option(0, Delivery_option.At_home, 20, "UPS"), "1");

            var order = new Order(customer, items, shipping);
            order.Set_shipping_Address(new Address("Shipping Street", new ZIPCode(5000, "Denmark", "Odense") ));

            // Act
            int orderId = orderHandler.Create_Order_Database(order);

            // Assert
            Assert.That(orderId, Is.GreaterThan(0));
        }
    }
}

