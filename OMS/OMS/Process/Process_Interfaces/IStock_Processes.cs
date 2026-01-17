using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process.Process_Interfaces
{
    internal interface IStock_Processes
    {






        // Create


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <param name="product_name"></param>
        /// <param name="avalible_quantity"></param>
        /// <param name="stock_quantity"></param>
        /// <returns></returns>
        public int Create_Product(int product_ID, string product_name, int avalible_quantity, int stock_quantity);









        // Get 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        public Product? Get_Product(int product_ID);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Product>? Get_All_Products();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        public int Get_Product_CurrentlyInstock(int product_ID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        public int Get_Product_CurrentlyAvaliable(int product_ID);








        // Update 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <param name="new_name"></param>
        /// <returns></returns>
        public bool Update_Product_Name(int product_ID, string new_name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <param name="new_instock"></param>
        /// <returns></returns>
        public bool Update_Product_Instock(int product_ID, int new_instock);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <param name="new_avaliable"></param>
        /// <returns></returns>
        public bool Update_Product_Avaliable(int product_ID, int new_avaliable);








        // Search

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_name"></param>
        /// <returns></returns>
        public List<Product>? Search_forProduct_Name(string product_name);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_minAvaliable"></param>
        /// <param name="product_maxAvaliable"></param>
        /// <returns></returns>
        public List<Product>? Search_forProduct_Avaliable(double product_minAvaliable, double product_maxAvaliable);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_minStock"></param>
        /// <param name="product_maxStock"></param>
        /// <returns></returns>
        public List<Product>? Search_forProduct_Stock(double product_minStock, double product_maxStock);








        // Delete 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        public bool Delete_Product(int product_ID);




    }
}
