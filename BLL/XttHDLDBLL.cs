using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class XttHDLDBLL
    {
        private XttHDLDAccess hdAccess = new XttHDLDAccess();

        // Cập nhật phương thức để lấy danh sách hợp đồng lao động của nhân viên theo tài khoản
        public List<XttHDLD> GetHDByTaiKhoan(string taiKhoan)
        {
            return hdAccess.GetHDByTaiKhoan(taiKhoan); // Gọi từ DAL và trả về danh sách hợp đồng lao động
        }
    }
}
