using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Access;
using OMS.DataTypes;
using OMS.Process.Process_Interfaces;

namespace OMS.Process
{

    /// <summary>
    /// 
    /// </summary>
    public class Process : IOrder_Processes, ICustomer_Processes, IItem_Processes, IStock_Processes, ILogin_Processes
    {

        private static Process process_instance;
        private Access.Access access_instance;
        private Order_Processor order_processor;
        private Customer_Processor customer_processor;
        private Stock_Processor stock_processor;
        private Login_Processor login_processor;




        /// <summary>
        /// 
        /// </summary>
        private Process() 
        {
            this.access_instance = Access.Access.GetInstance();
            this.order_processor = new Order_Processor();
            this.customer_processor = new Customer_Processor();
            this.stock_processor = new Stock_Processor();
            this.login_processor = new Login_Processor();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Process GetInstance()
        {
            if (process_instance == null)
            {
                process_instance = new Process();
            }
            return process_instance;
        }




        public int Create_Order(Customer customer_info, List<Item> item_List, Shipping shipping_info)
        {
            return order_processor.Create_Order(customer_info, item_List, shipping_info);
        }

        public int Create_Order(Customer customer_info, List<Item> item_List, Shipping shipping_info, Address shipping_address)
        {
            return order_processor.Create_Order(customer_info, item_List, shipping_info, shipping_address);
        }



        public int Create_Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address)
        {
            return customer_processor.Create_Customer(customer_type, first_name, last_name, email, phone_number, address);
        }

        public int Create_Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address, Affiliation affiliation)
        {
            return customer_processor.Create_Customer(customer_type, first_name, last_name, email, phone_number, address, affiliation);
        }



        public int Create_Product(int product_ID, string product_name, int avalible_quantity, int stock_quantity)
        {
            return stock_processor.Create_Product(product_ID, product_name, avalible_quantity, stock_quantity);
        }



        public int Create_Shipping(Shipping_Option shipping_option, string tracking_number)
        {
            return order_processor.Create_Shipping(shipping_option, tracking_number);
        }



        public int Create_Shipping_Options(Delivery_option delivery_option, double shipping_price, string company_name)
        {
            return order_processor.Create_Shipping_Options(delivery_option, shipping_price, company_name);
        }



        public int Create_Affiliation(int CVR, string name, Address address)
        {
            return customer_processor.Create_Affiliation(CVR, name, address);
        }

        public int Create_Affiliation(int CVR, string name, string road, int zip, string country, string city)
        {
            return customer_processor.Create_Affiliation(CVR, name, road, zip, country, city);
        }





        // Get methods.
        public Order Get_Order_Info(int order_id)
        {
            return order_processor.Get_Order_Info(order_id);
        }

        public string Get_Order_Info_Str(int order_id)
        {
            return order_processor.Get_Order_Info_Str(order_id);
        }

        public string[] Get_Order_Info_StrArray(int order_id)
        {
            return order_processor.Get_Order_Info_StrArray(order_id);
        }



        public List<Order> Get_CustomerOrders_List(int customer_id)
        {
            return customer_processor.Get_CustomerOrders_List(customer_id);
        }

        public string Get_CustomerOrders_List_Str(int customer_id)
        {
            return customer_processor.Get_CustomerOrders_List_Str(customer_id);
        }

        public string[] Get_CustomerOrders_List_StrArray(int customer_id)
        {
            return customer_processor.Get_CustomerOrders_List_StrArray(customer_id);
        }



        public Item Get_OrderItem(int order_id, int item_id)
        {
            return order_processor.Get_OrderItem(order_id, item_id);
        }

        public List<Item> Get_OrderItem_List(int order_id)
        {
            return order_processor.Get_OrderItem_List(order_id);
        }

        public double Get_OrderItem_PricePerUnit(int order_id, int item_id)
        {
            return order_processor.Get_OrderItem_PricePerUnit(order_id, item_id);
        }

        public int Get_OrderItem_Quantity(int order_id, int item_id)
        {
            return order_processor.Get_OrderItem_Quantity(order_id, item_id);
        }

        public ItemStatus Get_OrderItem_Status(int order_id, int item_id)
        {
            return order_processor.Get_OrderItem_Status(order_id, item_id); 
        }

        public ReturnStatus Get_OrderItem_ReturnedStatus(int order_id, int item_id)
        {
            return order_processor.Get_OrderItem_ReturnedStatus(order_id, item_id);
        }

        public int Get_OrderItem_ReturnedQuantity(int order_id, int item_id)
        {
            return order_processor.Get_OrderItem_ReturnedQuantity(order_id, item_id);
        }



        public Address Get_Order_ShippingAddress(int order_id)
        {
            return order_processor.Get_Order_ShippingAddress(order_id);
        }

        public string Get_Order_ShippingAddress_Str(int order_id)
        {
            return order_processor.Get_Order_ShippingAddress_Str(order_id);
        }

        public string[] Get_Order_ShippingAddress_StrArray(int order_id)
        {
            return order_processor.Get_Order_ShippingAddress_StrArray(order_id);
        }



        public Customer Get_Customer_Info(int customer_id)
        {
            return customer_processor.Get_Customer_Info(customer_id);
        }

        public string Get_Customer_Info_Str(int customer_id)
        {
            return customer_processor.Get_Customer_Info_Str(customer_id);
        }

        public string[] Get_Customer_Info_StrArray(int customer_id)
        {
            return customer_processor.Get_Customer_Info_StrArray(customer_id);
        }



        public Product Get_Product(int product_id)
        {
            return stock_processor.Get_Product(product_id);
        }

        public List<Product> Get_Product_List()
        {
            return stock_processor.Get_Product_List();
        }

        public int Get_Product_CurrentlyInstock(int product_id)
        {
            return stock_processor.Get_Product_CurrentlyInstock(product_id);
        }

        public int Get_Product_CurrentlyAvaliable(int product_id)
        {
            return stock_processor.Get_Product_CurrentlyAvaliable(product_id);
        }



        public int Get_Affiliation(int CVR)
        {
            return customer_processor.Get_Affiliation(CVR);
        }





        // Update methods.
        public bool Update_Order_Customer(int order_id, int customer_id)
        {
            return order_processor.Update_Order_Customer(order_id, customer_id);
        }

        public bool Update_Order_Address(int order_id, Address shipping_address)
        {
            return order_processor.Update_Order_Address(order_id, shipping_address);
        }

        public bool Update_Order_Status(int order_id, OrderStatus status)
        {
            return order_processor.Update_Order_Status(order_id, status);
        }

        public bool Update_Order_Procent_Discount(int order_id, double new_procent_discount)
        {
            return order_processor.Update_Order_Procent_Discount(order_id, new_procent_discount);
        }

        public bool Update_Order_Flat_Discount(int order_id, double new_flat_discount)
        {
            return order_processor.Update_Order_Flat_Discount(order_id, new_flat_discount);
        }


        public bool Add_Order_Procent_Discount(int order_id, double addTo_procent_discount)
        {
            return order_processor.Add_Order_Procent_Discount(order_id, addTo_procent_discount);
        }

        public bool Add_Order_Flat_Discount(int order_id, double addTo_flat_discount)
        {
            return order_processor.Add_Order_Flat_Discount(order_id, addTo_flat_discount);
        }

        public bool Add_Order_Item(int order_id, Item add_Item_to_ItemList)
        {
            return order_processor.Add_Order_Item(order_id, add_Item_to_ItemList);
        }



        public bool Update_Customer_FirstName(int customer_id, string new_firstName)
        {
            return customer_processor.Update_Customer_FirstName(customer_id, new_firstName);
        }

        public bool Update_Customer_LastName(int customer_id, string new_lastName)
        {
            return customer_processor.Update_Customer_LastName(customer_id, new_lastName);
        }

        public bool Update_Customer_Address(int customer_id, Address new_address)
        {
            return customer_processor.Update_Customer_Address(customer_id, new_address);
        }

        public bool Update_Customer_Email(int customer_id, string new_email)
        {
            return customer_processor.Update_Customer_Email(customer_id, new_email);
        }

        public bool Update_Customer_PhoneNumber(int customer_id, int new_phoneNumber)
        {
            return customer_processor.Update_Customer_PhoneNumber(customer_id, new_phoneNumber);
        }

        public bool Update_Customer_Type(int customer_id, CustomerType new_type)
        {
            return customer_processor.Update_Customer_Type(customer_id, new_type);
        }

        public bool Update_Customer_Affiliation(int customer_id, Affiliation new_affiliation)
        {
            return customer_processor.Update_Customer_Affiliation(customer_id, new_affiliation);
        }



        public bool Update_Product_Name(int product_id, string new_name)
        {
            return stock_processor.Update_Product_Name(product_id, new_name);
        }

        public bool Update_Product_Instock(int product_id, int new_instock)
        {
            return stock_processor.Update_Product_Instock(product_id, new_instock);
        }

        public bool Update_Product_Avaliable(int product_id, int new_instock)
        {
            return stock_processor.Update_Product_Avaliable(product_id, new_instock);
        }



        public bool Update_OrderItem_PricePerUnit(int order_id, int item_id, double new_pricePerUnit)
        {
            return order_processor.Update_OrderItem_PricePerUnit(order_id, item_id, new_pricePerUnit);
        }

        public bool Update_OrderItem_Quantity(int order_id, int item_id, int new_quantity)
        {
            return order_processor.Update_OrderItem_Quantity(order_id, item_id, new_quantity);
        }

        public bool Update_OrderItem_Status(int order_id, int item_id, ItemStatus new_status)
        {
            return order_processor.Update_OrderItem_Status(order_id, item_id, new_status);
        }

        public bool Update_OrderItem_ReturnedStatus(int order_id, int item_id, ReturnStatus new_returnedStatus)
        {
            return order_processor.Update_OrderItem_ReturnedStatus(order_id, item_id, new_returnedStatus);
        }

        public bool Update_OrderItem_ReturnedQuantity(int order_id, int item_id, int new_returnedQuantity)
        {
            return order_processor.Update_OrderItem_ReturnedQuantity(order_id, item_id, new_returnedQuantity);
        }





        // Delete methods.
        public bool Delete_Order(int order_id)
        {
            return order_processor.Delete_Order(order_id);
        }

        public bool Delete_Customer(int customer_id)
        {
            return customer_processor.Delete_Customer(customer_id);
        }

        public bool Delete_Product(int product_id)
        {
            return stock_processor.Delete_Product(product_id);
        }

        public bool Delete_ZIP(int zip)
        {
            return customer_processor.Delete_ZIP(zip);
        }

        public bool Delete_Affiliation(int cvr)
        {
            return customer_processor.Delete_Affiliation(cvr);
        }

        public bool Delete_OrderItem(int order_id, int item_id)
        {
            return order_processor.Delete_OrderItem(order_id, item_id);
        }
















    }
}
