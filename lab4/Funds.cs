using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
namespace lab4
{
    public  partial class Funds
    {
        private string nameOfFund;
        private string addressOfFund;
        public Funds(string nameOfFund, string addressOfFund)
        {
            this.Name = nameOfFund;
            this.Address = addressOfFund;
        }
        [Required(ErrorMessage = "Fund name is required")]
        [RegularExpression(@"^[a-zA-Z-а-яА-ЯіІїЇєЄ\s]+$", ErrorMessage = "Fund name must contain only letters and spaces")]
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
        [Required(ErrorMessage = "Address is required.")]
        [RegularExpression(@"^[a-zA-Z-а-яА-ЯіІїЇєЄ\s\d/,\.]+$", ErrorMessage = "Address contains invalid characters")]
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
                    throw new ArgumentException("Address must contain a comma (to separate street and building(or city))");
                }
                addressOfFund = value;
            }
        }
    }
}
