using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BaoHiem
    {
        public string SOBHYT { get; set; }         // Số bảo hiểm y tế
        public DateTime NGAYCAP { get; set; }     // Ngày cấp bảo hiểm y tế
        public DateTime GTSD { get; set; }        // Giá trị sử dụng từ ngày
        public DateTime? NGAYHETHAN { get; set; } // Ngày hết hạn
        public string NOIKHAMBENH { get; set; }   // Nơi khám bệnh
        public string MANV { get; set; }          // Mã nhân viên
        public string TRANGTHAI { get; set; }
    }
}