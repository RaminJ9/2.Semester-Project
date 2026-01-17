using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Access.Access_Interfaces
{
    internal interface IAddress_Handler
    {


        // Create 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public int Create_ZIPCode_Database(ZIPCode zipCode);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip"></param>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public int Create_ZIPCode_Database(int zip, string country, string city);






        // Fetch  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip_ID"></param>
        /// <returns></returns>
        public ZIPCode? Fetch_ZIPCode_Database(int zip_ID);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip_Code"></param>
        /// <returns></returns>
        public ZIPCode? Fetch_ZIPID_Database(int zip_Code);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ZIPCode>? Fetch_All_ZIPCode_Database();







        // Update 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip_ID"></param>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public int Update_ZIPCode_Database(int zip_ID, ZIPCode zipCode);









        // Delete 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zip_ID"></param>
        /// <returns></returns>
        public int Delete_ZIPCode_Database(int zip_ID);



    }
}
