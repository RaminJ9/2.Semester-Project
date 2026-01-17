using OMS.DataTypes;
using OMS.Process.Process_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process
{
    internal class Customer_Processor : ICustomer_Processes
    {

        private OMS.Access.Access access_instance;




        public Customer_Processor()
        {
            this.access_instance = OMS.Access.Access.GetInstance();
        }





        // Create 

        public int Create_Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address)
        {

        }


        public int Create_Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address, Affiliation affiliation)
        {

        }




        public int Create_Affiliation(int CVR, string name, Address address)
        {

        }


        public int Create_Affiliation(int CVR, string name, string road, int zip, string country, string city)
        {

        }





        // Get 

        public Customer Get_Customer_Info(int customer_id)
        {

        }


        public string Get_Customer_Info_Str(int customer_id)
        {

        }


        public string[] Get_Customer_Info_StrArray(int customer_id)
        {

        }




        public List<Order> Get_CustomerOrders_List(int customer_id)
        {

        }


        public string Get_CustomerOrders_List_Str(int customer_id)
        {

        }


        public string[] Get_CustomerOrders_List_StrArray(int customer_id)
        {

        }



        public int Get_Affiliation(int CVR)
        {

        }





        // Update 

        public bool Update_Customer_FirstName(int customer_id, string new_firstName)
        {

        }


        public bool Update_Customer_LastName(int customer_id, string new_lastName)
        {

        }


        public bool Update_Customer_Address(int customer_id, Address new_address)
        {

        }


        public bool Update_Customer_Email(int customer_id, string new_email)
        {

        }


        public bool Update_Customer_PhoneNumber(int customer_id, int new_phoneNumber)
        {

        }


        public bool Update_Customer_Type(int customer_id, CustomerType new_type)
        {

        }


        public bool Update_Customer_Affiliation(int customer_id, Affiliation new_affiliation)
        {

        }





        // Delete 

        public bool Delete_Customer(int customer_id)
        {

        }


        public bool Delete_ZIP(int zip)
        {

        }


        public bool Delete_Affiliation(int cvr)
        {

        }


    }
}
