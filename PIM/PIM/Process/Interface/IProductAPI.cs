using PIM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIM.Data
{
    public interface IProductAPI
    {
        public DatabaseConnection? GetConnection();
        public void SetConnection(DatabaseConnection connection);
        public Task<List<ProductDisplay>> FindProducts(string? filterTerm, string? searchTerm, string? sortBy, string? sortOrder);
        public Task<ProductFull> GetProduct(int sku);
        public Task<List<int>> GetAllSku();
        public Task<List<string>> GetAllCategories();
    }
}
