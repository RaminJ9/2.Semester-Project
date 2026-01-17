using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.DataTypes
{
    public interface IToString_Interface
    {



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToString();



        /// <summary>
        /// 
        /// </summary>
        /// <param name="assign_string"></param>
        /// <param name="separator_string"></param>
        /// <returns></returns>
        public string ToString_Custom(string assign_string = ":", string separator_string = "\n");



        /// <summary>
        /// 
        /// </summary>
        /// <param name="assign_string"></param>
        /// <returns></returns>
        public List<string> ToString_List(string assign_string = ":");


    }
}
