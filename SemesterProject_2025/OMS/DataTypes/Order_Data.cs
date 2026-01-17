using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Order_Data : </c> <br/>  
    /// A class that contains the informations related to an specific order, such as customer informations, items and shipping address. <br/>  
    /// </summary>
    public class Order_Data
    {

        protected Customer customer_info;
        protected List<Item> item_List;
        protected Shipping shipping_info;
        protected Address shipping_Address;

        protected OrderStatus status;

        protected double procent_discount;
        protected double flat_discount;
        protected double price_Total;
        protected double vat_Total;



        public Order_Data(Customer customer_info, List<Item> item_List, Shipping shipping_info)
        {
            this.customer_info = customer_info;
            this.item_List = item_List;
            this.shipping_info = shipping_info;
            shipping_Address = customer_info.address;
            status = OrderStatus.Awaiting;

            procent_discount = 0.0D;
            flat_discount = 0.0D;
            price_Total = 0.0D;
            vat_Total = 0.0D;
        }


        public Order_Data(Customer customer_info, List<Item> item_List, Shipping shipping_info, Address shipping_Address)
        {
            this.customer_info = customer_info;
            this.item_List = item_List;
            this.shipping_info = shipping_info;
            this.shipping_Address = shipping_Address;
            status = OrderStatus.Awaiting;

            procent_discount = 0.0D;
            flat_discount = 0.0D;
            price_Total = 0.0D;
            vat_Total = 0.0D;
        }




        public static int Compare(Order_Data order_data_1, Order_Data order_data_2)
        {
            return -1;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public virtual string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join( separator_string, ToString_Array(assign_string) );
        }


        public virtual string[] ToString_Array(string assign_string = ":")
        {
            string[] stringArray = new string[1];
            stringArray[0] = "";
            return stringArray;
        }



    }



    /// <summary>
    /// <c> OrderStatus : </c> <br/>  
    /// A enum indicating the overall status of a order. <br/>
    /// </summary>
    public enum OrderStatus
    {
        Awaiting,
        Ordered,
        Processing,
        Packing,
        Shipped,
        Delivered
    }
}

