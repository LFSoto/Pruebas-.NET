using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutomationPracticeDemo.Tests.Tests.Suscription5.Asserts
{
    public class SuscriptionInfo
    {
        private string email;
       

        public SuscriptionInfo(string email)
        {
           
            this.email = email;
           
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

       

    }
}
