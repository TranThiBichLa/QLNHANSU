using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class CongTacBLL
    {
        private CongTacAccess congTacAccess = new CongTacAccess();

        public Task<List<CongTac>> GetAll()
        {
            return congTacAccess.GetAllAsync();
        }

        public Task<List<CongTac>> Search(string maNhanVien, string maHD, string trangThai)
        {
            return congTacAccess.SearchAsync(maNhanVien, maHD, trangThai);
        }
    }
}