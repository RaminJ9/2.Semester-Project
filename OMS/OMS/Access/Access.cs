using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OMS.Process;
using OMS.Access.Access_Interfaces;
// Creator : Alexander Maach
namespace OMS.Access
{
    // To make sure the right reference to the datatypes are used.
    using OMS.DataTypes;



    /// <summary>
    /// 
    /// </summary>
    public class Access : IOrder_Handler, ICustomer_Handler, IAddress_Handler, IAffiliation_Handler, IShippingOptions_Handler, IStock_Handler, ILogin_Handler
    {
        // .  
        private static Access access_instance;

        // .  
        private Order_Handler order_handler;
        private Customer_Handler customer_handler;
        private Stock_Handler stock_handler;
        private Login_Handler login_handler;

        // .  
        private readonly string connString;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        private Access (string connString)
        {
            this.connString = connString;
            this.customer_handler = new Customer_Handler(this.connString);
            this.order_handler = new Order_Handler(this.connString, ref this.customer_handler);
            this.stock_handler = new Stock_Handler(this.connString);
            this.login_handler = new Login_Handler(this.connString);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="connString"></param>
        /// <returns></returns>
        public static Access GetInstance(string connString)
        {
            if (access_instance == null)
            {
                access_instance = new Access(connString);
            }
            return access_instance;
        }







        // Order

        public int Create_Order_Database(Order new_Order)
        {
            return order_handler.Create_Order_Database(new_Order);
        }


        public Order? Fetch_Order_Database(int order_ID)
        {
            return order_handler.Fetch_Order_Database(order_ID);
        }


        public int Update_Order_Database(int order_ID, Order updated_Order)
        {
            return order_handler.Update_Order_Database(order_ID, updated_Order);  
        }


        public int Delete_Order_Database(int order_ID)
        {
            return order_handler.Delete_Order_Database(order_ID);
        }


        public List<Order>? Search_forOrder_CustomerID(int customer_ID)
        {
            return order_handler.Search_forOrder_CustomerID(customer_ID);
        }


        public List<Order>? Search_forOrder_ProductID(int product_ID)
        {
            return order_handler.Search_forOrder_ProductID(product_ID);
        }


        public List<Order>? Search_forOrder_Email(string email)
        {
            return order_handler.Search_forOrder_Email(email);
        }


        public List<Order>? Search_forOrder_Date(DateTime created_on)
        {
            return order_handler.Search_forOrder_Date(created_on);
        }


        public List<Order>? Fetch_CustomerOrders_Database(int customer_ID)
        {
            return order_handler.Fetch_CustomerOrders_Database(customer_ID);
        }







        // Customer

        public int Create_Customer_Database(Customer new_Customer)
        {
            return customer_handler.Create_Customer_Database(new_Customer);
        }


        public Customer? Fetch_Customer_Database(int customer_ID)
        {
            return customer_handler.Fetch_Customer_Database(customer_ID);
        }


        public int Update_Customer_Database(int customer_ID, Customer updated_Customer)
        {
            return customer_handler.Update_Customer_Database(customer_ID, updated_Customer);
        }


        public int Delete_Customer_Database(int customer_ID)
        {
            return customer_handler.Delete_Customer_Database(customer_ID);
        }


        public List<Customer>? Search_forCustomer_OrderID(int order_ID)
        {
            return customer_handler.Search_forCustomer_OrderID(order_ID);
        }


        public List<Customer>? Search_forCustomer_ProductID(int product_ID)
        {
            return customer_handler.Search_forCustomer_ProductID(product_ID);
        }


        public List<Customer>? Search_forCustomer_Email(string email)
        {
            return customer_handler.Search_forCustomer_Email(email);
        }


        public List<Customer>? Search_forCustomer_Name(string first_Name, string last_Name)
        {
            return customer_handler.Search_forCustomer_Name(first_Name, last_Name);
        }




        // Address  
        public int Create_ZIPCode_Database(ZIPCode zipCode)
        {
            return customer_handler.Create_ZIPCode_Database(zipCode);
        }


        public int Create_ZIPCode_Database(int zip, string country, string city)
        {
            return customer_handler.Create_ZIPCode_Database(zip, country, city);
        }


        public ZIPCode? Fetch_ZIPCode_Database(int zip_ID)
        {
            return customer_handler.Fetch_ZIPCode_Database(zip_ID);
        }


        public ZIPCode? Fetch_ZIPID_Database(int zip_Code)
        {
            return customer_handler.Fetch_ZIPID_Database(zip_Code);
        }


        public List<ZIPCode>? Fetch_All_ZIPCode_Database()
        {
            return customer_handler.Fetch_All_ZIPCode_Database();
        }


        public int Update_ZIPCode_Database(int zip_ID, ZIPCode zipCode)
        {
            return customer_handler.Update_ZIPCode_Database(zip_ID, zipCode);   
        }


        public int Delete_ZIPCode_Database(int zip_ID)
        {
            return customer_handler.Delete_ZIPCode_Database(zip_ID);
        }






        // Affiliation 

        public int Create_Affiliation_Database(Affiliation affiliation)
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }


        public Affiliation? Fetch_Affiliation_Database(int affiliation_ID)
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }


        public List<Affiliation>? Fetch_All_Affiliation_Database()
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }


        public int Update_Affiliation_Database(int affiliation_ID, Affiliation updated_Customer)
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }



        public List<Affiliation>? Search_forAffiliation_CVR(int cvr)
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }



        public List<Affiliation>? Search_forAffiliation_Name(string name)
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }



        public List<Affiliation>? Search_forAffiliation_Address(Address address)
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }



        public int Delete_Affiliation_Database(int affiliation_ID)
        {
            throw new NotImplementedException("This feature is not implemented yet.");
        }











        // ShippingOptions

        public int Create_ShippingOption_Database(string delivery_option, double shipping_price, string company_name)
        {
            return order_handler.Create_ShippingOption_Database(delivery_option, shipping_price, company_name);
        }

        public int Create_ShippingOption_Database(Shipping_Option new_ShippingOption)
        {
            return order_handler.Create_ShippingOption_Database(new_ShippingOption);
        }


        public Shipping_Option? Fetch_ShippingOption_Database(int shippingOption_ID)
        {
            return order_handler.Fetch_ShippingOption_Database(shippingOption_ID);
        }


        public List<Shipping_Option>? Fetch_All_ShippingOptions_Database()
        {
            return order_handler.Fetch_All_ShippingOptions_Database();
        }


        public int Update_ShippingOption_Database(int shippingOption_ID, Shipping_Option updated_ShippingOption)
        {
            return order_handler.Update_ShippingOption_Database(shippingOption_ID, updated_ShippingOption);
        }


        public int Delete_ShippingOption_Database(int shippingOption_ID)
        {
            return order_handler.Delete_ShippingOption_Database(shippingOption_ID);
        }








        // Stock 

        public int Create_Product_Database(Product new_Product)
        {
            return stock_handler.Create_Product_Database(new_Product);
        }


        public Product? Fetch_Product_Database(int product_ID)
        {
            return stock_handler.Fetch_Product_Database(product_ID);
        }


        public List<Product>? Fetch_All_Products_Database()
        {
            return stock_handler.Fetch_All_Products_Database();
        }



        public int Update_Product_Database(int product_ID, Product updated_Product)
        {
            return stock_handler.Update_Product_Database(product_ID, updated_Product);
        }


        public List<Product>? Search_forProduct_Name(string product_name)
        {
            return stock_handler.Search_forProduct_Name(product_name);
        }


        public List<Product>? Search_forProduct_Avaliable(double product_minAvaliable, double product_maxAvaliable)
        {
            return stock_handler.Search_forProduct_Avaliable(product_minAvaliable, product_maxAvaliable);
        }


        public List<Product>? Search_forProduct_Stock(double product_minStock, double product_maxStock)
        {
            return stock_handler.Search_forProduct_Stock(product_minStock, product_maxStock);
        }


        public int Delete_Product_Database(int product_ID)
        {
            return stock_handler.Delete_Product_Database(product_ID);
        }






    }
}