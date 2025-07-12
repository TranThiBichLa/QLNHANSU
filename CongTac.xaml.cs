using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLNHANSU
{
    /// <summary>
    /// Interaction logic for CongTac.xaml
    /// </summary>
    public partial class CongTac : UserControl
    {
        private CongTacBLL congTacBLL;
        private ObservableCollection<DTO.CongTac> congTacList;

        public CongTac()
        {
            InitializeComponent();
            congTacBLL = new CongTacBLL();
            congTacList = new ObservableCollection<DTO.CongTac>();
            CongTacDataGrid.ItemsSource = congTacList;
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var congTacBLL = new CongTacBLL();
                var data = await Task.Run(() => congTacBLL.GetAll());
                congTacList.Clear();
                foreach (var item in data)
                {
                    congTacList.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu công tác: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private async void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string maNhanVien = txtMaNhanVien.Text.Trim(); // Assuming you named the TextBox correctly
                string maHD = txtMaHD.Text.Trim();
                string trangThai = cbTrangThai.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : null;

                var results = await Task.Run(() => congTacBLL.Search(maNhanVien, maHD, trangThai));
                congTacList.Clear();
                if (results.Any())
                {
                    foreach (var item in results)
                    {
                        congTacList.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("No matching results found.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during search: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void cbTrangThai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Get the selected status from the ComboBox
                string selectedStatus = ((ComboBox)sender).SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : null;

                // Optional: Validate if there's a valid selection
                if (string.IsNullOrEmpty(selectedStatus))
                {
                    return;
                }

                // Perform a search or filter based on the selected status
                var results = await Task.Run(() => congTacBLL.Search(null, null, selectedStatus));

                // Update the ObservableCollection to reflect the filtered data
                congTacList.Clear();
                foreach (var item in results)
                {
                    congTacList.Add(item);
                }

                // Optionally, notify the user if no matching records are found
                if (results.Count == 0)
                {
                    MessageBox.Show($"No records found for status: {selectedStatus}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during filtering: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}