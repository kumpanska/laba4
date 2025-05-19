using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using System.Xml.Linq;

namespace lab4
{
    /// <summary>
    /// Interaction logic for ExhibitForm.xaml
    /// </summary>
    public partial class ExhibitForm : Window
    {
        public Exhibit ExhibitResult { get; private set; }
        private List<AWorkOfArt> originalArtworks;
        private List<Funds> originalFundsList;
        private List<AWorkOfArt> workingArtworks;
        private List<Funds> workingFundsList;
        private bool isSaved = false;
        private bool isDataChanged = false;
        private bool isEdit = false;
        public ExhibitForm(List<AWorkOfArt> artworks, List<Funds> funds)
        {
            InitializeComponent();
            this.originalArtworks = artworks;
            this.originalFundsList = funds;
            InitializeWorkingData(artworks, funds);
            isEdit = false;
        }

        public ExhibitForm(List<AWorkOfArt> artworks, List<Funds> fundsList, Exhibit existingExhibit)
        {
            InitializeComponent();
            this.originalArtworks = artworks;
            this.originalFundsList = fundsList;
            this.ExhibitResult = existingExhibit;
            InitializeWorkingData(artworks, fundsList);
            this.isEdit = true;

            AWorkOfArt workingArtwork = workingArtworks.FirstOrDefault(a =>
                a.NameOfArt == existingExhibit.WorkOfArt.NameOfArt &&
                a.YearOfCreation == existingExhibit.WorkOfArt.YearOfCreation);

            Funds workingFund = workingFundsList.FirstOrDefault(f =>
                f.Name == existingExhibit.Funds.Name &&
                f.Address == existingExhibit.Funds.Address);

            lstWorkOfArt.SelectedItem = workingArtwork;
            lstFund.SelectedItem = workingFund;
            cboPlacement.SelectedItem = existingExhibit.Placement;
            txtCost.Text = existingExhibit.CostOfExhibit.ToString();
        }
        private void InitializeWorkingData(List<AWorkOfArt> artworks, List<Funds> funds)
        {
            this.workingArtworks = CreateDeepCopyOfArtworks(artworks);
            this.workingFundsList = CreateDeepCopyOfFunds(funds);
            lstWorkOfArt.ItemsSource = workingArtworks;
            lstFund.ItemsSource = workingFundsList;
            cboPlacement.ItemsSource = Enum.GetValues(typeof(Placement));
        }
        private List<AWorkOfArt> CreateDeepCopyOfArtworks(List<AWorkOfArt> source)
        {
            List<AWorkOfArt> result = new List<AWorkOfArt>();
            foreach (var item in source)
            {
                result.Add(new AWorkOfArt(
                    item.NameOfArt,
                    item.YearOfCreation,
                    item.Width,
                    item.Height,
                    item.Depth));
            }
            return result;
        }
        private List<Funds> CreateDeepCopyOfFunds(List<Funds> source)
        {
            List<Funds> result = new List<Funds>();
            foreach (var item in source)
            {
                result.Add(new Funds(item.Name, item.Address));
            }
            return result;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateOnLostFocus()&&SaveExhibit())
            {
                ChangesToOriginalLists();
                isSaved = true;
                DialogResult = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            isSaved = true;
            DialogResult = false;
            Close();
        }
        private void ChangesToOriginalLists()
        {
            foreach (var workingArt in workingArtworks)
            {
                var existing = originalArtworks.FirstOrDefault(a =>
                    a.NameOfArt == workingArt.NameOfArt &&
                    a.YearOfCreation == workingArt.YearOfCreation);

                if (existing == null)
                {
                    originalArtworks.Add(new AWorkOfArt(
                        workingArt.NameOfArt,
                        workingArt.YearOfCreation,
                        workingArt.Width,
                        workingArt.Height,
                        workingArt.Depth));
                }
                else
                {
                    existing.Width = workingArt.Width;
                    existing.Height = workingArt.Height;
                    existing.Depth = workingArt.Depth;
                }
            }

            foreach (var workingFund in workingFundsList)
            {
                var existing = originalFundsList.FirstOrDefault(f =>
                    f.Name == workingFund.Name &&
                    f.Address == workingFund.Address);

                if (existing == null)
                {
                    originalFundsList.Add(new Funds(workingFund.Name, workingFund.Address));
                }
            }
        }


        private bool IsDataChanged()
        {
            if (!isEdit)
            {
                return lstWorkOfArt.SelectedItem != null &&
                       lstFund.SelectedItem != null &&
                       cboPlacement.SelectedItem != null &&
                       !string.IsNullOrWhiteSpace(txtCost.Text);
            }
            else
            {
                return (lstWorkOfArt.SelectedItem != null &&
                        ((AWorkOfArt)lstWorkOfArt.SelectedItem).NameOfArt != ExhibitResult.WorkOfArt.NameOfArt ||
                        lstFund.SelectedItem != null &&
                        ((Funds)lstFund.SelectedItem).Name != ExhibitResult.Funds.Name ||
                        cboPlacement.SelectedItem != null &&
                        (Placement)cboPlacement.SelectedItem != ExhibitResult.Placement ||
                        txtCost.Text != ExhibitResult.CostOfExhibit.ToString()
                      );
            }
        }

        private bool SaveExhibit()
        {
            try
            {
                if (lstWorkOfArt.SelectedItem == null || lstFund.SelectedItem == null || cboPlacement.SelectedItem == null || string.IsNullOrEmpty(txtCost.Text))
                {
                    throw new ArgumentException("All fields must be filled.");
                }

                if (!int.TryParse(txtCost.Text, out int parsedCost) || parsedCost <= 0)
                {
                    throw new ArgumentException("Cost must be a positive integer.");
                }

                AWorkOfArt selectedArtwork = lstWorkOfArt.SelectedItem as AWorkOfArt;
                Funds selectedFund = lstFund.SelectedItem as Funds;
                Placement selectedPlacement = (Placement)cboPlacement.SelectedItem;
                AWorkOfArt originalArtwork = originalArtworks.FirstOrDefault(a =>
                    a.NameOfArt == selectedArtwork.NameOfArt &&
                    a.YearOfCreation == selectedArtwork.YearOfCreation);

                if (originalArtwork == null)
                {
                    originalArtwork = new AWorkOfArt(
                        selectedArtwork.NameOfArt,
                        selectedArtwork.YearOfCreation,
                        selectedArtwork.Width,
                        selectedArtwork.Height,
                        selectedArtwork.Depth);
                    originalArtworks.Add(originalArtwork);
                }
                Funds originalFund = originalFundsList.FirstOrDefault(f =>
                    f.Name == selectedFund.Name &&
                    f.Address == selectedFund.Address);
                if (originalFund == null)
                {
                    originalFund = new Funds(selectedFund.Name, selectedFund.Address);
                    originalFundsList.Add(originalFund);
                }
                if (isEdit)
                {
                    ExhibitResult.WorkOfArt = originalArtwork;
                    ExhibitResult.Funds = originalFund;
                    ExhibitResult.Placement = selectedPlacement;
                    ExhibitResult.CostOfExhibit = parsedCost;
                }
                else
                {
                    ExhibitResult = new Exhibit(originalArtwork, originalFund, selectedPlacement, parsedCost);
                }

                isSaved = true;
                return true;
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
                return false;
            }
        }

        private void btnAddFund_Click(object sender, RoutedEventArgs e)
        {
            Funds newFund = new Funds("New Fund", "Default Address,10");

            FundsForm form = new FundsForm(newFund);
            if (form.ShowDialog() == true)
            {
                Funds saveFund = new Funds(form.FundResult.Name, form.FundResult.Address);
                workingFundsList.Add(saveFund);
                lstFund.ItemsSource = null;
                lstFund.ItemsSource = workingFundsList;
                lstFund.SelectedItem = saveFund;
                isDataChanged = true;
            }
        }

        private void btnAddWorkOfArt_Click(object sender, RoutedEventArgs e)
        {
            AWorkOfArt newArtwork = new AWorkOfArt("New Artwork", 2020, 10, 10, 10);
            AWorkOfArtForm form = new AWorkOfArtForm(newArtwork);
            if (form.ShowDialog() == true)
            {
                AWorkOfArt savedArtwork = new AWorkOfArt(
                    form.ArtworkResult.NameOfArt,
                    form.ArtworkResult.YearOfCreation,
                    form.ArtworkResult.Width,
                    form.ArtworkResult.Height,
                    form.ArtworkResult.Depth);
                workingArtworks.Add(savedArtwork);
                lstWorkOfArt.ItemsSource = null;
                lstWorkOfArt.ItemsSource = workingArtworks;
                lstWorkOfArt.SelectedItem = savedArtwork;
                isDataChanged = true;
            }
        }

        private void btnEditWorkOfArt_Click(object sender, RoutedEventArgs e)
        {
            AWorkOfArt selectedArtwork = lstWorkOfArt.SelectedItem as AWorkOfArt;
            if (selectedArtwork != null)
            {
                AWorkOfArt artworkCopy = new AWorkOfArt(
                    selectedArtwork.NameOfArt,
                    selectedArtwork.YearOfCreation,
                    selectedArtwork.Width,
                    selectedArtwork.Height,
                    selectedArtwork.Depth);

                AWorkOfArtForm form = new AWorkOfArtForm(artworkCopy);
                if (form.ShowDialog() == true)
                {
                    selectedArtwork.NameOfArt = artworkCopy.NameOfArt;
                    selectedArtwork.YearOfCreation = artworkCopy.YearOfCreation;
                    selectedArtwork.Width = artworkCopy.Width;
                    selectedArtwork.Height = artworkCopy.Height;
                    selectedArtwork.Depth = artworkCopy.Depth;
                    lstWorkOfArt.ItemsSource = null;
                    lstWorkOfArt.ItemsSource = workingArtworks;
                    lstWorkOfArt.SelectedItem = selectedArtwork;
                    isDataChanged = true;
                }
            }
            else
            {
                MessageBox.Show("Please select a work of art to edit.");
            }
        }

        private void btnEditFund_Click(object sender, RoutedEventArgs e)
        {
            Funds selectedFund = lstFund.SelectedItem as Funds;
            if (selectedFund != null)
            {
                Funds fundCopy = new Funds(selectedFund.Name, selectedFund.Address);
                FundsForm form = new FundsForm(fundCopy);
                if (form.ShowDialog() == true)
                {
                    selectedFund.Name = fundCopy.Name;
                    selectedFund.Address = fundCopy.Address;
                    lstFund.ItemsSource = null;
                    lstFund.ItemsSource = workingFundsList;
                    lstFund.SelectedItem = selectedFund;
                    isDataChanged = true;
                }
            }
            else
            {
                MessageBox.Show("Please select a fund to edit.");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == null && !isSaved && (IsDataChanged() || isDataChanged))
            {
                MessageBoxResult result = MessageBox.Show("Чи зберегти зміни?", "Збереження", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (!SaveExhibit())
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        ChangesToOriginalLists();
                        DialogResult = true;
                    }
                }
                else if (result == MessageBoxResult.No)
                {
                    DialogResult = false;
                }
            }
        }
        private void lstWorkOfArt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditWorkOfArt.IsEnabled = lstWorkOfArt.SelectedItem != null;
        }
        private void lstFund_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditFund.IsEnabled = lstFund.SelectedItem != null;
        }
        private bool ValidateExhibitWithAttributes(Exhibit exhibit, out string errorMessage)
        {
            var context = new ValidationContext(exhibit);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            bool isValid = Validator.TryValidateObject(exhibit, context, results, validateAllProperties: true);

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
        private bool ValidateOnLostFocus()
        {
            try
            {
                if (lstWorkOfArt.SelectedItem == null)
                {
                    txtError.Text = "Select a work of art";
                    return false;
                }

                if (lstFund.SelectedItem == null)
                {
                    txtError.Text = "Select a fund";
                    return false;
                }

                if (cboPlacement.SelectedItem == null)
                {
                    txtError.Text = "Select a placement";
                    return false;
                }

                if (!int.TryParse(txtCost.Text, out int cost))
                {
                    txtError.Text = "Enter a valid cost";
                    return false;
                }

                var selectedArtwork = lstWorkOfArt.SelectedItem as AWorkOfArt;
                var selectedFund = lstFund.SelectedItem as Funds;
                var selectedPlacement = (Placement)cboPlacement.SelectedItem;

                Exhibit exhibit = new Exhibit(selectedArtwork, selectedFund, selectedPlacement, cost);

                if (!ValidateExhibitWithAttributes(exhibit, out string errorMessage))
                {
                    txtError.Text = errorMessage;
                    return false;
                }

                txtError.Text = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
                return false;
            }
        }


        private void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateOnLostFocus();
        }

    }
}