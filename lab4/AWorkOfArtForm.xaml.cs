using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace lab4
{
    /// <summary>
    /// Interaction logic for AWorkOfArtForm.xaml
    /// </summary>
    public partial class AWorkOfArtForm : Window
    {
        private string originalName;
        private int originalYear;
        private double originalWidth, originalHeight, originalDepth;
        private AWorkOfArt currentArtwork;
        private AWorkOfArt artworkToEdit;
        private bool isSaved = false;
        private bool isEdit = false;
        public AWorkOfArtForm()
        {
            InitializeComponent();
            currentArtwork = new AWorkOfArt("New Artwork", 2020, 10, 10, 10);
            txtName.Text = currentArtwork.NameOfArt;
            txtYear.Text = currentArtwork.YearOfCreation.ToString();
            txtWidth.Text = currentArtwork.Width.ToString();
            txtHeight.Text = currentArtwork.Height.ToString();
            txtDepth.Text = currentArtwork.Depth.ToString();
            isEdit = false;
        }
        public AWorkOfArtForm(AWorkOfArt artworkToEdit)
        {
            InitializeComponent();
            this.artworkToEdit = artworkToEdit;
            currentArtwork = new AWorkOfArt(
                artworkToEdit.NameOfArt,
                artworkToEdit.YearOfCreation,
                artworkToEdit.Width,
                artworkToEdit.Height,
                artworkToEdit.Depth
            );

            originalName = artworkToEdit.NameOfArt;
            originalYear = artworkToEdit.YearOfCreation;
            originalWidth = artworkToEdit.Width;
            originalHeight = artworkToEdit.Height;
            originalDepth = artworkToEdit.Depth;

            txtName.Text = artworkToEdit.NameOfArt;
            txtYear.Text = artworkToEdit.YearOfCreation.ToString();
            txtWidth.Text = artworkToEdit.Width.ToString();
            txtHeight.Text = artworkToEdit.Height.ToString();
            txtDepth.Text = artworkToEdit.Depth.ToString();
            isEdit = true;
        }
        public AWorkOfArt ArtworkResult => currentArtwork;
        private bool IsDataChanged()
        {
            return txtName.Text != originalName
                || txtYear.Text != originalYear.ToString()
                || txtWidth.Text != originalWidth.ToString()
                || txtHeight.Text != originalHeight.ToString()
                || txtDepth.Text != originalDepth.ToString();
        }
        private bool SaveArtwork()
        {
            try
            {
                if (!int.TryParse(txtYear.Text, out int year))
                    throw new ArgumentException("Year must be a valid integer.");
                if (!double.TryParse(txtWidth.Text, out double width))
                    throw new ArgumentException("Width must be a valid number.");
                if (!double.TryParse(txtHeight.Text, out double height))
                    throw new ArgumentException("Height must be a valid number.");
                if (!double.TryParse(txtDepth.Text, out double depth))
                    throw new ArgumentException("Depth must be a valid number.");

                var tempArtwork = new AWorkOfArt(txtName.Text, year, width, height, depth);
                if (!ValidateWorkOfArtWithAttributes(tempArtwork, out string validationErrors))
                {
                    txtError.Text = validationErrors;
                    return false;
                }
                currentArtwork.NameOfArt = tempArtwork.NameOfArt;
                currentArtwork.YearOfCreation = tempArtwork.YearOfCreation;
                currentArtwork.Width = tempArtwork.Width;
                currentArtwork.Height = tempArtwork.Height;
                currentArtwork.Depth = tempArtwork.Depth;

                if (isEdit)
                {
                    artworkToEdit.NameOfArt = currentArtwork.NameOfArt;
                    artworkToEdit.YearOfCreation = currentArtwork.YearOfCreation;
                    artworkToEdit.Width = currentArtwork.Width;
                    artworkToEdit.Height = currentArtwork.Height;
                    artworkToEdit.Depth = currentArtwork.Depth;
                }
                isSaved = true;
                txtError.Text = string.Empty;
                return true;
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
                return false;
            }
            catch (FormatException ex)
            {
                txtError.Text = "Invalid data: " + ex.Message;
                return false;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveArtwork())
            {
                isSaved = true;
                DialogResult = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            isSaved = true;
            DialogResult = false;
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isSaved && IsDataChanged())
            {
                MessageBoxResult result = MessageBox.Show("Чи зберегти зміни?", "Збереження", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (!SaveArtwork())
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        DialogResult = true;
                    }
                }
                else if (result == MessageBoxResult.No)
                {
                    DialogResult = false;
                }
            }
        }
        private bool ValidateWorkOfArtWithAttributes(AWorkOfArt workOfArt, out string errorMessage)
        {
            var context = new ValidationContext(workOfArt);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(workOfArt, context, results, validateAllProperties: true);

            if (!isValid)
            {
                errorMessage = string.Join(Environment.NewLine, results.Select(r => r.ErrorMessage));
            }
            else
            {
                errorMessage = string.Empty;
            }

            return isValid;
        }
        private void txtName_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateOnLostFocus();
        }
        private void txtYear_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateOnLostFocus();
        }
        private void txtWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateOnLostFocus();
        }
        private void txtHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateOnLostFocus();
        }
        private void txtDepth_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateOnLostFocus();
        }
        private void ValidateOnLostFocus()
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
                if (!double.TryParse(txtDepth.Text, out double depth))
                    throw new ArgumentException("Depth must be a valid number.");
                
                var tempArtwork = new AWorkOfArt(name, year, width, height, depth);

                if (!ValidateWorkOfArtWithAttributes(tempArtwork, out string validationErrors))
                {
                    txtError.Text = validationErrors;
                }
                else
                {
                    txtError.Text = string.Empty;
                }
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }

    }
}
