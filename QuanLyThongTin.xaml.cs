using BLL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QLNHANSU
{
    public partial class QuanLyThongTin : UserControl
    {
        private List<NhanVien> danhSachNhanVien = new List<NhanVien>();
        private List<HopDongLaoDong> danhSachHopDong = new List<HopDongLaoDong>();
        private readonly NhanVienBLL nhanVienBLL;
        private readonly HopDongLaoDongBLL hopDongBLL;

        BaoHiemBLL baoHiemBLL = new BaoHiemBLL();
        TrinhDoBLL trinhDoBLL = new TrinhDoBLL();

        public QuanLyThongTin(NhanVienBLL nhanVienBLL, HopDongLaoDongBLL hopDongBLL)
        {
            InitializeComponent();
            this.nhanVienBLL = nhanVienBLL;
            this.hopDongBLL = hopDongBLL;
            LoadData();
            LoadHopDongData();
            hopDongBLL.UpdateHopDongTinhTrang();
            LoadBaoHiemData();
            LoadTrinhDoData();
        }


        // Tải dữ liệu nhân viên vào DataGrid
        private void LoadData()
        {
            try
            {
                danhSachNhanVien = nhanVienBLL.GetAllNhanVien();
                if (danhSachNhanVien != null && danhSachNhanVien.Count > 0)
                {
                    myDataGrid.ItemsSource = danhSachNhanVien;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Tìm kiếm nhân viên
        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string maNV = tbMSNV.Text.Trim().ToLower();
            string tenNV = tbHoTen.Text.Trim().ToLower();

            var filteredData = danhSachNhanVien.Where(nv =>
                (string.IsNullOrEmpty(maNV) || nv.MANV.ToLower().Contains(maNV)) &&
                (string.IsNullOrEmpty(tenNV) || nv.HOTEN.ToLower().Contains(tenNV))
            ).ToList();

            myDataGrid.ItemsSource = filteredData;

            if (filteredData.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy nhân viên với mã {maNV.ToUpper()} và tên {tenNV.ToUpper()}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Lọc dữ liệu theo phòng ban, bộ phận, chức vụ
        private void FilterData(object sender, TextChangedEventArgs e)
        {
            string phongBan = tbPhongBan.Text.ToLower();
            string boPhan = tbBoPhan.Text.ToLower();
            string chucVu = tbChucVu.Text.ToLower();

            var filteredData = danhSachNhanVien.Where(nv =>
                (string.IsNullOrEmpty(phongBan) || nv.MAPB.ToLower().Contains(phongBan)) &&
                (string.IsNullOrEmpty(boPhan) || nv.MABP.ToLower().Contains(boPhan)) &&
                (string.IsNullOrEmpty(chucVu) || nv.MACV.ToLower().Contains(chucVu))
            ).ToList();

            myDataGrid.ItemsSource = filteredData;
        }

        // Xử lý khi nhấp chuột vào cột trạng thái
        private void myDataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var cellInfo = myDataGrid.CurrentCell;
            if (cellInfo.Column != null && cellInfo.Column.Header.ToString() == "Trạng thái")
            {
                var selectedItem = cellInfo.Item as NhanVien;

                if (selectedItem != null && selectedItem.TRANGTHAI == "Đang làm việc")
                {
                    var result = MessageBox.Show("Bạn có chắc muốn thay đổi trạng thái của nhân viên?",
                                                  "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Cập nhật trạng thái trong danh sách tạm thời
                        selectedItem.TRANGTHAI = "Đã thôi việc";

                        // Cập nhật vào CSDL
                        if (nhanVienBLL.UpdateTinhTrangNhanVien(selectedItem.MANV, selectedItem.TRANGTHAI))
                        {
                            MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            myDataGrid.Items.Refresh(); // Làm mới DataGrid
                        }
                        else
                        {
                            MessageBox.Show("Lỗi khi cập nhật trạng thái nhân viên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        private void myDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (myDataGrid.SelectedItem is NhanVien selectedNhanVien && selectedNhanVien.HINHANH != null)
            {
                // Mở form hiển thị hình ảnh
                ImageWindow displayImage = new ImageWindow(selectedNhanVien.HINHANH);
                displayImage.ShowDialog();
            }
        }
        // Lọc trạng thái nhân viên
        private void cbTrangThai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedValue = (cbTrangThai.SelectedItem as ComboBoxItem)?.Content.ToString();

            var filteredList = string.IsNullOrEmpty(selectedValue) || selectedValue == "Tất cả"
                ? danhSachNhanVien
                : danhSachNhanVien.Where(nv => nv.TRANGTHAI == selectedValue).ToList();

            myDataGrid.ItemsSource = filteredList;
        }

        private void EditMoreInfo_Click(object sender, RoutedEventArgs e)
        {
            // Lấy thông tin nhân viên từ dòng được chọn
            NhanVien selectedNhanVien = myDataGrid.SelectedItem as NhanVien;

            if (selectedNhanVien != null)
            {
                // Tạo instance của form CapNhat và truyền dữ liệu nhân viên vào
                EditInfo capNhatForm = new EditInfo(selectedNhanVien, nhanVienBLL);


                capNhatForm.ShowDialog();

                // Làm mới lại DataGrid sau khi chỉnh sửa thông tin
                LoadData();
            }
            else
            {
                MessageBox.Show("Không có nhân viên nào được chọn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        // Nút cập nhật dữ liệu
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoadData();
        }


        private void LoadHopDongData()
        {
            try
            {
                danhSachHopDong = hopDongBLL.GetAllHopDongs(); // Lấy dữ liệu từ BLL
                if (danhSachHopDong != null && danhSachHopDong.Count > 0)
                {
                    dataGridHopDong.ItemsSource = danhSachHopDong; // Gán dữ liệu vào DataGrid
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu hợp đồng để hiển thị.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu hợp đồng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void FilterHopDongData(object sender, TextChangedEventArgs e)
        {
            string maPhong = tbPhongBanHD.Text.Trim().ToLower();
            string maBoPhan = tbBoPhanHD.Text.Trim().ToLower();
            string maChucVu = tbChucVuHD.Text.Trim().ToLower();

            var filteredData = danhSachHopDong.Where(hd =>
                (string.IsNullOrEmpty(maPhong) || hd.MAPB.ToLower().Contains(maPhong)) &&
                (string.IsNullOrEmpty(maBoPhan) || hd.MABP.ToLower().Contains(maBoPhan)) &&
                (string.IsNullOrEmpty(maChucVu) || hd.MACV.ToLower().Contains(maChucVu))
            ).ToList();

            dataGridHopDong.ItemsSource = filteredData;

            if (filteredData.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hợp đồng nào phù hợp với các tiêu chí lọc.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void cbTrangThaiHD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lấy giá trị trạng thái được chọn từ ComboBox
            var selectedValue = (cbTrangThaiHD.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Lọc danh sách hợp đồng dựa trên trạng thái
            var filteredList = danhSachHopDong
                .Where(hd => hd.TINHTRANG == selectedValue)
                .ToList();

            // Cập nhật DataGrid
            dataGridHopDong.ItemsSource = filteredList;
        }


        private void btnTimKiemNgay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các DatePicker
                DateTime? ngayKy = dpNgayKy.SelectedDate;
                DateTime? ngayBatDau = dpNgayBatDau.SelectedDate;
                DateTime? ngayKetThuc = dpNgayKetThuc.SelectedDate;

                // Lọc danh sách hợp đồng dựa trên ngày
                var filteredContracts = danhSachHopDong.Where(hd =>
                    (!ngayKy.HasValue || hd.NGAYKYHD.Date == ngayKy.Value.Date) &&
                    (!ngayBatDau.HasValue || hd.NGBD.Date == ngayBatDau.Value.Date) &&
                    (!ngayKetThuc.HasValue || (hd.NGKT.HasValue && hd.NGKT.Value.Date == ngayKetThuc.Value.Date))
                ).ToList();

                // Hiển thị danh sách đã lọc
                dataGridHopDong.ItemsSource = filteredContracts;

                // Kiểm tra nếu không tìm thấy kết quả
                if (filteredContracts.Count == 0)
                {
                    MessageBox.Show("Không có hợp đồng nào phù hợp!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnTimKiemHD_Click(object sender, RoutedEventArgs e)
        {
            string maNV = tbMaNhanVien.Text.Trim();
            string maHD = tbMAHD.Text.Trim();

            // Filter contracts based on Mã Nhân Viên and Mã Hợp Đồng
            var filteredContracts = danhSachHopDong.Where(hd =>
                (string.IsNullOrEmpty(maNV) || hd.MANV.Contains(maNV)) &&
                (string.IsNullOrEmpty(maHD) || hd.MAHD.Contains(maHD))
            ).ToList();

            if (filteredContracts.Count > 0)
            {
                dataGridHopDong.ItemsSource = filteredContracts; // Update DataGrid with filtered data
            }
            else
            {
                MessageBox.Show($"Không tìm thấy hợp đồng với mã {maHD} của nhân viên có mã {maNV}",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnCapNhatDuLieu_Click(object sender, RoutedEventArgs e)
        {
            // Reset DataGrid to show all contracts
            dataGridHopDong.ItemsSource = danhSachHopDong;
        }

        private void EditMoreInfoHD_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected contract from the DataGrid
            var selectedContract = (sender as Button)?.Tag as HopDongLaoDong;
            if (selectedContract != null)
            {
                // Ensure you have valid instances of the required BLL classes
                var hopDongBLL = new HopDongLaoDongBLL(new HopDongLaoDongAccess(), new NhanVienAccess());
                var chucVuBLL = new ChucVuBLL(new ChucVuAccess());

                // Open the EditHopDong form
                var editWindow = new EditHopDong(selectedContract, hopDongBLL, chucVuBLL);
                if (editWindow.ShowDialog() == true)
                {
                    // Reload data after saving
                    LoadHopDongData();
                    MessageBox.Show("Thông tin hợp đồng đã được cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hợp đồng cần chỉnh sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        private List<BaoHiem> danhSachBaoHiem = new List<BaoHiem>();

        private void LoadBaoHiemData()
        {
            try
            {
                danhSachBaoHiem = baoHiemBLL.GetAllBaoHiem();
                if (danhSachBaoHiem != null && danhSachBaoHiem.Count > 0)
                {
                    dataGridBaoHiem.ItemsSource = danhSachBaoHiem;
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu bảo hiểm để hiển thị.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu bảo hiểm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void cbTrangThaiBaoHiem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTrangThai = (cbTrangThaiBaoHiem.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedTrangThai == "Tất cả")
            {
                LoadBaoHiemData();
            }
            else
            {
                var filteredList = danhSachBaoHiem
                    .Where(bh => bh.TRANGTHAI == selectedTrangThai)
                    .ToList();
                dataGridBaoHiem.ItemsSource = filteredList;
            }
        }

        private void FilterBaoHiemData(object sender, TextChangedEventArgs e)
        {
            string maBaoHiem = txtMaBaoHiem.Text.Trim().ToLower();
            string maNhanVien = txtMaNhanVien.Text.Trim().ToLower();
            string benhVien = txtBenhVien.Text.Trim().ToLower();
            string selectedTrangThai = (cbTrangThaiBaoHiem.SelectedItem as ComboBoxItem)?.Content.ToString();

            var filteredList = danhSachBaoHiem.Where(bh =>
                (string.IsNullOrEmpty(maBaoHiem) || bh.SOBHYT.ToLower().Contains(maBaoHiem)) &&
                (string.IsNullOrEmpty(maNhanVien) || bh.MANV.ToLower().Contains(maNhanVien)) &&
                (string.IsNullOrEmpty(benhVien) || bh.NOIKHAMBENH.ToLower().Contains(benhVien)) &&
                (selectedTrangThai == "Tất cả" || bh.TRANGTHAI == selectedTrangThai)
            ).ToList();

            dataGridBaoHiem.ItemsSource = filteredList;
        }

        private void EditMoreInfoBH_Click(object sender, RoutedEventArgs e)
        {
            BaoHiem selectedBaoHiem = (sender as Button)?.Tag as BaoHiem;

            if (selectedBaoHiem != null)
            {
                // Mở form EditBaoHiemWindow và truyền thông tin dòng được chọn
                EditBaoHiem editWindow = new EditBaoHiem(selectedBaoHiem);

                // Hiển thị form chỉnh sửa dưới dạng dialog
                if (editWindow.ShowDialog() == true)
                {
                    // Làm mới DataGrid sau khi chỉnh sửa
                    LoadBaoHiemData();
                }
            }
            else
            {
                MessageBox.Show("Không thể chỉnh sửa thông tin. Vui lòng chọn lại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private List<TrinhDo1> danhSachTrinhDo = new List<TrinhDo1>();
        private void LoadTrinhDoData()
        {
            try
            {

                danhSachTrinhDo = trinhDoBLL.GetAllTrinhDoNhanVien();

                if (danhSachTrinhDo != null && danhSachTrinhDo.Count > 0)
                {
                    dataGridTrinhDo.ItemsSource = danhSachTrinhDo;

                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void cbTrangThaiTrinhDo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTrangThai = (cbTrangThaiTrinhDo.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedTrangThai == "Tất cả")
            {
                LoadTrinhDoData(); // Tải lại toàn bộ dữ liệu nếu chọn "Tất cả"
            }
            else
            {
                var filteredList = danhSachTrinhDo
                    .Where(td => td.TRANGTHAI == selectedTrangThai)
                    .ToList();
                dataGridTrinhDo.ItemsSource = filteredList;
            }
        }
        private void FilterTrinhDoData(object sender, TextChangedEventArgs e)
        {
            string maNhanVien = txtMaNV_TD.Text.Trim().ToLower();
            string maTrinhDo = txtMaTD_TD.Text.Trim().ToLower();
            string thoiGianHieuLuc = txtThoiGian.Text.Trim();
            string selectedTrangThai = (cbTrangThaiTrinhDo.SelectedItem as ComboBoxItem)?.Content.ToString();

            var filteredList = danhSachTrinhDo.Where(td =>
                (string.IsNullOrEmpty(maNhanVien) || td.MANV.ToLower().Contains(maNhanVien)) &&
                (string.IsNullOrEmpty(maTrinhDo) || td.MATD.ToLower().Contains(maTrinhDo)) &&
                (string.IsNullOrEmpty(thoiGianHieuLuc) || (td.TGHOANTHANH.HasValue && td.TGHOANTHANH.Value.ToString() == thoiGianHieuLuc)) &&
                (selectedTrangThai == "Tất cả" || td.TRANGTHAI == selectedTrangThai)
            ).ToList();

            dataGridTrinhDo.ItemsSource = filteredList;
        }
        private void EditMoreInfoTD_Click(object sender, RoutedEventArgs e)
        {
            var selectedTrinhDo = (sender as Button)?.Tag as TrinhDo1;

            if (selectedTrinhDo != null)
            {
                EditTrinhDo editTrinhDoForm = new EditTrinhDo(selectedTrinhDo, trinhDoBLL);

                if (editTrinhDoForm.ShowDialog() == true)
                {
                    LoadTrinhDoData(); // Refresh DataGrid after editing
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn trình độ cần chỉnh sửa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



    }
}
