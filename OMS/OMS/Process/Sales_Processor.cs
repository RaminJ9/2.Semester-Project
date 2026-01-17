using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.Process
{
    public class Sales_Processor
    {



        private OMS.Access.Access access_instance;
        private OMS.Process.Order_Processor order_processor;




        public Sales_Processor(OMS.Access.Access access_instance, OMS.Process.Order_Processor order_processor)
        {
            this.access_instance = access_instance;
            this.order_processor = order_processor;
        }








        public string Generate_SalesReport()
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }



        public List<string> Generate_SalesReport_List()
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }



        public Dictionary<int, string> Generate_SalesReport_Dict()
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }












        public List<Order>? Search_forOrders_Product_Date(int product_ID, DateTime soldOn_Date, TimeStamp_Duration time_period = TimeStamp_Duration.Day)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null || soldOn_Date == null || time_period == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // The unfilted order list and the filtered order list. 
                List<Order> unfiltered_orders = order_processor.Search_forOrder_Date(soldOn_Date, time_period);
                List<Order> filtered_orders = new List<Order>();

                // .  
                foreach (Order order in unfiltered_orders)
                {
                    foreach (Item item in order.Get_item_List())
                    {
                        if (item.product_ID == product_ID)
                        {
                            filtered_orders.Add(order);
                        }
                    }
                }

                return filtered_orders;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }



        public int Search_forSold_Product_Date(int product_ID, DateTime soldOn_Date, TimeStamp_Duration time_period = TimeStamp_Duration.Day)
        {
            try
            {
                // .  
                List<Order> temp_Order_List = Search_forOrders_Product_Date(product_ID, soldOn_Date, time_period);

                int productsSold_Counter = 0;

                // . 
                foreach (Order order in temp_Order_List)
                {
                    List<Item> temp_Item_List = order.Get_item_List();

                    // .  
                    foreach (Item item in temp_Item_List)
                    {
                        if (item.product_ID == product_ID)
                        {
                            productsSold_Counter += item.quantity;
                        }
                    }
                }

                return productsSold_Counter;

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


    }








}
