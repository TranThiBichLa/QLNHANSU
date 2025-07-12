using BLL;
using DTO;
using System;
using System.Windows;

namespace QLNHANSU
{
    public partial class EditTrinhDo : Window
    {
        private TrinhDo1 _currentTrinhDo;
        private readonly TrinhDoBLL _trinhDoBLL;

        public EditTrinhDo(TrinhDo1 trinhDo, TrinhDoBLL trinhDoBLL)
        {
            InitializeComponent();

            _currentTrinhDo = trinhDo;
            _trinhDoBLL = trinhDoBLL;

            // Pre-fill the form fields
            LoadTrinhDoData();
        }

        private void LoadTrinhDoData()
        {
            txtMaTrinhDo.Text = _currentTrinhDo.MATD;
            txtTenTrinhDo.Text = _currentTrinhDo.TENTD;
            txtThoiGianHoanThanh.Text = _currentTrinhDo.TGHOANTHANH?.ToString();
            dpNgayHetHan1.SelectedDate = _currentTrinhDo.NGAYHETHAN;

            txtMaTrinhDo.IsEnabled = false; // Disable editing of the ID field
        }

        private void LuuThayDoiTrinhDo_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                _currentTrinhDo.TENTD = txtTenTrinhDo.Text.Trim();
                _currentTrinhDo.TGHOANTHANH = int.TryParse(txtThoiGianHoanThanh.Text, out var tg) ? tg : (int?)null;
                _currentTrinhDo.NGAYHETHAN = dpNgayHetHan1.SelectedDate;

                // Call the BLL to update the record
                bool isUpdated = _trinhDoBLL.UpdateTrinhDo(_currentTrinhDo);

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật trình độ thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi khi cập nhật trình độ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}