using BLL;
using DAL;
using DTO;
using System.Windows;
using System.Windows.Controls;

namespace QLNHANSU
{
    public partial class DashBoard : Window
    {
        // Khai báo các đối tượng BLL
        private readonly NhanVienBLL nhanVienBLL;
        private readonly HopDongLaoDongBLL hopDongBLL;
        private string taiKhoan;
        // Constructor
        public DashBoard(string userTaiKhoan)
        {
            InitializeComponent();

            // Khởi tạo các đối tượng Access và BLL
            var nhanVienAccess = new NhanVienAccess();
            var hopDongAccess = new HopDongLaoDongAccess();
            hopDongBLL = new HopDongLaoDongBLL(hopDongAccess, nhanVienAccess);
            nhanVienBLL = new NhanVienBLL(nhanVienAccess, hopDongBLL);

            // Load giao diện mặc định
            LoadUserControl(new ThongKe());
            taiKhoan = userTaiKhoan; 
        }

        // Nạp UserControl vào panelMainContent
        private void LoadUserControl(UserControl userControl)
        {
            panelMainContent.Children.Clear(); // Xóa nội dung cũ
            panelMainContent.Children.Add(userControl); // Thêm UserControl mới
        }

        // Xử lý sự kiện nút 'Cập nhật'
        private void Button_CapNhat_Click(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new CapNhat(nhanVienBLL, hopDongBLL));
        }

        
        // Xử lý sự kiện nút 'Quản lý thông tin'
        private void Button_QuanLyThongTin_Click(object sender, RoutedEventArgs e)
        {
           
                LoadUserControl(new QuanLyThongTin(nhanVienBLL, hopDongBLL));
            

        }
        private void Button_ThongKe_Click(object sender, RoutedEventArgs e)
        {

            LoadUserControl(new ThongKe());


        }

        // Xử lý sự kiện nút 'Công tác'
        private void Button_CongTac_Click(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new CongTac());
        }
        private void Button_ChamCong_Click(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new QuanLyChamCong());
        }
        private void Button_Luong_Click(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new QuanLyBangLuong());
        }
        // Xử lý sự kiện nút 'Thoát'
        private void Button_Thoat_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Đóng ứng dụng
        }

        // Xử lý sự kiện nút 'Thu nhỏ'
        private void Button_ThuNho_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Thu nhỏ cửa sổ
        }

        // Xử lý sự kiện nút 'Đăng xuất'
        private void Button_DangXuat_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Chuyển sang giao diện đăng nhập
                var loginWindow = new DangNhap();
                loginWindow.Show(); // Hiển thị cửa sổ đăng nhập
                this.Close(); // Đóng cửa sổ hiện tại
            }
        }


        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new TangCa());


        }
    }
}
