using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS;
using OMS.DataTypes;

namespace OMS.Access.Access_Interfaces
{
    internal interface IShippingOptions_Handler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_ShippingOption"></param>
        /// <returns></returns>
        public int Create_ShippingOption_Database(string delivery_option, double shipping_price, string company_name);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="new_ShippingOption"></param>
        /// <returns></returns>
        public int Create_ShippingOption_Database(Shipping_Option new_ShippingOption);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingOption_ID"></param>
        /// <returns></returns>
        public Shipping_Option? Fetch_ShippingOption_Database(int shippingOption_ID);



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Shipping_Option>? Fetch_All_ShippingOptions_Database();



        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingOption_ID"></param>
        /// <param name="updated_ShippingOption"></param>
        /// <returns></returns>
        public int Update_ShippingOption_Database(int shippingOption_ID, Shipping_Option updated_ShippingOption);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="shippingOption_ID"></param>
        /// <returns></returns>
        public int Delete_ShippingOption_Database(int shippingOption_ID);



    }
}
