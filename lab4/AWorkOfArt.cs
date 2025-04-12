using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab4
{
    public class AWorkOfArt
    {
        private string nameOfArt;
        private int yearOfCreation;
        private double height, width, length;
        public AWorkOfArt(string nameOfArt, int yearOfCreation, double width, double height, double length)
        {
            this.nameOfArt = nameOfArt;
            this.yearOfCreation = yearOfCreation;
            this.width = width;
            this.height = height;
            this.length = length;
        }
        public string NameOfArt
        {
            get { return nameOfArt; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name of a work of art can't be empty");
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
        public double Length
        {
            get { return length; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Length must be greater than zero");
                length = value;
            }
        }

    }
}
