using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BangLuongDTO
    {
        public string MaBangLuong {  get; set; }
        public string MANV { get; set; }

        public int Thang { get; set; }
        public int Nam { get; set; }
        public int LuongCoBan { get; set; }
        public int LuongTangCa { get; set; }
        public int PhuCap { get; set; }
        public int TIENBTRU { get; set; } // Thêm thuộc tính Tiền Bị Trừ
        public int TongLuong { get; set; }
    }
}
