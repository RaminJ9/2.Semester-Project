using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OMS.Access;
using OMS.Process.Process_Interfaces;
using PIM;
using PIM.Data;
// Creator : Alexander Maach
namespace OMS.Process
{
    // To make sure the right reference to the datatypes are used.
    using OMS.DataTypes;



    /// <summary>
    /// 
    /// </summary>
    public class Process : IOrder_Processes, ICustomer_Processes, IItem_Processes, IShippingOptions_Processes, IStock_Processes, ILogin_Processes
    {
        // . 
        private static Process process_instance;

        // . 
        private Access.Access access_instance;

        // . 
        private ProductsAPI productsAPI;

        // . 
        private Order_Processor order_processor;
        private Customer_Processor customer_processor;
        private Shipping_Processor shipping_processor;
        private Stock_Processor stock_processor;
        private Login_Processor login_processor;
        private Sales_Processor sales_processor;

        // .  
        private readonly string connString;




        /// <summary>
        /// 
        /// </summary>
        private Process(string connString, ProductsAPI productsAPI) 
        {
            // Load order = 1.  (Allows the other process to get the right instance.)
            this.access_instance = Access.Access.GetInstance(connString);
            this.connString = connString;

            // Load order = 2.
            this.productsAPI = productsAPI;

            // Load order = 3.
            this.order_processor = new Order_Processor(this.access_instance);
            this.customer_processor = new Customer_Processor(this.access_instance);
            this.shipping_processor = new Shipping_Processor(this.access_instance);
            this.stock_processor = new Stock_Processor(this.access_instance);
            this.login_processor = new Login_Processor(this.access_instance);
            this.sales_processor = new Sales_Processor(this.access_instance, this.order_processor);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_connString"></param>
        /// <param name="new_productsAPI"></param>
        /// <returns></returns>
        public static Process GetInstance(string new_connString, ProductsAPI new_productsAPI)
        {
            if (process_instance == null)
            {
                process_instance = new Process(new_connString, new_productsAPI);
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



        public int Create_Shipping_Option(Delivery_option delivery_option, double shipping_price, string company_name)
        {
            return shipping_processor.Create_Shipping_Option(delivery_option, shipping_price, company_name);
        }



        public int Create_Affiliation(int CVR, string name, Address address)
        {
            return customer_processor.Create_Affiliation(CVR, name, address);
        }

        public int Create_Affiliation(int CVR, string name, string road, int zip, string country, string city)
        {
            return customer_processor.Create_Affiliation(CVR, name, new Address( road, new ZIPCode(zip, country, city ) ));
        }



        public string Generate_SalesReport()
        {
            return sales_processor.Generate_SalesReport();
        }


        public List<string> Generate_SalesReport_List()
        {
            return sales_processor.Generate_SalesReport_List();
        }


        public Dictionary<int, string> Generate_SalesReport_Dict()
        {
            return sales_processor.Generate_SalesReport_Dict();
        }









        // Get methods.
        public Order Get_Order_Info(int order_ID)
        {
            return order_processor.Get_Order_Info(order_ID);
        }



        public List<Order>? Get_CustomerOrders_List(int customer_ID)
        {
            return customer_processor.Get_CustomerOrders_List(customer_ID);
        }



        public Item Get_OrderItem(int order_ID, int item_ID)
        {
            return order_processor.Get_OrderItem(order_ID, item_ID);
        }

        public List<Item> Get_OrderItem_List(int order_ID)
        {
            return order_processor.Get_OrderItem_List(order_ID);
        }

        public double Get_OrderItem_PricePerUnit(int order_ID, int item_ID)
        {
            return order_processor.Get_OrderItem_PricePerUnit(order_ID, item_ID);
        }

        public int Get_OrderItem_Quantity(int order_ID, int item_ID)
        {
            return order_processor.Get_OrderItem_Quantity(order_ID, item_ID);
        }

        public ItemStatus Get_OrderItem_Status(int order_ID, int item_ID)
        {
            return order_processor.Get_OrderItem_Status(order_ID, item_ID); 
        }

        public ReturnStatus Get_OrderItem_ReturnedStatus(int order_ID, int item_ID)
        {
            return order_processor.Get_OrderItem_ReturnedStatus(order_ID, item_ID);
        }

        public int Get_OrderItem_ReturnedQuantity(int order_ID, int item_ID)
        {
            return order_processor.Get_OrderItem_ReturnedQuantity(order_ID, item_ID);
        }



        public Address Get_Order_ShippingAddress(int order_ID)
        {
            return order_processor.Get_Order_ShippingAddress(order_ID);
        }



        public Customer? Get_Customer_Info(int customer_ID)
        {
            return customer_processor.Get_Customer_Info(customer_ID);
        }




        public Shipping_Option Get_Shipping_Option(int shippingOption_ID)
        {
            return shipping_processor.Get_Shipping_Option(shippingOption_ID);
        }


        public List<Shipping_Option> Get_All_Shipping_Options()
        {
            return shipping_processor.Get_All_Shipping_Options();
        }




        public Product Get_Product(int product_ID)
        {
            return stock_processor.Get_Product(product_ID);
        }

        public List<Product> Get_All_Products()
        {
            return stock_processor.Get_All_Products();
        }

        public int Get_Product_CurrentlyInstock(int product_ID)
        {
            return stock_processor.Get_Product_CurrentlyInstock(product_ID);
        }

        public int Get_Product_CurrentlyAvaliable(int product_ID)
        {
            return stock_processor.Get_Product_CurrentlyAvaliable(product_ID);
        }



        public Affiliation? Get_Affiliation(int CVR)
        {
            return customer_processor.Get_Affiliation(CVR);
        }





        // Update methods.
        public bool Update_Order_Customer(int order_ID, int customer_ID)
        {
            return order_processor.Update_Order_Customer(order_ID, customer_ID);
        }

        public bool Update_Order_Address(int order_ID, Address shipping_address)
        {
            return order_processor.Update_Order_Address(order_ID, shipping_address);
        }

        public bool Update_Order_Status(int order_ID, OrderStatus status)
        {
            return order_processor.Update_Order_Status(order_ID, status);
        }

        public bool Update_Order_Procent_Discount(int order_ID, double new_procent_discount)
        {
            return order_processor.Update_Order_Procent_Discount(order_ID, new_procent_discount);
        }

        public bool Update_Order_Flat_Discount(int order_ID, double new_flat_discount)
        {
            return order_processor.Update_Order_Flat_Discount(order_ID, new_flat_discount);
        }


        public bool Add_Order_Procent_Discount(int order_ID, double addTo_procent_discount)
        {
            return order_processor.Add_Order_Procent_Discount(order_ID, addTo_procent_discount);
        }

        public bool Add_Order_Flat_Discount(int order_ID, double addTo_flat_discount)
        {
            return order_processor.Add_Order_Flat_Discount(order_ID, addTo_flat_discount);
        }

        public bool Add_Order_Item(int order_ID, Item add_Item_to_ItemList)
        {
            return order_processor.Add_Order_Item(order_ID, add_Item_to_ItemList);
        }



        public bool Update_Customer_FirstName(int customer_ID, string new_firstName)
        {
            return customer_processor.Update_Customer_FirstName(customer_ID, new_firstName);
        }

        public bool Update_Customer_LastName(int customer_ID, string new_lastName)
        {
            return customer_processor.Update_Customer_LastName(customer_ID, new_lastName);
        }

        public bool Update_Customer_Address(int customer_ID, Address new_address)
        {
            return customer_processor.Update_Customer_Address(customer_ID, new_address);
        }

        public bool Update_Customer_Email(int customer_ID, string new_email)
        {
            return customer_processor.Update_Customer_Email(customer_ID, new_email);
        }

        public bool Update_Customer_PhoneNumber(int customer_ID, int new_phoneNumber)
        {
            return customer_processor.Update_Customer_PhoneNumber(customer_ID, new_phoneNumber);
        }

        public bool Update_Customer_Type(int customer_ID, CustomerType new_type)
        {
            return customer_processor.Update_Customer_Type(customer_ID, new_type);
        }

        public bool Update_Customer_Affiliation(int customer_ID, Affiliation new_affiliation)
        {
            return customer_processor.Update_Customer_Affiliation(customer_ID, new_affiliation);
        }



        public bool Update_Shipping_Option(int shippingOption_ID, Shipping_Option updated_ShippingOption)
        {
            return shipping_processor.Update_Shipping_Option(shippingOption_ID, updated_ShippingOption);
        }



        public bool Update_Product_Name(int product_ID, string new_name)
        {
            return stock_processor.Update_Product_Name(product_ID, new_name);
        }

        public bool Update_Product_Instock(int product_ID, int new_instock)
        {
            return stock_processor.Update_Product_Instock(product_ID, new_instock);
        }

        public bool Update_Product_Avaliable(int product_ID, int new_instock)
        {
            return stock_processor.Update_Product_Avaliable(product_ID, new_instock);
        }



        public bool Update_OrderItem_PricePerUnit(int order_ID, int item_ID, double new_pricePerUnit)
        {
            return order_processor.Update_OrderItem_PricePerUnit(order_ID, item_ID, new_pricePerUnit);
        }

        public bool Update_OrderItem_Quantity(int order_ID, int item_ID, int new_quantity)
        {
            return order_processor.Update_OrderItem_Quantity(order_ID, item_ID, new_quantity);
        }

        public bool Update_OrderItem_Status(int order_ID, int item_ID, ItemStatus new_status)
        {
            return order_processor.Update_OrderItem_Status(order_ID, item_ID, new_status);
        }

        public bool Update_OrderItem_ReturnedStatus(int order_ID, int item_ID, ReturnStatus new_returnedStatus)
        {
            return order_processor.Update_OrderItem_ReturnedStatus(order_ID, item_ID, new_returnedStatus);
        }

        public bool Update_OrderItem_ReturnedQuantity(int order_ID, int item_ID, int new_returnedQuantity)
        {
            return order_processor.Update_OrderItem_ReturnedQuantity(order_ID, item_ID, new_returnedQuantity);
        }








        // Search

        public List<Order> Search_forOrder_CustomerID(int customer_ID)
        {
            return order_processor.Search_forOrder_CustomerID(customer_ID);
        }


        public List<Order> Search_forOrder_ProductID(int product_ID)
        {
            return order_processor.Search_forOrder_ProductID(product_ID);
        }


        public List<Order> Search_forOrder_Email(string email)
        {
            return order_processor.Search_forOrder_Email(email);
        }


        public List<Order> Search_forOrder_Date(DateTime created_on)
        {
            return order_processor.Search_forOrder_Date(created_on);
        }


        public List<Order>? Search_forOrder_Date(DateTime created_on, TimeStamp_Duration time_period)
        {
            return order_processor.Search_forOrder_Date(created_on, time_period);
        }






        public List<Customer>? Search_forCustomer_OrderID(int order_ID)
        {
            return customer_processor.Search_forCustomer_OrderID(order_ID);
        }



        public List<Customer>? Search_forCustomer_ProductID(int product_ID)
        {
            return customer_processor.Search_forCustomer_ProductID(product_ID);
        }



        public List<Customer>? Search_forCustomer_Email(string email)
        {
            return customer_processor.Search_forCustomer_Email(email);
        }



        public List<Customer>? Search_forCustomer_Name(string first_Name, string last_Name)
        {
            return customer_processor.Search_forCustomer_Name(first_Name, last_Name);
        }




        public List<Item> Search_forOrderItem_ProductID(int order_ID, int product_ID)
        {
            return order_processor.Search_forOrderItem_ProductID(order_ID, product_ID);
        }



        public List<Product> Search_forProduct_Name(string product_name)
        {
            return stock_processor.Search_forProduct_Name(product_name);
        }



        public List<Product>? Search_forProduct_Avaliable(double product_minAvaliable, double product_maxAvaliable)
        {
            return stock_processor.Search_forProduct_Avaliable(product_minAvaliable, product_maxAvaliable);
        }



        public List<Product>? Search_forProduct_Stock(double product_minStock, double product_maxStock)
        {
            return stock_processor.Search_forProduct_Stock(product_minStock, product_maxStock);
        }



        public List<Order>? Search_forOrders_Product_Date(int product_ID, DateTime soldOn_Date, TimeStamp_Duration time_period = TimeStamp_Duration.Day)
        {
            return sales_processor.Search_forOrders_Product_Date(product_ID, soldOn_Date, time_period);
        }



        public int Search_forSold_Product_Date(int product_ID, DateTime soldOn_Date, TimeStamp_Duration time_period = TimeStamp_Duration.Day)
        {
            return sales_processor.Search_forSold_Product_Date(product_ID,soldOn_Date, time_period);    
        }











        // Delete methods.
        public bool Delete_Order(int order_ID)
        {
            return order_processor.Delete_Order(order_ID);
        }

        public bool Delete_Customer(int customer_ID)
        {
            return customer_processor.Delete_Customer(customer_ID);
        }

        public bool Delete_Shipping_Option(int shippingOption_ID)
        {
            return shipping_processor.Delete_Shipping_Option(shippingOption_ID);
        }

        public bool Delete_Product(int product_ID)
        {
            return stock_processor.Delete_Product(product_ID);
        }

        public bool Delete_Affiliation(int affiliation_ID)
        {
            return customer_processor.Delete_Affiliation(affiliation_ID);
        }

        public bool Delete_OrderItem(int order_ID, int item_ID)
        {
            return order_processor.Delete_OrderItem(order_ID, item_ID);
        }
















    }
}
