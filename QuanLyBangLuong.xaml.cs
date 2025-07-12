using BLL;
using DTO;
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
    /// Interaction logic for QuanLyBangLuong.xaml
    /// </summary>
    public partial class QuanLyBangLuong : UserControl
    {
        private BangLuongBLL bangLuongBLL;

        public QuanLyBangLuong()
        {
            InitializeComponent();
            bangLuongBLL = new BangLuongBLL();
            LoadBangLuong();
        }

        private void LoadBangLuong()
        {
            try
            {
                List<BangLuongDTO> bangLuongList = bangLuongBLL.GetAllBangLuong();
                TangCaDataGrid.ItemsSource = bangLuongList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu bảng lương: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtMaNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        private void cbThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();
        }

        private void cbNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();
        }
        private void cbMucLuong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterAndSortData();
        }

        private void FilterAndSortData()
        {
            try
            {
                // Lấy dữ liệu lọc từ ComboBox và TextBox
                string maNV = txtMaNhanVien.Text;
                int thang = cbThang.SelectedIndex + 1; // Tháng từ 1-12
                int nam = cbNam.SelectedItem != null ? int.Parse(((ComboBoxItem)cbNam.SelectedItem).Content.ToString()) : 0;

                string sapXep = cbMucLuong.SelectedItem != null ? ((ComboBoxItem)cbMucLuong.SelectedItem).Content.ToString() : "";

                // Gọi dữ liệu từ BLL
                List<BangLuongDTO> allRecords = bangLuongBLL.GetAllBangLuong();

                // Lọc dữ liệu theo Mã NV, Tháng, Năm
                var filteredRecords = allRecords.Where(record =>
                    (string.IsNullOrEmpty(maNV) || record.MANV.Contains(maNV)) &&
                    (thang == 0 || record.Thang == thang) &&
                    (nam == 0 || record.Nam == nam)
                ).ToList();

                // Sắp xếp dữ liệu theo Lương Cơ Bản
                if (sapXep == "A-Z")
                {
                    filteredRecords = filteredRecords.OrderBy(record => record.LuongCoBan).ToList();
                }
                else if (sapXep == "Z-A")
                {
                    filteredRecords = filteredRecords.OrderByDescending(record => record.LuongCoBan).ToList();
                }

                // Cập nhật DataGrid
                TangCaDataGrid.ItemsSource = filteredRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilterData()
        {
            try
            {
                string maNV = txtMaNhanVien.Text;
                int thang = cbThang.SelectedIndex + 1;
                int nam = int.TryParse((cbNam.SelectedItem as ComboBoxItem)?.Content?.ToString(), out int parsedNam) ? parsedNam : 0;

                List<BangLuongDTO> allRecords = bangLuongBLL.GetAllBangLuong();
                var filteredRecords = allRecords.FindAll(record =>
                    (string.IsNullOrEmpty(maNV) || record.MANV.Contains(maNV)) &&
                    (thang == 0 || record.Thang == thang) &&
                    (nam == 0 || record.Nam == nam)
                );

                TangCaDataGrid.ItemsSource = filteredRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
