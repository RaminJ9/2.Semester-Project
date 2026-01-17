using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process.Process_Interfaces
{
    internal interface ICustomer_Processes
    {

        // Create 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_type"></param>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="email"></param>
        /// <param name="phone_number"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public int Create_Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_type"></param>
        /// <param name="first_name"></param>
        /// <param name="last_name"></param>
        /// <param name="email"></param>
        /// <param name="phone_number"></param>
        /// <param name="address"></param>
        /// <param name="affiliation"></param>
        /// <returns></returns>
        public int Create_Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address, Affiliation affiliation);







        // Get 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns></returns>
        public Customer? Get_Customer_Info(int customer_ID);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns></returns>
        public List<Order>? Get_CustomerOrders_List(int customer_ID);







        // Update 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <param name="new_firstName"></param>
        /// <returns></returns>
        public bool Update_Customer_FirstName(int customer_ID, string new_firstName);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <param name="new_lastName"></param>
        /// <returns></returns>
        public bool Update_Customer_LastName(int customer_ID, string new_lastName);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <param name="new_address"></param>
        /// <returns></returns>
        public bool Update_Customer_Address(int customer_ID, Address new_address);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <param name="new_email"></param>
        /// <returns></returns>
        public bool Update_Customer_Email(int customer_ID, string new_email);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <param name="new_phoneNumber"></param>
        /// <returns></returns>
        public bool Update_Customer_PhoneNumber(int customer_ID, int new_phoneNumber);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <param name="new_type"></param>
        /// <returns></returns>
        public bool Update_Customer_Type(int customer_ID, CustomerType new_type);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <param name="new_affiliation"></param>
        /// <returns></returns>
        public bool Update_Customer_Affiliation(int customer_ID, Affiliation new_affiliation);







        // Search

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order_ID"></param>
        /// <returns></returns>
        public List<Customer>? Search_forCustomer_OrderID(int order_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product_ID"></param>
        /// <returns></returns>
        public List<Customer>? Search_forCustomer_ProductID(int product_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public List<Customer>? Search_forCustomer_Email(string email);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="first_Name"></param>
        /// <param name="last_Name"></param>
        /// <returns></returns>
        public List<Customer>? Search_forCustomer_Name(string first_Name, string last_Name);











        // Delete 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns></returns>
        public bool Delete_Customer(int customer_ID);



    }
}
