using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Work of art is required.")]
        public AWorkOfArt WorkOfArt
        {
            get { return workOfArt; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("WorkOfArt cannot be null.");
                workOfArt = value;
            }
        }
        [Required(ErrorMessage = "Funds information is required.")]
        public Funds Funds
        {
            get { return funds; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Funds cannot be null.");
                funds = value;
            }
        }

        public Placement Placement
        { get { return placement; }
            set { placement = value; }
        }
        [Range(1000, 3000000, ErrorMessage = "Value for cost must be between 1000 and 3000000.")]
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
