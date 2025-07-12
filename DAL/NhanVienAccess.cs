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
    public class NhanVienAccess : DatabaseAccess
    {
        public NhanVienAccess() : base() { }

        public bool InsertNhanVien(NhanVien nv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO NHANVIEN 
                        (MANV, HOTEN, NTNS, GT, CCCD, SDT, EMAIL, DIACHI, HINHANH, MAPB, MABP, MACV, TRANGTHAI) 
                        VALUES 
                        (@MANV, @HOTEN, @NTNS, @GT, @CCCD, @SDT, @EMAIL, @DIACHI, @HINHANH, @MAPB, @MABP, @MACV, N'Đang làm việc')";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@MANV", nv.MANV);
                cmd.Parameters.AddWithValue("@HOTEN", nv.HOTEN);
                cmd.Parameters.AddWithValue("@NTNS", nv.NTNS);
                cmd.Parameters.AddWithValue("@GT", nv.GT);
                cmd.Parameters.AddWithValue("@CCCD", nv.CCCD);
                cmd.Parameters.AddWithValue("@SDT", nv.SDT);
                cmd.Parameters.AddWithValue("@EMAIL", nv.EMAIL);
                cmd.Parameters.AddWithValue("@DIACHI", nv.DIACHI);
                cmd.Parameters.AddWithValue("@MAPB", nv.MAPB);
                cmd.Parameters.AddWithValue("@MABP", nv.MABP);
                cmd.Parameters.AddWithValue("@MACV", nv.MACV);
                cmd.Parameters.AddWithValue("@HINHANH", nv.HINHANH ?? (object)DBNull.Value);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception($"Lỗi SQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi hệ thống: {ex.Message}");
                }
            }
        }


        public bool IsValidMaPB(string maPB)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM PHONGBAN WHERE MAPB = @MAPB";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MAPB", maPB);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Kiểm tra Mã Bộ Phận có tồn tại và liên kết đúng với Phòng Ban hay không
        public bool IsValidMaBP(string maBP, string maPB)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) 
                         FROM BOPHAN 
                         WHERE MABP = @MABP AND MAPB = @MAPB";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MABP", maBP);
                cmd.Parameters.AddWithValue("@MAPB", maPB);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }



        public bool IsValidMaCV(string maCV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM CHUCVU WHERE MACV = @MACV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MACV", maCV);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


        public List<NhanVien> GetAllNhanVien()
        {
            List<NhanVien> danhSachNhanVien = new List<NhanVien>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT MANV, HOTEN, NTNS, GT, CCCD, SDT, EMAIL, DIACHI, MAPB, MABP, MACV, HINHANH, TRANGTHAI FROM NHANVIEN";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var nhanVien = new NhanVien
                        {
                            MANV = reader["MANV"].ToString(),
                            HOTEN = reader["HOTEN"].ToString(),
                            NTNS = Convert.ToDateTime(reader["NTNS"]),
                            GT = reader["GT"].ToString(),
                            CCCD = reader["CCCD"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            EMAIL = reader["EMAIL"].ToString(),
                            DIACHI = reader["DIACHI"].ToString(),
                            MAPB = reader["MAPB"].ToString(),
                            MABP = reader["MABP"].ToString(),
                            MACV = reader["MACV"].ToString(),
                            HINHANH = reader["HINHANH"] != DBNull.Value ? (byte[])reader["HINHANH"] : null,
                            TRANGTHAI = reader["TRANGTHAI"].ToString()


                    };

                        Console.WriteLine($"Mã NV: {nhanVien.MANV}, Họ Tên: {nhanVien.HOTEN}"); // Kiểm tra dữ liệu
                        danhSachNhanVien.Add(nhanVien);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi SQL: {ex.Message}");
                }
            }

            return danhSachNhanVien;
        }

        public bool UpdateNhanVienTinhTrang(string maNV, string tinhTrang)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE NHANVIEN SET TRANGTHAI = @TinhTrang WHERE MANV = @MANV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TinhTrang", tinhTrang);
                cmd.Parameters.AddWithValue("@MANV", maNV);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Trả về true nếu thành công
                }
                catch (SqlException ex)
                {
                    throw new Exception($"Lỗi SQL: {ex.Message}");
                }
            }
        }
        public bool UpdateNhanVien(NhanVien nv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE NHANVIEN 
                         SET HOTEN = @HOTEN, NTNS = @NTNS, GT = @GT, CCCD = @CCCD, 
                             SDT = @SDT, EMAIL = @EMAIL, DIACHI = @DIACHI, 
                             MAPB = @MAPB, MABP = @MABP, MACV = @MACV, HINHANH = @HINHANH
                         WHERE MANV = @MANV";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HOTEN", nv.HOTEN);
                cmd.Parameters.AddWithValue("@NTNS", nv.NTNS);
                cmd.Parameters.AddWithValue("@GT", nv.GT);
                cmd.Parameters.AddWithValue("@CCCD", nv.CCCD);
                cmd.Parameters.AddWithValue("@SDT", nv.SDT);
                cmd.Parameters.AddWithValue("@EMAIL", nv.EMAIL);
                cmd.Parameters.AddWithValue("@DIACHI", nv.DIACHI);
                cmd.Parameters.AddWithValue("@MAPB", nv.MAPB);
                cmd.Parameters.AddWithValue("@MABP", nv.MABP);
                cmd.Parameters.AddWithValue("@MACV", nv.MACV);
                cmd.Parameters.AddWithValue("@HINHANH", nv.HINHANH ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MANV", nv.MANV);

                try
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật thông tin nhân viên: {ex.Message}");
                }
            }
        }
        public bool DeleteNhanVien(string maNV)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            DELETE FROM TANGCA WHERE MANV = @MANV;
            DELETE FROM NV_TD WHERE MANV = @MANV;
            DELETE FROM CONGTAC WHERE MANV = @MANV;
            DELETE FROM HOPDONGLAODONG WHERE MANV = @MANV;
            DELETE FROM TAIKHOAN WHERE sTaiKhoan = @MANV;
            DELETE FROM BAOHIEM WHERE MANV = @MANV;
            DELETE FROM NHANVIEN WHERE MANV = @MANV;";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MANV", maNV);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa nhân viên: {ex.Message}");
                }
            }
        }


        private readonly IDbConnection db;

        public NhanVienAccess(IDbConnection dbConnection)
        {
            db = dbConnection;
        }
        public NhanVien GetNhanVienById(string manv)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                string query = "SELECT * FROM NHANVIEN WHERE MANV = @manv";

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;

                    var param = cmd.CreateParameter();
                    param.ParameterName = "@manv";
                    param.Value = manv;
                    cmd.Parameters.Add(param);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NhanVien
                            {
                                MANV = reader["MANV"].ToString(),
                                HOTEN = reader["HOTEN"].ToString(),
                                NTNS = Convert.ToDateTime(reader["NTNS"]),
                                GT = reader["GT"].ToString(),
                                CCCD = reader["CCCD"].ToString(),
                                SDT = reader["SDT"].ToString(),
                                EMAIL = reader["EMAIL"].ToString(),
                                DIACHI = reader["DIACHI"].ToString(),
                                MAPB = reader["MAPB"].ToString(),
                                MABP = reader["MABP"].ToString(),
                                MACV = reader["MACV"].ToString(),
                                TRANGTHAI = reader["TRANGTHAI"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }




    }
}