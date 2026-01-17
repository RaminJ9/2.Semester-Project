using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Item : </c> <br/>  
    /// A class that contains the informations related to an specific item, such as item Id, name, price, quantity, status and return status. <br/>  
    /// </summary>
    public class Item : IComparable<Item>, IToString_Interface
    {
        private int item_ID;
        public int product_ID;
        public string name;
        public double price_per_unit;
        public double procent_discount;
        public double flat_discount;
        public double vat_procent;
        public double total_net_price;
        public double total_vat;

        // The quantity of this item. 
        public int quantity;

        // The status of the item, in regards to an order. 
        public ItemStatus status;

        // The number of items returned. 
        public int returned_quantity;

        // The status of a return-request. 
        public ReturnStatus returned_status;

        // .
        public string reason_returned;

        public Item(Item item_object)
        {
            this.item_ID = item_object.Get_ID();
            this.product_ID = item_object.product_ID;
            this.name = item_object.name;
            this.quantity = item_object.quantity;
            this.price_per_unit = item_object.price_per_unit;
            this.procent_discount = item_object.procent_discount;
            this.flat_discount = item_object.flat_discount;
            this.vat_procent = item_object.vat_procent;


            // The status is set to "Awaiting" by default, ready to be processed.  
            this.status = item_object.status;

            // The return-status is set to "None" by default, since no return request has been made.  
            this.returned_status = item_object.returned_status;
            this.returned_quantity = item_object.returned_quantity;
            this.reason_returned = item_object.reason_returned;

            // .  
            this.total_net_price = Calculate_Total_Price();
            this.total_vat = Calculate_Total_VAT();
        }

        public Item(int product_ID, string name, double price, int quantity, double procent_discount = 0.0D, double flat_discount = 0.0D, double vat_procent = 0.0D)
        {
            this.item_ID = -1;
            this.product_ID = product_ID;
            this.name = name;
            this.quantity = quantity;
            this.price_per_unit = price;
            this.procent_discount = procent_discount;
            this.flat_discount = flat_discount;
            this.vat_procent = vat_procent;


            // The status is set to "Awaiting" by default, ready to be processed.  
            this.status = new ItemStatus();
            this.status = ItemStatus.Awaiting;

            // The return-status is set to "None" by default, since no return request has been made.  
            this.returned_status = new ReturnStatus();
            this.returned_status = ReturnStatus.None;
            this.returned_quantity = 0;
            this.reason_returned = "";

            // .  
            this.total_net_price = Calculate_Total_Price();
            this.total_vat = Calculate_Total_VAT();
        }

        public Item(int product_ID, string name, double price, int quantity, ItemStatus status, double procent_discount = 0.0D, double flat_discount = 0.0D, double vat_procent = 0.0D)
        {
            this.item_ID = -1;
            this.product_ID = product_ID;
            this.name = name;
            this.quantity = quantity;
            this.price_per_unit = price;
            this.procent_discount = procent_discount;
            this.flat_discount = flat_discount;
            this.vat_procent = vat_procent;

            // The status is set based on the parameter. 
            this.status = new ItemStatus();
            this.status = status;

            // The return-status is set to "None" by default, since no return request has been made.  
            this.returned_status = new ReturnStatus();
            this.returned_status = ReturnStatus.None;
            this.returned_quantity = 0;
            this.reason_returned = "";

            // .  
            this.total_net_price = Calculate_Total_Price();
            this.total_vat = Calculate_Total_VAT();
        }

        public Item(int product_ID, string name, double price, int quantity, ItemStatus status, ReturnStatus returned_status, int returned_quantity, string reason_returned, double procent_discount = 0.0D, double flat_discount = 0.0D, double vat_procent = 0.0D)
        {
            this.item_ID = -1;
            this.product_ID = product_ID;
            this.name = name;
            this.quantity = quantity;
            this.price_per_unit = price;
            this.procent_discount = procent_discount;
            this.flat_discount = flat_discount;
            this.vat_procent = vat_procent;

            // The status is set based on the parameter. 
            this.status = new ItemStatus();
            this.status = status;

            // The return-status is set based on the parameter.  
            this.returned_status = new ReturnStatus();
            this.returned_status = returned_status;
            this.returned_quantity = returned_quantity;
            this.reason_returned = reason_returned;

            // .  
            this.total_net_price = Calculate_Total_Price();
            this.total_vat = Calculate_Total_VAT();
        }

        public Item(int item_ID, int product_ID, string name, double price, int quantity, double procent_discount = 0.0D, double flat_discount = 0.0D, double vat_procent = 0.0D)
        {
            this.item_ID = item_ID;
            this.product_ID = product_ID;
            this.name = name;
            this.quantity = quantity;
            this.price_per_unit = price;
            this.procent_discount = procent_discount;
            this.flat_discount = flat_discount;
            this.vat_procent = vat_procent;


            // The status is set to "Awaiting" by default, ready to be processed.  
            this.status = new ItemStatus();
            this.status = ItemStatus.Awaiting;

            // The return-status is set to "None" by default, since no return request has been made.  
            this.returned_status = new ReturnStatus();
            this.returned_status = ReturnStatus.None;
            this.returned_quantity = 0;
            this.reason_returned = "";

            // .  
            this.total_net_price = Calculate_Total_Price();
            this.total_vat = Calculate_Total_VAT();
        }

        public Item(int item_ID, int product_ID, string name, double price, int quantity, ItemStatus status, double procent_discount = 0.0D, double flat_discount = 0.0D, double vat_procent = 0.0D)
        {
            this.item_ID = item_ID;
            this.product_ID = product_ID;
            this.name = name;
            this.quantity = quantity;
            this.price_per_unit = price;
            this.procent_discount = procent_discount;
            this.flat_discount = flat_discount;
            this.vat_procent = vat_procent;

            // The status is set based on the parameter. 
            this.status = new ItemStatus();
            this.status = status;

            // The return-status is set to "None" by default, since no return request has been made.  
            this.returned_status = new ReturnStatus();
            this.returned_status = ReturnStatus.None;
            this.returned_quantity = 0;
            this.reason_returned = "";

            // .  
            this.total_net_price = Calculate_Total_Price();
            this.total_vat = Calculate_Total_VAT();
        }

        public Item(int item_ID, int product_ID, string name, double price, int quantity, ItemStatus status, ReturnStatus returned_status, int returned_quantity, string reason_returned, double procent_discount = 0.0D, double flat_discount = 0.0D, double vat_procent = 0.0D)
        {
            this.item_ID = item_ID;
            this.product_ID = product_ID;
            this.name = name;
            this.quantity = quantity;
            this.price_per_unit = price;
            this.procent_discount = procent_discount;
            this.flat_discount = flat_discount;
            this.vat_procent = vat_procent;

            // The status is set based on the parameter. 
            this.status = new ItemStatus();
            this.status = status;

            // The return-status is set based on the parameter.  
            this.returned_status = new ReturnStatus();
            this.returned_status = returned_status;
            this.returned_quantity = returned_quantity;
            this.reason_returned = reason_returned;

            // .  
            this.total_net_price = Calculate_Total_Price();
            this.total_vat = Calculate_Total_VAT();
        }


        public int Get_ID()
        {
            return item_ID;
        }



        public double Calculate_Total_Price()
        {
            return this.price_per_unit * this.quantity * (1.0D - this.procent_discount) - this.flat_discount;
        }


        public double Calculate_Total_VAT()
        {
            return Calculate_Total_Price() * (1.0D - this.vat_procent);
        }



        public int CompareTo(Item? item_1)
        {
            if (item_1 == null) return 1;

            if ((this.product_ID - item_1.product_ID) != 0)
            {
                return this.product_ID - item_1.product_ID;
            }

            if (this.name.CompareTo(item_1.name) != 0)
            {
                return this.name.CompareTo(item_1.name);
            }

            if ((this.price_per_unit - item_1.price_per_unit) != 0.0D)
            {
                return (int) (this.price_per_unit - item_1.price_per_unit);
            }

            if ((this.procent_discount - item_1.procent_discount) != 0.0D)
            {
                return (int)(this.procent_discount - item_1.procent_discount);
            }

            if ((this.flat_discount - item_1.flat_discount) != 0.0D)
            {
                return (int)(this.flat_discount - item_1.flat_discount);
            }

            if ((this.vat_procent - item_1.vat_procent) != 0.0D)
            {
                return (int)(this.vat_procent - item_1.vat_procent);
            }

            if ((this.total_net_price - item_1.total_net_price) != 0.0D)
            {
                return (int)(this.total_net_price - item_1.total_net_price);
            }

            if ((this.total_vat - item_1.total_vat) != 0.0D)
            {
                return (int)(this.total_vat - item_1.total_vat);
            }

            if ((this.quantity - item_1.quantity) != 0)
            {
                return this.quantity - item_1.quantity;
            }

            if ((this.status - item_1.status) != 0)
            {
                return this.status - item_1.status;
            }

            if ((this.returned_quantity - item_1.returned_quantity) != 0)
            {
                return this.returned_quantity - item_1.returned_quantity;
            }

            if ((this.returned_status - item_1.returned_status) != 0)
            {
                return this.returned_status - item_1.returned_status;
            }

            return 0;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_List(assign_string));
        }


        public List<string> ToString_List(string assign_string = ":")
        {
            List<string> temp_string_1 = new List<string>();

            List<string> stringList = new List<string>();

            temp_string_1.Add( "Product ID " + assign_string + " " + this.product_ID );
            temp_string_1.Add( "Product Name " + assign_string + " " + this.name );
            temp_string_1.Add( "Price per " + assign_string + " " + this.price_per_unit );
            temp_string_1.Add( "Discount % " + assign_string + " " + this.procent_discount );
            temp_string_1.Add( "Discount flat " + assign_string + " " + this.flat_discount );
            temp_string_1.Add( "VAT % " + assign_string + " " + this.vat_procent );
            temp_string_1.Add( "Quantity " + assign_string + " " + this.quantity );
            temp_string_1.Add( "Total net price " + assign_string + " " + this.total_net_price );
            temp_string_1.Add( "Total VAT " + assign_string + " " + this.total_vat );
            temp_string_1.Add( "Status " + assign_string + " " + this.status.ToString() );
            temp_string_1.Add( "Quantity returned " + assign_string + " " + this.returned_quantity );
            temp_string_1.Add( "Return status " + assign_string + " " + this.returned_status.ToString() );
            temp_string_1.Add("Reason for return " + assign_string + " " + this.reason_returned);

            stringList.AddRange(temp_string_1);

            return stringList;
        }

    }






    /// <summary>
    /// <c> ItemStatus : </c> <br/>  
    /// A enum indicating the status of a item in relation to an order. <br/>
    /// </summary>
    public enum ItemStatus
    {
        Awaiting,
        Ordered,
        Backorder,
        Processing,
        Packing,
        Shipped,
        Delivered
    }



    /// <summary>
    /// <c> ReturnStatus : </c> <br/>  
    /// A enum indicating the status of return-request of a item, in relation to an order. <br/>
    /// </summary>
    public enum ReturnStatus
    {
        None,
        Requested,
        Confirmed,
        Returned,
        Accepted,
        Replaced,
        Refunded,
        Denied
    }


}
