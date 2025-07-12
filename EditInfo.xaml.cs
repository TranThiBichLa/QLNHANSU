using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Microsoft.Win32;




namespace QLNHANSU
{
    /// <summary>
    /// Interaction logic for EditInfo.xaml
    /// </summary>
    public partial class EditInfo : Window
    {
        private NhanVien currentNhanVien;
        private readonly NhanVienBLL nhanVienBLL;
      
        private byte[] avatarImageBytes = null;
        public EditInfo(NhanVien nv, NhanVienBLL nhanVienBLL)
        {
            InitializeComponent();
            currentNhanVien = nv;
            this.nhanVienBLL = nhanVienBLL;
            FillData();
        }
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(tbHOTEN.Text) || dbNGAYSINH.SelectedDate == null)
            {
                MessageBox.Show("Họ tên và ngày sinh không được để trống.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void FillData()
        {
            tbMANV.Text = currentNhanVien.MANV;
            tbHOTEN.Text = currentNhanVien.HOTEN;
            dbNGAYSINH.SelectedDate = currentNhanVien.NTNS;
            tbCCCD.Text = currentNhanVien.CCCD;
            tbSDT.Text = currentNhanVien.SDT;
            tbEMAIL.Text = currentNhanVien.EMAIL;
            tbDIACHI.Text = currentNhanVien.DIACHI;
            tbMAPHONG.Text = currentNhanVien.MAPB;
            tbMABOPHAN.Text = currentNhanVien.MABP;
            tbMACHUCVU.Text = currentNhanVien.MACV;

            if (currentNhanVien.GT == "Nam") rbGIOITINHNAM.IsChecked = true;
            else rbGIOITINHNU.IsChecked = true;

            if (currentNhanVien.HINHANH != null)
            {
                BitmapImage bitmap = new BitmapImage();
                using (var ms = new MemoryStream(currentNhanVien.HINHANH))
                {
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                }
                AvatarImageControl.Source = bitmap;
            }
        }

        // Sự kiện Sửa
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                currentNhanVien.HOTEN = tbHOTEN.Text;
                currentNhanVien.NTNS = dbNGAYSINH.SelectedDate.Value;
                currentNhanVien.GT = rbGIOITINHNAM.IsChecked == true ? "Nam" : "Nữ";
                currentNhanVien.CCCD = tbCCCD.Text;
                currentNhanVien.SDT = tbSDT.Text;
                currentNhanVien.EMAIL = tbEMAIL.Text;
                currentNhanVien.DIACHI = tbDIACHI.Text;
                currentNhanVien.MAPB = tbMAPHONG.Text;
                currentNhanVien.MABP = tbMABOPHAN.Text;
                currentNhanVien.MACV = tbMACHUCVU.Text;

                if (nhanVienBLL.UpdateNhanVien(currentNhanVien))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
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
                currentNhanVien.HINHANH = avatarImageBytes; // Cập nhật hình ảnh cho đối tượng NhanVien

                // Hiển thị hình ảnh trên giao diện
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                AvatarImageControl.Source = bitmap;
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị thông báo xác nhận xóa
            var result = MessageBox.Show("Bạn có muốn xóa nhân viên vĩnh viễn và toàn bộ dữ liệu liên quan?",
                                         "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Mở cửa sổ nhập mật khẩu
                EnterPasswordWindow passwordWindow = new EnterPasswordWindow();
                if (passwordWindow.ShowDialog() == true)
                {
                    string enteredPassword = passwordWindow.EnteredPassword;

                    // Kiểm tra mật khẩu
                    if (enteredPassword == GlobalData.LoggedInPassword)
                    {
                        try
                        {
                            // Gọi phương thức DeleteNhanVien từ BLL để xóa nhân viên
                            if (nhanVienBLL.DeleteNhanVien(currentNhanVien.MANV))
                            {
                                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close(); // Đóng cửa sổ sau khi xóa
                            }
                            else
                            {
                                MessageBox.Show("Xóa nhân viên thất bại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không đúng. Hủy xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }



    }
}
