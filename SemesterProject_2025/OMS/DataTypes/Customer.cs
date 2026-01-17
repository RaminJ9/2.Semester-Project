using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Customer : </c> <br/>  
    /// A class that contains the informations related to an specific customer, such as customer, address and affiliation. <br/>  
    /// </summary>
    public class Customer
    {
        public CustomerType customer_type;
        public string first_name;
        public string last_name;
        public string email;
        public int phone_number;
        public Address address;
        public Affiliation? affiliation;

        public Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, string road, int zip, string country, string city)
        {
            this.customer_type = customer_type;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone_number = phone_number;
            address = new Address(road, zip, country, city);
        }

        public Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address)
        {
            this.customer_type = customer_type;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone_number = phone_number;
            this.address = address;
        }

        public Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, string road, int zip, string country, string city, Affiliation affiliation)
        {
            this.customer_type = customer_type;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone_number = phone_number;
            address = new Address(road, zip, country, city);
            this.affiliation = affiliation;
        }

        public Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address, Affiliation affiliation)
        {
            this.customer_type = customer_type;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone_number = phone_number;
            this.address = address;
            this.affiliation = affiliation;
        }




        public static int Compare(Customer customer_1, Customer customer_2)
        {
            return -1;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_Array(assign_string));
        }


        public string[] ToString_Array(string assign_string = ":")
        {
            string[] stringArray = new string[1];
            stringArray[0] = "";
            return stringArray;
        }

    }


    /// <summary>
    /// <c> CustomerType : </c> <br/>  
    /// A enum indicating the type of customer. <br/>
    /// </summary>
    public enum CustomerType
    {
        Private,
        Business,
        Public
    }



}
