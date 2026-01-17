using OMS.DataTypes;
using OMS.Process.Process_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process
{
    internal class Stock_Processor : IStock_Processes
    {
        
        private OMS.Access.Access access_instance;




        public Stock_Processor()
        {
            this.access_instance = OMS.Access.Access.GetInstance();
        }




        // Create
        public int Create_Product(int product_ID, string product_name, int avalible_quantity, int stock_quantity)
        {

        }




        // Get 
        public Product Get_Product(int product_id)
        {

        }

        public List<Product> Get_Product_List()
        {

        }

        public int Get_Product_CurrentlyInstock(int product_id)
        {

        }

        public int Get_Product_CurrentlyAvaliable(int product_id)
        {

        }





        // Update 
        public bool Update_Product_Name(int product_id, string new_name)
        {

        }

        public bool Update_Product_Instock(int product_id, int new_instock)
        {

        }

        public bool Update_Product_Avaliable(int product_id, int new_instock)
        {

        }





        // Delete 
        public bool Delete_Product(int product_id)
        {

        }


    }
}
