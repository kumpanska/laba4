using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private List<Exhibit> editedExhibits;
        private List<Exhibit> originalExhibits;
        private string originalHallName;
        private bool isSaved = false;
        private bool isDataChanged = false;

        public ExhibitionHallForm(ExhibitionHall hall)
        {
            InitializeComponent();
            this.hall = hall;
            this.originalExhibits = hall.Exhibits.ToList();
            this.editedExhibits = new List<Exhibit>();
            foreach (var exhibit in hall.Exhibits)
            {
                Exhibit copy = CreateExhibitCopy(exhibit);
                editedExhibits.Add(copy);
            }

            this.originalHallName = hall.NameOfHall;
            txtHallName.Text = hall.NameOfHall;

            UpdateExhibitList();
        }
        private Exhibit CreateExhibitCopy(Exhibit original)
        {
            return new Exhibit(
                original.WorkOfArt,
                original.Funds,
                original.Placement,
                original.CostOfExhibit
            );
        }

        private void UpdateExhibitList()
        {
            listExhibits.Items.Clear();
            listExhibits.Items.Add(hall.ToShortString());
            listExhibits.Items.Add(new string('-', 50));
            foreach (var exhibit in editedExhibits)
            {
                listExhibits.Items.Add(exhibit.ToString());
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            List<AWorkOfArt> artworks = editedExhibits.Select(ex => ex.WorkOfArt).Distinct().ToList();
            List<Funds> fundsList = editedExhibits.Select(ex => ex.Funds).Distinct().ToList();
            ExhibitForm form = new ExhibitForm(artworks, fundsList);
            if (form.ShowDialog() == true)
            {
                editedExhibits.Add(form.ExhibitResult);
                isDataChanged = true;
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
                hall.Exhibits.Clear();
                foreach (var exhibit in editedExhibits)
                {
                    hall.Exhibits.Add(exhibit);
                }

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
            if (txtHallName.Text != originalHallName)
                return true;
            if (editedExhibits.Count != originalExhibits.Count)
                return true;
            if (isDataChanged)
                return true;
            for (int i = 0; i < editedExhibits.Count; i++)
            {
                var originalExhibit = originalExhibits[i];
                var editedExhibit = editedExhibits[i];

                if (editedExhibit.WorkOfArt != originalExhibit.WorkOfArt ||
                    editedExhibit.Funds != originalExhibit.Funds ||
                    editedExhibit.Placement != originalExhibit.Placement ||
                    editedExhibit.CostOfExhibit != originalExhibit.CostOfExhibit)
                {
                    return true;
                }
            }

            return false;
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (DialogResult == null && IsDataChanged() && !isSaved)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Чи зберегти зміни?",
                    "Збереження",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (!SaveHall())
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = listExhibits.SelectedIndex;
            if (selectedIndex < 2 || selectedIndex >= listExhibits.Items.Count)
            {
                MessageBox.Show("Please select a valid exhibit to edit.", "Edit Exhibit", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            Exhibit selectedExhibit = editedExhibits[selectedIndex - 2];
            List<AWorkOfArt> artworks = editedExhibits.Select(ex => ex.WorkOfArt).Distinct().ToList();
            List<Funds> fundsList = editedExhibits.Select(ex => ex.Funds).Distinct().ToList();

            ExhibitForm form = new ExhibitForm(artworks, fundsList, selectedExhibit);
            if (form.ShowDialog() == true)
            {
                isDataChanged = true;
                UpdateExhibitList();
            }
        }

    }
}
