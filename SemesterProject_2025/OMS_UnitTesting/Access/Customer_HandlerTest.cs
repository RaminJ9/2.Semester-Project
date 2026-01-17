using System.Security.Cryptography.X509Certificates;
using Npgsql;
using OMS.Access;
using OMS.Process;
using OMS.DataTypes;
using NUnit.Framework;

namespace OMS_UnitTesting.Access;

[TestFixture]

[TestOf(typeof(Customer_Handler))]
public class CustomerHandlerTests
{
    private const string TestConnString = "Host=localhost;Port=5432;Database=oms_test;Username=postgres;Password=1234";
    Customer_Handler handler;

    [SetUp]
    public void SetUp()
    {
        // Inject test connection string
        handler = new Customer_Handler(TestConnString);

        // Clear test tables for new tests
        using var conn = new NpgsqlConnection(TestConnString);
        conn.Open();
        using var cmd = new NpgsqlCommand("TRUNCATE customer, zip RESTART IDENTITY CASCADE;", conn);
        cmd.ExecuteNonQuery();
    }

    [Test]
    public void ZIP_test()
    {
        int zip_ID = handler.InsertZIP("Testland", "Testville", 12345);

        Assert.That(zip_ID, Is.GreaterThan(0));
    }

    [Test]
    public void Add_Customer_toDatabase_test()
    {
        var customer = new Customer(
            CustomerType.Private,
            "Testy",
            "Tester",
            "test@example.com",
            12345678,
            "Test Street",
            12345,
            "Testland",
            "Testville"
        );

        int customerId = handler.Add_Customer_toDatabase(customer);

        Assert.That(customerId, Is.GreaterThan(0));
    }

    [Test]
    public void Constructor_1_Test1()
    {
        
    }

    [Test]
    public void METHOD()
    {
        
    }
}