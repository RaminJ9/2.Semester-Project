using OMS.Access;
using OMS.DataTypes;
using NUnit.Framework;
using OMS.Access;
using OMS.DataTypes;
using System;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.GZip;
namespace OMS_UnitTesting.Access;
// Rasmus Rothausen


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
            customerHandler = new Customer_Handler(connString);
            orderHandler = new Order_Handler(connString, ref customerHandler);
        }

        [Test]
        public void InsertProduct_ValidProduct_ReturnsProductId()
        {
            // Arrange
            int productId = 1;
            string productName = "TestProduct";
            decimal price = 99.99m;
            int stock = 100;

            // Act
            int product_id = stockHandler.Create_Product_Database(productId, productName, stock);

            // Assert
            Assert.That(product_id, Is.GreaterThan(0));
        }

        [Test]
        public void InsertShippingOption_ValidData_ReturnsId()
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
        public void Add_Order_toDatabase_ValidOrder_ReturnsOrderId()
        {
            // Arrange
            var customer = new Customer(0, "Test", "User", "test@example.com", 12345678, new Address("Shipping Street", new ZIPCode(5000, "Denmark", "Odense") ));
            var items = new List<Item>
            {
                new Item(0, "name", 49.99, 2)
            };

            var shipping = new Shipping(0, new Shipping_Option(0, Delivery_option.At_home, 20, "UPS"), "1");

            var order = new Order(customer, items, shipping);
            order.Set_shipping_Address(new Address("Shipping Street", new ZIPCode(5000, "Denmark", "Odense") ));

            // Act
            int order_id = orderHandler.Create_Order_Database(order);

            // Assert
            Assert.That(order_id, Is.GreaterThan(0));
        }
    }
}

