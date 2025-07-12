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
    /// <summary>
    /// Interaction logic for XttCongTac.xaml
    /// </summary>
    public partial class XttCongTac : UserControl
    {
        private string taiKhoan; // Tài khoản đã đăng nhập
        private string matKhau;  // Mật khẩu của nhân viên
        private XttCongTacBLL congTacBLL = new XttCongTacBLL();

        public XttCongTac(string userTaiKhoan, string userMatKhau)
        {
            InitializeComponent();
            taiKhoan = userTaiKhoan;
            matKhau = userMatKhau; // Nhận tài khoản và mật khẩu từ form đăng nhập
            LoadCongTacInfo(); // Tải thông tin công tác khi giao diện được khởi tạo
        }

        // Phương thức tải thông tin công tác từ BLL
        private void LoadCongTacInfo()
        {
           
                // Lấy thông tin công tác từ BLL
                List<DTO.XttCongTac> congTacs = congTacBLL.GetCongTacInfo(taiKhoan, matKhau);

                if (congTacs != null && congTacs.Count > 0)
                {
                    // Gán dữ liệu vào DataGrid
                    dgCongTac.ItemsSource = congTacs;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin công tác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        

        // Chuyển đổi mảng byte thành hình ảnh
        private BitmapImage ConvertByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
            {
                return null; // Trả về null nếu không có dữ liệu hình ảnh
            }

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
