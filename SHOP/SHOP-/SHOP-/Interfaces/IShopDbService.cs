using PIM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PIM.Data;

namespace SHOP.Interfaces
{
    public interface IShopDbService
    {
        public DatabaseConnection? GetConnection();
        public Task SetConnection();
        public Task<Dictionary<string, string>> GetAllContents();
    }
}