using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Item : </c> <br/>  
    /// A class that contains the informations related to an specific item, such as item Id, name, price, quantity, status and return status. <br/>  
    /// </summary>
    public class Item
    {
        public int product_ID;
        public string name;
        public double price_per_unit;
        public double procent_discount;
        public double flat_discount;
        public double vat_procent;

        // The quantity of this item. 
        public int quantity;

        // The status of the item, in regards to an order. 
        public ItemStatus status;

        // The number of items returned. 
        public int returned_quantity;

        // The status of a return-request. 
        public ReturnStatus returned_status;


        public Item(int product_ID, double price, int quantity, double vat_procent = 0.0D)
        {
            this.product_ID = product_ID;
            this.quantity = quantity;
            price_per_unit = price;
            procent_discount = 0.0;
            flat_discount = 0.0;
            this.vat_procent = vat_procent;


            // The status is set to "Awaiting" by default, ready to be processed.  
            status = new ItemStatus();
            status = ItemStatus.Awaiting;

            // The return-status is set to "None" by default, since no return request has been made.  
            returned_status = new ReturnStatus();
            returned_status = ReturnStatus.None;
            returned_quantity = 0;
        }

        public Item(int product_ID, double price, int quantity, ItemStatus status, double vat_procent = 0.0D)
        {
            this.product_ID = product_ID;
            this.quantity = quantity;
            price_per_unit = price;
            procent_discount = 0.0;
            flat_discount = 0.0;
            this.vat_procent = vat_procent;

            // The status is set based on the parameter. 
            this.status = new ItemStatus();
            this.status = status;

            // The return-status is set to "None" by default, since no return request has been made.  
            returned_status = new ReturnStatus();
            returned_status = ReturnStatus.None;
            returned_quantity = 0;
        }

        public Item(int product_ID, double price, int quantity, ItemStatus status, ReturnStatus returned_status, int returned_quantity, double vat_procent = 0.0D)
        {
            this.product_ID = product_ID;
            this.quantity = quantity;
            price_per_unit = price;
            procent_discount = 0.0;
            flat_discount = 0.0;
            this.vat_procent = vat_procent;

            // The status is set based on the parameter. 
            this.status = new ItemStatus();
            this.status = status;

            // The return-status is set based on the parameter.  
            this.returned_status = new ReturnStatus();
            this.returned_status = returned_status;
            this.returned_quantity = returned_quantity;
        }




        public static int Compare(Item item_1, Item item_2)
        {
            return -1;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_Array(assign_string));
        }


        public string[] ToString_Array(string assign_string = ":")
        {
            string[] stringArray = new string[1];
            stringArray[0] = "";
            return stringArray;
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
