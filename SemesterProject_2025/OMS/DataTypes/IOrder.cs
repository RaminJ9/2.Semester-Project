using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataTypes
{
    public interface IOrder
    {

        public int Get_Order_ID();

        public int Get_Customer_ID();

        public int Get_Shipping_ID();



        public Customer Get_customer_info();

        public List<Item> Get_item_List();

        public Shipping Get_shipping_info();

        public Address Get_shipping_Address();



        public OrderStatus Get_order_status();



        public double Get_procent_discount();

        public double Get_flat_discount();

        public double Get_price_Total();

        public double Get_vat_Total();






        public bool Set_customer_info(Customer new_customer_info);

        public bool Set_item_List(List<Item> new_item_List);

        public bool AddTo_item_List(Item new_item);

        public bool Set_shipping_info(Shipping shipping_info);

        public bool Set_shipping_Address(Address new_shipping_Address);



        public bool Set_order_status(OrderStatus orderStatus);



        public bool Set_procent_discount(double procent_discount);

        public bool Set_flat_discount(double flat_discount);

        public bool Set_vat_procent_AllItems(double new_vat_procent);



        public bool Set_Shipping_TrackingNumber(string new_trackingNumber);



    }
}
