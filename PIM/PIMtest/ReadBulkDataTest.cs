using PIM.Data;

namespace PIMtest
{
    [TestFixture]
    internal class ReadBulkDataTest {
        [Test]
        public void ReadFileTest() {
            string pathNoMistake = new FindPath("File\\TestNewProducts.csv").GetPath();
            ReadBulkData noMistakes = new ReadBulkData(pathNoMistake);

            List <List<object>> data = noMistakes.GetProductInfo();
            List<List<string>> categoryData = noMistakes.GetCategoriesSortedByProduct();

            Assert.That(data[0][1], Is.EqualTo("SteelSeries"));//name
            Assert.That(data[2][2], Is.EqualTo(700));//sales_price_vat
            Assert.That(data[4][8], Is.EqualTo("Black"));//color

            Assert.That(categoryData[0][0], Is.EqualTo("Headset"));
            Assert.That(categoryData[2][2], Is.EqualTo("Work"));
            Assert.That(categoryData[4][1], Is.EqualTo("Gaming"));
        }

        [Test]
        public void AllInformationAvalibleTest() {
            string pathNoMistake = new FindPath("File\\TestNewProducts.csv").GetPath();
            ReadBulkData noMistakes = new ReadBulkData(pathNoMistake);

            Assert.That(noMistakes.GetAllInformationAvailable, Is.EqualTo(true));

            string pathWithMistake = new FindPath("File\\TestNewProductsWithMistakes.csv").GetPath();
            ReadBulkData WithMistakes = new ReadBulkData(pathWithMistake);

            Assert.That(WithMistakes.GetAllInformationAvailable, Is.EqualTo(false));
        }

        [Test]
        public void GetAmountOfProductsTest() {
            string pathNoMistake = new FindPath("File\\TestNewProducts.csv").GetPath();
            ReadBulkData noMistakes = new ReadBulkData(pathNoMistake);

            Assert.That(noMistakes.GetProductsAmount, Is.EqualTo(6));
        }

        [Test]
        public void GetMistakesInFileTest() {
            string pathWithMistake = new FindPath("File\\TestNewProductsWithMistakes.csv").GetPath();
            ReadBulkData WithMistakes = new ReadBulkData(pathWithMistake);

            Assert.That(WithMistakes.GetMistakesInFile()[0], Is.EqualTo("Line 1 is missing information"));
            Assert.That(WithMistakes.GetMistakesInFile()[1], Is.EqualTo("Line 2 has incorrect information"));
            Assert.That(WithMistakes.GetMistakesInFile()[2], Is.EqualTo("Line 6 is missing information"));
        }
    }
}
