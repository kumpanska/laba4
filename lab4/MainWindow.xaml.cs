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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.IO;
namespace lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string savePath = "data.json";
        private ExhibitionHall currentHall;
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            
        }
        private void LoadData()
        {
            try
            {
                string json = File.ReadAllText(savePath);
                DTOExhibitionHall dto = JsonSerializer.Deserialize<DTOExhibitionHall>(json);
                currentHall = ExhibitionHallMapper.FromDTO(dto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading data: " + ex.Message);
                currentHall = new ExhibitionHall("Error Hall");
            }
        }
        private void SaveData()
        {
            try 
            {
                DTOExhibitionHall dto = ExhibitionHallMapper.ToDTO(currentHall);
                string json = JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(savePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving data: " + ex.Message);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveData();
        }

        private void OpenHall_Click(object sender, RoutedEventArgs e)
        {
            ExhibitionHallForm form = new ExhibitionHallForm(currentHall);
            form.ShowDialog();
        }
    }
}
