using Npgsql;
using PIM.Data.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIM.Data;
public class DatabaseConnection
{
    string connectionString;
       
    public DatabaseConnection(string connectionString)
    {
        this.connectionString = connectionString;
    }

    // In charge of establishing a working connection to db
    async Task<NpgsqlConnection> CreateConnection() 
    {
        NpgsqlConnection con = new NpgsqlConnection(connectionString); // Create connection
        await con.OpenAsync(); // Activate/Open connection
        return con;
    }


    // Used for executing a query and returning response from DB.
    // Simply using the QueryBase will not work. Instead; create a class that inherits from QueryBase
    // ^(See Data/Queries/QueryBase.cs for further instructions)
    // Should only be used for queries where each row is equivelant to an object-model in the models folder
    // If no suitable object model exists, one can easily be added.
    // The template 'T' should be the same as the return type of the query.MapFromReader(); 
    public async Task<List<T>> ExecuteQueryAsync<T>(QueryBase query)
    {
        NpgsqlConnection connection = await CreateConnection(); // Creating connection

        using NpgsqlCommand command = new NpgsqlCommand(query.GetQuery(), connection);

        // Binds the set query paramaters (dictionary) to values in "actual" query (string)
        // Eg:
        // Q: SELECT * FROM products WHERE id = @id;
        // Params dict: {'@id': '42' }
        // Result Q: SELECT * FROM produts WHERE id = 42; 
        foreach (var param in query.GetParamaters())
        {
            command.Parameters.AddWithValue(param.Key, param.Value);
        }


        List<T> resultSet = new List<T>();
        
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync(); // The actual execution of the query
        try
        {
            while (await reader.ReadAsync()) // Loops until no more result-rows are available
            {
                T record = (T)query.MapFromReader(reader); // Call to method where 'reader' is turned into 'T' 
                resultSet.Add(record);
            }
        } catch (InvalidCastException e)
        {
            throw new Exception($"Type; {typeof(T)} cannot be mapped to by {query.GetType()} \n{e.Message}");
        }
        await connection.CloseAsync();
        return resultSet;
    }


    // Used for executing a query that doesn't expect response (rows) from DB.
    // Practical for INSERT, DELETE & UPDATE
    // Simply using the QueryBase will not work. Instead; create a class that inherits from QueryBase
    // ^(See Data/Queries/QueryBase.cs for further instructions)
    public async Task<bool> ExecuteQueryAsync(QueryBase query)
    {
        NpgsqlConnection connection = await CreateConnection(); // Creating connection

        using NpgsqlCommand command = new NpgsqlCommand(query.GetQuery(), connection);

        // Binds the set query parameters (dictionary) to values in "actual" query (string)
        // Eg:
        // Q: SELECT * FROM products WHERE id = @id;
        // Params dict: {'@id': '42' }
        // Result Q: SELECT * FROM produts WHERE id = 42; 
        foreach (var param in query.GetParamaters())
        {
            command.Parameters.AddWithValue(param.Key, param.Value);
        }


        int changedRows = await command.ExecuteNonQueryAsync(); // The actual execution of the query
        await connection.CloseAsync();

        return changedRows > 0; // Returns false when no rows were affected by the query.
    }

    // Used for executing multiple queries that doesn't expect response (rows) from DB.
    // Practical for batch/bulk INSERTs, DELETEs & UPDATEs
    // Simply using the QueryBase will not work. Instead; create a class that inherits from QueryBase
    // ^(See Data/Queries/QueryBase.cs for further instructions)
    public async Task<bool> ExecuteQueriesAsync(IEnumerable<QueryBase> queries)
    {
        NpgsqlConnection connection = await CreateConnection(); // Creating connection

        using NpgsqlTransaction transaction = await connection.BeginTransactionAsync();
        try
        {
            foreach(QueryBase query in queries)
            {
                using NpgsqlCommand command = new NpgsqlCommand(query.GetQuery(), connection);
            
                // Binds the set query parameters (dictionary) to values in "actual" query (string)
                // Eg:
                // Q: SELECT * FROM products WHERE id = @id;
                // Params dict: {'@id': '42' }
                // Result Q: SELECT * FROM produts WHERE id = 42; 
                foreach (var param in query.GetParamaters())
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                
                await command.ExecuteNonQueryAsync(); // The actual execution of the query
            }
        } 
        catch(Exception)
        {
            await transaction.RollbackAsync();
            return false;
        }

        await transaction.CommitAsync();
        await connection.CloseAsync();
        return true;


    }


    // Used to be important, but is no longer, since connections are managed dynamically per query.
    // Has been kept to ensure backwards compatability among other groups.
    public async Task<bool> StartAsync()
    {
        Console.WriteLine("This method is deprecated. Connections are now automatically managed");
        return true;

    }

    // Used to be important, but is no longer, since connections are managed dynamically per query.
    // Has been kept to ensure backwards compatability among other groups.
    public async Task<bool> StopAsync()
    {
        Console.WriteLine("This method is deprecated. Connections are now automatically managed");
        return true;

    }

    // Used to be important, but is no longer, since connections are managed dynamically per query.
    // Has been kept to ensure backwards compatability among other groups.
    public bool isConnected()
    {
        return true;
    }
}
