using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChucVu
    {
        public string MACV { get; set; }        // Mã chức vụ
        public string TENCV { get; set; }       // Tên chức vụ
        public int PHUCAP { get; set; }         // Phụ cấp
        public decimal HESOLUONG { get; set; }  // Hệ số lương
    }
}
