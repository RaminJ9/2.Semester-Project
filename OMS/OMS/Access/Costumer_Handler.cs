using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DataTypes;
using OMS.Access.Access_Interfaces;
using System.Collections;
using System.Reflection.Emit;
using System.Xml.Linq;
// Creater: Rasmus R/Alexander Maach
namespace OMS.Access
{

    public class Customer_Handler : ICustomer_Handler, IAddress_Handler
    {

        
        private readonly string connString;



        public Customer_Handler(string connString)
        {
            this.connString = connString;
        }





        // ######################### CREATE ######################### //


        public int Create_Customer_Database(Customer new_customer)
        {

            // calls all the insert method to create an new order in the database. 
            int temp_customer_ID, temp_zip_ID;

            // Step 1. insert ZIPCode.
            temp_zip_ID = Create_ZIPCode_Database(new_customer.address.zip, new_customer.address.country, new_customer.address.city);

            // Step 2. insert customer.
            temp_customer_ID = InsertCustomer(new_customer.first_name, new_customer.last_name, new_customer.email, new_customer.phone_number, new_customer.address.road, temp_zip_ID);

            return temp_customer_ID;

        }




        private int InsertCustomer(string first_name, string last_name, string email, int phone_number, string address, int zip_ID)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using var cmd = new NpgsqlCommand(@"
            INSERT INTO customer (first_name, last_name, email, phone_number, address, zip_ID) 
            VALUES (@first_name, @last_name, @email, @phone_number, @address, @zip_ID)
            RETURNING customer_ID", conn);

                cmd.Parameters.AddWithValue("first_name", first_name);
                cmd.Parameters.AddWithValue("last_name", last_name);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("phone_number", phone_number);
                cmd.Parameters.AddWithValue("address", address);
                cmd.Parameters.AddWithValue("zip_ID", zip_ID);

                return (int)cmd.ExecuteScalar();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error: Violation of unique constraint.");
                return -1;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error: Foreign key constraint violation.");
                return -1;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error: Not null constraint violation.");
                return -1;
            }
        }





        public int Create_ZIPCode_Database(ZIPCode zipCode)
        {
            // .  
            return Create_ZIPCode_Database(zipCode.zip, zipCode.country, zipCode.city);
        }




        public int Create_ZIPCode_Database(int zip_code, string country, string city)
        {
            try
            {
                // Create and connect to the database, using the "connString". 
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                int zip_ID;

                // Creates the Quary to be send/executed by the database. 
                // "RETURNING order_id" returns the ID.
                using (var cmd = new NpgsqlCommand(@"
            INSERT INTO zip (country, city, zip_code)
            VALUES (@country, @city, @zip_code)
            ON CONFLICT (zip_code, city, country) DO NOTHING
            RETURNING id", conn))
                {
                    // Fills up the quary with the data needed. 
                    cmd.Parameters.AddWithValue("country", country);
                    cmd.Parameters.AddWithValue("city", city);
                    cmd.Parameters.AddWithValue("zip_code", zip_code);

                    // Sends/executer the quary and returns an int value (the ID). 
                    var result = cmd.ExecuteScalar();


                    if (result != null)
                    {
                        zip_ID = (int)result;
                    }
                    else
                    {
                        // Creates the Quary to be send/executed by the database. 
                        // "RETURNING id" returns the ID.
                        using var fetchCmd = new NpgsqlCommand(@"
                    SELECT id FROM zip 
                    WHERE country = @country AND city = @city AND zip_code = @zip_code", conn);

                        // Fills up the quary with the data needed. 
                        fetchCmd.Parameters.AddWithValue("country", country);
                        fetchCmd.Parameters.AddWithValue("city", city);
                        fetchCmd.Parameters.AddWithValue("zip_code", zip_code);

                        // Sends/executer the quary and returns an int value (the ID). 
                        zip_ID = (int)fetchCmd.ExecuteScalar();
                    }
                }

                return zip_ID;
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error: Violation of unique constraint.");
                return -1;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error: Foreign key constraint violation.");
                return -1;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error: Not null constraint violation.");
                return -1;
            }
        }















        // ######################### FETCHERS ######################### //


        public ZIPCode? Fetch_ZIPCode_Database(int zip_ID)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
            SELECT zip_code, country, city 
            FROM zip 
            WHERE id = @zip_ID", conn);

            cmd.Parameters.AddWithValue("zip_ID", zip_ID);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int zip_id = (int)zip_ID;
                int zip_code = reader.GetInt32(reader.GetOrdinal("zip_code"));
                string country = reader.GetString(reader.GetOrdinal("country"));
                string city = reader.GetString(reader.GetOrdinal("city"));
                return new ZIPCode(zip_id, zip_code, country, city);
            }

            return null;
        }



        public ZIPCode? Fetch_ZIPID_Database(int zip_Code)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
            SELECT id, country, city 
            FROM zip 
            WHERE zip_code = @zip_Code", conn);

            cmd.Parameters.AddWithValue("zip_Code", zip_Code);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int zip_code = (int)zip_Code;
                int zip_id = reader.GetInt32(reader.GetOrdinal("id"));
                string country = reader.GetString(reader.GetOrdinal("country"));
                string city = reader.GetString(reader.GetOrdinal("city"));
                return new ZIPCode(zip_id, zip_code, country, city);
            }

            return null;
        }




        public List<ZIPCode>? Fetch_All_ZIPCode_Database()
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
            SELECT id, zip_code, country, city 
            FROM zip", conn);

            List<ZIPCode> zip_list = new List<ZIPCode>();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int zip_id = reader.GetInt32(reader.GetOrdinal("id"));
                int zip_code = reader.GetInt32(reader.GetOrdinal("zip_code"));
                string country = reader.GetString(reader.GetOrdinal("country"));
                string city = reader.GetString(reader.GetOrdinal("city"));
                zip_list.Add(new ZIPCode(zip_id, zip_code, country, city));
            }

            return zip_list;
        }




        public Customer? Fetch_Customer_Database(int customer_ID)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            // .c refers to the customer table, and .z refers to the zip table.
            using var cmd = new NpgsqlCommand(@"
            SELECT 
            c.customer_ID, c.first_name, c.last_name, c.email, c.phone_number, c.address, 
            z.zip_code, z.country, z.city, c.customer_type
            FROM customer c
            JOIN zip z ON c.zip_ID = z.id
            WHERE c.customer_ID = @customer_ID", conn);

            cmd.Parameters.AddWithValue("customer_ID", customer_ID);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Reads the current values from the database, and stores them in the variables.
                string firstName = reader.GetString(1);
                string lastName = reader.GetString(2);
                string email = reader.GetString(3);
                int phoneNumber = reader.GetInt32(4);
                string road = reader.GetString(5);
                int zip = reader.GetInt32(6);
                string country = reader.GetString(7);
                string city = reader.GetString(8);
                string customerTypeStr = reader.GetString(9);
                int zip_id = reader.GetInt32(10);

                // Parse customer_type enum from string
                if (!Enum.TryParse<CustomerType>(customerTypeStr, out var customerType))
                {
                    // Fallback if parsing fails
                    customerType = CustomerType.Private;
                }

                // Create the Customer object with the retrieved values
                return new Customer(
                    customer_ID,
                    customerType,
                    firstName,
                    lastName,
                    email,
                    phoneNumber,
                    new Address(road, new ZIPCode(zip_id, zip, country, city))
                );
            }

            return null;
        }











        // ######################### UPDATE ######################### //


        public int Update_Customer_Database(int customer_ID, Customer updated_Customer)
        {
            try
            {
                // Create and connect to the database, using the "connString". 
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // .  
                int result;

                // Creates the Quary to be send/executed by the database. 
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                UPDATE customer
                SET zip_code = @zip_code_value,
                    country = @country_value,
                    city = @city_value
                WHERE customer_id = @customer_id_value", conn))
                {
                    // Fills up the quary with the data needed. 
                    cmd.Parameters.AddWithValue("customer_id_value", customer_ID);
                    cmd.Parameters.AddWithValue("first_name_value", updated_Customer.first_name);
                    cmd.Parameters.AddWithValue("last_name_value", updated_Customer.last_name);
                    cmd.Parameters.AddWithValue("email_value", updated_Customer.email);
                    cmd.Parameters.AddWithValue("phone_number_value", updated_Customer.phone_number);
                    cmd.Parameters.AddWithValue("address_value", updated_Customer.address.road);
                    cmd.Parameters.AddWithValue("zip_id_value", updated_Customer.address.Get_ID());



                    // Sends/executer the quary and returns an int value (the ID). 
                    result = cmd.ExecuteNonQuery();
                }

                result += Update_ZIPCode_Database(( (ZIPCode)updated_Customer.address ).Get_ID(), (ZIPCode)updated_Customer.address);

                // Incase there is a conflict on .  
                if (result == null)
                {
                    throw new CustomerNotFoundException(customer_ID);
                }
                else
                {
                    // Returns the ID used to put the 
                    return result;
                }
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Violation of unique constraint.");
                return -23505;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Foreign key constraint violation.");
                return -23503;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Not null constraint violation.");
                return -23502;
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return -100;
            }
        }



        public int Update_ZIPCode_Database(int zip_ID, ZIPCode zipCode)
        {
            try
            {
                // Create and connect to the database, using the "connString". 
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // .  
                int result;

                // Creates the Quary to be send/executed by the database. 
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                UPDATE zip
                SET zip_code = @zip_code_value,
                    country = @country_value,
                    city = @city_value
                WHERE id = @id_value", conn))
                {
                    // Fills up the quary with the data needed. 
                    cmd.Parameters.AddWithValue("id_value", zipCode.Get_ID());
                    cmd.Parameters.AddWithValue("zip_code_value", zipCode.zip);
                    cmd.Parameters.AddWithValue("country_value", zipCode.country);
                    cmd.Parameters.AddWithValue("city_value", zipCode.city);

                    // Sends/executer the quary and returns an int value (the ID). 
                    result = cmd.ExecuteNonQuery();
                }

                // Incase there is a conflict on .  
                if (result == null)
                {
                    throw new ZIPCodeNotFoundException(zip_ID);
                }
                else
                {
                    // Returns the ID used to put the 
                    return result;
                }
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Violation of unique constraint.");
                return -23505;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Foreign key constraint violation.");
                return -23503;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Not null constraint violation.");
                return -23502;
            }
            catch (ZIPCodeNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return -100;
            }
        }












        // ######################### SEARCH ######################### //


        public List<Customer>? Search_forCustomer_OrderID(int order_ID)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Customer> customer_List = new List<Customer>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT 
                c.customer_id, c.first_name, c.last_name, c.email, c.phone_number, c.address, 
                z.id, z.zip_code, z.country, z.city, c.customer_type, o.customer_id
                FROM customer c
                JOIN zip z ON c.zip_ID = z.id
                JOIN orders o ON o.customer_id = c.customer_id
                WHERE o.order_id = @order_id_value", conn))
                {

                    cmd.Parameters.AddWithValue("order_id_value", order_ID);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int customer_id = reader.GetInt32(reader.GetOrdinal("c.customer_id"));
                            string firstName = reader.GetString(reader.GetOrdinal("c.first_name"));
                            string lastName = reader.GetString(reader.GetOrdinal("c.last_name"));
                            string email = reader.GetString(reader.GetOrdinal("c.email"));
                            int phoneNumber = reader.GetInt32(reader.GetOrdinal("c.phone_number"));
                            string road = reader.GetString(reader.GetOrdinal("c.address"));
                            int zip_id = reader.GetInt32(reader.GetOrdinal("z.id"));
                            int zip_code = reader.GetInt32(reader.GetOrdinal("z.zip_code"));
                            string country = reader.GetString(reader.GetOrdinal("z.country"));
                            string city = reader.GetString(reader.GetOrdinal("z.city"));

                            // Create the Customer object with the retrieved values
                            customer_List.Add(new Customer(CustomerType.Private, firstName, lastName, email, phoneNumber, new Address(road, new ZIPCode(zip_id, zip_code, country, city))));
                        }
                    }
                }

                // Incase there is a conflict on .  
                if (customer_List.Count == 0)
                {
                    throw new CustomerNotFoundException();
                }
                else
                {
                    // Returns 
                    return customer_List;
                }
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Violation of unique constraint.");
                throw;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Foreign key constraint violation.");
                throw;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Not null constraint violation.");
                throw;
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



        public List<Customer>? Search_forCustomer_ProductID(int product_ID)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Customer> customer_List = new List<Customer>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT 
                c.customer_id, c.first_name, c.last_name, c.email, c.phone_number, c.address, 
                z.id, z.zip_code, z.country, z.city, c.customer_type, o.customer_id
                FROM customer c
                JOIN zip z ON c.zip_ID = z.id
                JOIN orders o ON o.customer_id = c.customer_id
                JOIN item_list i ON i.order_id = o.order_id
                WHERE i.product_id = @product_id_value", conn))
                {

                    cmd.Parameters.AddWithValue("product_id_value", product_ID);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int customer_id = reader.GetInt32(reader.GetOrdinal("c.customer_id"));
                            string firstName = reader.GetString(reader.GetOrdinal("c.first_name"));
                            string lastName = reader.GetString(reader.GetOrdinal("c.last_name"));
                            string email = reader.GetString(reader.GetOrdinal("c.email"));
                            int phoneNumber = reader.GetInt32(reader.GetOrdinal("c.phone_number"));
                            string road = reader.GetString(reader.GetOrdinal("c.address"));
                            int zip_id = reader.GetInt32(reader.GetOrdinal("z.id"));
                            int zip_code = reader.GetInt32(reader.GetOrdinal("z.zip_code"));
                            string country = reader.GetString(reader.GetOrdinal("z.country"));
                            string city = reader.GetString(reader.GetOrdinal("z.city"));

                            // Create the Customer object with the retrieved values
                            customer_List.Add(new Customer(CustomerType.Private, firstName, lastName, email, phoneNumber, new Address(road, new ZIPCode(zip_id, zip_code, country, city) )));
                        }
                    }
                }

                // Incase there is a conflict on .  
                if (customer_List.Count == 0)
                {
                    throw new CustomerNotFoundException();
                }
                else
                {
                    // Returns 
                    return customer_List;
                }
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Violation of unique constraint.");
                throw;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Foreign key constraint violation.");
                throw;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Not null constraint violation.");
                throw;
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



        public List<Customer>? Search_forCustomer_Email(string email_)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Customer> customer_List = new List<Customer>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT 
                c.customer_id, c.first_name, c.last_name, c.email, c.phone_number, c.address, 
                z.id, z.zip_code, z.country, z.city, c.customer_type, o.customer_id
                FROM customer c
                JOIN zip z ON c.zip_ID = z.id
                JOIN orders o ON o.customer_id = c.customer_id
                WHERE c.email = @email_value", conn))
                {

                    cmd.Parameters.AddWithValue("email_value", email_);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int customer_id = reader.GetInt32(reader.GetOrdinal("c.customer_id"));
                            string firstName = reader.GetString(reader.GetOrdinal("c.first_name"));
                            string lastName = reader.GetString(reader.GetOrdinal("c.last_name"));
                            string email = reader.GetString(reader.GetOrdinal("c.email"));
                            int phoneNumber = reader.GetInt32(reader.GetOrdinal("c.phone_number"));
                            string road = reader.GetString(reader.GetOrdinal("c.address"));
                            int zip_id = reader.GetInt32(reader.GetOrdinal("z.id"));
                            int zip_code = reader.GetInt32(reader.GetOrdinal("z.zip_code"));
                            string country = reader.GetString(reader.GetOrdinal("z.country"));
                            string city = reader.GetString(reader.GetOrdinal("z.city"));

                            // Create the Customer object with the retrieved values
                            customer_List.Add(new Customer(CustomerType.Private, firstName, lastName, email, phoneNumber, new Address(road, new ZIPCode(zip_id, zip_code, country, city) )));
                        }
                    }
                }

                // Incase there is a conflict on .  
                if (customer_List.Count == 0)
                {
                    throw new CustomerNotFoundException();
                }
                else
                {
                    // Returns 
                    return customer_List;
                }
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Violation of unique constraint.");
                throw;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Foreign key constraint violation.");
                throw;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Not null constraint violation.");
                throw;
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



        public List<Customer>? Search_forCustomer_Name(string first_Name, string last_Name)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Customer> customer_List = new List<Customer>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT 
                c.customer_id, c.first_name, c.last_name, c.email, c.phone_number, c.address, 
                z.id, z.zip_code, z.country, z.city, c.customer_type, o.customer_id
                FROM customer c
                JOIN zip z ON c.zip_ID = z.id
                JOIN orders o ON o.customer_id = c.customer_id
                WHERE c.first_name = @first_name_value AND c.last_name = @last_name_value", conn))
                {

                    cmd.Parameters.AddWithValue("first_name_value", first_Name);
                    cmd.Parameters.AddWithValue("last_name_value", last_Name);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int customer_id = reader.GetInt32(reader.GetOrdinal("c.customer_id"));
                            string firstName = reader.GetString(reader.GetOrdinal("c.first_name"));
                            string lastName = reader.GetString(reader.GetOrdinal("c.last_name"));
                            string email = reader.GetString(reader.GetOrdinal("c.email"));
                            int phoneNumber = reader.GetInt32(reader.GetOrdinal("c.phone_number"));
                            string road = reader.GetString(reader.GetOrdinal("c.address"));
                            int zip_id = reader.GetInt32(reader.GetOrdinal("z.id"));
                            int zip_code = reader.GetInt32(reader.GetOrdinal("z.zip_code"));
                            string country = reader.GetString(reader.GetOrdinal("z.country"));
                            string city = reader.GetString(reader.GetOrdinal("z.city"));

                            // Create the Customer object with the retrieved values
                            customer_List.Add(new Customer(CustomerType.Private, firstName, lastName, email, phoneNumber, new Address(road, new ZIPCode(zip_id, zip_code, country, city) )));
                        }
                    }
                }

                // Incase there is a conflict on .  
                if (customer_List.Count == 0)
                {
                    throw new CustomerNotFoundException();
                }
                else
                {
                    // Returns 
                    return customer_List;
                }
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Violation of unique constraint.");
                throw;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Foreign key constraint violation.");
                throw;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Not null constraint violation.");
                throw;
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }










        // ######################### DELETE ######################### //


        public int Delete_Customer_Database(int customer_ID)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                DELETE FROM customer
                WHERE customer_id = @customer_id_value", conn))
                {

                    // Add the parameter to the SQL command
                    cmd.Parameters.AddWithValue("customer_id_value", customer_ID);


                    int rows_Deleted = cmd.ExecuteNonQuery();

                    // Incase there is a conflict on .  
                    if (rows_Deleted <= 0)
                    {
                        throw new CustomerNotFoundException(customer_ID);
                    }
                    else
                    {
                        // Returns the ID used to put the 
                        return customer_ID;
                    }
                }
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Violation of unique constraint.");
                return -23505;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Foreign key constraint violation.");
                return -23503;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Not null constraint violation.");
                return -23502;
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return -100;
            }
        }


        public int Delete_ZIPCode_Database(int zip_ID)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                DELETE FROM zip
                WHERE id = @zip_id_value", conn))
                {

                    // Add the parameter to the SQL command
                    cmd.Parameters.AddWithValue("zip_id_value", zip_ID);


                    int rows_Deleted = cmd.ExecuteNonQuery();

                    // Incase there is a conflict on .  
                    if (rows_Deleted <= 0)
                    {
                        throw new ZIPCodeNotFoundException(zip_ID);
                    }
                    else
                    {
                        // Returns the ID used to put the 
                        return zip_ID;
                    }
                }
            }
            catch (PostgresException ex) when (ex.SqlState == "23505") // Unique violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Violation of unique constraint.");
                return -23505;
            }
            catch (PostgresException ex) when (ex.SqlState == "23503") // Foreign key violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Foreign key constraint violation.");
                return -23503;
            }
            catch (PostgresException ex) when (ex.SqlState == "23502") // Not null violation
            {
                Console.WriteLine("Error Code " + ex.SqlState + ": " + ex.Message);
                Console.WriteLine("Error: Not null constraint violation.");
                return -23502;
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return -100;
            }
        }





    }













    public class CustomerNotFoundException : Exception
    {
        public int? Customer_ID { get; }

        public CustomerNotFoundException() : base($"Customer was not found.")
        {
        }

        public CustomerNotFoundException(int Customer_ID) : base($"Customer with ID {Customer_ID} was not found.")
        {
            this.Customer_ID = Customer_ID;
        }
    }





    public class ZIPCodeNotFoundException : Exception
    {
        public int? ZIP_ID { get; }

        public ZIPCodeNotFoundException() : base($"ZIP Code was not found.")
        {
        }

        public ZIPCodeNotFoundException(int ZIP_ID) : base($"ZIP Code with ID {ZIP_ID} was not found.")
        {
            this.ZIP_ID = ZIP_ID;
        }
    }




}
