using BLL;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QLNHANSU
{
    public partial class XttHDLD : UserControl
    {
        private string taiKhoan; // Tài khoản đã đăng nhập
        XttHDLDBLL tkBLL = new XttHDLDBLL();

        public XttHDLD(string userTaiKhoan)
        {
            InitializeComponent();
            taiKhoan = userTaiKhoan; // Nhận tài khoản từ form đăng nhập
            LoadNhanVienInfo(); // Load thông tin hợp đồng lao động khi khởi tạo
        }

        private void LoadNhanVienInfo()
        {
            // Lấy thông tin hợp đồng lao động
            List<DTO.XttHDLD> hdList = tkBLL.GetHDByTaiKhoan(taiKhoan); // Giả sử phương thức trả về một danh sách hợp đồng lao động

            if (hdList != null && hdList.Count > 0)
            {
                // Liên kết danh sách hợp đồng với DataGrid
                dgHDLD.ItemsSource = hdList;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin hợp đồng lao động.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
