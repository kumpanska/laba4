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
                    sb.AppendLine($"Exhibit: {exhibit.WorkOfArt.NameOfArt}");
                    sb.AppendLine($"Fund: {exhibit.Funds.Name}, address: {exhibit.Funds.Address}");
                    sb.AppendLine($"Exhibit Dimensions: {exhibit.WorkOfArt.Depth}x{exhibit.WorkOfArt.Width}x{exhibit.WorkOfArt.Height}");
                    sb.AppendLine($"Cost of Exhibit: {exhibit.CostOfExhibit}");
                    sb.AppendLine($"Creation Year: {exhibit.WorkOfArt.YearOfCreation}");
                    sb.AppendLine($"Placement: {exhibit.Placement}");
                    sb.AppendLine("--------------");
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
