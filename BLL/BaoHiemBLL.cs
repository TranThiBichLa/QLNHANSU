using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using static DTO.DangKi;

namespace BLL
{
    public class BaoHiemBLL
    {
        BaoHiemAccess baoHiemAccess = new BaoHiemAccess();

        public List<BaoHiem> GetAllBaoHiem()
        {
            return baoHiemAccess.GetAllBaoHiem();
        }
        public bool UpdateBaoHiem(BaoHiem baoHiem)
        {
            return baoHiemAccess.UpdateBaoHiem(baoHiem);
        }

    }
}