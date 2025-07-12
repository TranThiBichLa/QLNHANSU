using DTO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO.DangKi;

namespace BLL
{
    public class TangCaBLL
    {
        private readonly TangCaAccess tangCaAccess;

        public TangCaBLL()
        {
            tangCaAccess = new TangCaAccess();
        }

        // Lấy tất cả dữ liệu tăng ca
        public List<TangCa> GetAllTangCaRecords()
        {
            return tangCaAccess.GetTangCaRecords();
        }

        // Lấy dữ liệu tăng ca theo bộ lọc
        public List<TangCa> GetFilteredTangCaRecords(string maNhanVien, string loaiCa)
        {
            return tangCaAccess.GetTangCaRecords(maNhanVien, loaiCa);
        }
    }
}