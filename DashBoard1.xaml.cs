using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BLL;
using DTO;

namespace QLNHANSU
{
    public partial class DashBoard1 : Window
    {
        private string taiKhoan;  // Tài khoản đã đăng nhập
        private string matKhau;   // Mật khẩu của nhân viên (mã nhân viên)
        private XttCoBanBLL tkBLL = new XttCoBanBLL(); // Khởi tạo đối tượng BLL

        // Constructor nhận tài khoản và mật khẩu người dùng
        public DashBoard1(string userTaiKhoan, string userMatKhau)
        {
            InitializeComponent();
            taiKhoan = userTaiKhoan;
            matKhau = userMatKhau;

            LoadNhanVienInfo(); // Tải thông tin nhân viên khi khởi tạo
        }
      
        // Phương thức để load thông tin nhân viên từ cơ sở dữ liệu
        private void LoadNhanVienInfo()
        {

            // Gọi BLL và truyền tài khoản "staff" và mật khẩu là mã nhân viên
            DTO.XttCoBan nv = tkBLL.GetNhanVienInfo(taiKhoan, matKhau);

            if (nv != null)
            {
                // Hiển thị thông tin nhân viên
                txtMaNV.Text = nv.MANV;
                txtHoTen.Text = nv.HOTEN;
                dpNgaySinh.SelectedDate = nv.NTNS;
                txtEmail.Text = nv.EMAIL;
                txtDiaChi.Text = nv.DIACHI;
                txtSDT.Text = nv.SDT;

                // Kiểm tra giới tính và cập nhật checkbox tương ứng
                if (nv.GT)
                {
                    rbNam.IsChecked = true;
                }
                else
                {
                    rbNu.IsChecked = true;
                }

                // Hiển thị hình ảnh nếu có, nếu không thì hình ảnh mặc định
                if (nv.HinhAnh != null && nv.HinhAnh.Length > 0)
                {
                    imgHinhAnh.Source = ConvertByteArrayToImage(nv.HinhAnh);
                }
                else
                {
                    // Đặt ảnh mặc định nếu không có ảnh
                    imgHinhAnh.Source = new BitmapImage(new Uri("D:\\NĂM 2-HK1\\LẬP TRÌNH TRỰC QUAN\\quản lí nhân sự WPS\\QLNHANSU\\Image\\Screenshot 2024-12-24 150024.png"));
                }

                // Hiển thị thông tin phòng ban, bộ phận, chức vụ
                txtMaPB.Text = nv.MAPB;
                txtTenPB.Text = nv.TENPB;
                txtMaBP.Text = nv.MABP;
                txtTenBP.Text = nv.TENBP;
                txtMaCV.Text = nv.MACV;
                txtTenCV.Text = nv.TENCV;

                // Kiểm tra nếu thông tin bảo hiểm đã được cập nhật
                if (!string.IsNullOrEmpty(nv.SOBHYT))
                {
                    txtSoBHYT.Text = nv.SOBHYT;
                    dpNgayCap.SelectedDate = nv.NGAYCAP;
                    dpGTSD.SelectedDate = nv.GTSD;
                    dpNgayHetHanBHYT.SelectedDate = nv.NGAYHETHANBAOHIEM;
                    txtNoiKhamBenh.Text = nv.NOIKHAMBENH;
                }
                else
                {
                    MessageBox.Show("Bạn chưa cập nhật thông tin bảo hiểm y tế.");
                }

                // Hiển thị thông tin trình độ vào DataGrid
                if (!string.IsNullOrEmpty(nv.MATD))
                {
                    var trinhDoList = new List<DTO.XttCoBan> {
                new DTO.XttCoBan
                {
                    MATD = nv.MATD,
                    TENTD = nv.TENTD,
                    TGHOANTHANH = nv.TGHOANTHANH,
                    NGAYHETHANTRINHDO = nv.NGAYHETHANTRINHDO
                }
            };

                    dgTrinhDo.ItemsSource = trinhDoList;
                }
                else
                {
                    MessageBox.Show("Bạn chưa cập nhật thông tin trình độ.");
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Chuyển đổi mảng byte thành hình ảnh
        private BitmapImage ConvertByteArrayToImage(byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                return bitmap;
            }
        }

        // Phương thức load UserControl vào giao diện
        private void LoadUserControl(System.Windows.Controls.UserControl userControl)
        {
            panelMainContent.Children.Clear();
            panelMainContent.Children.Add(userControl);
        }

        // Thao tác khi nhấn nút minimize
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Thao tác khi nhấn nút close
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        // Sự kiện khi radio button "XttCoBan" được chọn
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new XttCoBan(taiKhoan,matKhau));
        }

        // Sự kiện khi radio button "DangKi1" được chọn
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new DangKi1(taiKhoan, matKhau));
        }

        // Sự kiện khi radio button "XttHDLD" được chọn
        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new XttHDLD(taiKhoan));
        }

        // Sự kiện đăng xuất
        private void RadioButton_Checked_7(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var loginWindow = new DangNhap();
                loginWindow.Show();
                this.Close();  // Đóng cửa sổ DashBoard
            }
        }

        // Sự kiện khi radio button "XttCongTac" được chọn
        private void RadioButton_Checked_6(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new XttCongTac(taiKhoan,matKhau));     
        }

        private void RadioButton_Checked_4(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new XttTangCa(taiKhoan));


        }
        private void RadioButton_Checked_8(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new ChamCong(matKhau));


        }
        private void RadioButton_Checked_9(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new XttLuong(matKhau));


        }


    }
}
