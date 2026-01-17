using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS;
using OMS.DataTypes;

namespace OMS.Access.Access_Interfaces
{
    internal interface IOrder_Handler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_Order"></param>
        /// <returns></returns>
        public int Create_Order_Database(Order new_Order);







        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <returns></returns>
        public Order? Fetch_Order_Database(int order_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns></returns>
        public List<Order>? Fetch_CustomerOrders_Database(int customer_ID);








        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="updated_Order"></param>
        /// <returns></returns>
        public int Update_Order_Database(int order_ID, Order updated_Order);









        // Search

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns></returns>
        public List<Order>? Search_forOrder_CustomerID(int customer_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        public List<Order>? Search_forOrder_ProductID(int product_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<Order>? Search_forOrder_Email(string email);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="created_on"></param>
        /// <returns></returns>
        public List<Order>? Search_forOrder_Date(DateTime created_on);













        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <returns></returns>
        public int Delete_Order_Database(int order_ID);



    }
}
