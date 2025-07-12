using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace QLNHANSU
{
    /// <summary>
    /// Interaction logic for XttCoBan.xaml
    /// </summary>
    public partial class XttCoBan : UserControl
    {
        private string taiKhoan; // Tài khoản đã đăng nhập
        private string matKhau; 

        XttCoBanBLL tkBLL = new XttCoBanBLL();
        public XttCoBan(string userTaiKhoan, string userMatKhau)
        {
            InitializeComponent();
            matKhau = userMatKhau;

            taiKhoan = userTaiKhoan; // Nhận tài khoản từ form đăng nhập
            LoadNhanVienInfo();
        }

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

        // Phương thức để xóa thông tin nhân viên cũ (reset form)
        private void ClearNhanVienInfo()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            dpNgaySinh.SelectedDate = null;
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();

            // Xóa giới tính
            rbNam.IsChecked = false;
            rbNu.IsChecked = false;

            // Xóa hình ảnh
            imgHinhAnh.Source = null;

            // Xóa thông tin phòng ban, bộ phận, chức vụ
            txtMaPB.Clear();
            txtTenPB.Clear();
            txtMaBP.Clear();
            txtTenBP.Clear();
            txtMaCV.Clear();
            txtTenCV.Clear();
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
    }
}
