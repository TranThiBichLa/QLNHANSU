using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class XttCongTac
    {  // Các thuộc tính cần thiết
        public string MaNhanVien { get; set; } // Mã nhân viên
        public string TenNhanVien { get; set; } // Tên nhân viên
        public string MaHopDong { get; set; } // Mã hợp đồng
        public DateTime NgayQuyetDinh { get; set; } // Ngày quyết định
        public string PhongBan { get; set; } // Tên phòng ban
        public string BoPhan { get; set; } // Tên bộ phận
        public string ChucVu { get; set; } // Tên chức vụ
        public decimal HeSoLuong { get; set; } // Hệ số lương
        public string TinhTrang { get; set; } // Tình trạng công tác
        public byte[] HinhAnh { get; set; } = Array.Empty<byte>();

        // Constructor mặc định
        public XttCongTac() { }

        // Constructor có tham số
        public XttCongTac(string maNhanVien, string tenNhanVien, string maHopDong, DateTime ngayQuyetDinh,
                          string phongBan, string boPhan, string chucVu, decimal heSoLuong, string tinhTrang, byte[] hinhAnh)
        {
            MaNhanVien = maNhanVien;
            TenNhanVien = tenNhanVien;
            MaHopDong = maHopDong;
            NgayQuyetDinh = ngayQuyetDinh;
            PhongBan = phongBan;
            BoPhan = boPhan;
            ChucVu = chucVu;
            HeSoLuong = heSoLuong;
            TinhTrang = tinhTrang;
            HinhAnh = hinhAnh ?? Array.Empty<byte>(); // Nếu null thì dùng mảng byte trống
        }
    }
}
