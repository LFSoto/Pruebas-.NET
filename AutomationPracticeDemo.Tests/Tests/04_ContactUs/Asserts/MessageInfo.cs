using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests._04_ContactUs.Asserts
{
    public class MessageInfo
    {
        private string name;
        private string email;
        private string subject;
        private string message;

        public MessageInfo(string name, string email, string subject, string message)
        {
            this.name = name;
            this.email = email;
            this.subject = subject;
            this.message = message;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Subject
        {
            get => subject;
            set => subject = value;
        }

        public string Message
        {
            get => message;
            set => message = value;
        }
    }

}
