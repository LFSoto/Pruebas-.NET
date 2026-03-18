using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Models
{
    public class AccountInformation
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
    }

    public class AddressInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string MobileNumber { get; set; }
    }

    public class UserData
    {
        public AccountInformation AccountInformation { get; set; }
        public AddressInformation AddressInformation { get; set; }
    }
}

