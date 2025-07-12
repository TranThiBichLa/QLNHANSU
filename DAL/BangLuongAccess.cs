using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class BangLuongAccess : DatabaseAccess
    {
        public List<BangLuongDTO> GetBangLuongByNhanVien(string maNV, int thang, int nam)
        {
            List<BangLuongDTO> bangLuongList = new List<BangLuongDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    THANG,
                    NAM,
                    LUONGCOBAN,
                    LUONGTANGCA,
                    PHUCAP,
                    TIENBTRU,
                    TONGLUONG
                FROM BANG_LUONG
                WHERE MANV = @MaNV AND THANG = @Thang AND NAM = @Nam";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BangLuongDTO bangLuong = new BangLuongDTO
                        {
                            Thang = reader.GetInt32(reader.GetOrdinal("THANG")),
                            Nam = reader.GetInt32(reader.GetOrdinal("NAM")),
                            LuongCoBan = reader.GetInt32(reader.GetOrdinal("LUONGCOBAN")),
                            LuongTangCa = reader.GetInt32(reader.GetOrdinal("LUONGTANGCA")),
                            PhuCap = reader.GetInt32(reader.GetOrdinal("PHUCAP")),
                            TIENBTRU = reader.GetInt32(reader.GetOrdinal("TIENBTRU")),
                            TongLuong = reader.GetInt32(reader.GetOrdinal("TONGLUONG"))
                        };

                        bangLuongList.Add(bangLuong);
                    }
                }
            }

            return bangLuongList;
        }
        public List<BangLuongDTO> GetAllBangLuong()
        {
            List<BangLuongDTO> bangLuongList = new List<BangLuongDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT 
                                MABANGLUONG,
                                MANV,
                                THANG,
                                NAM,
                                LUONGCOBAN,
                                LUONGTANGCA,
                                PHUCAP,
                                TIENBTRU,
                                TONGLUONG
                            FROM BANG_LUONG";

                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BangLuongDTO bangLuong = new BangLuongDTO
                        {
                            MaBangLuong = reader["MABANGLUONG"].ToString(),
                            MANV = reader["MANV"].ToString(),
                            Thang = reader.GetInt32(reader.GetOrdinal("THANG")),
                            Nam = reader.GetInt32(reader.GetOrdinal("NAM")),
                            LuongCoBan = reader.GetInt32(reader.GetOrdinal("LUONGCOBAN")),
                            LuongTangCa = reader.GetInt32(reader.GetOrdinal("LUONGTANGCA")),
                            PhuCap = reader.GetInt32(reader.GetOrdinal("PHUCAP")),
                            TIENBTRU = reader.GetInt32(reader.GetOrdinal("TIENBTRU")),
                            TongLuong = reader.GetInt32(reader.GetOrdinal("TONGLUONG"))
                        };

                        bangLuongList.Add(bangLuong);
                    }
                }
            }

            return bangLuongList;
        }
        public List<Tuple<int, int>> GetTongLuongTheoNam(int year)
        {
            var result = new List<Tuple<int, int>>();
            string query = @"
        SELECT BL.THANG, SUM(BL.TONGLUONG) AS TongLuong
        FROM BANG_LUONG BL
        INNER JOIN CHAMCONG CC 
            ON BL.MANV = CC.MANV 
            AND BL.THANG = MONTH(CC.NGAYCHAMCONG) 
            AND BL.NAM = YEAR(CC.NGAYCHAMCONG)
        WHERE BL.NAM = @Year
        GROUP BY BL.THANG
        ORDER BY BL.THANG";

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
                            int thang = reader.GetInt32(0); // Tháng
                            int tongLuong = reader.IsDBNull(1) ? 0 : reader.GetInt32(1) / 1_000_000; // Chuyển sang triệu

                            result.Add(new Tuple<int, int>(thang, tongLuong));
                        }
                    }
                }
            }

            return result;
        }


    }
}
