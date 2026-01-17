using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Access.Access_Interfaces
{
    internal interface IItemList_Handler
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_Item"></param>
        /// <returns></returns>
        public int Create_Item_Database(int order_ID, Item new_Item);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="new_Item_List"></param>
        /// <returns></returns>
        public int Create_ItemList_Database(int order_ID, List<Item> new_Item_List);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_Order"></param>
        /// <returns></returns>
        public int Create_ItemList_Database(Order new_Order);







        /// <summary>
        /// 
        /// </summary>
        /// <param name="item_ID"></param>
        /// <returns></returns>
        public Item? Fetch_Item_Database(int item_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <returns></returns>
        public List<Item>? Fetch_ItemList_Database(int order_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Item>? Fetch_All_Items_Database();







        /// <summary>
        /// 
        /// </summary>
        /// <param name="item_ID"></param>
        /// <param name="updated_Item"></param>
        /// <returns></returns>
        public int Update_Item_Database(int item_ID, int order_ID, Item updated_Item);







        // Search

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <returns></returns>
        public List<Item>? Search_forItem_OrderID(int order_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        public List<Item>? Search_forItem_ProductID(int product_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <param name="soldOn_Date"></param>
        /// <param name="time_period"></param>
        /// <returns></returns>
        public List<Order>? Search_forOrders_Item_Date(int product_ID, DateTime soldOn_Date, TimeStamp_Duration time_period);







        /// <summary>
        /// 
        /// </summary>
        /// <param name="item_ID"></param>
        /// <returns></returns>
        public int Delete_Item_Database(int item_ID);



    }
}
