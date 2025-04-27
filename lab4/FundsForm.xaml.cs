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
        private Funds currentFund;
        private bool isSaved = false;
        public Funds FundResult => currentFund;
        public FundsForm(Funds fund)
        {
            InitializeComponent();
            currentFund = fund;
            originalFundName = fund.Name;
            originalAddress = fund.Address;
            txtName.Text = fund.Name;
            txtAddress.Text = fund.Address;
        }
        private bool IsDataChanged()
        {
            return txtName.Text != originalFundName || txtAddress.Text != originalAddress;
        }
        private bool SaveFund()
        {
            try
            {
                string fundName = txtName.Text;
                string address = txtAddress.Text;
                currentFund.Name = fundName;
                currentFund.Address = address;
                DTOFunds dto = FundsMapper.ToDTO(currentFund);
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
            if (!isSaved && IsDataChanged())
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
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
