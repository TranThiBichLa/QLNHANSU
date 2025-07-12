using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class XttTangCaBLL
    {
        private XttTangCaAccess tangCaAccess = new XttTangCaAccess(); // Đảm bảo sử dụng đúng lớp Access

        public List<DTO.DangKi.TangCaDTO> GetTangCaByTaiKhoan(string taiKhoan)
        {
            return tangCaAccess.GetTangCaByTaiKhoan(taiKhoan); // Lấy dữ liệu từ DAL
        }


    }
}
