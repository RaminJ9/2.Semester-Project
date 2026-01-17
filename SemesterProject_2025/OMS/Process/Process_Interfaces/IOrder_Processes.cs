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




        /// <summary>
        /// 
        /// </summary>
        /// <param name="delivery_option"></param>
        /// <param name="shipping_price"></param>
        /// <param name="company_name"></param>
        /// <returns></returns>
        public int Create_Shipping_Options(Delivery_option delivery_option, double shipping_price, string company_name);








        // Get

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public Order Get_Order_Info(int order_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public string Get_Order_Info_Str(int order_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public string[] Get_Order_Info_StrArray(int order_id);





        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public Address Get_Order_ShippingAddress(int order_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public string Get_Order_ShippingAddress_Str(int order_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public string[] Get_Order_ShippingAddress_StrArray(int order_id);







        // Update 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public bool Update_Order_Customer(int order_id, int customer_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="shipping_address"></param>
        /// <returns></returns>
        public bool Update_Order_Address(int order_id, Address shipping_address);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool Update_Order_Status(int order_id, OrderStatus status);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="new_procent_discount"></param>
        /// <returns></returns>
        public bool Update_Order_Procent_Discount(int order_id, double new_procent_discount);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="new_flat_discount"></param>
        /// <returns></returns>
        public bool Update_Order_Flat_Discount(int order_id, double new_flat_discount);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="addTo_procent_discount"></param>
        /// <returns></returns>
        public bool Add_Order_Procent_Discount(int order_id, double addTo_procent_discount);
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="addTo_flat_discount"></param>
        /// <returns></returns>
        public bool Add_Order_Flat_Discount(int order_id, double addTo_flat_discount);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <param name="add_Item_to_ItemList"></param>
        /// <returns></returns>
        public bool Add_Order_Item(int order_id, Item add_Item_to_ItemList);






        // Delete 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_id"></param>
        /// <returns></returns>
        public bool Delete_Order(int order_id);









    }
}
