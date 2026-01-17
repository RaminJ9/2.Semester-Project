using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DataTypes;

namespace OMS.Access
{
    public class Customer_Handler
    {
        
        private readonly string connString;



        public Customer_Handler(string connString)
        {
            this.connString = connString;
        }


        public int Add_Customer_toDatabase (Customer new_customer)
        {

            // calls all the insert method to create an new order in the database. 
            int temp_customer_id, temp_zip_id;

            // Step 1. insert ZIP.
            temp_zip_id = InsertZIP(new_customer.address.country, new_customer.address.city, new_customer.address.zip);

            // Step 2. insert customer.
            temp_customer_id = InsertCustomer(new_customer.first_name, new_customer.last_name, new_customer.email, new_customer.phone_number, new_customer.address.road, temp_zip_id);

            return temp_customer_id;

        }




        public int InsertZIP(string country, string city, int zip_code)
        {
            // Create and connect to the database, using the "connString". 
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            int zip_id;

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
                    zip_id = (int)result;
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
                    zip_id = (int)fetchCmd.ExecuteScalar();
                }
            }

            return zip_id;
        }




        private int InsertCustomer(string first_name, string last_name, string email, int phone_number, string address, int zip_id)
        {
            // Create and connect to the database, using the "connString". 
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            // Creates the Quary to be send/executed by the database. 
            // "RETURNING order_id" returns the ID.
            using var cmd = new NpgsqlCommand(@"
            INSERT INTO customer (first_name, last_name, email, phone_number, address, zip_id) 
            VALUES (@first_name, @last_name, @email, @phone_number, @address, @zip_id)
            RETURNING customer_id", conn);

            // Fills up the quary with the data needed. 
            cmd.Parameters.AddWithValue("first_name", first_name);
            cmd.Parameters.AddWithValue("last_name", last_name);
            cmd.Parameters.AddWithValue("email", email);
            cmd.Parameters.AddWithValue("phone_number", phone_number);
            cmd.Parameters.AddWithValue("address", address);
            cmd.Parameters.AddWithValue("zip_id", zip_id);

            // Sends/executer the quary and returns an int value (the ID). 
            return (int)cmd.ExecuteScalar();

            //conn.Close();
        }

        // ######################### FETCHERS ######################### //

        public (int zipCode, string country, string city)? GetZIPByid(int zip_id)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
        SELECT zip_code, country, city 
        FROM zip 
        WHERE id = @zip_id", conn);

            cmd.Parameters.AddWithValue("zip_id", zip_id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int zip_code = reader.GetInt32(0);
                string country = reader.GetString(1);
                string city = reader.GetString(2);
                return (zip_code, country, city);
            }

            return null;
        }

        public Customer? GetCustomerById(int customer_id)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();
            // .c refers to the customer table, and .z refers to the zip table.
            using var cmd = new NpgsqlCommand(@"
        SELECT 
            c.customer_id, c.first_name, c.last_name, c.email, c.phone_number, c.address, 
            z.zip_code, z.country, z.city, c.customer_type
        FROM customer c
        JOIN zip z ON c.zip_id = z.id
        WHERE c.customer_id = @customer_id", conn);

            cmd.Parameters.AddWithValue("customer_id", customer_id);

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

                // Parse customer_type enum from string
                if (!Enum.TryParse<CustomerType>(customerTypeStr, out var customerType))
                {
                    // Fallback if parsing fails
                    customerType = CustomerType.Private;
                }

                // Create the Customer object with the retrieved values
                return new Customer(
                    customerType,
                    firstName,
                    lastName,
                    email,
                    phoneNumber,
                    road,
                    zip,
                    country,
                    city
                );
            }

            return null;
        }

    }
}
