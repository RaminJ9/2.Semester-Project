using OMS.DataTypes;
using OMS.Process.Process_Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
// Creator : Alexander Maach
namespace OMS.Process
{

    public class Customer_Processor : ICustomer_Processes, IAffiliation_Processes
    {

        private OMS.Access.Access access_instance;




        public Customer_Processor(OMS.Access.Access access_instance)
        {
            this.access_instance = access_instance;
        }



        

        // Create 

        public int Create_Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_type == null || first_name == null || last_name == null || email == null || phone_number == null || address == null)
                {
                    throw new ArgumentNullException();
                }


                // Creating temperary object of the "new" Customer. 
                Customer new_customer = new Customer(customer_type, first_name, last_name, email, phone_number, address);


                // 
                return access_instance.Create_Customer_Database(new_customer);
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


        public int Create_Customer(CustomerType customer_type, string first_name, string last_name, string email, int phone_number, Address address, Affiliation affiliation)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_type == null || first_name == null || last_name == null || email == null || phone_number == null || address == null || affiliation == null)
                {
                    throw new ArgumentNullException();
                }


                // Creating temperary object of the "new" Customer. 
                Customer new_customer = new Customer(customer_type, first_name, last_name, email, phone_number, address, affiliation);


                // 
                return access_instance.Create_Customer_Database(new_customer);
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




        public int Create_Affiliation(int CVR, string name, Address address)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (CVR == null || name == null || address == null)
                {
                    throw new ArgumentNullException();
                }


                // Creating temperary object of the "new" Customer. 
                Affiliation new_affiliation = new Affiliation(CVR, name, address);


                // 
                return access_instance.Create_Affiliation_Database(new_affiliation);
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


        public int Create_Affiliation(int CVR, string name, string road, ZIPCode zip_object)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (CVR == null || name == null || road == null || zip_object == null)
                {
                    throw new ArgumentNullException();
                }

                if (zip_object.Get_ID() < 0)
                {
                    throw new ArgumentException();
                }


                // Creating temperary object of the "new" Customer. 
                Address new_address = new Address(road, zip_object);


                // 
                return Create_Affiliation(CVR, name, new_address);
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

        public Customer? Get_Customer_Info(int customer_ID)
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
                return access_instance.Fetch_Customer_Database(customer_ID);
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




        public List<Order>? Get_CustomerOrders_List(int customer_ID)
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
                return access_instance.Fetch_CustomerOrders_Database(customer_ID);
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



        public Affiliation? Get_Affiliation(int affiliation_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (affiliation_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (affiliation_ID < 0)
                {
                    throw new ArgumentException("Affiliation ID can't be below zero.");
                }


                // . 
                return access_instance.Fetch_Affiliation_Database(affiliation_ID);
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



        public List<Affiliation>? Get_All_Affiliation()
        {
            try
            {
                // . 
                return access_instance.Fetch_All_Affiliation_Database();
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





        // Update 

        public bool Update_Customer_FirstName(int customer_ID, string new_firstName)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_ID == null || new_firstName == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (customer_ID < 0)
                {
                    throw new ArgumentException("Customer ID can't be below zero.");
                }

                // . 
                Customer Updated_Customer_Info = access_instance.Fetch_Customer_Database(customer_ID);

                // . 
                Updated_Customer_Info.first_name = new_firstName;


                // . 
                if (access_instance.Update_Customer_Database(customer_ID, Updated_Customer_Info) >= 0)
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


        public bool Update_Customer_LastName(int customer_ID, string new_lastName)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_ID == null || new_lastName == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (customer_ID < 0)
                {
                    throw new ArgumentException("Customer ID can't be below zero.");
                }

                // . 
                Customer Updated_Customer_Info = access_instance.Fetch_Customer_Database(customer_ID);

                // . 
                Updated_Customer_Info.last_name = new_lastName;


                // . 
                if (access_instance.Update_Customer_Database(customer_ID, Updated_Customer_Info) >= 0)
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


        public bool Update_Customer_Address(int customer_ID, Address new_address)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_ID == null || new_address == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (customer_ID < 0)
                {
                    throw new ArgumentException("Customer ID can't be below zero.");
                }

                // . 
                Customer Updated_Customer_Info = access_instance.Fetch_Customer_Database(customer_ID);

                // . 
                Updated_Customer_Info.address = new_address;


                // . 
                if (access_instance.Update_Customer_Database(customer_ID, Updated_Customer_Info) >= 0)
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


        public bool Update_Customer_Email(int customer_ID, string new_email)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_ID == null || new_email == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (customer_ID < 0)
                {
                    throw new ArgumentException("Customer ID can't be below zero.");
                }

                // . 
                Customer Updated_Customer_Info = access_instance.Fetch_Customer_Database(customer_ID);

                // . 
                Updated_Customer_Info.email = new_email;


                // . 
                if (access_instance.Update_Customer_Database(customer_ID, Updated_Customer_Info) >= 0)
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


        public bool Update_Customer_PhoneNumber(int customer_ID, int new_phoneNumber)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_ID == null || new_phoneNumber == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (customer_ID < 0)
                {
                    throw new ArgumentException("Customer ID can't be below zero.");
                }

                // . 
                Customer Updated_Customer_Info = access_instance.Fetch_Customer_Database(customer_ID);

                // . 
                Updated_Customer_Info.phone_number = new_phoneNumber;


                // . 
                if (access_instance.Update_Customer_Database(customer_ID, Updated_Customer_Info) >= 0)
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


        public bool Update_Customer_Type(int customer_ID, CustomerType new_type)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_ID == null || new_type == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (customer_ID < 0)
                {
                    throw new ArgumentException("Customer ID can't be below zero.");
                }

                // . 
                Customer Updated_Customer_Info = access_instance.Fetch_Customer_Database(customer_ID);

                // . 
                Updated_Customer_Info.customer_type = new_type;


                // . 
                if (access_instance.Update_Customer_Database(customer_ID, Updated_Customer_Info) >= 0)
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


        public bool Update_Customer_Affiliation(int customer_ID, Affiliation new_affiliation)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (customer_ID == null || new_affiliation == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (customer_ID < 0)
                {
                    throw new ArgumentException("Customer ID can't be below zero.");
                }

                // . 
                Customer Updated_Customer_Info = access_instance.Fetch_Customer_Database(customer_ID);

                // . 
                Updated_Customer_Info.affiliation = new_affiliation;


                // . 
                if (access_instance.Update_Customer_Database(customer_ID, Updated_Customer_Info) >= 0)
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


        public bool Update_Affiliation(int affiliation_ID, Affiliation new_affiliation)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (affiliation_ID == null || new_affiliation == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (affiliation_ID < 0)
                {
                    throw new ArgumentException("Customer ID can't be below zero.");
                }


                // . 
                if (access_instance.Update_Affiliation_Database(affiliation_ID, new_affiliation) >= 0)
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

        public List<Customer>? Search_forCustomer_OrderID(int order_ID)
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
                return access_instance.Search_forCustomer_OrderID(order_ID);
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



        public List<Customer>? Search_forCustomer_ProductID(int product_ID)
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
                return access_instance.Search_forCustomer_ProductID(product_ID);
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



        public List<Customer>? Search_forCustomer_Email(string email)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (email == null)
                {
                    throw new ArgumentNullException();
                }


                // . 
                return access_instance.Search_forCustomer_Email(email);
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



        public List<Customer>? Search_forCustomer_Name(string first_Name, string last_Name)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (first_Name == null || last_Name == null)
                {
                    throw new ArgumentNullException();
                }


                // . 
                return access_instance.Search_forCustomer_Name(first_Name, last_Name);
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


        public List<Affiliation>? Search_forAffiliation_CVR(int cvr)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (cvr == null)
                {
                    throw new ArgumentNullException();
                }


                // . 
                return access_instance.Search_forAffiliation_CVR(cvr);
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



        public List<Affiliation>? Search_forAffiliation_Name(string name)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (name == null)
                {
                    throw new ArgumentNullException();
                }


                // . 
                return access_instance.Search_forAffiliation_Name(name);
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



        public List<Affiliation>? Search_forAffiliation_Address(Address address)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (address == null)
                {
                    throw new ArgumentNullException();
                }


                // . 
                return access_instance.Search_forAffiliation_Address(address);
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

        public bool Delete_Customer(int customer_ID)
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
                if (access_instance.Delete_Customer_Database(customer_ID) >= 0)
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


        public bool Delete_Affiliation(int affiliation_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (affiliation_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (affiliation_ID < 0)
                {
                    throw new ArgumentException("affiliation ID can't be below zero.");
                }


                // . 
                if (access_instance.Delete_Affiliation_Database(affiliation_ID) >= 0)
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
        

    }
}
