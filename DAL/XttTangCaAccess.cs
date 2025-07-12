using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class XttTangCaAccess: DatabaseAccess
    {
        public List<DTO.DangKi.TangCaDTO> GetTangCaByTaiKhoan(string taiKhoan)
        {
            List<DTO.DangKi.TangCaDTO> tangCaList = new List<DTO.DangKi.TangCaDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
        SELECT 
            T.MATANGCA, 
            T.NGAYTANGCA, 
            T.SOGIO, 
            T.MALOAICA, 
            L.HESOLOAICA, 
            T.MANV, 
            T.LUONGTANGCA
        FROM 
            TANGCA T
        JOIN 
            LOAICA L ON T.MALOAICA = L.MALOAICA
        JOIN 
            TAIKHOAN TK ON T.MANV = TK.sMatKhau
        WHERE 
            TK.sTaiKhoan = @TaiKhoan";  // Lọc theo tài khoản của người đăng nhập

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan); // Thêm tham số tài khoản vào câu truy vấn

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DTO.DangKi.TangCaDTO tangCa = new DTO.DangKi.TangCaDTO
                    {
                        MaTangCa = reader["MATANGCA"].ToString(),
                        NgayTangCa = Convert.ToDateTime(reader["NGAYTANGCA"]),
                        SoGioTangCa = Convert.ToInt32(reader["SOGIO"]),
                        MaLoaiCa = reader["MALOAICA"].ToString(),
                        HeSoLoaiCa = Convert.ToSingle(reader["HESOLOAICA"]),
                        MaNhanVien = reader["MANV"].ToString(),
                        LuongTangCa = Convert.ToDouble(reader["LUONGTANGCA"])
                    };
                    tangCaList.Add(tangCa);
                }
                reader.Close();
            }

            return tangCaList;
        }

    }
}
