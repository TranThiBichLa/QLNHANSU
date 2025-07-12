//using BLL;
//using DTO;
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
using BLL;
using DTO;

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
            TaiKhoan taikhoan = new TaiKhoan();
            taikhoan.sTaiKhoan = txttaikhoan.Text;  // Tên tài khoản
            taikhoan.sMatKhau = txtmatkhau.Password;  // Mật khẩu (mã nhân viên)

            string userRole;
            string result = TKBLL.CheckLogin(taikhoan, out userRole); // Kiểm tra đăng nhập và lấy quyền người dùng

            switch (result)
            {
                case "request_taikhoan":
                    MessageBox.Show("Tài khoản không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "request_password":
                    MessageBox.Show("Mật khẩu không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "invalid_login":
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "success":
                    GlobalData.LoggedInPassword = taikhoan.sMatKhau; // Lưu mật khẩu

                    // Phân quyền dựa vào userRole
                    if (userRole == "Quản lý")
                    {
                        DashBoard dashboard = new DashBoard(taikhoan.sTaiKhoan); // Gọi dashboard của quản lý, truyền tài khoản và mật khẩu
                        dashboard.Show();
                    }
                    else if (userRole == "Nhân viên")
                    {
                        DashBoard1 dashboard = new DashBoard1(taikhoan.sTaiKhoan, taikhoan.sMatKhau); // Gọi dashboard của nhân viên, truyền tài khoản và mật khẩu
                        dashboard.Show();
                    }
                    this.Close();
                    break;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

      

      
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }
    }
}
