using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TrinhDo1
    {

        public string MANV { get; set; }        // Mã nhân viên
        public string HOTEN { get; set; }      // Họ và tên nhân viên
        public string MATD { get; set; }       // Mã trình độ
        public string TENTD { get; set; }      // Tên trình độ
        public int? TGHOANTHANH { get; set; }  // Thời gian hoàn thành (năm)
        public DateTime? NGAYHETHAN { get; set; } // Ngày hết hạn (nếu có)
        public string TRANGTHAI { get; set; }  // Trạng thái (Còn hiệu lực / Đã hết hiệu lực)


    }
}