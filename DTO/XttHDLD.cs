using System;

namespace DTO
{
    public class XttHDLD
    {
        public string MaHD { get; set; }        // Mã hợp đồng lao động (Khóa chính)
        public DateTime? NgayKyHD { get; set; } // Ngày ký hợp đồng lao động
        public DateTime? NGBD { get; set; }     // Ngày bắt đầu hợp đồng
        public DateTime? NGKT { get; set; }    // Ngày kết thúc hợp đồng (có thể NULL)
        public string TinhTrang { get; set; }  // Tình trạng hợp đồng (Đang có hiệu lực hoặc Hết hiệu lực)
        public string MaNV { get; set; }       // Mã nhân viên (Liên kết tới bảng NHANVIEN)
        public string MaPB { get; set; }       // Mã phòng ban
        public string MaBP { get; set; }       // Mã bộ phận
        public string MaCV { get; set; }       // Mã chức vụ
        public int MucLuongCoBan { get; set; } // Mức lương cơ bản (phải > 0)

        // Constructor không tham số
        public XttHDLD() { }

        // Constructor đầy đủ tham số
        public XttHDLD(string maHD, DateTime ngayKyHD, DateTime ngBD, DateTime? ngKT, string tinhTrang,
                                  string maNV, string maPB, string maBP, string maCV, int mucLuongCoBan)
        {
            MaHD = maHD;
            NgayKyHD = ngayKyHD;
            NGBD = ngBD;
            NGKT = ngKT;
            TinhTrang = tinhTrang;
            MaNV = maNV;
            MaPB = maPB;
            MaBP = maBP;
            MaCV = maCV;
            MucLuongCoBan = mucLuongCoBan;
        }

        // Override phương thức ToString() để dễ dàng hiển thị thông tin
        public override string ToString()
        {
            return $"Mã hợp đồng: {MaHD}, Ngày ký: {NgayKyHD:yyyy-MM-dd}, Ngày bắt đầu: {NGBD:yyyy-MM-dd}, " +
                   $"Ngày kết thúc: {(NGKT.HasValue ? NGKT.Value.ToString("yyyy-MM-dd") : "NULL")}, Tình trạng: {TinhTrang}, " +
                   $"Mã nhân viên: {MaNV}, Mã phòng ban: {MaPB}, Mã bộ phận: {MaBP}, Mã chức vụ: {MaCV}, Mức lương cơ bản: {MucLuongCoBan}";
        }
    }
}
