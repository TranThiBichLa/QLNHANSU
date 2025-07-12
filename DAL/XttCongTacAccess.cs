using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class XttCongTacAccess : DatabaseAccess
    {
        // Lấy thông tin công tác của nhân viên dựa trên tài khoản và mật khẩu
        public List<XttCongTac> GetCongTacsInfo(string taiKhoan, string matKhau)
        {
            List<XttCongTac> congTacs = new List<XttCongTac>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT DISTINCT 
    nv.MANV, 
    nv.HOTEN, 
    hd.MAHD, 
    ct.NGAYQD, 
    pb.TENPB, 
    bp.TENBP, 
    cv.TENCV, 
    cv.HESOLUONG, 
    ct.TINHTRANG,
    nv.HINHANH
FROM NHANVIEN nv
LEFT JOIN CONGTAC ct ON ct.MANV = nv.MANV
INNER JOIN PHONGBAN pb ON nv.MAPB = pb.MAPB
INNER JOIN BOPHAN bp ON ct.MABP = bp.MABP
INNER JOIN CHUCVU cv ON ct.MACV = cv.MACV
INNER JOIN HOPDONGLAODONG hd ON ct.MAHD = hd.MAHD
INNER JOIN TAIKHOAN tk ON nv.MANV = tk.sMatKhau  
WHERE tk.sTaiKhoan = @TaiKhoan AND tk.sMatKhau = @MatKhau;
";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau); // Sử dụng matKhau trong truy vấn

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var congTac = new XttCongTac
                        {
                            MaNhanVien = reader["MANV"].ToString(),
                            TenNhanVien = reader["HOTEN"].ToString(),
                            MaHopDong = reader["MAHD"].ToString(),
                            NgayQuyetDinh = reader["NGAYQD"] != DBNull.Value ? Convert.ToDateTime(reader["NGAYQD"]) : default(DateTime),
                            PhongBan = reader["TENPB"].ToString(),
                            BoPhan = reader["TENBP"].ToString(),
                            ChucVu = reader["TENCV"].ToString(),
                            HeSoLuong = reader["HESOLUONG"] != DBNull.Value ? Convert.ToDecimal(reader["HESOLUONG"]) : 0,
                            TinhTrang = reader["TINHTRANG"].ToString(),
                            HinhAnh = reader["HINHANH"] != DBNull.Value ? (byte[])reader["HINHANH"] : null
                        };
                        congTacs.Add(congTac);
                    }
                }
            }
            return congTacs;
        }
    }
}
