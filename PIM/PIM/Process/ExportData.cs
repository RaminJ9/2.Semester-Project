using PIM.Data;
using PIM.Process.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PIM.Process
{
    public class ExportData {
        //Atributes ---------------------------------------------------------------------
        ProductsAPI productsAPI = ProductsAPI.GetProductAPI();

        //Methodes -------------------------------------------------------------------------
        public async Task ExportNow(string? categoryTerm = null, string? searchTerm = null) {
            List<List<object>> data = await GetData(categoryTerm, searchTerm);
            MakeFile(data);
        }
        private async Task<List<List<object>>> GetData(string? categoryTerm = null, string? searchTerm = null) {
            List<ProductView> data = await productsAPI.GetAllProductInformation(categoryTerm, searchTerm);
            List<List<object>> datalist = new List<List<object>>();

            foreach (var item in data) {
                datalist.Add(item.ToList());
            }

            return datalist;
        }
        private void MakeFile(List<List<object>> data) {
            bool fileExist = true;

            string path = "";
            string baseName = "ExportData";
            int version = 0;
            string fileType = ".csv";
        
            while (fileExist) {
                 path = Getpath(baseName,fileType,version);

                if (File.Exists(path)) {
                    version++;
                }
                else { fileExist = false; }
            }
            
            
            
            List<string> dataList = new List<string>();
            string dataString = "";

            foreach (var item in data) {
                dataList.Add(String.Join(";", item));
            }

            dataString = String.Join("\n", dataList);

            FileStream fs = File.Create(path);

            using (StreamWriter sw = new StreamWriter(fs)) {
                sw.WriteLine(dataString);
            }
        }
        static private string Getpath(string baseName, string fileType, int version) {
            string path = "";

            if (version == 0) {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", $"{baseName}{fileType}");
            }
            else {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", $"{baseName}({version}){fileType}");
            }
            return path;
        }
    }
}
