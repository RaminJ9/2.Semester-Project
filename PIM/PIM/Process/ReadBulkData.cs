using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace PIM.Data
{
    public class ReadBulkData {
        //Attributes-------------------------------------------------------------------------------
        List<List<object>> productInfo = new List<List<object>>();
        HashSet<string> categories = new HashSet<string>();
        List<List<string>> categoriesSortedByProduct = new List<List<string>>();
        bool AllInformationAvailable = true;
        List<string> mistakesInFile = new List<string>();

        //Constructor------------------------------------------------------------------------------
        public ReadBulkData(string filePath) {
            //Adds File to the path, because the files are going to that folder
            ReadFile(filePath);
        }

        //Methodes---------------------------------------------------------------------------------
        private void ReadFile(string filePath) {
            //Find and open readable file
            using StreamReader reader = new StreamReader(filePath);

            List<object> productParameters;
            List<string> categoriesForProduct;
            int productNumber = 0;//used in the exceptions, so it informs which is incorrect

            while (reader.EndOfStream == false) {
                productNumber++;

                //Has to be initialized her so that a new is made for each line if the file
                productParameters = new List<object>();
                
                //Reads every line of the file and splits them at ;
                string line = reader.ReadLine();
                string[] lineSplit = new string[11];
                lineSplit = line.Split(";");

                try {
                    productParameters.Add(lineSplit[0]);
                    productParameters.Add(lineSplit[1]);
                    productParameters.Add(double.Parse(lineSplit[2]));
                    productParameters.Add(double.Parse(lineSplit[3])); ;
                    productParameters.Add(Int32.Parse(lineSplit[4]));
                    productParameters.Add(Int32.Parse(lineSplit[5]));
                    productParameters.Add(Int32.Parse(lineSplit[6]));
                    productParameters.Add(double.Parse(lineSplit[7]));
                    productParameters.Add(lineSplit[8]);
                    productParameters.Add(lineSplit[9]);
                    
                    productInfo.Add(productParameters);

                    string categoryLine = lineSplit[10];
                    lineSplit = categoryLine.Split(",");

                    categoriesForProduct = new List<string>();

                    foreach (string category in lineSplit) {
                        //Adds categories to a list<list<string>> so that is easy to see which categories goes to which product
                        categoriesForProduct.Add(category);
                        
                        //Adds categories so it's easy to check if the category is in the database, or needs to be added
                        categories.Add(category);
                    }
                    categoriesSortedByProduct.Add(categoriesForProduct);
                }
                catch (FormatException) {
                    string message = $"Line {productNumber} has incorrect information";
                    mistakesInFile.Add(message);
                    Console.WriteLine(message);
                    AllInformationAvailable = false;
                }
                catch {
                    string message = $"Line {productNumber} is missing information";
                    mistakesInFile.Add(message);
                    Console.WriteLine(message);
                    AllInformationAvailable = false;
                }
            }
            reader.Close();
        }

        public List<List<object>> GetProductInfo() => productInfo;
        public List<List<string>> GetCategoriesSortedByProduct() => categoriesSortedByProduct;
        public HashSet<string> GetCategories() => categories;
        public bool GetAllInformationAvailable() => AllInformationAvailable;
        public List<string> GetMistakesInFile() => mistakesInFile;
        public int GetProductsAmount() => productInfo.Count();

    }
}

