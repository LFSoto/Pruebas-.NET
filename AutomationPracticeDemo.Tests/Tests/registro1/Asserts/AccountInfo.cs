using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.registro1.Asserts
{
    public class AccountInfo
    {
        private string title;
        private string name;
        private string password;
        private string dateOfBirth;

        public AccountInfo(string title, string name, string password, string dateOfBirth)
        {
            this.title = title;
            this.name = name;
            this.password = password;
            this.dateOfBirth = dateOfBirth;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string DateOfBirth
        {
            get => dateOfBirth;
            set => dateOfBirth = value;
        }

    }

}
