using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using OMS.Access.Access_Interfaces;
using OMS.DataTypes;
// Creator : Alexander Maach
namespace OMS.Access
{
    public class Stock_Handler : IStock_Handler
    {


        private readonly string connString;

        public Stock_Handler(string connString)
        {
            this.connString = connString;

        }
   
        public int Create_Product_Database(int product_ID, string product_name, int stock_quantity)
        {
            // . 
            return Create_Product_Database( new Product(product_ID, product_name, stock_quantity, stock_quantity) );
        }




        // Under construction. ;) 
        public int Create_Product_Database(Product new_Product)
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
                INSERT INTO product (product_id, name, stock_quantity, avaliable_quantity)
                VALUES (@product_id_value, @name_value, @stock_quantity_value, @avaliable_quantity_value)
                ON CONFLICT (product_id) DO NOTHING
                RETURNING product_id", conn) )
                {
                    // Fills up the quary with the data needed. 
                    cmd.Parameters.AddWithValue("product_id_value", new_Product.Get_Product_ID());
                    cmd.Parameters.AddWithValue("name_value", new_Product.Get_Name());
                    cmd.Parameters.AddWithValue("stock_quantity_value", new_Product.Get_Stock_Quantity());
                    cmd.Parameters.AddWithValue("avaliable_quantity_value", new_Product.Get_Avaliable_Quantity());

                    // Sends/executer the quary and returns an int value (the ID). 
                    result = cmd.ExecuteNonQuery();
                }

                // Incase there is a conflict on .  
                if (result == null)
                {
                    throw new ProductNotFoundException(new_Product.Get_Product_ID());
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







        public Product? Fetch_Product_Database(int product_ID)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT product_id, name, stock_quantity, avaliable_quantity
                FROM product
                WHERE product_id = @product_id_value", conn))
                {
                    // Add the parameter to prevent SQL injection
                    cmd.Parameters.AddWithValue("product_id_value", product_ID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Returns true if there's a row to read  
                        {
                            int productId = reader.GetInt32(reader.GetOrdinal("product_id")); // or reader["product_id"]  
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            int stockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity"));
                            int availableQuantity = reader.GetInt32(reader.GetOrdinal("avaliable_quantity"));

                            // Returns Product.  
                            Product product = new Product(productId, name, availableQuantity, stockQuantity);
                            return product;
                        }
                        else
                        {
                            throw new ProductNotFoundException(product_ID);
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



        public List<Product>? Fetch_All_Products_Database()
        {
            // . 
            List<Product> products = new List<Product>();

            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT product_id, name, stock_quantity, avaliable_quantity
                FROM product", conn))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Returns true if there's a row to read  
                        {
                            int productId = reader.GetInt32(reader.GetOrdinal("product_id")); // or reader["product_id"]  
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            int stockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity"));
                            int availableQuantity = reader.GetInt32(reader.GetOrdinal("avaliable_quantity"));

                            // Returns Product.  
                            products.Add(new Product(productId, name, availableQuantity, stockQuantity));
                        }

                        if(products.Count > 0)
                        {
                            return products;
                        }
                        else
                        {
                            throw new ProductNotFoundException();
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







        public int Update_Product_Database(int product_ID, Product updated_Product)
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
                UPDATE product
                    SET name = @name_value,
                    stock_quantity = @stock_quantity_value,
                    avaliable_quantity = @avaliable_quantity_value
                WHERE product_id = @product_id_value", conn))
                {
                    // Fills up the quary with the data needed. 
                    cmd.Parameters.AddWithValue("product_id_value", updated_Product.Get_Product_ID());
                    cmd.Parameters.AddWithValue("name_value", updated_Product.Get_Name());
                    cmd.Parameters.AddWithValue("stock_quantity_value", updated_Product.Get_Stock_Quantity());
                    cmd.Parameters.AddWithValue("avaliable_quantity_value", updated_Product.Get_Avaliable_Quantity());

                    // Sends/executer the quary and returns an int value (the ID). 
                    result = cmd.ExecuteNonQuery();
                }

                // Incase there is a conflict on .  
                if (result == null)
                {
                    throw new ProductNotFoundException(updated_Product.Get_Product_ID());
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







        // Search

        public List<Product>? Search_forProduct_Name(string product_name)
        {
            // . 
            List<Product> products = new List<Product>();

            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT product_id, name, stock_quantity, avaliable_quantity
                FROM product
                WHERE name = @name_value", conn))
                {

                    // Add the parameter to prevent SQL injection
                    cmd.Parameters.AddWithValue("name_value", product_name);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Returns true if there's a row to read  
                        {
                            int productId = reader.GetInt32(reader.GetOrdinal("product_id")); // or reader["product_id"]  
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            int stockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity"));
                            int availableQuantity = reader.GetInt32(reader.GetOrdinal("avaliable_quantity"));

                            // Returns Product.  
                            products.Add(new Product(productId, name, availableQuantity, stockQuantity));
                        }

                        if (products.Count > 0)
                        {
                            return products;
                        }
                        else
                        {
                            throw new ProductNotFoundException();
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



        public List<Product>? Search_forProduct_Avaliable(double product_minAvaliable, double product_maxAvaliable)
        {
            // . 
            List<Product> products = new List<Product>();

            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT product_id, name, stock_quantity, avaliable_quantity
                FROM product
                WHERE avaliable_quantity >= @minAvaliable_value AND avaliable_quantity <= @maxAvaliable_value", conn))
                {

                    // Add the parameter to the SQL command
                    cmd.Parameters.AddWithValue("minAvaliable_value", product_minAvaliable); 
                    cmd.Parameters.AddWithValue("maxAvaliable_value", product_maxAvaliable); 


                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Returns true if there's a row to read  
                        {
                            int productId = reader.GetInt32(reader.GetOrdinal("product_id")); // or reader["product_id"]  
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            int stockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity"));
                            int availableQuantity = reader.GetInt32(reader.GetOrdinal("avaliable_quantity"));

                            // Returns Product.  
                            products.Add(new Product(productId, name, availableQuantity, stockQuantity));
                        }

                        if (products.Count > 0)
                        {
                            return products;
                        }
                        else
                        {
                            throw new ProductNotFoundException();
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



        public List<Product>? Search_forProduct_Stock(double product_minStock, double product_maxStock)
        {
            // . 
            List<Product> products = new List<Product>();

            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT product_id, name, stock_quantity, avaliable_quantity
                FROM product
                WHERE stock_quantity >= @minStock_value AND stock_quantity <= @maxStock_value", conn))
                {

                    // Add the parameter to the SQL command
                    cmd.Parameters.AddWithValue("minStock_value", product_minStock);
                    cmd.Parameters.AddWithValue("maxStock_value", product_maxStock);


                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Returns true if there's a row to read  
                        {
                            int productId = reader.GetInt32(reader.GetOrdinal("product_id")); // or reader["product_id"]  
                            string name = reader.GetString(reader.GetOrdinal("name"));
                            int stockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity"));
                            int availableQuantity = reader.GetInt32(reader.GetOrdinal("avaliable_quantity"));

                            // Returns Product.  
                            products.Add(new Product(productId, name, availableQuantity, stockQuantity));
                        }

                        if (products.Count > 0)
                        {
                            return products;
                        }
                        else
                        {
                            throw new ProductNotFoundException();
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







        public int Delete_Product_Database(int product_ID)
        {
            try
            {
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand(@"
                DELETE FROM product
                WHERE product_id = @product_id_value", conn))
                {

                    // Add the parameter to the SQL command
                    cmd.Parameters.AddWithValue("product_id_value", product_ID);


                    int rows_Deleted = cmd.ExecuteNonQuery();

                    // Incase there is a conflict on .  
                    if (rows_Deleted <= 0)
                    {
                        throw new ProductNotFoundException(product_ID);
                    }
                    else
                    {
                        // Returns the ID used to put the 
                        return product_ID;
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









    }









    public class ProductNotFoundException : Exception
    {
        public int? ProductID { get; }

        public ProductNotFoundException() : base($"Product was not found.")
        {
        }

        public ProductNotFoundException(int productID) : base($"Product with ID {productID} was not found.")
        {
            this.ProductID = productID;
        }
    }









}
