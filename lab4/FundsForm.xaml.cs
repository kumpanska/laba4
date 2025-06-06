﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel.DataAnnotations;
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
        private Funds newFund;
        private bool isSaved = false;
        private bool isDataChanged = false;
        private bool isEdit = false;
        public Funds FundResult => newFund;
        public FundsForm()
        {
            InitializeComponent();
            newFund = new Funds("New Fund", "Default Address,10");
            txtName.Text = newFund.Name;
            txtAddress.Text = newFund.Address;
            isEdit = false;
        }
        public FundsForm(Funds fundToEdit)
        {
            InitializeComponent();
            this.fundToEdit = fundToEdit;
            newFund = new Funds(fundToEdit.Name, fundToEdit.Address);
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
                Funds tempFund = new Funds(fundName,address);
                if (!ValidateFundWithAttributes(tempFund, out string errorMessage))
                {
                    txtError.Text = errorMessage;
                    return false;
                }
                newFund.Name = fundName;
                newFund.Address = address;
                if (isEdit && fundToEdit != null)
                {
                    fundToEdit.Name = newFund.Name;
                    fundToEdit.Address = newFund.Address;
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
                    MessageBoxButton.YesNo,
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
            }
        }
        private bool ValidateFundWithAttributes(Funds fund, out string errorMessage)
        {
            var context = new ValidationContext(fund);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(fund, context, results, validateAllProperties: true);

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

        private void txtAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateOnLostFocus();
        }

        private void ValidateOnLostFocus()
        {
            try
            {
                var tempFund = new Funds(txtName.Text, txtAddress.Text);
                if (!ValidateFundWithAttributes(tempFund, out string validationErrors))
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
