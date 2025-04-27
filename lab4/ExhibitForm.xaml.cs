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
        public ExhibitForm(List<AWorkOfArt> artworks, List<Funds> funds)
        {
            InitializeComponent();
            this.artworks = artworks;
            this.fundsList = funds;
            cboWorkOfArt.ItemsSource = artworks;
            cboFund.ItemsSource = funds;
            cboPlacement.ItemsSource = Enum.GetValues(typeof(Placement));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnAddFund_Click(object sender, RoutedEventArgs e)
        {
            Funds newFund = new Funds("","");
            FundsForm form = new FundsForm(newFund);
            if (form.ShowDialog() == true)
            {
                Funds saveFund = form.FundResult;
                fundsList.Add(saveFund);
                cboFund.ItemsSource = null;
                cboFund.ItemsSource = fundsList;
            }
        }

        private void btnAddWorkOfArt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
