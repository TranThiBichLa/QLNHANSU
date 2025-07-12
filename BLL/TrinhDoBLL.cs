using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class TrinhDoBLL
    {
        private readonly TrinhDoAccess trinhDoDAL = new TrinhDoAccess();

        public List<TrinhDo1> GetAllTrinhDoNhanVien()
        {
            return trinhDoDAL.GetTrinhDoNhanVien();
        }
        public bool UpdateTrinhDo(TrinhDo1 trinhDo)
        {
            return trinhDoDAL.UpdateTrinhDo(trinhDo);
        }

    }
}