using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using System.Diagnostics;
using OMS.DataTypes;
using System.ComponentModel.DataAnnotations;
using NuGet.Packaging.Signing;
using System.Data;
using OMS.Access.Access_Interfaces;
using Tmds.DBus.Protocol;

// Creater: Rasmus R

namespace OMS.Access
{
    public class Order_Handler : IOrder_Handler, IItemList_Handler, IShippingOptions_Handler
    {
        private Customer_Handler customer_handler;

        private readonly string connString;



        public Order_Handler(string connString, ref Customer_Handler customer_handler)
        {
            this.customer_handler = customer_handler;
        }








        // ######################### CREATE ######################### //




        public int Create_Order_Database(Order new_Order)
        {
            // calls all the insert method to create an new order in the database. 
            int temp_order_ID;

            // Step 1. insert ZIPCode.
            // Step 2. insert customer.
            customer_handler.Create_Customer_Database(new_Order.Get_customer_info());

            // Step 3. insert shipping options. Insert additional delervery company
            InsertShipping(new_Order.Get_Order_ID(), "1234", 0);

            // Step 4. insert order.
            DateTime temp_shipping_date = new DateTime();
            temp_order_ID = InsertOrder((new_Order.Get_order_status()).ToString(), temp_shipping_date, (decimal)new_Order.Get_price_Total(), (new_Order.Get_shipping_Address()).road, 123, 122, new_Order.Get_Customer_ID(), (int)new_Order.Get_vat_Total());

            // Step 5. insert product.
            int temp_result;
            foreach (Item item in new_Order.Get_item_List())
            {
                temp_result = Create_ItemList_Database(new_Order.Get_Order_ID(), new_Order.Get_item_List());
                if (temp_result < 0)
                {
                    return temp_result;
                }
            }

            // .  
            return temp_order_ID;

        }



        public int Create_ItemList_Database(Order new_Order)
        {
            return Create_ItemList_Database(new_Order.Get_Order_ID(), new_Order.Get_item_List());
        }



        private int InsertOrder(string status, DateTime created_on, decimal total, string shipping_address, decimal delivery_fee, int shipping_option_id, int customer_id, int vat_total)
        {
            try
            {
                // Create and connect to the database, using the "connString". 
                const string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // Creates the Quary to be send/executed by the database. 
                // "RETURNING order_id" returns the ID.
                using var cmd = new NpgsqlCommand(@"
                INSERT INTO orders (shipping_option_id, delivery_fee, status, created_on, total, shipping_address, customer_id, vat_total)
                VALUES (@shipping_option_id, @delivery_fee, @status, @created_on, @total, @shipping_address, @customer_id, @vat_total)
                RETURNING order_id", conn);

                // Fills up the quary with the data needed. 
                cmd.Parameters.AddWithValue("shipping_option_id", shipping_option_id);
                cmd.Parameters.AddWithValue("delivery_fee", delivery_fee);
                cmd.Parameters.AddWithValue("status", status);
                cmd.Parameters.AddWithValue("created_on", created_on);
                cmd.Parameters.AddWithValue("total", total);
                cmd.Parameters.AddWithValue("shipping_address", shipping_address);
                cmd.Parameters.AddWithValue("customer_id", customer_id);
                cmd.Parameters.AddWithValue("vat_total", vat_total);

                // Sends/executer the quary and returns the ID value
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


        public int Create_ItemList_Database(int order_ID, List<Item> new_Item_List)
        {
            int temp_result;

            // .  
            foreach (Item item in new_Item_List)
            {
                temp_result = Create_Item_Database(order_ID, item);

                if (temp_result < 0)
                {
                    return temp_result;
                }
            }

            // . 
            return order_ID;
        }


        public int Create_Item_Database(int order_ID, Item new_Item)
        {
            try
            {
                // Create and connect to the database, using the "connString". 
                const string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // Creates the Quary to be send/executed by the database. 
                // "RETURNING order_id" returns the ID.
                using var cmd = new NpgsqlCommand(@"
                INSERT INTO item_list (order_id, product_id, quantity, price_per_unit) 
                VALUES (@order_id, @product_id, @quantity, @price_per_unit) 
                RETURNING item_id", conn);

                // Fills up the quary with the data needed. 
                cmd.Parameters.AddWithValue("order_id", order_ID);
                cmd.Parameters.AddWithValue("product_id", new_Item.product_ID);
                cmd.Parameters.AddWithValue("quantity", new_Item.quantity);
                cmd.Parameters.AddWithValue("price_per_unit", new_Item.price_per_unit);

                // Sends/executer the quary and returns an int value (the ID). 
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



        // Create a forign key refrencing the company doing the delivery, and remove the status
        private int InsertShipping(int order_id, string tracking_number, int shipping_options_id)
        { try
            {
                // Create and connect to the database, using the "connString". 
                const string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // Creates the Quary to be send/executed by the database. 
                // "RETURNING order_id" returns the ID.
                using var cmd = new NpgsqlCommand(@"
                INSERT INTO shipping (order_id, tracking_number, shipping_options_id) 
                VALUES (@order_id, @tracking_number, @shipping_options_id) 
                ON CONFLICT (tracking_number) DO NOTHING
                RETURNING shipping_id", conn);

                // Fills up the quary with the data needed. 
                cmd.Parameters.AddWithValue("order_id", order_id);
                cmd.Parameters.AddWithValue("tracking_number", tracking_number);
                cmd.Parameters.AddWithValue("shipping_options_id", shipping_options_id);

                // Sends/executer the quary and returns an int value (the ID). 
                var result = cmd.ExecuteScalar();

                // Check if still works?
                if (result != null)
                {
                    return (int)result;
                }

                // Creates the Quary to be send/executed by the database. 
                using var fetchCmd = new NpgsqlCommand("SELECT shipping_id FROM shipping WHERE tracking_number = @tracking_number", conn);
                // Fills up the quary with the data needed. 
                fetchCmd.Parameters.AddWithValue("tracking_number", tracking_number);

                // Sends/executer the quary and returns an int value (the ID). 
                return (int)fetchCmd.ExecuteScalar();
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


        public int Create_ShippingOption_Database(string delivery_option, double shipping_price, string company_name)
        {
            try
            {
                // Create and connect to the database, using the "connString". 
                const string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // Creates the Quary to be send/executed by the database. 
                // "RETURNING order_id" returns the ID.
                using var cmd = new NpgsqlCommand(@"
                INSERT INTO shipping_options (delivery_option, price, delivery_company) 
                VALUES (@delivery_option, @price, @delivery_company) RETURNING id", conn);

                // Fills up the quary with the data needed. 
                cmd.Parameters.AddWithValue("delivery_option", delivery_option);
                cmd.Parameters.AddWithValue("price", shipping_price);
                cmd.Parameters.AddWithValue("delivery_company", company_name);

                // Sends/executer the quary and returns an int value (the ID). 
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




        public int Create_ShippingOption_Database(Shipping_Option new_ShippingOption)
        {
            try
            {
                // Create and connect to the database, using the "connString". 
                const string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // Creates the Quary to be send/executed by the database. 
                // "RETURNING order_id" returns the ID.
                using var cmd = new NpgsqlCommand(@"
                INSERT INTO shipping_options (delivery_option, price, delivery_company) 
                VALUES (@delivery_option, @price, @delivery_company) RETURNING id", conn);

                // Fills up the quary with the data needed. 
                cmd.Parameters.AddWithValue("delivery_option", new_ShippingOption.delivery_option);
                cmd.Parameters.AddWithValue("price", new_ShippingOption.shipping_price);
                cmd.Parameters.AddWithValue("delivery_company", new_ShippingOption.company_name);

                // Sends/executer the quary and returns an int value (the ID). 
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












        // ######################### FETCHERS ######################### //



        public int Get_ReturnedItemList(string reason, string return_status, DateTime processed_on, int quantity_returned, int order_id, int item_id)
        {
            // Create and connect to the database, using the "connString". 
            const string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            // Creates the Quary to be send/executed by the database. 
            // "RETURNING order_id" returns the ID.
            using var cmd = new NpgsqlCommand(@"
            INSERT INTO returned_item_list (order_id, item_id, reason, return_status, processed_on, quantity_returned) 
            VALUES (@order_id, @item_id, @reason, @return_status, @processed_on, @quantity_returned) 
            RETURNING return_id", conn);

            // Fills up the quary with the data needed. 
            cmd.Parameters.AddWithValue("order_id", order_id);
            cmd.Parameters.AddWithValue("item_id", item_id);
            cmd.Parameters.AddWithValue("reason", reason);
            cmd.Parameters.AddWithValue("return_status", return_status);
            cmd.Parameters.AddWithValue("processed_on", processed_on);
            cmd.Parameters.AddWithValue("quantity_returned", quantity_returned);

            // Sends/executer the quary and returns an int value (the ID).
            return (int)cmd.ExecuteScalar();
        }


        public Shipping GetShippingByOrderId(int order_id)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
            SELECT 
            s.shipping_id, s.tracking_number,
            so.delivery_option, so.price, so.delivery_company
            FROM shipping s
            JOIN shipping_options so ON s.shipping_options_id = so.id
            WHERE s.order_id = @order_id", conn);

            cmd.Parameters.AddWithValue("order_id", order_id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                throw new Exception($"Shipping info for Order ID {order_id} not found.");

            var shipping_id = reader.GetInt32(reader.GetOrdinal("shipping_id"));
            var shipping_option_id = reader.GetInt32(reader.GetOrdinal("shipping_options_id"));
            var tracking_number = reader.GetString(reader.GetOrdinal("tracking_number"));
            var delivery_option = reader.GetString(reader.GetOrdinal("delivery_option"));
            var price = reader.GetDecimal(reader.GetOrdinal("price"));
            var company = reader.GetString(reader.GetOrdinal("delivery_company"));

            Shipping_Option shipping_Option = new Shipping_Option(shipping_option_id, (Delivery_option)Enum.Parse(typeof(Delivery_option), delivery_option), (double)price, company);
            Shipping shipping = new Shipping(shipping_id, shipping_Option, tracking_number);
            return shipping;
        }

        public Order? Fetch_Order_Database(int order_id)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
            SELECT order_id, customer_id, created_on, shipping_option_id, shipping_address, status, total, vat_total
            FROM orders
            WHERE order_id = @order_id", conn);

            cmd.Parameters.AddWithValue("order_id", order_id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                throw new Exception($"Order with ID {order_id} not found.");

            int customer_id = reader.GetInt32(reader.GetOrdinal("customer_id"));
            DateTime created_on = reader.GetDateTime("created_on");
            string status = reader.GetString(reader.GetOrdinal("status"));
            string shippingAddress = reader.GetString(reader.GetOrdinal("shipping_address"));
            double total = (double)reader.GetDecimal(reader.GetOrdinal("total"));
            double vatTotal = (double)reader.GetDecimal(reader.GetOrdinal("vat_total"));

            // Fetch related entities
            Customer customer = customer_handler.Fetch_Customer_Database(customer_id);
            List<Item> itemList = Fetch_ItemList_Database(order_id);
            Shipping shipping = GetShippingByOrderId(order_id);

            var order = new Order(order_id, created_on, customer, shipping, itemList);
            order.Set_flat_discount(0);
            order.Set_procent_discount(1);
            order.Set_order_status(Enum.Parse<OrderStatus>(status, true));

            return order;
        }



        public List<Order>? Fetch_CustomerOrders_Database(int customer_ID)
        {
            try
            {
                // .  
                List<Order> customer_orders = new List<Order>();
                List<int> customer_orderIDs = new List<int>();

                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // .  
                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT order_id
                FROM orders
                WHERE customer_id = @customer_id_value", conn))
                {
                    // Add the parameter to prevent SQL injection
                    cmd.Parameters.AddWithValue("customer_id_value", customer_ID);

                    // .   
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Returns true if there's a row to read  
                        {
                            int productID = reader.GetInt32(reader.GetOrdinal("order_id"));
                            customer_orderIDs.Add(productID);
                        }
                    }
                }

                // .  
                foreach (int order_ID in customer_orderIDs)
                {
                    customer_orders.Add(Fetch_Order_Database(order_ID));
                }

                // .  
                return customer_orders;
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
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }




        public Item? Fetch_Item_Database(int item_ID)
        {
            Item item;

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
            SELECT 
            i.item_id, i.order_id, i.product_id, i.quantity, i.price_per_unit, p.name
            FROM item_list i
            JOIN product p ON i.product_id = p.product_id
            WHERE i.item_id = @item_id_value", conn);
            cmd.Parameters.AddWithValue("order_id_value", item_ID);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int item_id = reader.GetInt32(reader.GetOrdinal("item_id"));
                string item_name = reader.GetString(reader.GetOrdinal("p.name"));
                int product_id = reader.GetInt32(reader.GetOrdinal("product_id"));
                int quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                decimal price_per_unit = reader.GetDecimal(reader.GetOrdinal("price_per_unit"));

                item = new Item(product_id, item_name, (double)price_per_unit, quantity);

                return item;
            }
            else
            {
                return null;
            }
        }



        public List<Item>? Fetch_ItemList_Database(int order_ID)
        {
            List<Item> items = new List<Item>();

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
            SELECT 
            i.item_id, i.order_id, i.product_id, i.quantity, i.price_per_unit, p.name
            FROM item_list i
            JOIN product p ON i.product_id = p.product_id
            WHERE i.order_id = @order_id", conn);
            cmd.Parameters.AddWithValue("order_id", order_ID);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int item_id = reader.GetInt32(reader.GetOrdinal("item_id"));
                string item_name = reader.GetString(reader.GetOrdinal("p.name"));
                int product_id = reader.GetInt32(reader.GetOrdinal("product_id"));
                int quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                decimal price_per_unit = reader.GetDecimal(reader.GetOrdinal("price_per_unit"));

                var new_item = new Item(item_id, product_id, item_name, (double)price_per_unit, quantity);
                items.Add(new_item);
            }

            return items;
        }



        public List<Item>? Fetch_All_Items_Database()
        {
            List<Item> items = new List<Item>();

            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
            SELECT 
            i.item_id, i.order_id, i.product_id, i.quantity, i.price_per_unit, p.name
            FROM item_list i
            JOIN product p ON i.product_id = p.product_id", conn);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int item_id = reader.GetInt32(reader.GetOrdinal("item_id"));
                string item_name = reader.GetString(reader.GetOrdinal("p.name"));
                int product_id = reader.GetInt32(reader.GetOrdinal("product_id"));
                int quantity = reader.GetInt32(reader.GetOrdinal("quantity"));
                decimal price_per_unit = reader.GetDecimal(reader.GetOrdinal("price_per_unit"));

                var new_item = new Item(product_id, item_name, (double)price_per_unit, quantity);
                items.Add(new_item);
            }

            return items;
        }


        public Shipping_Option? Fetch_ShippingOption_Database(int shippingOption_ID)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT id, delivery_option, price, delivery_company
                FROM shipping_options
                WHERE id = @id_value", conn))
                {
                    // Add the parameter to prevent SQL injection
                    cmd.Parameters.AddWithValue("id_value", shippingOption_ID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Returns true if there's a row to read  
                        {
                            int Id = reader.GetInt32(reader.GetOrdinal("id"));  
                            string delivery_option = reader.GetString(reader.GetOrdinal("delivery_option"));
                            double price = reader.GetDouble(reader.GetOrdinal("price"));
                            string delivery_company = reader.GetString(reader.GetOrdinal("delivery_company"));

                            // Returns Product.  
                            Shipping_Option shipping_Option = new Shipping_Option(shippingOption_ID, Enum.Parse<Delivery_option>(delivery_option, true), price, delivery_company);
                            return shipping_Option;
                        }
                        else
                        {
                            throw new ShippingOptionNotFoundException(shippingOption_ID);
                        }
                    }
                }

                return null;
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
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public List<Shipping_Option>? Fetch_All_ShippingOptions_Database()
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                List<Shipping_Option> shipping_Options = new List<Shipping_Option>();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT id, delivery_option, price, delivery_company
                FROM shipping_options", conn))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Returns true if there's a row to read  
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            string delivery_option = reader.GetString(reader.GetOrdinal("delivery_option"));
                            double price = reader.GetDouble(reader.GetOrdinal("price"));
                            string delivery_company = reader.GetString(reader.GetOrdinal("delivery_company"));

                            // Returns Product.  
                            shipping_Options.Add( new Shipping_Option(id, Enum.Parse<Delivery_option>(delivery_option, true), price, delivery_company) );
                        }



                        if (shipping_Options.Count > 0)
                        {
                            return shipping_Options;
                        }
                        else
                        {
                            throw new ShippingOptionNotFoundException();
                        }
                    }
                }

                return null;
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
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }














        // ######################### UPDATE ######################### //



        public int Update_Order_Database(int order_ID, Order updated_Order)
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
                UPDATE orders
                SET customer_id = @customer_id_value, 
                    status = @status_value, 
                    total = @total_value_, 
                    vat_total = @vat_total_value,
                    shipping_option_id = @shipping_option_id_value, 
                    shipping_address = @shipping_address_value 
                WHERE order_id = @order_id_value", conn))
                {
                    // Fills up the quary with the data needed. 
                    cmd.Parameters.AddWithValue("order_id_value", order_ID);
                    cmd.Parameters.AddWithValue("customer_id_value", updated_Order.Get_Customer_ID());
                    cmd.Parameters.AddWithValue("status_value", (updated_Order.Get_order_status()).ToString() );
                    cmd.Parameters.AddWithValue("total_value_:", updated_Order.Get_price_Total());
                    cmd.Parameters.AddWithValue("vat_total_value", updated_Order.Get_vat_Total());
                    cmd.Parameters.AddWithValue("shipping_option_id_value", (updated_Order.Get_shipping_info()).shipping_option.Get_ID());
                    cmd.Parameters.AddWithValue("shipping_address_value", (updated_Order.Get_shipping_info()).shipping_option.Get_ID());

                    // Sends/executer the quary and returns an int value (the ID). 
                    result = cmd.ExecuteNonQuery();
                }

                // Incase there is a conflict on .  
                if (result == null)
                {
                    throw new ProductNotFoundException(order_ID);
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
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return -100;
            }
        }



        public int Update_Item_Database(int item_ID, int order_ID, Item updated_Item)
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
                UPDATE item_list
                SET order_id = @order_id_value, 
                    product_id = @product_id_value, 
                    quantity = @quantity_value, 
                    price_per_unit = @price_per_unit_value
                    procent_discount = @procent_discount_value, 
                    flat_discount = @flat_discount_value, 
                    total_price = @total_price_value
                WHERE item_id = @item_id_value", conn))
                {
                    // Fills up the quary with the data needed. 
                    cmd.Parameters.AddWithValue("item_id_value", item_ID);
                    cmd.Parameters.AddWithValue("order_id_value", order_ID);
                    cmd.Parameters.AddWithValue("product_id_value", updated_Item.product_ID);
                    cmd.Parameters.AddWithValue("quantity_value", updated_Item.quantity);
                    cmd.Parameters.AddWithValue("price_per_unit_value:", updated_Item.price_per_unit);
                    cmd.Parameters.AddWithValue("procent_discount_value", updated_Item.procent_discount);
                    cmd.Parameters.AddWithValue("flat_discount_value", updated_Item.flat_discount);
                    cmd.Parameters.AddWithValue("total_price_value", updated_Item.total_net_price);

                    // Sends/executer the quary and returns an int value (the ID). 
                    result = cmd.ExecuteNonQuery();
                }

                // Incase there is a conflict on .  
                if (result == null)
                {
                    throw new ProductNotFoundException(order_ID);
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
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return -100;
            }
        }


        public int Update_ShippingOption_Database(int shippingOption_ID, Shipping_Option updated_ShippingOption)
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
                UPDATE item_list
                SET delivery_option = @delivery_option_value, 
                    price = @price_value, 
                    delivery_company = @delivery_company_value
                WHERE id = @id_value", conn))
                {
                    // Fills up the quary with the data needed. 
                    cmd.Parameters.AddWithValue("id_value", shippingOption_ID);
                    cmd.Parameters.AddWithValue("delivery_option_value", (updated_ShippingOption.delivery_option).ToString() );
                    cmd.Parameters.AddWithValue("price_value", updated_ShippingOption.shipping_price);
                    cmd.Parameters.AddWithValue("delivery_company_value", updated_ShippingOption.company_name);

                    // Sends/executer the quary and returns an int value (the ID). 
                    result = cmd.ExecuteNonQuery();
                }

                // Incase there is a conflict on .  
                if (result == null)
                {
                    throw new ShippingOptionNotFoundException(shippingOption_ID);
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
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return -100;
            }
        }












        // ######################### SEARCH ######################### //



        public List<Order>? Search_forOrder_CustomerID(int customer_ID)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Order> order_List = new List<Order>();
                List<int> orderID_List = new List<int>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT 
                c.customer_id, o.customer_id, o.order_id
                FROM customer c
                JOIN orders o ON o.customer_id = c.customer_id
                WHERE c.customer_id = @customer_id_value", conn))
                {

                    cmd.Parameters.AddWithValue("customer_id_value", customer_ID);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int order_id = reader.GetInt32(reader.GetOrdinal("o.order_id"));

                            // Create the Customer object with the retrieved values
                            orderID_List.Add(order_id);
                        }
                    }
                }

                // .   
                conn.Close();

                // .  
                foreach (int orderID in orderID_List)
                {
                    order_List.Add( Fetch_Order_Database(orderID) );
                }

                // Incase there is a conflict on .  
                if (order_List.Count == 0)
                {
                    throw new OrderNotFoundException();
                }
                else
                {
                    // Returns 
                    return order_List;
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




        public List<Order>? Search_forOrder_ProductID(int product_ID)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Order> order_List = new List<Order>();
                List<int> orderID_List = new List<int>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT 
                p.product_id, i.product_id, i.item_id, i.order_id, o.order_id, c.customer_id
                FROM product p
                JOIN item_list i ON p.product_id = p.product_id
                JOIN orders o ON i.order_id = o.order_id
                WHERE p.product_id = @product_id_value", conn))
                {

                    cmd.Parameters.AddWithValue("product_id_value", product_ID);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int order_id = reader.GetInt32(reader.GetOrdinal("o.order_id"));

                            // Create the Customer object with the retrieved values
                            orderID_List.Add(order_id);
                        }
                    }
                }

                // .   
                conn.Close();

                // .  
                foreach (int orderID in orderID_List)
                {
                    order_List.Add(Fetch_Order_Database(orderID));
                }

                // Incase there is a conflict on .  
                if (order_List.Count == 0)
                {
                    throw new OrderNotFoundException();
                }
                else
                {
                    // Returns 
                    return order_List;
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




        public List<Order>? Search_forOrder_Email(string email)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Order> order_List = new List<Order>();
                List<int> orderID_List = new List<int>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT 
                c.customer_id, o.customer_id, o.order_id
                FROM customer c
                JOIN orders o ON o.customer_id = c.customer_id
                WHERE c.email = @email_value", conn))
                {

                    cmd.Parameters.AddWithValue("email_value", email);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int order_id = reader.GetInt32(reader.GetOrdinal("o.order_id"));

                            // Create the Customer object with the retrieved values
                            orderID_List.Add(order_id);
                        }
                    }
                }

                // .   
                conn.Close();

                // .  
                foreach (int orderID in orderID_List)
                {
                    order_List.Add(Fetch_Order_Database(orderID));
                }

                // Incase there is a conflict on .  
                if (order_List.Count == 0)
                {
                    throw new OrderNotFoundException();
                }
                else
                {
                    // Returns 
                    return order_List;
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




        public List<Order>? Search_forOrder_Date(DateTime created_on)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Order> order_List = new List<Order>();
                List<int> orderID_List = new List<int>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT o.order_id
                FROM order o
                WHERE o.created_on = @created_on_value", conn))
                {

                    cmd.Parameters.AddWithValue("created_on_value", created_on);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int order_id = reader.GetInt32(reader.GetOrdinal("o.order_id"));

                            // Create the Customer object with the retrieved values
                            orderID_List.Add(order_id);
                        }
                    }
                }

                // .   
                conn.Close();

                // .  
                foreach (int orderID in orderID_List)
                {
                    order_List.Add(Fetch_Order_Database(orderID));
                }

                // Incase there is a conflict on .  
                if (order_List.Count == 0)
                {
                    throw new OrderNotFoundException();
                }
                else
                {
                    // Returns 
                    return order_List;
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





        public List<Item>? Search_forItem_OrderID(int order_ID)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Item> item_List = new List<Item>();
                List<int> itemID_List = new List<int>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT i.item_id, 
                    i.order_id
                FROM item_list i
                WHERE i.order_id = @order_id_value", conn))
                {

                    cmd.Parameters.AddWithValue("order_id_value", order_ID);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int item_id = reader.GetInt32(reader.GetOrdinal("i.item_id"));

                            // Create the Customer object with the retrieved values
                            itemID_List.Add(item_id);
                        }
                    }
                }

                // .   
                conn.Close();

                // .  
                foreach (int itemID in itemID_List)
                {
                    item_List.Add(Fetch_Item_Database(itemID));
                }

                // Incase there is a conflict on .  
                if (item_List.Count == 0)
                {
                    throw new OrderNotFoundException();
                }
                else
                {
                    // Returns 
                    return item_List;
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




        public List<Item>? Search_forItem_ProductID(int product_ID)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Item> item_List = new List<Item>();
                List<int> itemID_List = new List<int>();

                // .c refers to the customer table, and .z refers to the zip table.
                using (var cmd = new NpgsqlCommand(@"
                SELECT i.item_id, 
                    i.product_id
                FROM item_list i
                WHERE i.product_id = @product_id_value", conn))
                {

                    cmd.Parameters.AddWithValue("product_id_value", product_ID);

                    using (var reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // Reads the current values from the database, and stores them in the variables. 
                            int item_id = reader.GetInt32(reader.GetOrdinal("i.item_id"));

                            // Create the Customer object with the retrieved values
                            itemID_List.Add(item_id);
                        }
                    }
                }

                // .   
                conn.Close();

                // .  
                foreach (int itemID in itemID_List)
                {
                    item_List.Add(Fetch_Item_Database(itemID));
                }

                // Incase there is a conflict on .  
                if (item_List.Count == 0)
                {
                    throw new OrderNotFoundException();
                }
                else
                {
                    // Returns 
                    return item_List;
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


        
        
        public List<Order>? Search_forOrders_Item_Date(int product_ID, DateTime soldOn_Date, TimeStamp_Duration time_period)
        {
            try
            {
                // .  
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                // . 
                List<Order> unfiltered_order_List = new List<Order>();
                List<Order> order_List = new List<Order>();

                unfiltered_order_List.AddRange( Search_forOrder_ProductID(product_ID) );

                // Setting the start and end date for the 2
                DateTime startDate = soldOn_Date, endDate = soldOn_Date;

                if (time_period == TimeStamp_Duration.Day) { endDate.AddDays(1); }
                else if (time_period == TimeStamp_Duration.Week) { endDate.AddDays(1); }
                else if (time_period == TimeStamp_Duration.Month) { endDate.AddMonths(1); }
                else if (time_period == TimeStamp_Duration.Quarter) { endDate.AddMonths(3); }
                else if (time_period == TimeStamp_Duration.Half_year) { endDate.AddMonths(6); }
                else if (time_period == TimeStamp_Duration.Year) { endDate.AddYears(1); }

                // .  
                foreach (Order temp_order in unfiltered_order_List)
                {
                    if ( (temp_order.Get_CreatedOn_TimeStamp() >= startDate) && (temp_order.Get_CreatedOn_TimeStamp() <= endDate) )
                    {
                        order_List.Add(temp_order);
                    }
                }

                // Incase there is a conflict on .  
                if (order_List.Count == 0)
                {
                    throw new OrderNotFoundException();
                }
                else
                {
                    // Returns 
                    return order_List;
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



        public int Delete_Order_Database(int order_ID)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                DELETE FROM orders
                WHERE order_id = @order_id_value", conn))
                {

                    // Add the parameter to the SQL command
                    cmd.Parameters.AddWithValue("order_id_value", order_ID);


                    int rows_Deleted = cmd.ExecuteNonQuery();

                    // Incase there is a conflict on .  
                    if (rows_Deleted <= 0)
                    {
                        throw new OrderNotFoundException(order_ID);
                    }
                    else
                    {
                        // Returns the ID used to put the 
                        return order_ID;
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




        public int Delete_Item_Database(int item_ID)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                DELETE FROM item_list
                WHERE item_id = @item_id_value", conn))
                {

                    // Add the parameter to the SQL command
                    cmd.Parameters.AddWithValue("item_id_value", item_ID);


                    int rows_Deleted = cmd.ExecuteNonQuery();

                    // Incase there is a conflict on .  
                    if (rows_Deleted <= 0)
                    {
                        throw new OrderNotFoundException(item_ID);
                    }
                    else
                    {
                        // Returns the ID used to put the 
                        return item_ID;
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



        public int Delete_ShippingOption_Database(int shippingOption_ID)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                DELETE FROM shipping_options
                WHERE id = @id_value", conn))
                {

                    // Add the parameter to the SQL command
                    cmd.Parameters.AddWithValue("id_value", shippingOption_ID);


                    int rows_Deleted = cmd.ExecuteNonQuery();

                    // Incase there is a conflict on .  
                    if (rows_Deleted <= 0)
                    {
                        throw new ShippingOptionNotFoundException(shippingOption_ID);
                    }
                    else
                    {
                        // Returns the ID used to put the 
                        return shippingOption_ID;
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












    public class OrderNotFoundException : Exception
    {
        public int? Order_ID { get; }

        public OrderNotFoundException() : base($"Order was not found.")
        {
        }

        public OrderNotFoundException(int Order_ID) : base($"Order with ID {Order_ID} was not found.")
        {
            this.Order_ID = Order_ID;
        }
    }





    public class ShippingOptionNotFoundException : Exception
    {
        public int? shippingOption_ID { get; }

        public ShippingOptionNotFoundException() : base($"Shipping Option was not found.")
        {
        }

        public ShippingOptionNotFoundException(int shippingOption_ID) : base($"Shipping Option with ID {shippingOption_ID} was not found.")
        {
            this.shippingOption_ID = shippingOption_ID;
        }
    }







}
