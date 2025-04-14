using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public static class AWorkOfArtMapper
    {
        public static AWorkOfArt FromDTO(DTOAWorkOfArt dto)
        {
            return new AWorkOfArt(
                dto.NameOfAWorkOfArt,
                dto.YearOfCreation,
                dto.Width,
                dto.Height,
                dto.Length
            );
        }
        public static DTOAWorkOfArt ToDTO(AWorkOfArt artwork)
        {
            return new DTOAWorkOfArt
            {
                NameOfAWorkOfArt = artwork.NameOfArt,
                YearOfCreation = artwork.YearOfCreation,
                Width = artwork.Width,
                Height = artwork.Height,
                Length = artwork.Length
            };
        }
    }
}
