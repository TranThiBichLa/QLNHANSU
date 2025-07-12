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
using System.Windows.Shapes;

namespace QLNHANSU
{
    /// <summary>
    /// Interaction logic for EditBaoHiem.xaml
    /// </summary>
    public partial class EditBaoHiem : Window
    {
        private BaoHiem currentBaoHiem;

        public EditBaoHiem(BaoHiem baoHiem)
        {
            InitializeComponent();
            currentBaoHiem = baoHiem;

            // Hiển thị thông tin vào form
            txtSoBHYT.Text = currentBaoHiem.SOBHYT;
            dpNgayCap.SelectedDate = currentBaoHiem.NGAYCAP;
            txtGiaTriSuDung.SelectedDate = currentBaoHiem.GTSD;
            dpNgayHetHan.SelectedDate = currentBaoHiem.NGAYHETHAN;
            txtNoiKhamBenh.Text = currentBaoHiem.NOIKHAMBENH;
        }



        private void LuuThayDoiBaoHiem_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy thông tin chỉnh sửa từ form
                currentBaoHiem.NGAYCAP = dpNgayCap.SelectedDate ?? DateTime.Now;
                currentBaoHiem.GTSD = txtGiaTriSuDung.SelectedDate ?? DateTime.Now;
                currentBaoHiem.NGAYHETHAN = dpNgayHetHan.SelectedDate;
                currentBaoHiem.NOIKHAMBENH = txtNoiKhamBenh.Text.Trim();

                // Gọi BLL để cập nhật thông tin vào cơ sở dữ liệu
                BaoHiemBLL baoHiemBLL = new BaoHiemBLL();
                bool isUpdated = baoHiemBLL.UpdateBaoHiem(currentBaoHiem);

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true; // Đóng form và trả về kết quả
                }
                else
                {
                    MessageBox.Show("Lỗi khi cập nhật thông tin bảo hiểm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}