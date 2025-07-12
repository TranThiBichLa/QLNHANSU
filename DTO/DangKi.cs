using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DangKi
    {
        public class BaoHiemDTO
        {
            public string SoBHYT { get; set; } // Số bảo hiểm y tế
            public DateTime? NgayCap { get; set; } // Ngày cấp
            public DateTime? GiaTriSuDung { get; set; } // Giá trị sử dụng từ ngày
            public DateTime? NgayHetHan { get; set; } // Ngày hết hạn (có thể null)
            public string NoiKhamBenh { get; set; } // Nơi khám bệnh
            public string MaNV { get; set; } // Mã nhân viên
        }

        public class TrinhDoDTO
        {
            public string MaTrinhDo { get; set; } // Mã trình độ
            public string TenTrinhDo { get; set; } // Tên trình độ
            public int? ThoiGianHoanThanh { get; set; } // Thời gian hoàn thành (có thể null)
            public DateTime? NgayHetHan { get; set; } // Ngày hết hạn (có thể null)
            public string MaNV { get; set; }
        }

        public class TangCaDTO
        {
            public string MaNhanVien { get; set; } // Mã nhân viên
            public string MaLoaiCa { get; set; } // Mã loại ca
            public string MaTangCa { get; set; } // Mã tăng ca
            public DateTime NgayTangCa { get; set; } // Ngày tăng ca
            public int SoGioTangCa { get; set; } // Số giờ tăng ca
            public double LuongTangCa { get; set; }
            public float HeSoLoaiCa { get; set; }

        }
    }
}
