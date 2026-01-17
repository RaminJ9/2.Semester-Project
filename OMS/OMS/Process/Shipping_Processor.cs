using OMS.Process.Process_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS;
using OMS.DataTypes;
// Creator : Alexander Maach
namespace OMS.Process
{
    public class Shipping_Processor : IShippingOptions_Processes
    {



        private OMS.Access.Access access_instance;




        public Shipping_Processor(OMS.Access.Access access_instance)
        {
            this.access_instance = access_instance;
        }







        // Create

        public int Create_Shipping_Option(Delivery_option delivery_option, double shipping_price, string company_name)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (delivery_option == null || shipping_price == null || company_name == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (shipping_price < 0)
                {
                    throw new ArgumentException("Shipping price can't be below zero.");
                }


                // Creating temperary object of the "new" order. 
                Shipping_Option new_shipping_option = new Shipping_Option(delivery_option, shipping_price, company_name);



                // 
                return access_instance.Create_ShippingOption_Database(new_shipping_option);


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


        public Shipping_Option Get_Shipping_Option(int shippingOption_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (shippingOption_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (shippingOption_ID < 0)
                {
                    throw new ArgumentException("Shipping option ID can't be below zero.");
                }


                // 
                return access_instance.Fetch_ShippingOption_Database(shippingOption_ID);

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





        public List<Shipping_Option> Get_All_Shipping_Options()
        {
            // 
            return access_instance.Fetch_All_ShippingOptions_Database();
        }





        public bool Update_Shipping_Option(int shippingOption_ID, Shipping_Option updated_ShippingOption)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (shippingOption_ID == null || updated_ShippingOption == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (shippingOption_ID < 0)
                {
                    throw new ArgumentException("Shipping option ID can't be below zero.");
                }


                // 
                if (access_instance.Update_ShippingOption_Database(shippingOption_ID, updated_ShippingOption) >= 0)
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








        public bool Delete_Shipping_Option(int shippingOption_ID)
        {
            try
            {
                // Checking for non-initiate parameter. 
                if (shippingOption_ID == null)
                {
                    throw new ArgumentNullException();
                }


                // .  
                if (shippingOption_ID < 0)
                {
                    throw new ArgumentException("Shipping option ID can't be below zero.");
                }


                // 
                if (access_instance.Delete_ShippingOption_Database(shippingOption_ID) >= 0)
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
