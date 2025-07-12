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
    /// Interaction logic for XttLuong.xaml
    /// </summary>
    public partial class XttLuong : UserControl
    {
        private BangLuongBLL bangLuongBLL = new BangLuongBLL();
        private string maNhanVien; // Mã nhân viên (được lấy từ tài khoản đăng nhập)

        public XttLuong(string maNV)
        {
            InitializeComponent();
            maNhanVien = maNV;
        }

        // Xử lý sự kiện khi nhấn nút "Xem Lương"
        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int thang = int.Parse((cbThang.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "0");
                int nam = int.Parse((cbNam.SelectedItem as ComboBoxItem)?.Content.ToString() ?? DateTime.Now.Year.ToString());

                // Lấy dữ liệu bảng lương từ BLL
                List<BangLuongDTO> bangLuongRecords = bangLuongBLL.GetBangLuongByNhanVien(maNhanVien, thang, nam);

                // Gắn dữ liệu vào DataGrid
                dgBangLuong.ItemsSource = bangLuongRecords;

                if (bangLuongRecords.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu bảng lương.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
