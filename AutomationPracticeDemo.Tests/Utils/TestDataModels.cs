namespace AutomationPracticeDemo.Tests.Utils
{
    public class ContactData
    {
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public bool attachFile { get; set; }
    }

    public class RegisterData
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string mobile_number { get; set; }
    }

    public class NewsletterData
    {
        public string email { get; set; }
    }
}
