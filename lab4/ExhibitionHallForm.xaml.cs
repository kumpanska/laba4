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
    /// Interaction logic for ExhibitionHallForm.xaml
    /// </summary>
    public partial class ExhibitionHallForm : Window
    {
        private ExhibitionHall hall;
        private List<Exhibit> exhibits;
        private string originalHallName;
        bool isSaved = false;
        public ExhibitionHallForm(ExhibitionHall hall)
        {
            InitializeComponent();
            this.hall = hall;
            this.exhibits = hall.Exhibits;
            this.originalHallName = hall.NameOfHall;
            txtHallName.Text = hall.NameOfHall;
            UpdateExhibitList();
        }
        private void UpdateExhibitList()
        {
            listExhibits.Items.Clear();
            listExhibits.Items.Add(hall.ToShortString());
            listExhibits.Items.Add(new string('-', 50));
            foreach (var exhibit in exhibits)
            {
                listExhibits.Items.Add(exhibit.ToString());
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            List<AWorkOfArt> artworks = exhibits.Select(ex => ex.WorkOfArt).Distinct().ToList();
            List<Funds> fundsList = exhibits.Select(ex => ex.Funds).Distinct().ToList();
            ExhibitForm form = new ExhibitForm(artworks, fundsList);
            if (form.ShowDialog() == true)
            {
                hall.AddExhibit(form.ExhibitResult);
                exhibits = hall.Exhibits;
                UpdateExhibitList();
            }

        }
        private void listExhibits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEdit.IsEnabled = listExhibits.SelectedItem != null && listExhibits.SelectedIndex >= 2;
        }
        
        private bool SaveHall()
        {
            try
            {
                string hallName = txtHallName.Text;

                if (string.IsNullOrEmpty(hallName))
                    throw new ArgumentException("Hall name cannot be empty.");

                hall.NameOfHall = hallName;
                hall.Exhibits = exhibits;
                isSaved = true;
                return true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        private bool IsDataChanged()
        {
            return txtHallName.Text != originalHallName || isSaved == false;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveHall())
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isSaved && IsDataChanged())
            {
                MessageBoxResult result = MessageBox.Show("Чи зберегти зміни?", "Збереження", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (!SaveHall())
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
           
        }

    }
}
