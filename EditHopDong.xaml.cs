using BLL;
using DTO;
using System;
using System.Windows;
using System.Windows.Controls;
using DAL;

namespace QLNHANSU
{
    public partial class EditHopDong : Window
    {
        private readonly HopDongLaoDongBLL hopDongBLL;
        private readonly ChucVuBLL chucVuBLL;
        private HopDongLaoDong currentContract;
        private decimal originalHeSoLuong;


        public EditHopDong(HopDongLaoDong contract, HopDongLaoDongBLL hopDongBLL, ChucVuBLL chucVuBLL)
        {
            InitializeComponent();

            this.hopDongBLL = hopDongBLL ?? throw new ArgumentNullException(nameof(hopDongBLL));
            this.chucVuBLL = chucVuBLL ?? throw new ArgumentNullException(nameof(chucVuBLL));
            currentContract = contract ?? throw new ArgumentNullException(nameof(contract));

            PopulateFields();
        }

        private void PopulateFields()
        {
            // Điền các giá trị vào các ô tương ứng
            tbMAHD.Text = currentContract.MAHD;
            dpNGAYKY.SelectedDate = currentContract.NGAYKYHD;
            dpNGBD.SelectedDate = currentContract.NGBD;
            dpNGKT.SelectedDate = currentContract.NGKT;
            tbLUONGCOBAN.Text = currentContract.MUCLUONGCOBAN.ToString();
            tbMANV_HOPDONG.Text = currentContract.MANV;
            tb_MAPB_HOPDONG.Text = currentContract.MAPB;
            tb_MABP_HOPDONG.Text = currentContract.MABP;
            tb_MACV_HOPDONG.Text = currentContract.MACV;

            // Lưu giá trị ban đầu của HESOLUONG
            originalHeSoLuong = currentContract.HESOLUONG;

            // Set TextBox về trống
            tbHESOLUONG.Text = string.Empty;
        }


        private void tb_MACV_HOPDONG_TextChanged(object sender, TextChangedEventArgs e)
        {
            string macv = tb_MACV_HOPDONG.Text.Trim();

            if (!string.IsNullOrEmpty(macv))
            {
                var chucVu = chucVuBLL.GetChucVuById(macv);
                if (chucVu != null)
                {
                    tbHESOLUONG.Text = chucVu.HESOLUONG.ToString("0.00");
                }
                else
                {
                    tbHESOLUONG.Clear();
                }
            }
        }

        private void btnEditContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input và cập nhật thông tin
                currentContract.NGAYKYHD = dpNGAYKY.SelectedDate ?? throw new Exception("Ngày ký không được để trống.");
                currentContract.NGBD = dpNGBD.SelectedDate ?? throw new Exception("Ngày bắt đầu không được để trống.");
                currentContract.NGKT = dpNGKT.SelectedDate ?? throw new Exception("Ngày kết thúc không được để trống.");
                currentContract.MUCLUONGCOBAN = int.Parse(tbLUONGCOBAN.Text);
                currentContract.MANV = tbMANV_HOPDONG.Text;
                currentContract.MAPB = tb_MAPB_HOPDONG.Text;
                currentContract.MABP = tb_MABP_HOPDONG.Text;
                currentContract.MACV = tb_MACV_HOPDONG.Text;

                // Kiểm tra và cập nhật hệ số lương
                if (!string.IsNullOrWhiteSpace(tbHESOLUONG.Text))
                {
                    currentContract.HESOLUONG = decimal.Parse(tbHESOLUONG.Text);
                }
                else
                {
                    currentContract.HESOLUONG = originalHeSoLuong;
                }

                // Gọi BLL để cập nhật trạng thái và lưu hợp đồng
                if (hopDongBLL.UpdateHopDong(currentContract))
                {
                    DialogResult = true; // Đóng form và thông báo thành công
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật hợp đồng. Vui lòng thử lại sau.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void deleteHDButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa hợp đồng này?",
                                 "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Kiểm tra trạng thái hợp đồng
                if (currentContract.TINHTRANG != "Hết hiệu lực")
                {
                    MessageBox.Show("Chỉ có thể xóa hợp đồng đã hết hiệu lực.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Mở cửa sổ nhập mật khẩu
                EnterPasswordWindow passwordWindow = new EnterPasswordWindow();
                if (passwordWindow.ShowDialog() == true)
                {
                    string enteredPassword = passwordWindow.EnteredPassword;

                    // Kiểm tra mật khẩu (Giả sử mật khẩu đăng nhập được lưu trong `GlobalData`)
                    if (enteredPassword == GlobalData.LoggedInPassword)
                    {
                        try
                        {
                            // Gọi BLL để xóa hợp đồng
                            if (hopDongBLL.DeleteHopDong(currentContract.MAHD))
                            {
                                MessageBox.Show("Xóa hợp đồng thành công!", "Thông báo",
                                                MessageBoxButton.OK, MessageBoxImage.Information);
                                DialogResult = true; // Đóng cửa sổ sau khi xóa
                            }
                            else
                            {
                                MessageBox.Show("Xóa hợp đồng thất bại.", "Lỗi",
                                                MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không đúng. Hủy xóa!", "Lỗi",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        }
    }
}
