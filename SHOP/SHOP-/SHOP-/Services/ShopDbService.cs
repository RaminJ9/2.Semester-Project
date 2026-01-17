using Npgsql;
using NpgsqlTypes;
using PIM.Access.Queries;
using PIM.Data.Queries;
using PIM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PIM.Data;
using SHOP.Database.Query;
using SHOP.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SHOP.Services;
public class ShopDbService : IShopDbService
{
    private static readonly ShopDbService shopDbService = new();
    DatabaseConnection? connection; // The one and only connection needed for operating entire PIM

    public ShopDbService() { }

    public static ShopDbService GetShopDBService()
    {
        return shopDbService;
    }

    public DatabaseConnection? GetConnection()
    {
        return connection;
    }
    public async Task SetConnection() // Has to be called before use (With a started connection)
    {
        // database SHOP connection:

        string connectionStringShop = $"Host=localhost;Port=5432;Username={UserCredentials.username};Password={UserCredentials.password};Database=SHOP";
        // string connectionString = $"Host=localhost;Port=5432;Username={UserCredentials.username};Password={UserCredentials.password};Database=Pim Clone"; // database laptop
 
        //var conn = new NpgsqlConnection(connectionString);
        //conn.Open();
        
        DatabaseConnection? dbConnectionShop = new DatabaseConnection(connectionStringShop);
       
        bool startedShop =  await dbConnectionShop.StartAsync().ConfigureAwait(false);
       
        if (!startedShop || !dbConnectionShop.isConnected())
        {
            // Handle connection failure (log error, show message, exit?)
            Console.Error.WriteLine("FATAL: Failed to start database connection!");
            // Optionally throw an exception or handle gracefully
            throw new Exception("Database connection could not be established.");
        }
        Console.WriteLine("Database connection started successfully.");
        
        // 5. Set Connection in shop
        connection = dbConnectionShop;
    }

    // Method for finding products, with the added functionalities:
    // Filtering: Will filter out any products, that arent in the category 'filterTerm'
    // ('filterTerm' should be assigned either null or the name of a category)
    // Searching: Will only return results whose 'name' attribute includes 'searchTerm'
    // (Searching follows the (PG)SQL format LIKE '%' || searchTerm || '%'

 
    public async Task<Dictionary<string, string>> GetAllContents() {
        if (connection == null) {
            throw new Exception("Cannot access method \"GetCategories\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"GetCategories\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        Dictionary<string, object?> param = new Dictionary<string, object?>();

        GetContentsQuery query = new GetContentsQuery(param);

        List<(string, string)> results = await connection.ExecuteQueryAsync<(string, string)>(query);

        Dictionary<string, string> contents = new Dictionary<string, string>();

        foreach (var result in results) {
            (string content, string id) = result;
            contents.Add(content, id);
        }

        return contents;
    }
}
