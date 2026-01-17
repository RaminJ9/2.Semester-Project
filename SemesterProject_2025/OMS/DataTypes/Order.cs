using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Order : </c> <br/>  
    /// A class that represent a specific order in the OMS system. <br/>  
    /// </summary>
    public class Order : Order_Data, IOrder
    {
        private readonly int order_ID;
        private readonly int customer_ID;
        private readonly int shipping_ID;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_info"></param>
        /// <param name="item_List"></param>
        /// <param name="shipping_info"></param>
        public Order(Customer customer_info, List<Item> item_List, Shipping shipping_info) : base(customer_info, item_List, shipping_info)
        {
            order_ID = -1;
            customer_ID = -1;
            shipping_ID = -1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_info"></param>
        /// <param name="item_List"></param>
        /// <param name="shipping_info"></param>
        /// <param name="shipping_Address"></param>
        public Order(Customer customer_info, List<Item> item_List, Shipping shipping_info, Address shipping_Address) : base(customer_info, item_List, shipping_info, shipping_Address)
        {
            order_ID = -1;
            customer_ID = -1;
            shipping_ID = -1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="customer_ID"></param>
        /// <param name="shipping_ID"></param>
        /// <param name="customer_info"></param>
        /// <param name="shipping_info"></param>
        /// <param name="item_List"></param>
        public Order(int order_ID, int customer_ID, int shipping_ID, Customer customer_info, Shipping shipping_info, List<Item> item_List) : base(customer_info, item_List, shipping_info)
        {
            this.order_ID = order_ID;
            this.customer_ID = customer_ID;
            this.shipping_ID = shipping_ID;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <param name="customer_ID"></param>
        /// <param name="shipping_ID"></param>
        /// <param name="customer_info"></param>
        /// <param name="shipping_info"></param>
        /// <param name="item_List"></param>
        /// <param name="shipping_Address"></param>
        public Order(int order_ID, int customer_ID, int shipping_ID, Customer customer_info, Shipping shipping_info, List<Item> item_List, Address shipping_Address) : base(customer_info, item_List, shipping_info, shipping_Address)
        {
            this.order_ID = order_ID;
            this.customer_ID = customer_ID;
            this.shipping_ID = shipping_ID;
        }





        public int Get_Order_ID()
        {
            return order_ID;
        }

        public int Get_Customer_ID()
        {
            return customer_ID;
        }

        public int Get_Shipping_ID()
        {
            return shipping_ID;
        }



        public Customer Get_customer_info()
        {
            return customer_info;
        }

        public List<Item> Get_item_List()
        {
            return item_List;
        }

        public Shipping Get_shipping_info()
        {
            return shipping_info;
        }

        public Address Get_shipping_Address()
        {
            return shipping_Address;
        }



        public OrderStatus Get_order_status()
        {
            return status;
        }



        public double Get_procent_discount()
        {
            return procent_discount;
        }

        public double Get_flat_discount()
        {
            return flat_discount;
        }

        public double Get_price_Total()
        {
            price_Total = Calculate_price_Total();
            return price_Total;
        }

        public double Get_vat_Total()
        {
            vat_Total = Calculate_vat_Total();
            return vat_Total;
        }






        private bool Calculate_price_Total(ref double temp_price_Total, ref double temp_vat_Total, ref double temp_itemPrice)
        {
            // . 
            foreach (Item item in item_List)
            {
                temp_itemPrice = item.price_per_unit * item.quantity * (1.0D - item.procent_discount) - item.flat_discount;

                temp_vat_Total += temp_itemPrice * (1.0D - item.vat_procent);

                temp_price_Total += temp_itemPrice;
            }

            // . 
            temp_price_Total = temp_price_Total * procent_discount;
            temp_vat_Total = temp_vat_Total * procent_discount;

            // .
            temp_vat_Total = temp_vat_Total - flat_discount / (temp_price_Total / temp_vat_Total);
            temp_price_Total = temp_price_Total - flat_discount;

            // . 
            if (customer_info.customer_type == CustomerType.Private)
            {
                temp_price_Total = temp_price_Total + temp_vat_Total;
            }
            else
            {
                temp_vat_Total = 0.0D;
            }

            return true;
        }

        private double Calculate_price_Total()
        {
            double temp_price_Total = 0.0D;
            double temp_vat_Total = 0.0D;

            double temp_itemPrice = 0;

            // . 
            Calculate_price_Total(ref temp_price_Total, ref temp_vat_Total, ref temp_itemPrice);

            return temp_price_Total;
        }

        private double Calculate_vat_Total()
        {
            double temp_price_Total = 0.0D;
            double temp_vat_Total = 0.0D;

            double temp_itemPrice = 0;

            // . 
            Calculate_price_Total(ref temp_price_Total, ref temp_vat_Total, ref temp_itemPrice);

            return temp_vat_Total;
        }






        public bool Set_customer_info(Customer new_customer_info)
        {
            customer_info = new_customer_info;
            return true;
        }

        public bool Set_item_List(List<Item> new_item_List)
        {
            item_List = new_item_List;
            return true;
        }

        public bool AddTo_item_List(Item new_item)
        {
            item_List.Add(new_item);
            return true;
        }

        public bool Set_shipping_info(Shipping shipping_info)
        {
            this.shipping_info = shipping_info;
            return true;
        }

        public bool Set_shipping_Address(Address new_shipping_Address)
        {
            shipping_Address = new_shipping_Address;
            return true;
        }



        public bool Set_order_status(OrderStatus orderStatus)
        {
            status = orderStatus;
            return true;
        }



        public bool Set_procent_discount(double procent_discount)
        {
            if (procent_discount >= 0.0D && procent_discount <= 1.0D)
            {
                this.procent_discount = procent_discount;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Set_flat_discount(double flat_discount)
        {
            if (flat_discount >= 0.0D)
            {
                this.flat_discount = flat_discount;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Set_vat_procent_AllItems(double new_vat_procent)
        {
            foreach (Item item in item_List)
            {
                item.vat_procent = new_vat_procent;
            }
            return true;
        }



        public bool Set_Shipping_TrackingNumber(string new_trackingNumber)
        {
            this.shipping_info.tracking_number = new_trackingNumber;
        }







        public static int Compare(Order order_1, Order order_2)
        {
            return -1;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public override string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_Array(assign_string));
        }


        public override string[] ToString_Array(string assign_string = ":")
        {
            string[] stringArray = new string[1];
            stringArray[0] = "";
            return stringArray;
        }





    }



}

