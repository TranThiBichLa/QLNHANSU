using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TangCa
    {
        public string MaTangCa { get; set; }
        public DateTime NgayTangCa { get; set; }
        public int SoGio { get; set; }
        public int Luong1GioTangCa { get; set; } = 30000;
        public string TenLoaiCa { get; set; }
        public decimal HeSoLoaiCa { get; set; }
        public decimal LuongTangCa { get; set; }
        public string MaNhanVien { get; set; }
    }

}