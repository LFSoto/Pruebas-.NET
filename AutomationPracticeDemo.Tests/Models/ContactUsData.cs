using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.ContactUsData
{
    public class ContactMessage
    {
        public string Name { get; set; }     // corresponde a "name" en el JSON
        public string Email { get; set; }    // corresponde a "email"
        public string Subject { get; set; }  // corresponde a "subject"
        public string Message { get; set; }  // corresponde a "message"
    }

}
