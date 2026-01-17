   //namespace OMSAvaloniaApplication.Access
   //{
   // public int InsertItemList(int order_id, int product_id, int quantity, decimal price_per_unit)
   // {
   //     const string connString = "Host=localhost;Port=5432;Database=oms;Username=postgres;Password=1234";
   //     using var conn = new NpgsqlConnection(connString);
   //     conn.Open();

   //     using var cmd = new NpgsqlCommand(@"
   //     INSERT INTO item_list (order_id, product_id, quantity, price_per_unit) 
   //     VALUES (@order_id, @product_id, @quantity, @price_per_unit) 
   //     RETURNING item_id", conn);

   //     cmd.Parameters.AddWithValue("order_id", order_id);
   //     cmd.Parameters.AddWithValue("product_id", product_id);
   //     cmd.Parameters.AddWithValue("quantity", quantity);
   //     cmd.Parameters.AddWithValue("price_per_unit", price_per_unit);

   //     return (int)cmd.ExecuteScalar();
   //     conn.Close();
   // }
   //}