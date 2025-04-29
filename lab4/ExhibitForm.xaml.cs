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
    /// Interaction logic for ExhibitForm.xaml
    /// </summary>
    public partial class ExhibitForm : Window
    {
        public Exhibit ExhibitResult { get; private set; }
        private List<AWorkOfArt> artworks;
        private List<Funds> fundsList;
        private bool isSaved = false;
        private bool isDataChanged = false;
        private bool isEdit = false;
        public ExhibitForm(List<AWorkOfArt> artworks, List<Funds> funds)
        {
            InitializeComponent();
            this.artworks = artworks;
            this.fundsList = funds;
            lstWorkOfArt.ItemsSource = artworks;
            lstFund.ItemsSource = funds;
            cboPlacement.ItemsSource = Enum.GetValues(typeof(Placement));
            this.Title = "Create New Exhibit";
        }
        public ExhibitForm(List<AWorkOfArt> artworks, List<Funds> fundsList, Exhibit existingExhibit)
        {
            InitializeComponent();
            this.artworks = artworks;
            this.fundsList = fundsList;
            this.ExhibitResult = existingExhibit;
            this.isEdit = true;
            lstWorkOfArt.ItemsSource = artworks;
            lstFund.ItemsSource = fundsList;
            cboPlacement.ItemsSource = Enum.GetValues(typeof(Placement));
            lstWorkOfArt.SelectedItem = existingExhibit.WorkOfArt;
            lstFund.SelectedItem = existingExhibit.Funds;
            cboPlacement.SelectedItem = existingExhibit.Placement;
            txtCost.Text = existingExhibit.CostOfExhibit.ToString();
            this.Title = "Edit Exhibit";

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveExhibit())
            {
                DialogResult = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
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
                return (lstWorkOfArt.SelectedItem != ExhibitResult.WorkOfArt ||
                      lstFund.SelectedItem != ExhibitResult.Funds ||
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
                if (isEdit)
                {
                    ExhibitResult.WorkOfArt = selectedArtwork;
                    ExhibitResult.Funds = selectedFund;
                    ExhibitResult.Placement = selectedPlacement;
                    ExhibitResult.CostOfExhibit = parsedCost;
                }
                else
                {
                    ExhibitResult = new Exhibit(selectedArtwork, selectedFund, selectedPlacement, parsedCost);
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
                Funds saveFund = form.FundResult;
                fundsList.Add(saveFund);
                lstFund.ItemsSource = null;
                lstFund.ItemsSource = fundsList;
                isDataChanged = true;
            }
        }

        private void btnAddWorkOfArt_Click(object sender, RoutedEventArgs e)
        {
            AWorkOfArt newArtwork = new AWorkOfArt("New Artwork", 2020, 10, 10, 10);
            AWorkOfArtForm form = new AWorkOfArtForm(newArtwork);
            if (form.ShowDialog() == true)
            {
                AWorkOfArt savedArtwork = form.ArtworkResult;
                artworks.Add(savedArtwork);
                lstWorkOfArt.ItemsSource = null;
                lstWorkOfArt.ItemsSource = artworks;
                isDataChanged = true;
            }
        }

        private void btnEditWorkOfArt_Click(object sender, RoutedEventArgs e)
        {
            AWorkOfArt selectedArtwork = lstWorkOfArt.SelectedItem as AWorkOfArt;
            if (selectedArtwork != null)
            {
                AWorkOfArtForm form = new AWorkOfArtForm(selectedArtwork);
                if (form.ShowDialog() == true)
                {
                    lstWorkOfArt.ItemsSource = null;
                    lstWorkOfArt.ItemsSource = artworks;
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
                FundsForm form = new FundsForm(selectedFund);
                if (form.ShowDialog() == true)
                {
                    lstFund.ItemsSource = null;
                    lstFund.ItemsSource = fundsList;
                }
            }
            else
            {
                MessageBox.Show("Please select a fund to edit.");
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isSaved && (IsDataChanged() || isDataChanged))
            {
                MessageBoxResult result = MessageBox.Show("Чи зберегти зміни?", "Збереження", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (!SaveExhibit())
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
