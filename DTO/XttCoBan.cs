using System;
using System.Collections.Generic;

namespace DTO
{
    public class XttCoBan
    {
        // Thông tin cá nhân
        public string MANV { get; set; }
        public string HOTEN { get; set; }
        public DateTime NTNS { get; set; }
        public bool GT { get; set; } // Giới tính: True = Nam, False = Nữ
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public string EMAIL { get; set; }
        public string DIACHI { get; set; }

        // Phòng ban & chức vụ
        public string MAPB { get; set; }
        public string TENPB { get; set; }
        public string MABP { get; set; }
        public string TENBP { get; set; }
        public string MACV { get; set; }
        public string TENCV { get; set; }

        // Bảo hiểm y tế
        public string SOBHYT { get; set; }
        public DateTime? NGAYCAP { get; set; }
        public DateTime? GTSD { get; set; }
        public DateTime? NGAYHETHANBAOHIEM { get; set; } // Nullable DateTime
        public string NOIKHAMBENH { get; set; }

        // Trình độ
        public string MATD { get; set; }
        public string TENTD { get; set; }
        public int? TGHOANTHANH { get; set; }
        public DateTime? NGAYHETHANTRINHDO { get; set; } // Nullable DateTime

        // Danh sách các trình độ của nhân viên
        public List<TrinhDo> TrinhDoList { get; set; } = new List<TrinhDo>();

        // Hình ảnh
        public byte[] HinhAnh { get; set; } = Array.Empty<byte>();

        // Constructor mặc định
        public XttCoBan() { }

        // Constructor có tham số
        public XttCoBan(string manv, string hoten, DateTime ntns, bool gt, string cccd, string sdt, string email, string diachi,
                        string mapb, string tenpb, string mabp, string tenbp, string macv, string tencv,
                        string sobhyt, DateTime ngaycap, DateTime gtsd, DateTime? ngayhethanbaohiem, string noikhambenh,
                        string matd, string tentd, int tghoanthan, DateTime? ngayhethantrinhdo, byte[] hinhAnh)
        {
            // Thông tin cá nhân
            MANV = manv;
            HOTEN = hoten;
            NTNS = ntns;
            GT = gt;
            CCCD = cccd;
            SDT = sdt;
            EMAIL = email;
            DIACHI = diachi;

            // Phòng ban & chức vụ
            MAPB = mapb;
            TENPB = tenpb;
            MABP = mabp;
            TENBP = tenbp;
            MACV = macv;
            TENCV = tencv;

            // Bảo hiểm y tế
            SOBHYT = sobhyt;
            NGAYCAP = ngaycap;
            GTSD = gtsd;
            NGAYHETHANBAOHIEM = ngayhethanbaohiem;
            NOIKHAMBENH = noikhambenh;

            // Trình độ
            MATD = matd;
            TENTD = tentd;
            TGHOANTHANH = tghoanthan;
            NGAYHETHANTRINHDO = ngayhethantrinhdo;
            HinhAnh = hinhAnh ?? Array.Empty<byte>(); // Nếu null thì dùng mảng byte trống
        }
    }

    // Lớp trình độ để chứa thông tin về trình độ của nhân viên
    public class TrinhDo
    {
        public string MATD { get; set; } // Mã trình độ
        public string TENTD { get; set; } // Tên trình độ
        public int? TGHOANTHANH { get; set; } // Thời gian hoàn thành
        public DateTime? NGAYHETHANTRINHDO { get; set; } // Ngày hết hạn của trình độ
    }
}
