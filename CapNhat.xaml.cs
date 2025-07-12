using BLL;
using DAL;
using DTO;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QLNHANSU
{
    public partial class CapNhat : UserControl
    {
        private readonly NhanVienBLL nhanVienBLL;
        private readonly HopDongLaoDongBLL hopDongBLL;
        private readonly ChucVuBLL chucVuBLL;
        private byte[] avatarImageBytes = null;

        public CapNhat(NhanVienBLL nhanVienBLL, HopDongLaoDongBLL hopDongBLL)
        {
            InitializeComponent();
            this.nhanVienBLL = nhanVienBLL;
            this.hopDongBLL = hopDongBLL;
            var chucVuAccess = new ChucVuAccess();
            chucVuBLL = new ChucVuBLL(chucVuAccess);
        }
        
        // Sự kiện Thêm
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInputs()) return;

                NhanVien nv = new NhanVien
                {
                    MANV = tbMANV.Text,
                    HOTEN = tbHOTEN.Text,
                    NTNS = dbNGAYSINH.SelectedDate.Value,
                    GT = rbGIOITINHNAM.IsChecked == true ? "Nam" : "Nữ",
                    CCCD = tbCCCD.Text,
                    SDT = tbSDT.Text,
                    EMAIL = tbEMAIL.Text,
                    DIACHI = tbDIACHI.Text,
                    HINHANH = avatarImageBytes,
                    MAPB = cmbPhonBan.Text,
                    MABP = cmbBoPhan.Text,
                    MACV = cmbChucVu.Text
                };

                bool isAdded = nhanVienBLL.AddNhanVien(nv);

                if (isAdded)
                {
                    MessageBox.Show("Thêm nhân viên thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        

        // Sự kiện Xóa
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }
        

        // Sự kiện chọn ảnh
        private void AvatarImageControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                avatarImageBytes = File.ReadAllBytes(filePath);

                // Hiển thị hình ảnh trên ImageControl
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                AvatarImageControl.Source = bitmap;
            }
        }

        // Validate dữ liệu đầu vào
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(tbHOTEN.Text) || string.IsNullOrWhiteSpace(tbMANV.Text) || dbNGAYSINH.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        // Reset form
        private void ResetForm()
        {
            tbMANV.Clear();
            tbHOTEN.Clear();
            dbNGAYSINH.SelectedDate = null;
            tbCCCD.Clear();
            tbSDT.Clear();
            tbEMAIL.Clear();
            tbDIACHI.Clear();
           /* tbMAPHONG.Clear();
            tbMABOPHAN.Clear();
            tbMACHUCVU.Clear();*/

            AvatarImageControl.Source = new BitmapImage(new Uri(@"D:\QLNHANSU_MAINFILE\Resources\avatar-trang-4.jpg"));
            avatarImageBytes = null;
        }
        private void ResetFormHD()
        {
            tbMAHD.Clear();
            dpNGAYKY.SelectedDate = null;
            dpNGBD.SelectedDate = null;
            dpNGKT.SelectedDate = null;
            tbMANV_HOPDONG.Clear();
            tbLUONGCOBAN.Clear();
            tbHESOLUONG.Clear();
            
        }

        private void btnAddContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(tbMAHD.Text) || string.IsNullOrWhiteSpace(tbMANV_HOPDONG.Text) ||
                    dpNGAYKY.SelectedDate == null || dpNGBD.SelectedDate == null || string.IsNullOrWhiteSpace(cmb_MAPB_HOPDONG.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Tạo đối tượng hợp đồng
                var hopDong = new HopDongLaoDong
                {
                    MAHD = tbMAHD.Text,
                    MANV = tbMANV_HOPDONG.Text,
                    NGAYKYHD = dpNGAYKY.SelectedDate.Value,
                    NGBD = dpNGBD.SelectedDate.Value,
                    NGKT = dpNGKT.SelectedDate,
                    MAPB = cmb_MAPB_HOPDONG.Text,
                    MABP = cmb_MABP_HOPDONG.Text,
                    MACV = cmb_MACV_HOPDONG.Text,
                    MUCLUONGCOBAN = int.TryParse(tbLUONGCOBAN.Text, out var luong) ? luong : 0,
                    HESOLUONG = decimal.TryParse(tbHESOLUONG.Text, out var hesoluong) ? hesoluong : 0
                    
                };

                // Thêm hợp đồng
                if (hopDongBLL.AddHopDong(hopDong))
                {
                    MessageBox.Show("Thêm hợp đồng lao động thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm hợp đồng lao động thất bại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tb_MACV_HOPDONG_TextChanged(object sender, TextChangedEventArgs e)
        {
            string macv = cmb_MACV_HOPDONG.Text.Trim();

            if (!string.IsNullOrEmpty(macv))
            {
                var chucVu = chucVuBLL.GetChucVuById(macv); // Phương thức này lấy thông tin chức vụ từ BLL
                if (chucVu != null)
                {
                    tbHESOLUONG.Text = chucVu.HESOLUONG.ToString("0.00"); // Điền hệ số lương vào TextBox
                }
                else
                {
                    tbHESOLUONG.Clear();
                }
            }
        }

        private void deleteHDButton_Click(object sender, RoutedEventArgs e)
        {
            ResetFormHD();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
