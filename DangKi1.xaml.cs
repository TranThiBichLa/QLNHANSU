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
using System.Data.SqlClient;
using static DTO.DangKi;

namespace QLNHANSU
{

    public partial class DangKi1 : UserControl
    {
        private string maNhanVien; // Mã nhân viên
        private DangKiBLL dangKiBLL = new DangKiBLL();

        public DangKi1(string userTaiKhoan, string matkhau)
        {
            InitializeComponent();
            // Lấy mã nhân viên từ tài khoản đăng nhập
            maNhanVien = dangKiBLL.GetNhanVienByTaiKhoan(userTaiKhoan, matkhau);

            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Không tìm thấy mã nhân viên liên kết với tài khoản. Vui lòng kiểm tra lại.");
                this.IsEnabled = false; // Vô hiệu hóa giao diện
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                TangCaDTO tangCa = new TangCaDTO
                {
                    MaNhanVien = maNhanVien,
                    MaTangCa = ((TextBox)this.FindName("txtMaTangCa")).Text,
                    MaLoaiCa = ((TextBox)this.FindName("txtMaLoaiCa")).Text,
                    NgayTangCa = ((DatePicker)this.FindName("dpNgayTangCa")).SelectedDate ?? DateTime.MinValue,
                    SoGioTangCa = int.TryParse(((TextBox)this.FindName("txtSoGioTangCa")).Text, out int soGio) ? soGio : 0
                };

                if (dangKiBLL.DangKyTangCa(maNhanVien,tangCa))
                {
                    MessageBox.Show("Đăng ký tăng ca thành công!");
                }
                else
                {
                    MessageBox.Show("Đăng ký tăng ca thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void DangKiBaoHien_Button(object sender, RoutedEventArgs e)
        {
            try
            { // Lấy giá trị từ giao diện
                BaoHiemDTO baoHiem = new BaoHiemDTO
                {
                    SoBHYT = ((TextBox)this.FindName("txtSoBHYT")).Text,
                    NgayCap = ((DatePicker)this.FindName("dpNgayCap")).SelectedDate ?? DateTime.MinValue,
                    GiaTriSuDung = ((DatePicker)this.FindName("txtGiaTriSuDung")).SelectedDate ?? DateTime.MinValue,
                    NgayHetHan = ((DatePicker)this.FindName("dpNgayHetHan")).SelectedDate,
                    NoiKhamBenh = ((TextBox)this.FindName("txtNoiKhamBenh")).Text,
                    MaNV = maNhanVien
                };

                if (dangKiBLL.DangKyBaoHiem(maNhanVien,baoHiem))
                {
                    MessageBox.Show("Đăng ký bảo hiểm thành công!");
                }
                else
                {
                    MessageBox.Show("Đăng ký bảo hiểm thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void DangKiTrinhDo_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                TrinhDoDTO trinhDo = new TrinhDoDTO
                {
                    MaTrinhDo = ((TextBox)this.FindName("txtMaTrinhDo")).Text,
                    TenTrinhDo = ((TextBox)this.FindName("txtTenTrinhDo")).Text,
                    ThoiGianHoanThanh = int.TryParse(((TextBox)this.FindName("txtThoiGianHoanThanh")).Text, out int thoiGianHoanThanh) ? thoiGianHoanThanh : 0,
                    NgayHetHan = ((DatePicker)this.FindName("dpNgayHetHan1")).SelectedDate,
                    MaNV = maNhanVien

                };

                if (dangKiBLL.DangKyTrinhDo(maNhanVien, trinhDo))
                {
                    MessageBox.Show("Đăng ký trình độ thành công!");
                }
                else
                {
                    MessageBox.Show("Đăng ký trình độ thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
    }
}
