using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class XttHDLDAccess : DatabaseAccess
    {
        // Lấy danh sách hợp đồng lao động của nhân viên theo tài khoản
        public List<XttHDLD> GetHDByTaiKhoan(string taiKhoan)
        {
            List<XttHDLD> hdList = new List<XttHDLD>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        -- Thông tin nhân viên
                        nv.MANV, nv.HOTEN, nv.NTNS, nv.GT, nv.CCCD, nv.SDT, nv.EMAIL, nv.DIACHI,
                        -- Phòng ban, bộ phận, chức vụ
                        pb.MAPB, pb.TENPB, bp.MABP, bp.TENBP, cv.MACV, cv.TENCV,
                        -- Hợp đồng lao động
                        hd.MAHD, hd.NGAYKYHD, hd.NGBD, hd.NGKT, hd.TINHTRANG, hd.MUCLUONGCOBAN
                    FROM NHANVIEN nv
                    -- Kết nối với bảng Phòng ban, Bộ phận và Chức vụ
                    INNER JOIN PHONGBAN pb ON nv.MAPB = pb.MAPB
                    INNER JOIN BOPHAN bp ON nv.MABP = bp.MABP
                    INNER JOIN CHUCVU cv ON nv.MACV = cv.MACV
                    -- Kết nối với bảng Tài khoản
                    INNER JOIN TAIKHOAN tk ON nv.MANV = tk.sMatKhau
                    -- Kết nối với bảng Hợp đồng lao động
                    LEFT JOIN HOPDONGLAODONG hd ON nv.MANV = hd.MANV
                    WHERE tk.sTaiKhoan = @TaiKhoan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var hd = new XttHDLD
                        {
                            // Thông tin cá nhân
                            MaNV = reader["MANV"].ToString(),
                            // Phòng ban, bộ phận, chức vụ
                            MaPB = reader["MAPB"].ToString(),
                            MaBP = reader["MABP"].ToString(),
                            MaCV = reader["MACV"].ToString(),
                            // Hợp đồng lao động
                            MaHD = reader["MAHD"].ToString(),
                            NgayKyHD = reader["NGAYKYHD"] != DBNull.Value ? Convert.ToDateTime(reader["NGAYKYHD"]) : (DateTime?)null,
                            NGBD = reader["NGBD"] != DBNull.Value ? Convert.ToDateTime(reader["NGBD"]) : (DateTime?)null,
                            NGKT = reader["NGKT"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["NGKT"]) : null,
                            TinhTrang = reader["TINHTRANG"].ToString(),
                            MucLuongCoBan = reader["MUCLUONGCOBAN"] != DBNull.Value ? Convert.ToInt32(reader["MUCLUONGCOBAN"]) : 0
                        };
                        hdList.Add(hd);
                    }
                }
            }

            return hdList;
        }
    }
}
