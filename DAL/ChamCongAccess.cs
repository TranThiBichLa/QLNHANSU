using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class ChamCongAccess : DatabaseAccess
    {
        public List<ChamCongDTO> GetChamCong(string maNV, int thang, int nam)
        {
            List<ChamCongDTO> chamCongList = new List<ChamCongDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "EXEC proc_GetChamCong @MaNV, @Thang, @Nam";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ChamCongDTO chamCong = new ChamCongDTO
                        {
                            NgayChamCong = reader.GetDateTime(reader.GetOrdinal("NgayChamCong")),
                            MaLoaiCa = reader["MaLoaiCa"].ToString(),
                            SoGio = reader.GetInt32(reader.GetOrdinal("SoGio")),
                            TrangThai = reader["TrangThai"]?.ToString(),
                            LuongNgay = reader.GetDecimal(reader.GetOrdinal("LuongNgay")),
                            GhiChu = reader["GhiChu"]?.ToString()
                        };

                        chamCongList.Add(chamCong);
                    }
                }
            }

            return chamCongList;
        }
        public List<ChamCongDTO> GetChamCong()
        {
            List<ChamCongDTO> chamCongList = new List<ChamCongDTO>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT 
                MACHAMCONG,
                MANV,
                NGAYCHAMCONG,
                TRANGTHAI,
                MALOAICA,
                SOGIO,
                HESOLOAICA,
                LUONGNGAY,
                GHICHU
            FROM CHAMCONG";

                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ChamCongDTO chamCong = new ChamCongDTO
                        {
                            MaChamCong = reader["MaChamCong"].ToString(),
                            MANV = reader["MANV"].ToString(),
                            NgayChamCong = reader.GetDateTime(reader.GetOrdinal("NgayChamCong")),
                            TrangThai = reader["TrangThai"]?.ToString(),
                            MaLoaiCa = reader["MaLoaiCa"]?.ToString(),
                            SoGio = reader.IsDBNull(reader.GetOrdinal("SoGio")) ? 0 : reader.GetInt32(reader.GetOrdinal("SoGio")),
                            HeSoLoaiCa = reader.IsDBNull(reader.GetOrdinal("HeSoLoaiCa")) ? 0 : reader.GetDecimal(reader.GetOrdinal("HeSoLoaiCa")),
                            LuongNgay = reader.IsDBNull(reader.GetOrdinal("LuongNgay")) ? 0 : reader.GetDecimal(reader.GetOrdinal("LuongNgay")),
                            GhiChu = reader["GhiChu"]?.ToString()
                        };

                        chamCongList.Add(chamCong);
                    }
                }
            }

            return chamCongList;
        }


        // Thêm bản ghi chấm công
        public bool InsertChamCong(ChamCongDTO chamCong)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            INSERT INTO CHAMCONG (MANV, NGAYCHAMCONG, TRANGTHAI, MALOAICA, SOGIO, GHICHU)
            VALUES (@MaNV, @NgayChamCong, @TrangThai, @MaLoaiCa, @SoGio, @GhiChu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", chamCong.MANV);
                cmd.Parameters.AddWithValue("@NgayChamCong", chamCong.NgayChamCong);
                cmd.Parameters.AddWithValue("@TrangThai", chamCong.TrangThai);
                cmd.Parameters.AddWithValue("@MaLoaiCa", (object)chamCong.MaLoaiCa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SoGio", chamCong.SoGio);
                cmd.Parameters.AddWithValue("@GhiChu", chamCong.GhiChu);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public List<int> GetDistinctNhanVienByYear(int year)
        {
            var monthlyEmployeeCounts = new List<int>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT MONTH(NgayChamCong) AS Thang, COUNT(DISTINCT MANV) AS SoLuongNhanVien
            FROM CHAMCONG
            WHERE YEAR(NgayChamCong) = @Year
            GROUP BY MONTH(NgayChamCong)
            ORDER BY Thang";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Year", year);

                    using (var reader = command.ExecuteReader())
                    {
                        var monthlyCounts = new Dictionary<int, int>();
                        while (reader.Read())
                        {
                            int month = reader.GetInt32(reader.GetOrdinal("Thang"));
                            int count = reader.GetInt32(reader.GetOrdinal("SoLuongNhanVien"));
                            monthlyCounts[month] = count;
                        }

                        // Xử lý các tháng không có dữ liệu, đảm bảo danh sách đầy đủ 12 tháng
                        for (int i = 1; i <= 12; i++)
                        {
                            monthlyEmployeeCounts.Add(monthlyCounts.ContainsKey(i) ? monthlyCounts[i] : 0);
                        }
                    }
                }
            }

            return monthlyEmployeeCounts;
        }


    }


}
