using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BLL
{
    public class XttCoBanBLL
    {
        private XttCoBanAccess tkAccess = new XttCoBanAccess(); // Khởi tạo đối tượng DAL

        // Cập nhật phương thức để nhận tài khoản và mật khẩu và trả về thông tin của nhân viên bao gồm thông tin trình độ
        public XttCoBan GetNhanVienInfo(string taiKhoan, string matKhau)
        {
            // Lấy thông tin nhân viên từ DAL
            XttCoBan nv = tkAccess.GetNhanVienByTaiKhoan(taiKhoan, matKhau);

            if (nv != null)
            {
                // Lấy thông tin trình độ của nhân viên từ DAL
                nv.TrinhDoList = tkAccess.GetTrinhDoByNhanVien(taiKhoan, matKhau);
            }

            return nv; // Trả về đối tượng nhân viên cùng thông tin trình độ
        }
    }
}
