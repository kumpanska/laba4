using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class ExhibitionHall
    {
        private string nameOfHall;
        private List<Exhibit> exhibits;
        public ExhibitionHall(string nameOfHall)
        {
            this.NameOfHall = nameOfHall;
            this.Exhibits = new List<Exhibit>();
        }
        public string NameOfHall
        {
            get { return nameOfHall; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name of the hall cannot be empty.");
                nameOfHall = value;
            }
        }
        public List<Exhibit> Exhibits
        {
            get { return exhibits; }
            set 
            { 
                if (value == null)
                {
                    throw new ArgumentNullException("Exhibits list can`t be null");
                }
                exhibits = value;
            }
        }
        public void AddExhibit(Exhibit exhibit)
        {
            if (exhibit == null)
                throw new ArgumentNullException("Exhibit cannot be null.");
            exhibits.Add(exhibit);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Exhibition Hall: {nameOfHall}");
            if (exhibits.Count == 0)
            {
                sb.AppendLine("No exhibits in this hall.");
            }
            else
            {
                foreach (Exhibit exhibit in exhibits)
                {
                    sb.AppendLine($"{exhibit.WorkOfArt.NameOfArt}: ({exhibit.WorkOfArt.YearOfCreation}), cost: {exhibit.CostOfExhibit}, overall size: {exhibit.WorkOfArt.Length}x{exhibit.WorkOfArt.Width}x{exhibit.WorkOfArt.Height}$");
                }
            }
            return sb.ToString();
        }

        public string ToShortString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Exhibition Hall: {NameOfHall}");
            sb.AppendLine($"Exhibits: {Exhibits.Count} items from {Exhibits.Min(e => e.WorkOfArt.YearOfCreation)} to {Exhibits.Max(e => e.WorkOfArt.YearOfCreation)}");
            return sb.ToString();
        }
    }
}
