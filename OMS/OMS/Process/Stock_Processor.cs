using OMS.DataTypes;
using OMS.Process.Process_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
// Creator : Alexander Maach
namespace OMS.Process
{
    public class Stock_Processor : IStock_Processes
    {
        
        private OMS.Access.Access access_instance;




        public Stock_Processor(OMS.Access.Access access_instance)
        {
            this.access_instance = access_instance;
        }


        

        // Create
        public int Create_Product(int product_ID, string product_name, int avalible_quantity, int stock_quantity)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null || product_name == null || avalible_quantity == null || stock_quantity == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }
                if (string.IsNullOrEmpty(product_name))
                {
                    throw new ArgumentException("Product name can't be empty.");
                }
                if (avalible_quantity < 0)
                {
                    throw new ArgumentException("Avalible quantity can't be below zero.");
                }
                if (stock_quantity < 0)
                {
                    throw new ArgumentException("Quantity in stock can't be below zero.");
                }


                // Creating temperary object of the "new" order. 
                Product new_Product = new Product(product_ID, product_name, avalible_quantity, stock_quantity);


                // 
                return access_instance.Create_Product_Database(new_Product);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                return -10;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return -20;
            }
        }










        // Get 
        public Product Get_Product(int product_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }


                // Creating temperary object of the "new" order. 
                return access_instance.Fetch_Product_Database(product_ID);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public List<Product> Get_All_Products()
        {
            try
            {
                // Creating temperary object of the "new" order. 
                return access_instance.Fetch_All_Products_Database();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public int Get_Product_CurrentlyInstock(int product_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // Creating temperary object of the "new" order. 
                Product temp_product = access_instance.Fetch_Product_Database(product_ID);


                // Creating temperary object of the "new" order. 
                return temp_product.Get_Avaliable_Quantity();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public int Get_Product_CurrentlyAvaliable(int product_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }

                // Creating temperary object of the "new" order. 
                Product temp_product = access_instance.Fetch_Product_Database(product_ID);


                // Creating temperary object of the "new" order. 
                return temp_product.Get_Stock_Quantity();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }





        // Update 
        public bool Update_Product_Name(int product_ID, string new_name)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null || new_name == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }


                // Creating temperary object of the "new" order. 
                Product updated_product = access_instance.Fetch_Product_Database(product_ID);


                // . 
                updated_product.Set_Product_Name(new_name);


                // 
                if (access_instance.Update_Product_Database(updated_product.Get_Product_ID(), updated_product) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public bool Update_Product_Instock(int product_ID, int new_instock)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null || new_instock == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }


                // Creating temperary object of the "new" order. 
                Product updated_product = access_instance.Fetch_Product_Database(product_ID);


                // . 
                updated_product.Set_Stock_Quantity(new_instock);


                // 
                if (access_instance.Update_Product_Database(updated_product.Get_Product_ID(), updated_product) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public bool Update_Product_Avaliable(int product_ID, int new_avaliable)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null || new_avaliable == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }


                // Creating temperary object of the "new" order. 
                Product updated_product = access_instance.Fetch_Product_Database(product_ID);


                // . 
                updated_product.Set_Avaliable_Quantity(new_avaliable);


                // 
                if (access_instance.Update_Product_Database(updated_product.Get_Product_ID(), updated_product) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }







        // Search

        public List<Product> Search_forProduct_Name(string product_name)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_name == null)
                {
                    throw new ArgumentNullException();
                }


                // . 
                return access_instance.Search_forProduct_Name(product_name);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }




        public List<Product>? Search_forProduct_Avaliable(double product_minAvaliable, double product_maxAvaliable)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_minAvaliable == null || product_maxAvaliable == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (!(product_minAvaliable <= product_maxAvaliable))
                {
                    throw new ArgumentException("product_minAvaliable was greater then product_maxAvaliable.");
                }


                // . 
                return access_instance.Search_forProduct_Avaliable(product_minAvaliable, product_maxAvaliable);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }




        public List<Product>? Search_forProduct_Stock(double product_minStock, double product_maxStock)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_minStock == null || product_maxStock == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (!(product_minStock <= product_maxStock))
                {
                    throw new ArgumentException("product_minStock was greater then product_maxStock.");
                } 


                // . 
                return access_instance.Search_forProduct_Stock(product_minStock, product_maxStock);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }









        // Delete 
        public bool Delete_Product(int product_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (product_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (product_ID < 0)
                {
                    throw new ArgumentException("Product ID can't be below zero.");
                }


                // 
                if (access_instance.Delete_Product_Database(product_ID) >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        
    }



}
