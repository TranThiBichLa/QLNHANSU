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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        TaiKhoan taikhoan = new TaiKhoan();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        public DangNhap()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin đăng nhập từ giao diện
            taikhoan.sTaiKhoan = txttaikhoan.Text;
            taikhoan.sMatKhau = txtmatkhau.Password; // Lấy mật khẩu từ PasswordBox

            // Kiểm tra thông tin đăng nhập
            string getuser = TKBLL.CheckLogin(taikhoan);

            // Xử lý kết quả kiểm tra
            switch (getuser)
            {
                case "request_taikhoan":
                    MessageBox.Show("Tài khoản không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                case "request_password":
                    MessageBox.Show("Mật khẩu không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                case "Tài khoản hoặc mật khẩu không chính xác!":
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                default:
                    // Đăng nhập thành công

                    // Mở giao diện chính và đóng form đăng nhập
                    DashBoard mainWindow = new DashBoard();
                    mainWindow.Show();
                    this.Close();
                    break;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
