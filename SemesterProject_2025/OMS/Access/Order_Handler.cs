using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using System.Diagnostics;
using OMS.DataTypes;

namespace OMS.Access
{
    public class Order_Handler
    {
        private Customer_Handler customer_handler;

        private readonly string connString;



        public Order_Handler(string connString, ref Customer_Handler customer_handler)
        {
            this.customer_handler = customer_handler;
        }




        public int Add_Order_toDatabase(Order new_order)
        {
            // calls all the insert method to create an new order in the database. 
            int temp_order_ID;

            // Step 1. insert ZIP.
            // Step 2. insert customer.
            customer_handler.Add_Customer_toDatabase(new_order.Get_customer_info());

            // Step 3. insert shipping options. Insert additional delervery company
            InsertShipping(new_order.Get_Order_ID(), "1234", 0);

            DateTime temp_shipping_date = new DateTime();
            // Step 4. insert order.
            temp_order_ID = InsertOrder((new_order.Get_order_status()).ToString(), temp_shipping_date, (decimal)new_order.Get_price_Total(), (new_order.Get_shipping_Address()).road, 123, 122, new_order.Get_Customer_ID(), (int)new_order.Get_vat_Total());

            // Step 5. insert product.
            foreach (Item item in new_order.Get_item_List())
            {
                InsertItemList(new_order.Get_Order_ID(), item.product_ID, item.quantity, (decimal)item.price_per_unit);
            }
            return temp_order_ID;

        }



        private int InsertOrder(string status, DateTime created_on, decimal total, string shipping_address, decimal delivery_fee, int shipping_option_id, int customer_id, int vat_total)
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

            // Sends/executer the quary and returns an int value (the ID). 
            return (int)cmd.ExecuteScalar();

            //conn.Close();
        }


        public int InsertProduct(string product_name, decimal price, int stock_quantity)
        {
            // Create and connect to the database, using the "connString". 
            const string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            // Creates the Quary to be send/executed by the database. 
            // "RETURNING order_id" returns the ID.
            using var cmd = new NpgsqlCommand(@"
            INSERT INTO product (name, price, stock_quantity) 
            VALUES (@name, @price, @stock_quantity) 
            RETURNING product_id", conn);

            // Fills up the quary with the data needed. 
            cmd.Parameters.AddWithValue("name", product_name);
            cmd.Parameters.AddWithValue("price", price);
            cmd.Parameters.AddWithValue("stock_quantity", stock_quantity);

            // Sends/executer the quary and returns an int value (the ID). 
            return (int)cmd.ExecuteScalar();

            //conn.Close();
        }

        public int InsertItemList(int order_id, int product_id, int quantity, decimal price_per_unit)
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
            cmd.Parameters.AddWithValue("order_id", order_id);
            cmd.Parameters.AddWithValue("product_id", product_id);
            cmd.Parameters.AddWithValue("quantity", quantity);
            cmd.Parameters.AddWithValue("price_per_unit", price_per_unit);

            // Sends/executer the quary and returns an int value (the ID). 
            return (int)cmd.ExecuteScalar();

        }

        // Create a forign key refrencing the company doing the delivery, and remove the status
        private int InsertShipping(int order_id, string tracking_number, int shipping_options_id)
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

            //conn.Close();
        }


        public int InsertShippingOption(string delivery_option, decimal delivery_price, string delivery_company)
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
            cmd.Parameters.AddWithValue("price", delivery_price);
            cmd.Parameters.AddWithValue("delivery_company", delivery_company);

            // Sends/executer the quary and returns an int value (the ID). 
            return (int)cmd.ExecuteScalar();

            //conn.Close();
        }

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
            var tracking = reader.GetString(reader.GetOrdinal("tracking_number"));
            var delivery_option = reader.GetInt32(reader.GetOrdinal("delivery_option"));
            var price = reader.GetDecimal(reader.GetOrdinal("price"));
            var company = reader.GetString(reader.GetOrdinal("delivery_company"));

            var Option = new Shipping_Option(order_id, (Delivery_option) delivery_option, price, company); 
            return new Shipping_Option((Delivery_option)delivery_option, shipping_price tracking_number); 
        }
        public Order GetOrderById(int order_id)
        {
            using var conn = new NpgsqlConnection(connString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
        SELECT order_id, customer_id, shipping_option_id, shipping_address, status, total, vat_total
        FROM orders
        WHERE order_id = @order_id", conn);

            cmd.Parameters.AddWithValue("order_id", order_id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                throw new Exception($"Order with ID {order_id} not found.");

            int customer_id = reader.GetInt32(reader.GetOrdinal("customer_id"));
            string status = reader.GetString(reader.GetOrdinal("status"));
            string shippingAddress = reader.GetString(reader.GetOrdinal("shipping_address"));
            double total = (double)reader.GetDecimal(reader.GetOrdinal("total"));
            double vatTotal = (double)reader.GetDecimal(reader.GetOrdinal("vat_total"));

            // Fetch related entities
            Customer customer = customer_handler.GetCustomerById(customer_id);
            List<Item> items = GetItemsByOrderId(order_id);
            Shipping shipping = GetShippingByOrderId(order_id);

            // Instantiate the Order (adapt constructor as needed)
            var order = new Order(order_id, customerId, shipping.GetId(), customer, shipping, items);
            order.Set_shipping_Address(new Address(shippingAddress));
            order.Set_flat_discount(0); 
            order.Set_procent_discount(1);
            order.Set_order_status(Enum.Parse<OrderStatus>(status, true));

            return order;
        }

    }
}
