using BLL;
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
    /// Interaction logic for XttTangCa.xaml
    /// </summary>
    public partial class XttTangCa : UserControl
    {
        private string taiKhoan; // Tài khoản đã đăng nhập
        DangKiBLL tcBLL = new DangKiBLL();

        public XttTangCa(string userTaiKhoan)
        {
            InitializeComponent(); 
            taiKhoan = userTaiKhoan; // Nhận tài khoản từ form đăng nhập
            LoadNhanVienInfo(); // Load thông tin hợp đồng lao động khi khởi tạo

        }
        private void LoadNhanVienInfo()
        {
            // Lấy thông tin tăng ca từ BLL
            List<DTO.DangKi.TangCaDTO> tcList = tcBLL.GetTCByTaiKhoan(taiKhoan); // Phương thức trả về danh sách thông tin tăng ca

            if (tcList != null && tcList.Count > 0)
            {
                // Liên kết danh sách dữ liệu với DataGrid
                dgHDLD.ItemsSource = tcList;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin tăng ca.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
