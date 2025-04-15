using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Funds
    {
        private string nameOfFund;
        private string addressOfFund;
        public Funds(string nameOfFund, string addressOfFund)
        {
            this.Name = nameOfFund;
            this.Address = addressOfFund;
        }
        public string Name
        {
            get { return nameOfFund; }
            set 
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name can't be empty");
                nameOfFund = value;
            }
        }
        public string Address
        {
            get { return addressOfFund; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Address can't be empty");
                addressOfFund = value;
            }
        }
    }
}
