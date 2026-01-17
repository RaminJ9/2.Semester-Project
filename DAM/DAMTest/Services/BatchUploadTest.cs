namespace DAMTest
{
    using NUnit.Framework;
    using Testcontainers.PostgreSql;
    using System.IO;
    using System.Threading.Tasks;
    using DAM_1; // Your app's namespace

    [TestFixture]
    public class BatchUploadIntegrationTests
    {
        private PostgreSqlContainer _pgContainer;
        private string _testDirectory;

        [SetUp]
        public async Task Setup()
        {
            _pgContainer = new PostgreSqlBuilder()
                .WithDatabase("testdb")
                .WithUsername("testuser")
                .WithPassword("testpass")
                .Build();

            await _pgContainer.StartAsync();

            _testDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_testDirectory);

            File.WriteAllText(Path.Combine(_testDirectory, "test1.jpg"), "Test content");
            File.WriteAllText(Path.Combine(_testDirectory, "test2.png"), "Test content");

        }

        [TearDown]
        public async Task Cleanup()
        {
            if (Directory.Exists(_testDirectory))
            {
                foreach (var file in Directory.GetFiles(_testDirectory))
                {
                    File.Delete(file);
                }
                Directory.Delete(_testDirectory);
            }

            await _pgContainer.DisposeAsync();
        }

        [Test]
        public void InsertFilesFromDirectory_ShouldInsertSuccessfully()
        {
            // Use a wrapper or subclass to override connection
            var service = new FileDatabaseService(_pgContainer.GetConnectionString());

            service.InsertFilesFromDirectory(_testDirectory);

            using var conn = new Npgsql.NpgsqlConnection(_pgContainer.GetConnectionString());
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM products;";
            var count = (long)cmd.ExecuteScalar();

            Assert.That(count, Is.EqualTo(26), "Expected 26 files inserted into database.");
        }

        
    }
}
