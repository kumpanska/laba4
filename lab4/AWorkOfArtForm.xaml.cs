using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab4
{
    /// <summary>
    /// Interaction logic for AWorkOfArtForm.xaml
    /// </summary>
    public partial class AWorkOfArtForm : Window
    {
        private string originalName;
        private int originalYear;
        private double originalWidth, originalHeight, originalLength;
        private AWorkOfArt currentArtwork;
        private bool isSaved = false;
        public AWorkOfArtForm(AWorkOfArt artwork)
        {
            InitializeComponent();
            currentArtwork = artwork;
            originalName = artwork.NameOfArt;
            originalYear = artwork.YearOfCreation;
            originalWidth = artwork.Width;
            originalHeight = artwork.Height;
            originalLength = artwork.Length;
            txtName.Text = artwork.NameOfArt;
            txtYear.Text = artwork.YearOfCreation.ToString();
            txtWidth.Text = artwork.Width.ToString();
            txtHeight.Text = artwork.Height.ToString();
            txtLength.Text = artwork.Length.ToString();
        }
        private bool IsDataChanged()
        {
            return txtName.Text != originalName
                || txtYear.Text != originalYear.ToString()
                || txtWidth.Text != originalWidth.ToString()
                || txtHeight.Text != originalHeight.ToString()
                || txtLength.Text != originalLength.ToString();
        }
        private bool SaveArtwork()
        {
            try
            {
                string name = txtName.Text;
                if (!int.TryParse(txtYear.Text, out int year))
                    throw new ArgumentException("Year must be a valid integer.");
                if (!double.TryParse(txtWidth.Text, out double width))
                    throw new ArgumentException("Width must be a valid number.");
                if (!double.TryParse(txtHeight.Text, out double height))
                    throw new ArgumentException("Height must be a valid number.");
                if (!double.TryParse(txtLength.Text, out double length))
                    throw new ArgumentException("Length must be a valid number.");

                currentArtwork.NameOfArt = name;
                currentArtwork.YearOfCreation = year;
                currentArtwork.Width = width;
                currentArtwork.Height = height;
                currentArtwork.Length = length;

                DTOAWorkOfArt dto = AWorkOfArtMapper.ToDTO(currentArtwork);

                isSaved = true;
                return true;
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
                return false;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveArtwork())
            {
                DialogResult = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
        private void Window_CLosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isSaved && IsDataChanged())
            {
                MessageBoxResult result = MessageBox.Show("Чи зберегти зміни?", "Збереження", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (!SaveArtwork())
                    {
                        e.Cancel = true;
                    }
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
