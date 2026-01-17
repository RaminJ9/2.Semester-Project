using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    public interface IOrder
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Get_Order_ID();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Get_Customer_ID();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Get_Shipping_ID();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime Get_CreatedOn_TimeStamp();





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Customer Get_customer_info();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Item> Get_item_List();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Shipping Get_shipping_info();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Address Get_shipping_Address();





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OrderStatus Get_order_status();





        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Get_procent_discount();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Get_flat_discount();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Get_price_Total();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double Get_vat_Total();








        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_customer_info"></param>
        /// <returns></returns>
        public bool Set_customer_info(Customer new_customer_info);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_item_List"></param>
        /// <returns></returns>
        public bool Set_item_List(List<Item> new_item_List);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_item"></param>
        /// <returns></returns>
        public bool AddTo_item_List(Item new_item);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipping_info"></param>
        /// <returns></returns>
        public bool Set_shipping_info(Shipping shipping_info);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_shipping_Address"></param>
        /// <returns></returns>
        public bool Set_shipping_Address(Address new_shipping_Address);






        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public bool Set_order_status(OrderStatus orderStatus);






        /// <summary>
        /// 
        /// </summary>
        /// <param name="procent_discount"></param>
        /// <returns></returns>
        public bool Set_procent_discount(double procent_discount);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="flat_discount"></param>
        /// <returns></returns>
        public bool Set_flat_discount(double flat_discount);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_vat_procent"></param>
        /// <returns></returns>
        public bool Set_vat_procent_AllItems(double new_vat_procent);






        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_trackingNumber"></param>
        /// <returns></returns>
        public bool Set_Shipping_TrackingNumber(string new_trackingNumber);



    }
}
