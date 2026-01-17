using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DataTypes;
using OMS.Process.Process_Interfaces;

namespace OMS.Process
{
    internal class Order_Processor : IOrder_Processes, IItem_Processes
    {

        private OMS.Access.Access access_instance;


        private Order? current_Order;





        public Order_Processor()
        {
            this.access_instance = OMS.Access.Access.GetInstance();
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


                // Checking if the products in the "item_List" exists in the database.
                int productID_Check_Result = Check_Database_ProductID_Exist(item_List);
                if (productID_Check_Result < 0)
                {
                    throw new ArgumentException("Product ID doesn't exist in database. ID = " + productID_Check_Result);
                }


                // Checking if the customer already exists in the database.



                // Creating temperary object of the "new" order. 
                Order new_order = new Order(customer_info, item_List, shipping_info, shipping_address);

                // 
                return access_instance.Add_Order_toDatabase(new_order);

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




        public int Create_Shipping_Options(Delivery_option delivery_option, double shipping_price, string company_name)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (delivery_option == null || shipping_price == null || company_name == null)
                {
                    throw new ArgumentNullException();
                }


                // Checking if the shipping option already exists in the database.



                // Creating temperary object of the "new" order. 
                Shipping_Option new_shipping_option = new Shipping_Option(delivery_option, shipping_price, company_name);



                // 
                return access_instance.Add_ShippingOption_toDatabase(new_shipping_option);


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

        public Order Get_Order_Info(int order_id)
        {

        }


        public string Get_Order_Info_Str(int order_id)
        {
            return (Get_Order_Info(order_id)).ToString();
        }


        public string[] Get_Order_Info_StrArray(int order_id)
        {
            return (Get_Order_Info(order_id)).ToString_Array();
        }





        public Address Get_Order_ShippingAddress(int order_id)
        {

        }


        public string Get_Order_ShippingAddress_Str(int order_id)
        {

        }


        public string[] Get_Order_ShippingAddress_StrArray(int order_id)
        {

        }



        public Item Get_OrderItem(int order_id, int item_id)
        {

        }


        public List<Item> Get_OrderItem_List(int order_id)
        {

        }


        public double Get_OrderItem_PricePerUnit(int order_id, int item_id)
        {

        }


        public int Get_OrderItem_Quantity(int order_id, int item_id)
        {

        }


        public ItemStatus Get_OrderItem_Status(int order_id, int item_id)
        {

        }


        public ReturnStatus Get_OrderItem_ReturnedStatus(int order_id, int item_id)
        {

        }


        public int Get_OrderItem_ReturnedQuantity(int order_id, int item_id)
        {

        }







        // Update 

        public bool Update_Order_Customer(int order_id, int customer_id)
        {

        }


        public bool Update_Order_Address(int order_id, Address shipping_address)
        {

        }


        public bool Update_Order_Status(int order_id, OrderStatus status)
        {

        }


        public bool Update_Order_Procent_Discount(int order_id, double new_procent_discount)
        {

        }


        public bool Update_Order_Flat_Discount(int order_id, double new_flat_discount)
        {

        }




        public bool Add_Order_Procent_Discount(int order_id, double addTo_procent_discount)
        {

        }


        public bool Add_Order_Flat_Discount(int order_id, double addTo_flat_discount)
        {

        }


        public bool Add_Order_Item(int order_id, Item add_Item_to_ItemList)
        {

        }



        public bool Update_OrderItem_PricePerUnit(int order_id, int item_id, double new_pricePerUnit)
        {

        }


        public bool Update_OrderItem_Quantity(int order_id, int item_id, int new_quantity)
        {

        }


        public bool Update_OrderItem_Status(int order_id, int item_id, ItemStatus new_status)
        {

        }


        public bool Update_OrderItem_ReturnedStatus(int order_id, int item_id, ReturnStatus new_returnedStatus)
        {

        }


        public bool Update_OrderItem_ReturnedQuantity(int order_id, int item_id, int new_returnedQuantity)
        {

        }







        // Delete 

        public bool Delete_Order(int order_id)
        {

        }


        public bool Delete_OrderItem(int order_id, int item_id)
        {

        }





        // Private Methodes 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item_List"></param>
        /// <returns></returns>
        private int Check_Database_ProductID_Exist(List<Item> item_List)
        {
            // Goes through all Items on the list and checks if the product exists.
            foreach (Item item in item_List)
            {
                if ( Check_Database_ProductID_Exist(item.product_ID) == false )
                {
                    return -(item.product_ID);
                }
            }
            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        private bool Check_Database_ProductID_Exist(int product_ID)
        {

            return true /* add function call to the datebase, if the product_ID exists. */ ;
        }









    }
}
