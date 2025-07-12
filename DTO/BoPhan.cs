using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BoPhan
    {
        public string MABP { get; set; }    // Mã bộ phận
        public string TENBP { get; set; }   // Tên bộ phận
        public string MAPB { get; set; }    // Mã phòng ban (khóa ngoại)
    }
}
