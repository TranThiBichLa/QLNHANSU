using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace QLNHANSU
{
    public partial class QuanLyChamCong : UserControl
    {
        private readonly ChamCongBLL chamcongBLL;

        public QuanLyChamCong()
        {
            InitializeComponent();
            chamcongBLL = new ChamCongBLL();
            LoadData();
        }

        // Load tất cả dữ liệu ban đầu
        private void LoadData()
        {
            try
            {
                List<DTO.ChamCongDTO> chamCongRecords = chamcongBLL.GetChamCong();
                TangCaDataGrid.ItemsSource = chamCongRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện TextChanged cho Mã Nhân Viên
        private void txtMaNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        // Sự kiện SelectionChanged cho các ComboBox
        private void cbLoaiCa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();
        }

        private void cbTrangThai_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        // Lọc dữ liệu theo các tiêu chí
        private void FilterData()
        {
            try
            {
                // Lấy giá trị từ các bộ lọc
                string maNhanVien = txtMaNhanVien.Text.Trim(); // Mã nhân viên từ TextBox
                string selectedLoaiCa = (cbLoaiCa.SelectedItem as ComboBoxItem)?.Content?.ToString();
                string selectedTrangThai = (cbTrangThai.SelectedItem as ComboBoxItem)?.Content?.ToString();
                int? selectedThang = cbThang.SelectedItem != null ? int.Parse(((ComboBoxItem)cbThang.SelectedItem).Content.ToString()) : (int?)null;
                int? selectedNam = cbNam.SelectedItem != null ? int.Parse(((ComboBoxItem)cbNam.SelectedItem).Content.ToString()) : (int?)null;

                // Lấy toàn bộ dữ liệu từ BLL
                List<DTO.ChamCongDTO> allRecords = chamcongBLL.GetChamCong();

                // Lọc dữ liệu
                var filteredRecords = allRecords.Where(record =>
                    (string.IsNullOrEmpty(maNhanVien) || record.MANV.Contains(maNhanVien)) &&
                    (string.IsNullOrEmpty(selectedLoaiCa) || selectedLoaiCa == "Tất cả" || record.MaLoaiCa == selectedLoaiCa) &&
                    (string.IsNullOrEmpty(selectedTrangThai) || selectedTrangThai == "Tất cả" || record.TrangThai == selectedTrangThai) &&
                    (!selectedThang.HasValue || record.NgayChamCong.Month == selectedThang) &&
                    (!selectedNam.HasValue || record.NgayChamCong.Year == selectedNam)
                ).ToList();

                // Cập nhật DataGrid
                TangCaDataGrid.ItemsSource = filteredRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
