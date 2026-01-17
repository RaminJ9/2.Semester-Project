using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DataTypes;
using OMS.Process.Process_Interfaces;
// Creator : Alexander Maach
namespace OMS.Process
{
    public class Order_Processor : IOrder_Processes, IItem_Processes
    {

        private OMS.Access.Access access_instance;


        private Order? current_Order;





        public Order_Processor(OMS.Access.Access access_instance)
        {
            this.access_instance = access_instance;
        }




        

        // Create

        public int Create_Order(Customer customer_info, List<Item> item_List, Shipping shipping_info)
        {
            return Create_Order(customer_info, item_List, shipping_info, customer_info.address);
        }


        public int Create_Order(Customer customer_info, List<Item> item_List, Shipping shipping_info, Address shipping_address)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_info == null || item_List == null || shipping_info == null || shipping_address == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    for (int i = 0; i < item_List.Count; i++)
                    {
                        if (item_List[i] == null)
                        {
                            throw new ArgumentNullException("item number = " + i);
                        }
                    }
                }

                // .  
                if (item_List.Count != 0)
                {
                    foreach (Item item in item_List)
                    {
                        if (item.product_ID < 0)
                        {
                            throw new ArgumentException("Product ID can't be below zero.");
                        }
                    }
                }


                // . 


                // Creating temperary object of the "new" order. 
                Order new_order = new Order(customer_info, item_List, shipping_info, shipping_address);


                // 
                return access_instance.Create_Order_Database(new_order);

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                return -10;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return -20;
            }
        }










        // Get

        public Order Get_Order_Info(int order_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }


                // . 
                return access_instance.Fetch_Order_Database(order_ID);
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





        public Address Get_Order_ShippingAddress(int order_ID)
        {
            return (Get_Order_Info(order_ID)).Get_shipping_Address();
        }



        public Item Get_OrderItem(int order_ID, int product_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Item ID can't be below zero.");
                }


                // 
                Order temp_order = Get_Order_Info(order_ID);


                foreach (Item item in temp_order.Get_item_List()) 
                {
                    if ( item.product_ID == product_ID)
                    {
                        return item;
                    }
                }


                // . 
                throw new Exception("Item was not found ID " + product_ID + " was not found in order " + order_ID); 


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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }


        public List<Item> Get_OrderItem_List(int order_ID)
        {
            return (Get_Order_Info(order_ID)).Get_item_List();
        }


        public double Get_OrderItem_PricePerUnit(int order_ID, int product_ID)
        {
            return (Get_OrderItem(order_ID, product_ID)).price_per_unit;
        }


        public int Get_OrderItem_Quantity(int order_ID, int product_ID)
        {
            return (Get_OrderItem(order_ID, product_ID)).quantity;
        }


        public ItemStatus Get_OrderItem_Status(int order_ID, int product_ID)
        {
            return (Get_OrderItem(order_ID, product_ID)).status;
        }


        public ReturnStatus Get_OrderItem_ReturnedStatus(int order_ID, int product_ID)
        {
            return (Get_OrderItem(order_ID, product_ID)).returned_status;
        }


        public int Get_OrderItem_ReturnedQuantity(int order_ID, int product_ID)
        {
            return (Get_OrderItem(order_ID, product_ID)).returned_quantity;
        }







        // Update 

        public bool Update_Order_Customer(int order_ID, int customer_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || customer_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0 || customer_ID < 0)
                {
                    throw new ArgumentException("ID can't be below zero.");
                }

                // . 
                Customer temp_Customer_Info = access_instance.Fetch_Customer_Database(customer_ID);
                Order temp_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                Order Updated_Order_Info = new Order(temp_Order_Info.Get_Order_ID(), temp_Order_Info.Get_CreatedOn_TimeStamp(), temp_Customer_Info, temp_Order_Info.Get_shipping_info(), temp_Order_Info.Get_item_List(), temp_Order_Info.Get_shipping_Address());


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        public bool Update_Order_Address(int order_ID, Address new_shipping_address)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || new_shipping_address == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                Updated_Order_Info.Set_shipping_Address(new_shipping_address);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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


        public bool Update_Order_Status(int order_ID, OrderStatus status)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || status == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("item ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                Updated_Order_Info.Set_order_status(status);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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


        public bool Update_Order_Procent_Discount(int order_ID, double new_procent_discount)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || new_procent_discount == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("item ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                Updated_Order_Info.Set_procent_discount(new_procent_discount);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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


        public bool Update_Order_Flat_Discount(int order_ID, double new_flat_discount)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || new_flat_discount == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("item ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                Updated_Order_Info.Set_flat_discount(new_flat_discount);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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




        public bool Add_Order_Procent_Discount(int order_ID, double addTo_procent_discount)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || addTo_procent_discount == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("item ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                Updated_Order_Info.Set_procent_discount( Updated_Order_Info.Get_procent_discount() + addTo_procent_discount );


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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


        public bool Add_Order_Flat_Discount(int order_ID, double addTo_flat_discount)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || addTo_flat_discount == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("item ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                Updated_Order_Info.Set_flat_discount(Updated_Order_Info.Get_flat_discount() + addTo_flat_discount);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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


        public bool Add_Order_Item(int order_ID, Item add_Item_to_ItemList)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || add_Item_to_ItemList == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }
                else if (add_Item_to_ItemList.product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                Updated_Order_Info.AddTo_item_List(add_Item_to_ItemList);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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



        public bool Update_OrderItem_PricePerUnit(int order_ID, int product_ID, double new_pricePerUnit)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || product_ID == null || new_pricePerUnit == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }
                else if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                List<Item> temp_ItemList = Updated_Order_Info.Get_item_List();

                // .  
                foreach (Item item in temp_ItemList)
                {
                    if (item.product_ID == product_ID)
                    {
                        item.price_per_unit = new_pricePerUnit;
                    }
                }

                // . 
                Updated_Order_Info.Set_item_List(temp_ItemList);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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


        public bool Update_OrderItem_Quantity(int order_ID, int product_ID, int new_quantity)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || product_ID == null || new_quantity == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }
                else if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }
                else if (new_quantity < 0)
                {
                    throw new ArgumentException("New quantity can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                List<Item> temp_ItemList = Updated_Order_Info.Get_item_List();

                // .  
                foreach (Item item in temp_ItemList)
                {
                    if (item.product_ID == product_ID)
                    {
                        item.quantity = new_quantity;
                    }
                }

                // . 
                Updated_Order_Info.Set_item_List(temp_ItemList);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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


        public bool Update_OrderItem_Status(int order_ID, int product_ID, ItemStatus new_status)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || product_ID == null || new_status == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }
                else if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                List<Item> temp_ItemList = Updated_Order_Info.Get_item_List();

                // .  
                foreach (Item item in temp_ItemList)
                {
                    if (item.product_ID == product_ID)
                    {
                        item.status = new_status;
                    }
                }

                // . 
                Updated_Order_Info.Set_item_List(temp_ItemList);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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


        public bool Update_OrderItem_ReturnedStatus(int order_ID, int product_ID, ReturnStatus new_returnedStatus)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || product_ID == null || new_returnedStatus == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }
                else if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                List<Item> temp_ItemList = Updated_Order_Info.Get_item_List();

                // .  
                foreach (Item item in temp_ItemList)
                {
                    if (item.product_ID == product_ID)
                    {
                        item.returned_status = new_returnedStatus;
                    }
                }

                // . 
                Updated_Order_Info.Set_item_List(temp_ItemList);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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


        public bool Update_OrderItem_ReturnedQuantity(int order_ID, int product_ID, int new_returnedQuantity)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || product_ID == null || new_returnedQuantity == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }
                else if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                List<Item> temp_ItemList = Updated_Order_Info.Get_item_List();

                // .  
                foreach (Item item in temp_ItemList)
                {
                    if (item.product_ID == product_ID)
                    {
                        item.returned_quantity = new_returnedQuantity;
                    }
                }

                // . 
                Updated_Order_Info.Set_item_List(temp_ItemList);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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











        // Search

        public List<Order> Search_forOrder_CustomerID(int customer_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (customer_ID < 0)
                {
                    throw new ArgumentException("Customer ID can't be below zero.");
                }


                // . 
                return access_instance.Search_forOrder_CustomerID(customer_ID);
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



        public List<Order> Search_forOrder_ProductID(int product_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }


                // . 
                return access_instance.Search_forOrder_ProductID(product_ID);
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



        public List<Order> Search_forOrder_Email(string email)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (email == null)
                {
                    throw new ArgumentNullException();
                }


                // . 
                return access_instance.Search_forOrder_Email(email);
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



        public List<Order> Search_forOrder_Date(DateTime created_on)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (created_on == null)
                {
                    throw new ArgumentNullException();
                }


                // . 
                return access_instance.Search_forOrder_Date(created_on);
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


        public List<Order> Search_forOrder_Date(DateTime created_on, TimeStamp_Duration time_period)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (created_on == null || time_period == null)
                {
                    throw new ArgumentNullException();
                }


                // Combined list of all Orders, in that time period. 
                List<Order> combined_Orders = new List<Order>();

                // Setting the start and end date for the 2
                DateTime startDate = created_on, endDate = created_on;

                if (time_period == TimeStamp_Duration.Day) { endDate.AddDays(1); }
                else if (time_period == TimeStamp_Duration.Week) { endDate.AddDays(1); }
                else if (time_period == TimeStamp_Duration.Month) { endDate.AddMonths(1); }
                else if (time_period == TimeStamp_Duration.Quarter) { endDate.AddMonths(3); }
                else if (time_period == TimeStamp_Duration.Half_year) { endDate.AddMonths(6); }
                else if (time_period == TimeStamp_Duration.Year) { endDate.AddYears(1); }

                // .
                for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
                {
                    List<Order>? ordersOnDate = access_instance.Search_forOrder_Date(date);

                    if (ordersOnDate != null)
                    {
                        combined_Orders.AddRange(ordersOnDate);
                    }
                }

                // .  
                return combined_Orders;
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



        public List<Item> Search_forOrderItem_ProductID(int order_ID, int product_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || product_ID == null)
                {
                    throw new ArgumentNullException();
                }

                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // .  
                List<Item> sorted_OrderItem_List = new List<Item>();
                List<Item> temp_OrderItem_List = Get_OrderItem_List(order_ID);

                // .  
                foreach (Item item in temp_OrderItem_List)
                {
                    if ( item.product_ID == product_ID )
                    {
                        sorted_OrderItem_List.Add(item);
                    }
                }

                // .  
                return sorted_OrderItem_List;
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












        // Delete 

        public bool Delete_Order(int order_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }


                // . 
                if (access_instance.Delete_Order_Database(order_ID) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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


        public bool Delete_OrderItem(int order_ID, int product_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (order_ID == null || product_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (order_ID < 0)
                {
                    throw new ArgumentException("Order ID can't be below zero.");
                }
                else if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // . 
                Order Updated_Order_Info = access_instance.Fetch_Order_Database(order_ID);

                // . 
                List<Item> temp_ItemList = Updated_Order_Info.Get_item_List();
                List<Item> new_ItemList = new List<Item>();

                // .  
                foreach (Item item in temp_ItemList)
                {
                    if (item.product_ID != product_ID)
                    {
                        new_ItemList.Add(item);
                    }
                }

                // . 
                Updated_Order_Info.Set_item_List(new_ItemList);


                // . 
                if (access_instance.Update_Order_Database(Updated_Order_Info.Get_Order_ID(), Updated_Order_Info) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

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
        




        // Private Methodes 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item_List"></param>
        /// <returns></returns>
        //private List<int> Check_Database_ProductID_Exist(List<Item> item_List)
        //{
        //    // Goes through all Items on the list and checks if the product exists.
        //    foreach (Item item in item_List)
        //    {
        //        if ( Check_Database_ProductID_Exist(item.product_ID) == false )
        //        {
        //            return -(item.product_ID);
        //        }
        //    }
        //    return 0;
        //}


    }


}
