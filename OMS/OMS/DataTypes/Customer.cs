using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    /// <summary>
    /// <c> Customer : </c> <br/>  
    /// A class that contains the informations related to an specific customer, such as customer, address and affiliation. <br/>  
    /// </summary>
    public class Customer : IComparable<Customer>, IToString_Interface
    {
        private int customer_ID;
        public CustomerType customer_type;
        public string first_name;
        public string last_name;
        public string email;
        public int phone_number;
        public Address address;
        public Affiliation? affiliation;


        public Customer(Customer customer_object)
        {
            this.customer_ID = customer_object.Get_ID();
            this.customer_type = customer_object.customer_type;
            this.first_name = customer_object.first_name;
            this.last_name = customer_object.last_name;
            this.email = customer_object.email;
            this.phone_number = customer_object.phone_number;
            this.address = customer_object.address;

            if (customer_object.affiliation != null)
            {
                this.affiliation = customer_object.affiliation;
            }
        }

        public Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address)
        {
            this.customer_ID = -1;
            this.customer_type = customer_type;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone_number = phone_number;
            this.address = address;
        }

        public Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address, Affiliation affiliation)
        {
            this.customer_ID = -1;
            this.customer_type = customer_type;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone_number = phone_number;
            this.address = address;
            this.affiliation = affiliation;
        }

        public Customer(int customer_ID, CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address)
        {
            this.customer_ID = customer_ID;
            this.customer_type = customer_type;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone_number = phone_number;
            this.address = address;
        }

        public Customer(int customer_ID, CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address, Affiliation affiliation)
        {
            this.customer_ID = customer_ID;
            this.customer_type = customer_type;
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone_number = phone_number;
            this.address = address;
            this.affiliation = affiliation;
        }


        public int Get_ID()
        {
            return customer_ID;
        }




        public int CompareTo(Customer? customer_1)
        {
            if (customer_1 == null) return 1;

            if((this.customer_type - customer_1.customer_type) != 0)
            {
                return this.customer_type - customer_1.customer_type;
            }

            if (this.first_name.CompareTo(customer_1.first_name) != 0)
            {
                return this.first_name.CompareTo(customer_1.first_name);
            }

            if (this.last_name.CompareTo(customer_1.last_name) != 0)
            {
                return this.last_name.CompareTo(customer_1.last_name);
            }

            if (this.email.CompareTo(customer_1.email) != 0)
            {
                return this.email.CompareTo(customer_1.email);
            }

            if ((this.phone_number - customer_1.phone_number) != 0)
            {
                return this.phone_number - customer_1.phone_number;
            }

            if (address.CompareTo(customer_1.address) != 0)
            {
                return address.CompareTo(customer_1.address);
            }

            if( (this.affiliation != null) && (customer_1.affiliation != null) )
            {
                if (affiliation.CompareTo(customer_1.affiliation) != 0)
                {
                    return affiliation.CompareTo(customer_1.affiliation);
                }
            }
            else if( (this.affiliation == null) && (customer_1.affiliation != null) )
            {
                return -1;
            }
            else if( (this.affiliation != null) && (customer_1.affiliation == null) )
            {
                return 1;
            }

            return 0;
        }


        public override string ToString()
        {
            return ToString_Custom(":", "\n");
        }


        public string ToString_Custom(string assign_string = ":", string separator_string = "\n")
        {
            return String.Join(separator_string, ToString_List(assign_string));
        }


        public List<string> ToString_List(string assign_string = ":")
        {
            List<string> temp_string_1 = new List<string>();

            List<string> temp_string_2 = address.ToString_List(assign_string);

            List<string> stringList = new List<string>();


            temp_string_1.Add( "Customer type " + assign_string + " " + this.customer_type.ToString() );
            temp_string_1.Add( "First name " + assign_string + " " + this.first_name );
            temp_string_1.Add( "Last name " + assign_string + " " + this.last_name );
            temp_string_1.Add( "Email " + assign_string + " " + this.email );
            temp_string_1.Add( "Phone number " + assign_string + " " + this.phone_number );

            stringList.AddRange(temp_string_1);
            stringList.AddRange(temp_string_2);


            if (affiliation != null)
            {
                List<string> temp_string_3 = affiliation.ToString_List(assign_string);
                stringList.AddRange(temp_string_3);
            }

            return stringList;
        }

    }



    /// <summary>
    /// <c> CustomerType : </c> <br/>  
    /// A enum indicating the type of customer. <br/>
    /// </summary>
    /// 
    public enum CustomerType
    {
        Private,
        Business,
        Public
    }
}
