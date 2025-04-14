using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class DTOExhibitionHall
    {
        public string NameOfExhibitionHall { get; set; }
        public List<DTOExhibit> Exhibits { get; set; } = new List<DTOExhibit>();
    }
}
