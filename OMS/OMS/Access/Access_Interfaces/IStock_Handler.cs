using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS;
using OMS.DataTypes;

namespace OMS.Access.Access_Interfaces
{
    internal interface IStock_Handler
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_Product"></param>
        /// <returns></returns>
        public int Create_Product_Database(Product new_Product);







        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        public Product? Fetch_Product_Database(int product_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Product>? Fetch_All_Products_Database();







        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <param name="updated_Product"></param>
        /// <returns></returns>
        public int Update_Product_Database(int product_ID, Product updated_Product);







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







        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        public int Delete_Product_Database(int product_ID);



    }
}
