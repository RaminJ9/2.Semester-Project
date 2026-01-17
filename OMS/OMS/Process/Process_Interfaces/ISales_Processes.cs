using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process.Process_Interfaces
{
    public interface ISales_Processes
    {




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Generate_SalesReport();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string>? Generate_SalesReport_List();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string>? Generate_SalesReport_Dict();







        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <param name="soldOn_Date"></param>
        /// <param name="time_period"></param>
        /// <returns></returns>
        public List<Order>? Search_forOrders_Product_Date(int product_ID, DateTime soldOn_Date, TimeStamp_Duration time_period = TimeStamp_Duration.Day);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <param name="soldOn_Date"></param>
        /// <param name="time_period"></param>
        /// <returns></returns>
        public int Search_forSold_Product_Date(int product_ID, DateTime soldOn_Date, TimeStamp_Duration time_period = TimeStamp_Duration.Day);




    }
}
