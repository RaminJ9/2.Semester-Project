using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Data {
    public class AddData {
        //Attributes ---------------------------------------------------------------------
        ProductsAPI productsAPI = ProductsAPI.GetProductAPI();
        
        int productAmount;
        List<List<object>> productInfo;
        List<List<string>> categoriesSortedByProduct;
        HashSet<string> categories;
        bool allInformationAvailable;
        string mistakesInFile = "";

        //Constructor ---------------------------------------------------------------
        public AddData(string filePath) {
            ReadBulkData readBulkData = new ReadBulkData(filePath);

            productAmount = readBulkData.GetProductsAmount();
            productInfo = readBulkData.GetProductInfo();
            categoriesSortedByProduct = readBulkData.GetCategoriesSortedByProduct();
            categories = readBulkData.GetCategories();
            allInformationAvailable = readBulkData.GetAllInformationAvailable();
            mistakesInFile = string.Join("\n", readBulkData.GetMistakesInFile());
        }
        public AddData(List<object> productInfo, List<string> categories) {
            this.productInfo = new List<List<object>>();
            categoriesSortedByProduct = new List<List<string>>();
            this.categories = new HashSet<string>();
            
            productAmount = 1;
            this.productInfo.Add(productInfo);
            categoriesSortedByProduct.Add(categories);
            foreach (var category in categories) {
                this.categories.Add(category);
            }
            mistakesInFile = "This is not a file";
            if (productInfo.Count == 10) { allInformationAvailable = true; }
            else { allInformationAvailable = false; }
        }

        //Methods -------------------------------------------------------------------------
        public async Task AddDataNow() {
            int SKU = await GetSKU();
            (Dictionary<string, int> categoriesInDatabase, HashSet<string> newCategories) = await GetCategories(categories);
            
            await AddProduct(productInfo);
            await AddCategory(newCategories);
            await AddTypes(SKU, categoriesSortedByProduct, categoriesInDatabase, productAmount);
        }
        private async Task<(Dictionary<string, int>, HashSet<string>)> GetCategories(HashSet<string> allCategories = null) {
                    //Makes a dictionary of all the category in the database (including the new categories), the key is the category and the id. Used when adding to the types table.
                    //It also makes a HashSet of the items that has to be added to the database. Used in AddCategory(newCategories) to add the categories to the database :D

                    HashSet<string> newCategories = new HashSet<string>();
                    Dictionary<string, int> categories = await productsAPI.GetCategories();

                    if (allCategories != null) {
                        foreach (string category in allCategories) {
                            bool containsCategory = categories.ContainsKey(category);
                            if (containsCategory == false) {
                                categories.Add(category, categories.Count + 1);
                                newCategories.Add(category);
                            }
                        }
                    }
                    return (categories, newCategories);
                }
        private async Task<int> GetSKU() {
            int lastSKU = await productsAPI.GetLastSKU() + 1;

            return lastSKU;
        }
        private async Task AddProduct(List<List<object>> allProductParameters) {
            //Adds one new product to the database
            if (allProductParameters.Count == 1) { await productsAPI.AddProduct(allProductParameters[0]); }
            
            //Adds multiple new products to the database
            else { await productsAPI.AddProducts(allProductParameters); }      
        }
        private async Task AddCategory(HashSet<string> newCategories) {
            //Adds one new categories to the database
            if (newCategories.Count == 1) { await productsAPI.AddCategory(newCategories.First()); }
            
            //Adds multiple new categories to the database
            else { await productsAPI.AddCategories(newCategories); }
        }
        private async Task AddTypes(int productSKU, List<List<string>> CategoriesSortedByProduct, Dictionary<string, int> categoriesInDatabase, int productAmount = 1) {
            List<(int, int)> links = new();
            
            for (int i = 0; i < productAmount; i++) {
                for (int j = 0; j < CategoriesSortedByProduct[i].Count; j++) {
                    links.Add((productSKU, categoriesInDatabase[CategoriesSortedByProduct[i][j]]));
                    //i is the product number, so 0 would be the first product from the document
                    //j is the category number for that specific product
                    //categories[2][0] would be "Monitor" for NewProducts.csv, because that is category number 0 for product number 2
                }
                productSKU++;
            }
            //Adds one new row in types
            if (links.Count == 1) { await productsAPI.AddType(links[0].Item1, links[0].Item2); }

            //Adds multiple new rows in types
            else { await productsAPI.AddTypes(links); }
        }

        //Getters
        public bool GetAllInformationAvailable() => allInformationAvailable;
        public int GetProductAmount() => productAmount;
        public string GetMistakesInFile() => mistakesInFile;
    }
}
