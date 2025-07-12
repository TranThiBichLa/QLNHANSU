using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace DAL
{
    public class TaiKhoanAccess : DatabaseAccess
    {
        public DataTable GetUserWithRole(TaiKhoan taikhoan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("proc_login", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm tham số cho stored procedure
                cmd.Parameters.AddWithValue("@user", taikhoan.sTaiKhoan);  // Tên tài khoản
                cmd.Parameters.AddWithValue("@pass", taikhoan.sMatKhau);    // Mật khẩu

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }

        public string CheckLogin(TaiKhoan taikhoan, out string userRole)
        {
            userRole = string.Empty; // Khởi tạo giá trị quyền mặc định

            if (string.IsNullOrEmpty(taikhoan.sTaiKhoan))
            {
                return "request_taikhoan"; // Trả về lỗi nếu tài khoản trống
            }
            if (string.IsNullOrEmpty(taikhoan.sMatKhau))
            {
                return "request_password"; // Trả về lỗi nếu mật khẩu trống
            }

            // Lấy dữ liệu từ cơ sở dữ liệu và kiểm tra
            DataTable result = GetUserWithRole(taikhoan);
            if (result.Rows.Count > 0)
            {
                // Lấy quyền của người dùng từ kết quả trả về
                userRole = result.Rows[0]["sTenQuyen"].ToString();
                return "success"; // Đăng nhập thành công
            }

            return "invalid_login"; // Nếu không tìm thấy người dùng
        }
    }

}
