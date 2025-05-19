using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace lab4
{
    public class AWorkOfArt
    {
        private string nameOfArt;
        private int yearOfCreation;
        private double height, width, depth;
        public AWorkOfArt(string nameOfArt, int yearOfCreation, double width, double height, double depth)
        {
            this.NameOfArt = nameOfArt;
            this.YearOfCreation = yearOfCreation;
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
        }
        public string NameOfArt
        {
            get { return nameOfArt; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name of a work of art can't be empty");
                if (!value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                    throw new ArgumentException("Name of a work of art must contain only letters and spaces.");
                nameOfArt = value;
            }
        }
        public int YearOfCreation
        {
            get { return yearOfCreation; }
            set
            {
                if (value<=0)
                    throw new ArgumentException("Year of creation must be greater than zero");
                yearOfCreation = value;
            }
        }
       
        [Required(ErrorMessage ="Width is required")]
        [Range(10,5000, ErrorMessage ="Value for width must be between 10 and 5000.")]
        public double Width
        {
            get { return width; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Width must be greater than zero");
                width = value;
            }
        }
        [Required(ErrorMessage = "Height is required")]
        [Range(10, 2000, ErrorMessage = "Value for height must be between 20 and 2000.")]
        public double Height
        {
            get { return height; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Height must be greater than zero");
                height = value;
            }
        }
        [Required(ErrorMessage = "Depth is required")]
        [Range(0.5, 15, ErrorMessage = "Value for depth must be between 0,5 and 15.")]
        public double Depth
        {
            get { return depth; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Depth must be greater than zero");
                depth = value;
            }
        }

    }
}
