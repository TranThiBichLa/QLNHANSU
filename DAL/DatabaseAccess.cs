using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
using System.Data;
namespace DAL
{
    public class SqlConnectionData
    {//tạo chuỗi kết nối với cơ sở dữ liệu
        public static SqlConnection Connect()
        {
            string strcon = @"Data Source=LAPTOP-MSDUJDE8\MSSQLMYSERVER;Initial Catalog=QuanLyNhanSu;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
            SqlConnection conn = new SqlConnection(strcon); //khởi tạo connect
            return conn;
        }

    }
    public class DatabaseAccess
    {
        public static string CheckLoginDTO(TaiKhoan taikhoan)
        {
            string user = null;
            // hàm connect tới csdl
            SqlConnection conn = SqlConnectionData.Connect();
            conn.Open();
            SqlCommand command = new SqlCommand("proc_login", conn);// tự đặt tên ""
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@user", taikhoan.sTaiKhoan);//tự đặt tên ""
            command.Parameters.AddWithValue("@pass", taikhoan.sMatKhau);//tự đặt tên ""
            //kiểm tra quyền các bạn thêm 1 cái parameter...
            command.Connection = conn;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user = reader.GetString(0);
                    return user;
                }
                reader.Close();
                conn.Close();
            }
            else
            {
                return "Tài khoản hoặc mật khẩu không chính xác!";
            }
            return user;
        }
    }
}
