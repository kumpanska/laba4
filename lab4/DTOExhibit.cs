using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class DTOExhibit
    {
        public DTOAWorkOfArt AWorkOfArt { get; set; }
        public DTOFunds Fund { get; set; }
        public Placement Placement { get; set; }
        public int CostOfExhibit { get; set; }
    }
}
