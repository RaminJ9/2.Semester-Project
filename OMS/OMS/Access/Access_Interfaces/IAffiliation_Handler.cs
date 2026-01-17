using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Access.Access_Interfaces
{
    public interface IAffiliation_Handler
    {

        // Create 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="affiliation"></param>
        /// <returns></returns>
        public int Create_Affiliation_Database(Affiliation affiliation);





        // Fetch 


        /// <summary>
        /// 
        /// </summary>
        /// <param name="affiliation_ID"></param>
        /// <returns></returns>
        public Affiliation? Fetch_Affiliation_Database(int affiliation_ID);



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Affiliation>? Fetch_All_Affiliation_Database();





        // Update 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="affiliation_ID"></param>
        /// <param name="updated_Customer"></param>
        /// <returns></returns>
        public int Update_Affiliation_Database(int affiliation_ID, Affiliation updated_Customer);







        // Search


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cvr"></param>
        /// <returns></returns>
        public List<Affiliation>? Search_forAffiliation_CVR(int cvr);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Affiliation>? Search_forAffiliation_Name(string name);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public List<Affiliation>? Search_forAffiliation_Address(Address address);





        // Delete 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="affiliation_ID"></param>
        /// <returns></returns>
        public int Delete_Affiliation_Database(int affiliation_ID);



    }
}
