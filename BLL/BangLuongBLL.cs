using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using static BLL.BangLuongBLL;
namespace BLL
{
    public class BangLuongBLL
    {
        
            private BangLuongAccess bangLuongDAL = new BangLuongAccess();

            public List<BangLuongDTO> GetBangLuongByNhanVien(string maNV, int thang, int nam)
            {
                return bangLuongDAL.GetBangLuongByNhanVien(maNV, thang, nam);
            }
        public List<BangLuongDTO> GetAllBangLuong()
        {
            return bangLuongDAL.GetAllBangLuong();
        }

        public List<Tuple<int, int>> GetTongLuongTheoNam(int year)
        {
            return bangLuongDAL.GetTongLuongTheoNam(year);
        }

    }
}
