using PIM.Access.Queries;
using PIM.Data.Queries;
using PIM.Models;
using PIM.Process.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PIM.Data;
public class ProductsAPI : IProductAPI
{
    private static readonly ProductsAPI productsAPI = new();
    DatabaseConnection? connection; // The one and only connection needed for operating entire PIM

    private ProductsAPI() { }

    public static ProductsAPI GetProductAPI()
    {
        return productsAPI;
    }

    public DatabaseConnection? GetConnection()
    {
        return connection;
    }
    public void SetConnection(DatabaseConnection connection) // Has to be called before use (With a started connection)
    {
        this.connection = connection;
    }

    // Method for finding products, with the added functionalities:
    // Filtering: Will filter out any products, that arent in the category 'filterTerm'
    // ('filterTerm' should be assigned either null or the name of a category)
    // Searching: Will only return results whose 'name' attribute includes 'searchTerm'
    // (Searching follows the (PG)SQL format LIKE '%' || searchTerm || '%'
    public async Task<List<ProductDisplay>> FindProducts(string? filterTerm = null, string? searchTerm = null, string? sortBy = null, string? sortOrder = null)
    {
        if (connection == null) 
        {
            throw new Exception("Cannot access method \"FindProducts\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected())
        {
            throw new Exception("Cannot access method \"FindProducts\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }
        
        // Used for inserting the parameters into the SQL query later
        Dictionary<string, object?> param = new Dictionary<string, object?>(){
        { "@filterTerm", filterTerm ?? (object)DBNull.Value},
        {"@searchTerm", searchTerm ?? (object)DBNull.Value}
        };

        FindProductsQuery query = new FindProductsQuery(param); // Creating the query with the relevant paramaters

        List<ProductDisplay> rawResults = await connection.ExecuteQueryAsync<ProductDisplay>(query); // Executing the query via (DatabaseConnection) connection

        List<ProductDisplay> results;
        switch (sortBy)
        {
            case "name":
                results = rawResults.OrderBy(p => p.name).ToList();
                break;
            case "price":
                results = rawResults.OrderBy(p => p.priceInclVAT).ToList();
                break;
            default:
                results = rawResults.OrderBy(p => p.sku).ToList();
                break;

        }
        if (sortOrder == "DESC")
        {
            results.Reverse();
        }

        return results;

    }

    public async Task<List<int>> GetAllSku()
    {
        if (connection == null)
        {
            throw new Exception("Cannot access method \"FindProducts\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected())
        {
            throw new Exception("Cannot access method \"FindProducts\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        GetAllSkuQuery query = new GetAllSkuQuery(new Dictionary<string, object?>()); 

        List<int> results = await connection.ExecuteQueryAsync<int>(query); // Executing the query via (DatabaseConnection) connection

        return results;
    }
    public async Task<List<string>> GetAllCategories()
    {
        Dictionary<string, int> raw =  await GetCategories();
        return raw.Keys.ToList();

    }

    internal async Task<bool> DeleteProductById(int sku)
    {

        if (connection == null) 
        {
            throw new Exception("Cannot access method \"DeleteProductById\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected())
        {
            throw new Exception("Cannot access method \"DeleteProductById\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        Dictionary<string, object?> param = new Dictionary<string, object?>();
            param.Add("@id", sku);

        DeleteSingleQuery query = new DeleteSingleQuery(param);
        
         bool succes = await connection.ExecuteQueryAsync(query);
        
        return succes;
    }


    internal async Task AddProduct(List<object> productInfo) {

        if (connection == null) {
            throw new Exception("Cannot access method \"AddProducts\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"AddProducts\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        Dictionary<string, object?> param = new Dictionary<string, object?>();
        param.Add("@name", productInfo[0]);
        param.Add("@manufacturer", productInfo[1]);
        param.Add("@retail_price", productInfo[2]);
        param.Add("@sales_price_vat", productInfo[3]);
        param.Add("@height", productInfo[4]);
        param.Add("@width", productInfo[5]);
        param.Add("@depth", productInfo[6]);
        param.Add("@weight", productInfo[7]);
        param.Add("@color", productInfo[8]);
        param.Add("@long_descr", productInfo[9]);
            
        AddProductsQuery query = new AddProductsQuery(param);

        await connection.ExecuteQueryAsync(query);
    }
    internal async Task AddProducts(List<List<object>> productInfo) {

        if (connection == null) {
            throw new Exception("Cannot access method \"AddProducts\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"AddProducts\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }
        
        List<AddProductsQuery> queries = new List<AddProductsQuery>();

        foreach (List<object> product in productInfo) {
            Dictionary<string, object?> param = new Dictionary<string, object?>();

            param.Add("@name", product[0]);
            param.Add("@manufacturer", product[1]);
            param.Add("@retail_price", product[2]);
            param.Add("@sales_price_vat", product[3]);
            param.Add("@height", product[4]);
            param.Add("@width", product[5]);
            param.Add("@depth", product[6]);
            param.Add("@weight", product[7]);
            param.Add("@color", product[8]);
            param.Add("@long_descr", product[9]);

            queries.Add(new AddProductsQuery(param));
        }

        await connection.ExecuteQueriesAsync(queries);
    }
    internal async Task AddCategory(string category) {

        if (connection == null) {
            throw new Exception("Cannot access method \"AddCategory\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"AddCategory\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        Dictionary<string, object?> param = new Dictionary<string, object?>();
        param.Add("@category", category);

        AddCategoriesQuery query = new AddCategoriesQuery(param);

        await connection.ExecuteQueryAsync(query);
    }
    internal async Task AddCategories(IEnumerable<string> categories) {

        if (connection == null) {
            throw new Exception("Cannot access method \"AddCategory\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"AddCategory\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        List<AddCategoriesQuery> queries = new List<AddCategoriesQuery>();

        foreach (string category in categories) {
            Dictionary<string, object?> param = new Dictionary<string, object?>();

            param.Add("@category", category);

            queries.Add(new AddCategoriesQuery(param));
        }

        await connection.ExecuteQueriesAsync(queries);
    }
    internal async Task AddType(int product, int category) {

        if (connection == null) {
            throw new Exception("Cannot access method \"AddTypes\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"AddTypes\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        Dictionary<string, object?> param = new Dictionary<string, object?>();
        param.Add("@product", product);
        param.Add("@category", category);

        AddTypesQuery query = new AddTypesQuery(param);

        await connection.ExecuteQueryAsync(query);
    }
    internal async Task AddTypes(List<(int, int)> links) {

        if (connection == null) {
            throw new Exception("Cannot access method \"AddTypes\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"AddTypes\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        List<AddTypesQuery> queries = new List<AddTypesQuery>();

        foreach((int, int) link in links) {
            Dictionary<string, object?> param = new Dictionary<string, object?>();

            param.Add("@product", link.Item1);
            param.Add("@category", link.Item2);

            queries.Add(new AddTypesQuery(param));
        }

        await connection.ExecuteQueriesAsync(queries);
    }
    public async Task<int> GetLastSKU() {

        if (connection == null) {
            throw new Exception("Cannot access method \"GetLastSKU\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"GetLastSKU\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        Dictionary<string, object?> param = new Dictionary<string, object?>();

        GetLastSKUQuery query = new GetLastSKUQuery(param);

        List<int> results = await connection.ExecuteQueryAsync<int>(query);

        return results[0];
    }
    internal async Task<Dictionary<string, int>> GetCategories() {
        if (connection == null) {
            throw new Exception("Cannot access method \"GetCategories\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"GetCategories\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        Dictionary<string, object?> param = new Dictionary<string, object?>();

        GetCategoriesQuery query = new GetCategoriesQuery(param);

        List<(string, int)> results = await connection.ExecuteQueryAsync<(string, int)>(query);

        Dictionary<string, int> categories = new Dictionary<string, int>();

        foreach (var result in results) {
            (string category, int id) = result;
            categories.Add(category, id);
        }

        return categories;
    }

    public async Task<ProductFull> GetProduct(int sku)
    {
        if (connection == null)
        {
            throw new Exception("Cannot access method \"FindProducts\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected())
        {
            throw new Exception("Cannot access method \"FindProducts\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        Dictionary<string, object?> param = new Dictionary<string, object?>();
        param.Add("@id", sku);

        GetProductBySkuQuery query = new GetProductBySkuQuery(param);

        List<ProductFull> results = await connection.ExecuteQueryAsync<ProductFull>(query);

        return results[0];
    }
    internal async Task<List<ProductView>> GetAllProductInformation(string? categoryTerm = null, string? searchTerm = null) {
        if (connection == null) {
            throw new Exception("Cannot access method \"GetAllProductInformation\" without supplying DatabaseConnection first");
        }
        if (!connection.isConnected()) {
            throw new Exception("Cannot access method \"GetAllProductInformation\" without starting DatabaseConnection first \n(Use method StartAsync)");
        }

        Dictionary<string, object?> param = new Dictionary<string, object?>();
        param.Add("@categoryTerm", categoryTerm ?? (object)DBNull.Value);
        param.Add("@searchTerm", searchTerm ?? (object)DBNull.Value);

        GetAllProductInformationQuery query = new GetAllProductInformationQuery(param);

        List<ProductView> results = await connection.ExecuteQueryAsync<ProductView>(query);

        return results;
    }
}
