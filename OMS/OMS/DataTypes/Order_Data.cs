using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Order_Data : </c> <br/>  
    /// A class that contains the informations related to an specific order, such as customer informations, items and shipping address. <br/>  
    /// </summary>
    public class Order_Data : IComparable<Order_Data>, IToString_Interface
    {

        protected Customer customer_info;
        protected List<Item> item_List;
        protected Shipping shipping_info;
        protected Address shipping_Address;

        protected OrderStatus status;

        protected double procent_discount;
        protected double flat_discount;
        protected double net_price_Total;
        protected double vat_Total;
        protected double price_Total;



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




        public int CompareTo(Order_Data? order_data_1)
        {
            if (order_data_1 == null) return 1;

            if ( (this.customer_info.CompareTo( ((Order)order_data_1).Get_customer_info() )) != 0)
            {
                return this.customer_info.CompareTo( ((Order)order_data_1).Get_customer_info() );
            }

            List<Item> temp_item_List = ((Order)order_data_1).Get_item_List();

            if ((this.item_List.Count - temp_item_List.Count) != 0)
            {
                return this.item_List.Count - temp_item_List.Count;
            }

            for (int i = 0; i < this.item_List.Count; i++)
            {
                if ( ( ((Item) (this.item_List[i])).CompareTo(temp_item_List[i])) != 0)
                {
                    return ((Item) (this.item_List[i])).CompareTo(temp_item_List[i]);
                }
            }

            if ((this.shipping_info.CompareTo(((Order)order_data_1).Get_shipping_info())) != 0)
            {
                return this.shipping_info.CompareTo(((Order)order_data_1).Get_shipping_info());
            }

            if ((this.shipping_Address.CompareTo(((Order)order_data_1).Get_shipping_Address())) != 0)
            {
                return this.shipping_Address.CompareTo(((Order)order_data_1).Get_shipping_Address());
            }

            if ((this.status - ((Order)order_data_1).Get_order_status()) != 0)
            {
                return this.status - ((Order)order_data_1).Get_order_status();
            }

            if ((this.procent_discount - ((Order)order_data_1).Get_procent_discount()) != 0.0D)
            {
                return (int)(this.procent_discount - ((Order)order_data_1).Get_procent_discount());
            }

            if ((this.flat_discount - ((Order)order_data_1).Get_flat_discount()) != 0.0D)
            {
                return (int)(this.flat_discount - ((Order)order_data_1).Get_flat_discount());
            }

            if ((this.price_Total - ((Order)order_data_1).Get_price_Total()) != 0.0D)
            {
                return (int)(this.price_Total - ((Order)order_data_1).Get_price_Total());
            }

            if ((this.vat_Total - ((Order)order_data_1).Get_vat_Total()) != 0.0D)
            {
                return (int)(this.vat_Total - ((Order)order_data_1).Get_vat_Total());
            }

            return 0;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public virtual string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_List(assign_string));
        }


        public virtual List<string> ToString_List(string assign_string = ":")
        {
            List<string> temp_string_1 = new List<string>();

            List<string> temp_string_2 = customer_info.ToString_List(assign_string);

            List<string> temp_string_3 = shipping_info.ToString_List(assign_string);

            List<string> temp_string_4 = shipping_Address.ToString_List(assign_string);

            List<string> temp_string_5 = Item_List_ToString_List(assign_string);

            List<string> stringList = new List<string>();


            temp_string_1.Add( "Order stats " + assign_string + " " + this.status.ToString() );
            temp_string_1.Add( "Order discount % " + assign_string + " " + this.procent_discount );
            temp_string_1.Add( "Order discount flat " + assign_string + " " + this.flat_discount );
            temp_string_1.Add( "Total price " + assign_string + " " + this.price_Total );
            temp_string_1.Add( "Total VAT " + assign_string + " " + this.vat_Total );


            stringList.AddRange(temp_string_1);
            stringList.AddRange(temp_string_2);
            stringList.AddRange(temp_string_3);
            stringList.AddRange(temp_string_4);
            stringList.AddRange(temp_string_5);

            return stringList;
        }



        public List<string> Item_List_ToString_List(string assign_string = ":")
        {
            List<string> temp_itemList_String = new List<string>();

            // .  
            foreach (Item item in item_List)
            {
                List<string> temp_item_List = item.ToString_List(assign_string);

                // . 
                foreach(string single_string in temp_item_List)
                {
                    temp_itemList_String.Add(single_string);
                }
            }

            return temp_itemList_String;
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

