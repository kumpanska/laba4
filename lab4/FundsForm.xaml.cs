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
    /// Interaction logic for FundsForm.xaml
    /// </summary>
    public partial class FundsForm : Window
    {
        private string originalFundName;
        private string originalAddress;
        private Funds fundToEdit;
        private Funds currentFund;
        private bool isSaved = false;
        private bool isDataChanged = false;
        private bool isEdit = false;
        public Funds FundResult => currentFund;
        public FundsForm()
        {
            InitializeComponent();
            this.Title = "Create New Fund";
            currentFund = new Funds("New Fund", "Default Address,10");
            txtName.Text = currentFund.Name;
            txtAddress.Text = currentFund.Address;
            isEdit = false;
        }
        public FundsForm(Funds fundToEdit)
        {
            InitializeComponent();
            this.Title = "Edit Fund";
            this.fundToEdit = fundToEdit;
            currentFund = new Funds(fundToEdit.Name, fundToEdit.Address);
            originalFundName = fundToEdit.Name;
            originalAddress = fundToEdit.Address;
            txtName.Text = fundToEdit.Name;
            txtAddress.Text = fundToEdit.Address;
            isEdit = true;
        }

        private bool IsDataChanged()
        {
            return txtName.Text != originalFundName ||
                   txtAddress.Text != originalAddress ||
                   isDataChanged;
        }
        private bool SaveFund()
        {
            try
            {
                string fundName = txtName.Text;
                string address = txtAddress.Text;

                if (string.IsNullOrWhiteSpace(fundName))
                    throw new ArgumentException("Fund name cannot be empty.");
                if (string.IsNullOrWhiteSpace(address))
                    throw new ArgumentException("Address cannot be empty.");
                if (!fundName.All(c => char.IsLetter(c) && char.IsWhiteSpace(c)))
                    throw new ArgumentException("Fund name must contain only letters and spaces.");
                currentFund.Name = fundName;
                currentFund.Address = address;

                if (isEdit && fundToEdit != null)
                {
                    fundToEdit.Name = currentFund.Name;
                    fundToEdit.Address = currentFund.Address;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveFund())
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == null && !isSaved && IsDataChanged())
            {
                MessageBoxResult result = MessageBox.Show(
                    "Чи зберегти зміни?",
                    "Збереження",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.Yes)
                {
                    if (!SaveFund())
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
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
