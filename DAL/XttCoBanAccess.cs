using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class XttCoBanAccess : DatabaseAccess
    {
        // Phương thức để lấy thông tin trình độ của nhân viên
        public List<TrinhDo> GetTrinhDoByNhanVien(string taiKhoan, string matKhau)
        {
            List<TrinhDo> trinhDos = new List<TrinhDo>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        td.MATD, 
                        td.TENTD, 
                        td.TGHOANTHANH, 
                        nvt.NGAYHETHAN AS NGAYHETHANTRINHDO
                    FROM NHANVIEN nv
                    INNER JOIN TAIKHOAN tk ON nv.MANV = tk.sMatKhau
                    LEFT JOIN NV_TD nvt ON nv.MANV = nvt.MANV
                    LEFT JOIN TRINHDO td ON nvt.MATD = td.MATD
                    WHERE tk.sTaiKhoan = @TaiKhoan AND tk.sMatKhau = @MatKhau";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var trinhDo = new TrinhDo
                    {
                        MATD = reader["MATD"].ToString(),
                        TENTD = reader["TENTD"].ToString(),
                        TGHOANTHANH = reader["TGHOANTHANH"] != DBNull.Value ? (int?)Convert.ToInt32(reader["TGHOANTHANH"]) : null,
                        NGAYHETHANTRINHDO = reader["NGAYHETHANTRINHDO"] != DBNull.Value ? (DateTime?)reader["NGAYHETHANTRINHDO"] : null
                    };

                    trinhDos.Add(trinhDo);
                }

                reader.Close();
            }

            return trinhDos;
        }

        // Phương thức để lấy thông tin nhân viên theo tài khoản và mật khẩu
        public XttCoBan GetNhanVienByTaiKhoan(string taiKhoan, string matKhau)
        {
            XttCoBan nv = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        nv.MANV, nv.HOTEN, nv.NTNS, nv.GT, nv.CCCD, nv.SDT, nv.EMAIL, nv.DIACHI, nv.HINHANH,
                        pb.MAPB, pb.TENPB, bp.MABP, bp.TENBP, cv.MACV, cv.TENCV,
                        bh.SOBHYT, bh.NGAYCAP, bh.GTSD, bh.NGAYHETHAN AS NGAYHETHANBAOHIEM, bh.NOIKHAMBENH,
                        td.MATD, td.TENTD, td.TGHOANTHANH, nvt.NGAYHETHAN AS NGAYHETHANTRINHDO
                    FROM NHANVIEN nv
                    INNER JOIN PHONGBAN pb ON nv.MAPB = pb.MAPB
                    INNER JOIN BOPHAN bp ON nv.MABP = bp.MABP
                    INNER JOIN CHUCVU cv ON nv.MACV = cv.MACV
                    INNER JOIN TAIKHOAN tk ON nv.MANV = tk.sMatKhau
                    LEFT JOIN BAOHIEM bh ON nv.MANV = bh.MANV
                    LEFT JOIN NV_TD nvt ON nv.MANV = nvt.MANV
                    LEFT JOIN TRINHDO td ON nvt.MATD = td.MATD
                    WHERE tk.sTaiKhoan = @TaiKhoan AND tk.sMatKhau = @MatKhau";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nv = new XttCoBan
                    {
                        MANV = reader["MANV"].ToString(),
                        HOTEN = reader["HOTEN"].ToString(),
                        NTNS = Convert.ToDateTime(reader["NTNS"]),
                        GT = reader["GT"].ToString() == "Nam",
                        CCCD = reader["CCCD"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        EMAIL = reader["EMAIL"].ToString(),
                        DIACHI = reader["DIACHI"].ToString(),
                        HinhAnh = reader["HINHANH"] as byte[],
                        MAPB = reader["MAPB"].ToString(),
                        TENPB = reader["TENPB"].ToString(),
                        MABP = reader["MABP"].ToString(),
                        TENBP = reader["TENBP"].ToString(),
                        MACV = reader["MACV"].ToString(),
                        TENCV = reader["TENCV"].ToString(),
                        SOBHYT = reader["SOBHYT"] != DBNull.Value ? reader["SOBHYT"].ToString() : null,
                        NGAYCAP = reader["NGAYCAP"] != DBNull.Value ? (DateTime?)reader["NGAYCAP"] : null,
                        GTSD = reader["GTSD"] != DBNull.Value ? (DateTime?)reader["GTSD"] : null,
                        NGAYHETHANBAOHIEM = reader["NGAYHETHANBAOHIEM"] != DBNull.Value ? (DateTime?)reader["NGAYHETHANBAOHIEM"] : null,
                        NOIKHAMBENH = reader["NOIKHAMBENH"] != DBNull.Value ? reader["NOIKHAMBENH"].ToString() : null,
                        MATD = reader["MATD"] != DBNull.Value ? reader["MATD"].ToString() : null,
                        TENTD = reader["TENTD"] != DBNull.Value ? reader["TENTD"].ToString() : null,
                        TGHOANTHANH = reader["TGHOANTHANH"] != DBNull.Value ? (int?)Convert.ToInt32(reader["TGHOANTHANH"]) : null,
                        NGAYHETHANTRINHDO = reader["NGAYHETHANTRINHDO"] != DBNull.Value ? (DateTime?)reader["NGAYHETHANTRINHDO"] : null
                    };
                }
                reader.Close();
            }

            return nv;
        }
    }
}
