using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace QLNHANSU
{
    public partial class ChamCong : UserControl
    {
        private ChamCongBLL chamCongBLL = new ChamCongBLL();
        private string maNhanVien; // Mã nhân viên (được lấy từ tài khoản đăng nhập)

        public ChamCong(string maNV)
        {
            InitializeComponent();
            maNhanVien = maNV;
        }

        // Xử lý sự kiện khi nhấn "Đăng Ký"
        private void DangKiBaoHien_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                string ngayChamCong = dpNgayChamCong.SelectedDate?.ToString("yyyy-MM-dd");
                string maLoaiCa = null;
                if (cbLoaiCa.SelectedItem is ComboBoxItem selectedItem)
                {
                    string selectedLoaiCa = selectedItem.Content.ToString();

                    // Gán mã loại ca dựa trên tên loại ca
                    

                    switch (selectedLoaiCa)
                    {
                        case "Ca ngày":
                            maLoaiCa = "MAL01";
                            break;
                        case "Ca đêm":
                            maLoaiCa = "MAL02";
                            break;
                        case "Ngày nghỉ":
                            maLoaiCa = "MAL03";
                            break;
                        case "Ngày lễ":
                            maLoaiCa = "MAL04";
                            break;
                        default:
                            maLoaiCa = null;
                            break;
                    }

                    
                }
                string trangThai = ((ComboBoxItem)cbTrangThai.SelectedItem)?.Content?.ToString();
                int soGio = int.TryParse(txtSoGioLamViec.Text, out int result) ? result : 0;
                string ghiChu = txtGhiChu.Text;

                if (string.IsNullOrEmpty(ngayChamCong) || string.IsNullOrEmpty(trangThai) || string.IsNullOrEmpty(maLoaiCa))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                // Tạo DTO
                ChamCongDTO chamCong = new ChamCongDTO
                {
                    MANV = maNhanVien, // Lấy từ tài khoản đăng nhập
                    NgayChamCong = DateTime.Parse(ngayChamCong),
                    TrangThai = trangThai,
                    MaLoaiCa = maLoaiCa,
                    SoGio = soGio,
                    GhiChu = ghiChu
                };

                // Thêm chấm công
                if (chamCongBLL.InsertChamCong(chamCong))
                {
                    MessageBox.Show("Đăng ký chấm công thành công!");
                }
                else
                {
                    MessageBox.Show("Đăng ký chấm công thất bại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }



        // Xử lý sự kiện khi nhấn "Tìm Kiếm"
        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy thông tin từ giao diện
                int thang = int.Parse(((ComboBoxItem)cbThang.SelectedItem).Content.ToString());
                int nam = int.Parse(((ComboBoxItem)cbNam.SelectedItem).Content.ToString());

                // Gọi BLL để lấy dữ liệu
                List<ChamCongDTO> data = chamCongBLL.GetChamCong(maNhanVien, thang, nam);

                // Gắn dữ liệu vào DataGrid
                if (data.Count > 0)
                {
                    dgThongTinChamCong.ItemsSource = data;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu chấm công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgThongTinChamCong.ItemsSource = null; // Xóa dữ liệu cũ trong DataGrid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }
}
