using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Data;

namespace BLL
{
    public class TaiKhoanBLL
    {
        TaiKhoanAccess tkAccess = new TaiKhoanAccess();
        public string CheckLogin(TaiKhoan taikhoan, out string userRole)
        {
            userRole = string.Empty; // Biến để lưu quyền người dùng

            if (string.IsNullOrEmpty(taikhoan.sTaiKhoan))
            {
                return "request_taikhoan";  // Kiểm tra nếu tài khoản trống
            }
            if (string.IsNullOrEmpty(taikhoan.sMatKhau))
            {
                return "request_password";  // Kiểm tra nếu mật khẩu trống
            }

            // Gọi phương thức DAL để kiểm tra tài khoản và mật khẩu
            DataTable result = tkAccess.GetUserWithRole(taikhoan);
            if (result.Rows.Count > 0)
            {
                userRole = result.Rows[0]["sTenQuyen"].ToString();
                return "success";  // Đăng nhập thành công
            }
            return "invalid_login";  // Nếu không tìm thấy người dùng
        }


        private DatabaseAccess dbAccess = new DatabaseAccess();

        public DataTable GetTaiKhoan()
        {
            string query = "SELECT * FROM TaiKhoan"; // Ví dụ
            return dbAccess.GetData(query);
        }
    }
}
