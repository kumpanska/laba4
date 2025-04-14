using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public static class ExhibitMapper
    {
        public static Exhibit FromDTO(DTOExhibit dto)
        {
            return new Exhibit(
                AWorkOfArtMapper.FromDTO(dto.AWorkOfArt),
                FundsMapper.FromDTO(dto.Fund),
                dto.Placement,
                dto.CostOfExhibit
            );
        }
        public static DTOExhibit ToDTO(Exhibit exhibit)
        {
            return new DTOExhibit
            {
                AWorkOfArt = AWorkOfArtMapper.ToDTO(exhibit.WorkOfArt), 
                Fund = FundsMapper.ToDTO(exhibit.Funds),                
                Placement = exhibit.Placement,                           
                CostOfExhibit = exhibit.CostOfExhibit
            };
        }
      
    }
}
