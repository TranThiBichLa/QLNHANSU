using BLL;
using DTO;
using DAL;
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

namespace QLNHANSU
{
    /// <summary>
    /// Interaction logic for TangCa.xaml
    /// </summary>
    public partial class TangCa : UserControl
    {
        private readonly TangCaBLL tangCaBLL;

        public TangCa()
        {
            InitializeComponent();
            tangCaBLL = new TangCaBLL();
            LoadTangCaData();
        }

        private void LoadTangCaData()
        {
            try
            {
                List<DTO.TangCa> tangCaRecords = tangCaBLL.GetAllTangCaRecords();
                TangCaDataGrid.ItemsSource = tangCaRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xử lý sự kiện khi nhấn nút "Tìm Kiếm"
        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text; // Lấy mã nhân viên từ TextBox
            string loaiCa = cbLoaiCa.SelectedItem is ComboBoxItem selectedItem ? selectedItem.Content.ToString() : "Tất cả";

            try
            {
                List<DTO.TangCa> tangCaRecords = tangCaBLL.GetFilteredTangCaRecords(maNhanVien, loaiCa);
                TangCaDataGrid.ItemsSource = tangCaRecords; // Gắn dữ liệu tìm kiếm vào DataGrid
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xử lý sự kiện khi thay đổi giá trị ComboBox
        private void cbLoaiCa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnTimKiem_Click(sender, e); // Gọi lại sự kiện tìm kiếm
        }
    }
}
