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




        /// <summary>
        /// 
        /// </summary>
        /// <param name="CVR"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public int Create_Affiliation(int CVR, string name, Address address);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CVR"></param>
        /// <param name="name"></param>
        /// <param name="road"></param>
        /// <param name="zip"></param>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public int Create_Affiliation(int CVR, string name, string road, int zip, string country, string city);





        // Get 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public Customer Get_Customer_Info(int customer_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public string Get_Customer_Info_Str(int customer_id);

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public string[] Get_Customer_Info_StrArray(int customer_id);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public List<Order> Get_CustomerOrders_List(int customer_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public string Get_CustomerOrders_List_Str(int customer_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public string[] Get_CustomerOrders_List_StrArray(int customer_id);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="CVR"></param>
        /// <returns></returns>
        public int Get_Affiliation(int CVR);





        // Update 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="new_firstName"></param>
        /// <returns></returns>
        public bool Update_Customer_FirstName(int customer_id, string new_firstName);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="new_lastName"></param>
        /// <returns></returns>
        public bool Update_Customer_LastName(int customer_id, string new_lastName);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="new_address"></param>
        /// <returns></returns>
        public bool Update_Customer_Address(int customer_id, Address new_address);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="new_email"></param>
        /// <returns></returns>
        public bool Update_Customer_Email(int customer_id, string new_email);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="new_phoneNumber"></param>
        /// <returns></returns>
        public bool Update_Customer_PhoneNumber(int customer_id, int new_phoneNumber);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="new_type"></param>
        /// <returns></returns>
        public bool Update_Customer_Type(int customer_id, CustomerType new_type);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <param name="new_affiliation"></param>
        /// <returns></returns>
        public bool Update_Customer_Affiliation(int customer_id, Affiliation new_affiliation);





        // Delete 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public bool Delete_Customer(int customer_id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <returns></returns>
        public bool Delete_ZIP(int zip);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cvr"></param>
        /// <returns></returns>
        public bool Delete_Affiliation(int cvr);



    }
}
