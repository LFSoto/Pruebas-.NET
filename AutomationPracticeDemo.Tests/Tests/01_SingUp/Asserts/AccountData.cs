using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests._01_SingUp.Asserts
{
    public class AccountData
    {
        private AccountInfo _accountInformation;
        private AddressInfo _addressInformation;

        public AccountData(AccountInfo accountInformation, AddressInfo addressInformation)
        {
            _accountInformation = accountInformation;
            _addressInformation = addressInformation;
        }

        public AccountInfo AccountInformation
        {
            get => _accountInformation;
            set => _accountInformation = value;
        }

        public AddressInfo AddressInformation
        {
            get => _addressInformation;
            set => _addressInformation = value;
        }

        public AccountInfo GetAccountInformation()
        {
            return this._accountInformation;
        }
        public AddressInfo GetAddressInformation()
        {
            return this._addressInformation;
        }
    }
}
