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
            this.nameOfHall = nameOfHall;
            this.exhibits = new List<Exhibit>();
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
        public List<Exhibit> Exhibits => exhibits;
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
                    sb.AppendLine($"{exhibit.WorkOfArt.NameOfArt}: ({exhibit.WorkOfArt.YearOfCreation}), cost: {exhibit.CostOfExhibit} $");
                }
            }
            return sb.ToString();
        }

        public string ToShortString()
        {
            if (exhibits.Count == 0)
                return $"{nameOfHall}: no exhibits.";
            return $"{nameOfHall}: creation year from {exhibits.First().WorkOfArt.YearOfCreation} to {exhibits.Last().WorkOfArt.YearOfCreation}";
        }
    }
}
