using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ChucVuBLL
    {
        private readonly ChucVuAccess chucVuAccess;

        public ChucVuBLL(ChucVuAccess chucVuAccess)
        {
            this.chucVuAccess = chucVuAccess;
        }

        public ChucVu GetChucVuById(string macv)
        {
            return chucVuAccess.GetChucVuById(macv);
        }
    }
}
