using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CongTac
    {

        public string MaNhanVien { get; set; }
        public string MaHD { get; set; }
        public string MaPhongBan { get; set; }
        public string MaBoPhan { get; set; }
        public string MaChucVu { get; set; }
        public DateTime NgayQuyetDinh { get; set; } // Ensure this property exists
        public string TinhTrangCongTac { get; set; }


    }
}