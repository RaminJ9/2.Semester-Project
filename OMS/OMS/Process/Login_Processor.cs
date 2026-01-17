using OMS.Process.Process_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Creator : Alexander Maach
namespace OMS.Process
{
    public class Login_Processor : ILogin_Processes
    {

        private OMS.Access.Access access_instance;




        public Login_Processor(OMS.Access.Access access_instance)
        {
            this.access_instance = access_instance;
        }




    }
}
