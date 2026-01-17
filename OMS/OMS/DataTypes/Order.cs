using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Order : </c> <br/>  
    /// A class that represent a specific order in the OMS system. <br/>  
    /// </summary>
    public class Order : Order_Data, IOrder, IComparable<Order>, IToString_Interface
    {
        private readonly int order_ID;
        private readonly int customer_ID;
        private readonly int shipping_ID;

        private readonly DateTime created_on;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_info"></param>
        /// <param name="item_List"></param>
        /// <param name="shipping_info"></param>
        public Order(Customer customer_info, List<Item> item_List, Shipping shipping_info) : base(customer_info, item_List, shipping_info)
        {
            this.order_ID = -1;
            this.customer_ID = customer_info.Get_ID();
            this.shipping_ID = shipping_info.Get_ID();
            this.created_on = DateTime.MinValue;
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
            this.order_ID = -1;
            this.customer_ID = customer_info.Get_ID();
            this.shipping_ID = shipping_info.Get_ID();
            this.created_on = DateTime.MinValue;
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
        public Order(int order_ID, DateTime created_on, Customer customer_info, Shipping shipping_info, List<Item> item_List) : base(customer_info, item_List, shipping_info)
        {
            this.order_ID = order_ID;
            this.customer_ID = customer_info.Get_ID();
            this.shipping_ID = shipping_info.Get_ID();
            this.created_on = created_on;
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
        public Order(int order_ID, DateTime created_on, Customer customer_info, Shipping shipping_info, List<Item> item_List, Address shipping_Address) : base(customer_info, item_List, shipping_info, shipping_Address)
        {
            this.order_ID = order_ID;
            this.customer_ID = customer_info.Get_ID();
            this.shipping_ID = shipping_info.Get_ID();
            this.created_on= created_on;
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

        public DateTime Get_CreatedOn_TimeStamp()
        {
            return created_on;
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
                temp_itemPrice = item.Calculate_Total_Price();

                temp_vat_Total += item.Calculate_Total_VAT();

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
            return true;
        }







        public int CompareTo(Order? order_1)
        {
            if (order_1 == null) return 1;

            if ((this.order_ID - order_1.order_ID) != 0)
            {
                return this.order_ID - order_1.order_ID;
            }

            if ((this.customer_ID - order_1.customer_ID) != 0)
            {
                return this.customer_ID - order_1.customer_ID;
            }

            if ((this.shipping_ID - order_1.shipping_ID) != 0)
            {
                return this.shipping_ID - order_1.shipping_ID;
            }

            if (DateTime.Compare(this.created_on, order_1.created_on) != 0)
            {
                return DateTime.Compare(this.created_on, order_1.created_on);
            }

            if (base.CompareTo( (Order_Data) order_1 ) != 0)
            {
                return base.CompareTo((Order_Data)order_1);
            }


            return 0;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public override string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_List(assign_string));
        }


        public override List<string> ToString_List(string assign_string = ":")
        {
            List<string> temp_string_1 = new List<string>();

            List<string> temp_string_2 = base.ToString_List(assign_string);

            List<string> stringList = new List<string>();

            temp_string_1.Add( "Order ID " + assign_string + " " + this.order_ID );
            temp_string_1.Add( "Customer ID " + assign_string + " " + this.customer_ID );
            temp_string_1.Add( "Shipping ID " + assign_string + " " + this.shipping_ID );

            stringList.AddRange(temp_string_1);
            stringList.AddRange(temp_string_2);

            return stringList;
        }

    }




}

