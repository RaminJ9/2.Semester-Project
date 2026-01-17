using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PIM.Access.Queries;
using PIM.Data;

namespace PIM;


public static class Authentication
{
    static ProductsAPI _productsAPI = ProductsAPI.GetProductAPI();
    static DatabaseConnection _databaseConnection = _productsAPI.GetConnection(); 

    // Delete user from database
    public static async Task DeleteUserAsync(string user)
    {
        if (_productsAPI == null || _databaseConnection == null)
        {
            _productsAPI = ProductsAPI.GetProductAPI();
            _databaseConnection = _productsAPI.GetConnection(); 
        }

        if (!_databaseConnection.isConnected())
        {
           await _databaseConnection.StartAsync();
        }
        Dictionary<string, object?> param = new Dictionary<string, object?>(){
            { "@user", user},
        };
        DeleteUserQuery deleteUserQuery = new DeleteUserQuery(param);
        await _databaseConnection.ExecuteQueryAsync(deleteUserQuery);
    }

    // Fetch all username from database
    public static async Task<List<string>> GetAllUsersAsync()
    {
        if (_productsAPI == null || _databaseConnection == null)
        {
            _productsAPI = ProductsAPI.GetProductAPI();
            _databaseConnection = _productsAPI.GetConnection(); 
        }

        if (!_databaseConnection.isConnected())
        {
            await _databaseConnection.StartAsync();
        }
        Dictionary<string, object?> param = new Dictionary<string, object?>(){};
        GetAllUserQuery getAllUserQuery = new GetAllUserQuery(param);
        List<string> users = await _databaseConnection.ExecuteQueryAsync<string>(getAllUserQuery);
        
        return users;
        
    }

    // fetch all passwords from database
    public static async Task<List<string>> GetAllPasswordsAsync()
    {
        if (_productsAPI == null || _databaseConnection == null)
        {
            _productsAPI = ProductsAPI.GetProductAPI();
            _databaseConnection = _productsAPI.GetConnection(); 
        }

        if (!_databaseConnection.isConnected())
        {
            await _databaseConnection.StartAsync();
        }
        Dictionary<string, object?> param = new Dictionary<string, object?>(){};
        GetAllPasswordsQuery getAllPasswordsQuery = new GetAllPasswordsQuery(param);
        List<string> passwords = await _databaseConnection.ExecuteQueryAsync<string>(getAllPasswordsQuery);
        
        return passwords;
    }
    
    //Insert user into database
    public static async Task InsertUserAsync(string username, string password)
    {
        if (_productsAPI == null || _databaseConnection == null)
        {
            _productsAPI = ProductsAPI.GetProductAPI();
            _databaseConnection = _productsAPI.GetConnection(); 
        }

        if (!_databaseConnection.isConnected())
        {
            await _databaseConnection.StartAsync();
        }
        Dictionary<string, object?> param = new Dictionary<string, object?>(){
            {"@username", username},
            {"@password", password ?? (object)DBNull.Value}
        };
        InsertUserQuery InsertUserQuery = new InsertUserQuery(param);
        await _databaseConnection.ExecuteQueryAsync(InsertUserQuery);
    }
}
