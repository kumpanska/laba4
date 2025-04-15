using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public static class ExhibitionHallMapper
    {
        public static ExhibitionHall FromDTO(DTOExhibitionHall dto)
        {
            ExhibitionHall hall = new ExhibitionHall(dto.NameOfExhibitionHall);

            foreach (DTOExhibit dtoExhibit in dto.Exhibits)
            {
                Exhibit exhibit = ExhibitMapper.FromDTO(dtoExhibit);
                hall.AddExhibit(exhibit);
            }
            return hall;
        }
        public static DTOExhibitionHall ToDTO(ExhibitionHall hall)
        {
            DTOExhibitionHall dto = new DTOExhibitionHall();
            dto.NameOfExhibitionHall = hall.NameOfHall;
            dto.Exhibits = new List<DTOExhibit>();
            foreach (Exhibit exhibit in hall.Exhibits)
            {
                DTOExhibit dtoExhibit = ExhibitMapper.ToDTO(exhibit);
                dto.Exhibits.Add(dtoExhibit);
            }

            return dto;
        }
    }
}
