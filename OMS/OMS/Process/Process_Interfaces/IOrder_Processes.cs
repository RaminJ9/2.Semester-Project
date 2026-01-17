using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process.Process_Interfaces
{
    internal interface IOrder_Processes
    {

        // Create

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_info"></param>
        /// <param name="item_List"></param>
        /// <param name="shipping_info"></param>
        /// <returns></returns>
        public int Create_Order(Customer customer_info, List<Item> item_List, Shipping shipping_info);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_info"></param>
        /// <param name="item_List"></param>
        /// <param name="shipping_info"></param>
        /// <param name="shipping_address"></param>
        /// <returns></returns>
        public int Create_Order(Customer customer_info, List<Item> item_List, Shipping shipping_info, Address shipping_address);










        // Get

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <returns></returns>
        public Order? Get_Order_Info(int order_ID);





        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <returns></returns>
        public Address? Get_Order_ShippingAddress(int order_ID);







        // Update 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="shipping_address"></param>
        /// <returns></returns>
        public bool Update_Order_Address(int order_ID, Address shipping_address);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool Update_Order_Status(int order_ID, OrderStatus status);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="new_procent_discount"></param>
        /// <returns></returns>
        public bool Update_Order_Procent_Discount(int order_ID, double new_procent_discount);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="new_flat_discount"></param>
        /// <returns></returns>
        public bool Update_Order_Flat_Discount(int order_ID, double new_flat_discount);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="addTo_procent_discount"></param>
        /// <returns></returns>
        public bool Add_Order_Procent_Discount(int order_ID, double addTo_procent_discount);
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="addTo_flat_discount"></param>
        /// <returns></returns>
        public bool Add_Order_Flat_Discount(int order_ID, double addTo_flat_discount);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="add_Item_to_ItemList"></param>
        /// <returns></returns>
        public bool Add_Order_Item(int order_ID, Item add_Item_to_ItemList);








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
        /// <param name="created_on"></param>
        /// <param name="time_period"></param>
        /// <returns></returns>
        public List<Order>? Search_forOrder_Date(DateTime created_on, TimeStamp_Duration time_period);









        // Delete 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <returns></returns>
        public bool Delete_Order(int order_ID);









    }
}
