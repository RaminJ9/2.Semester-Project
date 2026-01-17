using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process.Process_Interfaces
{
    internal interface IShippingOptions_Processes
    {



        /// <summary>
        /// 
        /// </summary>
        /// <param name="delivery_option"></param>
        /// <param name="shipping_price"></param>
        /// <param name="company_name"></param>
        /// <returns></returns>
        public int Create_Shipping_Option(Delivery_option delivery_option, double shipping_price, string company_name);








        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingOption_ID"></param>
        /// <returns></returns>
        public Shipping_Option? Get_Shipping_Option(int shippingOption_ID);



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Shipping_Option>? Get_All_Shipping_Options();






        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingOption_ID"></param>
        /// <param name="updated_ShippingOption"></param>
        /// <returns></returns>
        public bool Update_Shipping_Option(int shippingOption_ID, Shipping_Option updated_ShippingOption);







        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingOption_ID"></param>
        /// <returns></returns>
        public bool Delete_Shipping_Option(int shippingOption_ID);








    }
}
