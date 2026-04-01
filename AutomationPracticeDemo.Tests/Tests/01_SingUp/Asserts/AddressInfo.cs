
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace AutomationPracticeDemo.Tests.Tests._01_SingUp.Asserts
{

    public class AddressInfo
    {
        private string firstName;
        private string lastName;
        private string company;
        private string address;
        private string address2;
        private string country;
        private string state;
        private string city;
        private string zipcode;
        private string mobileNumber;

        public AddressInfo(string firstName, string lastName, string company, string address, string address2,
            string country, string state, string city, string zipcode, string mobileNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.company = company;
            this.address = address;
            this.address2 = address2;
            this.country = country;
            this.state = state;
            this.city = city;
            this.zipcode = zipcode;
            this.mobileNumber = mobileNumber;
        }

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }
        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }
        public string Company
        {
            get => company;
            set => company = value;
        }
        public string Address
        {
            get => address;
            set => address = value;
        }
        public string Address2
        {
            get => address2;
            set => address2 = value;
        }
        public string Country
        {
            get => country;
            set => country = value;
        }
        public string State
        {
            get => state;
            set => state = value;
        }
        public string City
        {
            get => city;
            set => city = value;
        }
        public string Zipcode
        {
            get => zipcode;
            set => zipcode = value;
        }
        public string MobileNumber
        {
            get => mobileNumber;
            set => mobileNumber = value;
        }
    }

}
