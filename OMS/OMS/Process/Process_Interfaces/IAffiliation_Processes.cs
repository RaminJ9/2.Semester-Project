using OMS.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process.Process_Interfaces
{
    public interface IAffiliation_Processes
    {

        // Create 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CVR"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public int Create_Affiliation(int CVR, string name, Address address);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="CVR"></param>
        /// <param name="name"></param>
        /// <param name="road"></param>
        /// <param name="zip"></param>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public int Create_Affiliation(int CVR, string name, string road, ZIPCode zip_object);





        // Get 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="affiliation_ID"></param>
        /// <returns></returns>
        public Affiliation? Get_Affiliation(int affiliation_ID);



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Affiliation>? Get_All_Affiliation();





        // Update 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="affiliation_ID"></param>
        /// <param name="new_affiliation"></param>
        /// <returns></returns>
        public bool Update_Affiliation(int affiliation_ID, Affiliation new_affiliation);







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
        public bool Delete_Affiliation(int affiliation_ID);



    }
}
