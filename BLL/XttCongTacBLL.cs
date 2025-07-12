using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class XttCongTacBLL
    {
        private XttCongTacAccess congTacAccess = new XttCongTacAccess();

        // Phương thức lấy thông tin công tác của nhân viên
        public List<XttCongTac> GetCongTacInfo(string taiKhoan, string matKhau)
        {
            // Gọi từ DAL và trả về danh sách công tác của nhân viên
            return congTacAccess.GetCongTacsInfo(taiKhoan, matKhau);
        }
    }
}
