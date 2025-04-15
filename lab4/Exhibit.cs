using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Exhibit
    {
        private AWorkOfArt workOfArt;
        private Funds funds;
        private Placement placement;
        private int costOfExhibit;
        public Exhibit(AWorkOfArt workOfArt, Funds funds, Placement placement, int costOfExhibit)
        {
            this.WorkOfArt = workOfArt;
            this.Funds = funds;
            this.Placement = placement;
            this.CostOfExhibit = costOfExhibit;
        }
        public AWorkOfArt WorkOfArt
        {
            get { return workOfArt; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("AWorkOfArt cannot be null.");
                workOfArt = value;
            }
        }

        public Funds Funds
        {
            get { return funds; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Fund cannot be null.");
                funds = value;
            }
        }
        public Placement Placement
        { get { return placement; }
            set { placement = value; }
        }

        public int CostOfExhibit
        {
            get { return costOfExhibit; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Cost must be greater than zero.");
                costOfExhibit = value;
            }
        }
    }
}
