using OMS.Process.Process_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Process
{
    internal class Login_Processor : ILogin_Processes
    {

        private OMS.Access.Access access_instance;




        public Login_Processor()
        {
            this.access_instance = OMS.Access.Access.GetInstance();
        }




    }
}
