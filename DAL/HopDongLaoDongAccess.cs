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
    public class HopDongLaoDongAccess : DatabaseAccess
    {
        public bool InsertHopDong(HopDongLaoDong hopDong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Lấy giá trị HESOLUONG từ bảng CHUCVU
                string getHESOLUONGQuery = "SELECT HESOLUONG FROM CHUCVU WHERE MACV = @MACV";
                SqlCommand cmdHESOLUONG = new SqlCommand(getHESOLUONGQuery, conn);
                cmdHESOLUONG.Parameters.AddWithValue("@MACV", hopDong.MACV);

                conn.Open();
                decimal hesoluong = (decimal)cmdHESOLUONG.ExecuteScalar();

                string query = @"INSERT INTO HOPDONGLAODONG (MAHD, NGAYKYHD, NGBD, NGKT, TINHTRANG, MANV, MAPB, MABP, MACV, MUCLUONGCOBAN, HESOLUONG)
                         VALUES (@MAHD, @NGAYKYHD, @NGBD, @NGKT, @TINHTRANG, @MANV, @MAPB, @MABP, @MACV, @MUCLUONGCOBAN, @HESOLUONG)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MAHD", hopDong.MAHD);
                cmd.Parameters.AddWithValue("@NGAYKYHD", hopDong.NGAYKYHD);
                cmd.Parameters.AddWithValue("@NGBD", hopDong.NGBD);
                cmd.Parameters.AddWithValue("@NGKT", hopDong.NGKT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TINHTRANG", hopDong.TINHTRANG);
                cmd.Parameters.AddWithValue("@MANV", hopDong.MANV);
                cmd.Parameters.AddWithValue("@MAPB", hopDong.MAPB);
                cmd.Parameters.AddWithValue("@MABP", hopDong.MABP);
                cmd.Parameters.AddWithValue("@MACV", hopDong.MACV);
                cmd.Parameters.AddWithValue("@MUCLUONGCOBAN", hopDong.MUCLUONGCOBAN);
                cmd.Parameters.AddWithValue("@HESOLUONG", hesoluong);  // Chèn giá trị HESOLUONG hợp lệ

                return cmd.ExecuteNonQuery() > 0;
            }
        }


        public bool UpdateHopDong(HopDongLaoDong hopDong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE HOPDONGLAODONG
SET 
    NGAYKYHD = @NGAYKYHD, 
    NGBD = @NGBD, 
    NGKT = @NGKT, 
    TINHTRANG = @TINHTRANG, 
    MAPB = @MAPB, 
    MABP = @MABP, 
    MACV = @MACV, 
    MUCLUONGCOBAN = @MUCLUONGCOBAN, 
    HESOLUONG = (SELECT HESOLUONG FROM CHUCVU WHERE MACV = @MACV)
WHERE MAHD = @MAHD;
";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MAHD", hopDong.MAHD);
                cmd.Parameters.AddWithValue("@NGAYKYHD", hopDong.NGAYKYHD);
                cmd.Parameters.AddWithValue("@NGBD", hopDong.NGBD);
                cmd.Parameters.AddWithValue("@NGKT", hopDong.NGKT ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TINHTRANG", hopDong.TINHTRANG);
                cmd.Parameters.AddWithValue("@MAPB", hopDong.MAPB);
                cmd.Parameters.AddWithValue("@MABP", hopDong.MABP);
                cmd.Parameters.AddWithValue("@MACV", hopDong.MACV);
                cmd.Parameters.AddWithValue("@MUCLUONGCOBAN", hopDong.MUCLUONGCOBAN);
                cmd.Parameters.AddWithValue("@HESOLUONG", hopDong.HESOLUONG);

                conn.Open(); 
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteHopDong(string mahd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM HOPDONGLAODONG WHERE MAHD = @MAHD";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MAHD", mahd);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<HopDongLaoDong> GetAllHopDongs()
        {
            List<HopDongLaoDong> hopDongs = new List<HopDongLaoDong>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM HOPDONGLAODONG";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        hopDongs.Add(new HopDongLaoDong
                        {
                            MAHD = reader["MAHD"].ToString(),
                            NGAYKYHD = Convert.ToDateTime(reader["NGAYKYHD"]),
                            NGBD = Convert.ToDateTime(reader["NGBD"]),
                            NGKT = reader["NGKT"] != DBNull.Value ? (DateTime?)reader["NGKT"] : null,
                            TINHTRANG = reader["TINHTRANG"].ToString(),
                            MANV = reader["MANV"].ToString(),
                            MAPB = reader["MAPB"]?.ToString(),
                            MABP = reader["MABP"]?.ToString(),
                            MACV = reader["MACV"]?.ToString(),
                            MUCLUONGCOBAN = Convert.ToInt32(reader["MUCLUONGCOBAN"]),
                            HESOLUONG = reader["HESOLUONG"] != DBNull.Value ? Convert.ToDecimal(reader["HESOLUONG"]) : (decimal)0.00

                    });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy danh sách hợp đồng: {ex.Message}");
                }
            }

            return hopDongs;
        }

        public List<HopDongLaoDong> GetContractsByNhanVien(string manv)
        {
            List<HopDongLaoDong> contracts = new List<HopDongLaoDong>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM HOPDONGLAODONG WHERE MANV = @MANV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MANV", manv);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    contracts.Add(new HopDongLaoDong
                    {
                        MAHD = reader["MAHD"].ToString(),
                        NGAYKYHD = Convert.ToDateTime(reader["NGAYKYHD"]),
                        NGBD = Convert.ToDateTime(reader["NGBD"]),
                        NGKT = reader["NGKT"] != DBNull.Value ? (DateTime?)reader["NGKT"] : null,
                        TINHTRANG = reader["TINHTRANG"].ToString(),
                        MANV = reader["MANV"].ToString(),
                        MAPB = reader["MAPB"]?.ToString(),
                        MABP = reader["MABP"]?.ToString(),
                        MACV = reader["MACV"]?.ToString(),
                        MUCLUONGCOBAN = Convert.ToInt32(reader["MUCLUONGCOBAN"]),
                        HESOLUONG = Convert.ToDecimal(reader["HESOLUONG"])
                    });
                }
            }

            return contracts;
        }


        public List<Tuple<string, int>> GetNhanVienTheoViTri(string viTri, int year)
        {
            var result = new List<Tuple<string, int>>();
            string query = "";

            // Chọn query phù hợp dựa trên loại vị trí
            switch (viTri)
            {
                case "Phòng Ban":
                    query = @"
                SELECT PB.TENPB AS Ten, COUNT(DISTINCT HD.MANV) AS SoLuongNhanVien
                FROM HOPDONGLAODONG HD
                JOIN PHONGBAN PB ON HD.MAPB = PB.MAPB
                WHERE YEAR(HD.NGBD) <= @Year 
                  AND (HD.NGKT IS NULL OR YEAR(HD.NGKT) >= @Year)
                GROUP BY PB.TENPB";
                    break;
                case "Bộ Phận":
                    query = @"
                SELECT BP.TENBP AS Ten, COUNT(DISTINCT HD.MANV) AS SoLuongNhanVien
                FROM HOPDONGLAODONG HD
                JOIN BOPHAN BP ON HD.MABP = BP.MABP
                WHERE YEAR(HD.NGBD) <= @Year 
                  AND (HD.NGKT IS NULL OR YEAR(HD.NGKT) >= @Year)
                GROUP BY BP.TENBP";
                    break;
                case "Chức vụ":
                    query = @"
                SELECT CV.TENCV AS Ten, COUNT(DISTINCT HD.MANV) AS SoLuongNhanVien
                FROM HOPDONGLAODONG HD
                JOIN CHUCVU CV ON HD.MACV = CV.MACV
                WHERE YEAR(HD.NGBD) <= @Year 
                  AND (HD.NGKT IS NULL OR YEAR(HD.NGKT) >= @Year)
                GROUP BY CV.TENCV";
                    break;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Year", year);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Tuple<string, int>(
                                reader["Ten"].ToString(),
                                Convert.ToInt32(reader["SoLuongNhanVien"])
                            ));
                        }
                    }
                }
            }

            return result;
        }



    }
}
