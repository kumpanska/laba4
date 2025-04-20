using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
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
                {
                    throw new ArgumentException("Address can't be empty");
                }
                if (!Regex.IsMatch(value, @"[a-zA-Zа-яА-ЯіІїЇєЄ]"))
                {
                    throw new ArgumentException("Address must contain at least one letter.");
                }
                if (!Regex.IsMatch(value, @"\d+(/\d+)?"))
                {
                    throw new ArgumentException("Address must contain at least one number, optionally in format like '7/1'.");
                }
                if (!value.Contains(','))
                {
                    throw new ArgumentException("Address must contain a comma (to separate street and building)");
                }
                addressOfFund = value;
            }
        }
    }
}
