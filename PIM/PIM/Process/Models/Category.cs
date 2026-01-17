using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Process.Models
{
    internal class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static List<Category> FromDictionary(Dictionary<string, int> dict)
        {
            List<Category> result = new();
            foreach (KeyValuePair<string, int> pair in dict)
            {
                result.Add(new Category(pair.Value, pair.Key));
            }
            return result;
        }


        public override string ToString() 
        {
            return Name;
        }
    }
}
