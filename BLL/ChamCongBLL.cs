using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class ChamCongBLL
    {
        private ChamCongAccess chamCongDAL = new ChamCongAccess();

        public ChamCongBLL()
        {
            chamCongDAL = new ChamCongAccess();
        }
        public List<ChamCongDTO> GetChamCong(string maNV, int thang, int nam)
        {
            
            return chamCongDAL.GetChamCong(maNV, thang, nam);
        }
        public List<ChamCongDTO> GetChamCong()
        {

            return chamCongDAL.GetChamCong();
        }
        public List<int> GetDistinctNhanVienByYear(int year)
        {
            return chamCongDAL.GetDistinctNhanVienByYear(year);
        }

        public bool InsertChamCong(ChamCongDTO chamCong)
        {
            return chamCongDAL.InsertChamCong(chamCong);
        }
        

    }
}
