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
    public class CongTacAccess : DatabaseAccess
    {
        public async Task<List<CongTac>> GetAllAsync()
        {
            List<CongTac> list = new List<CongTac>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CONGTAC", conn);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    list.Add(new CongTac
                    {
                        MaNhanVien = reader["MANV"].ToString(),
                        MaHD = reader["MAHD"].ToString(),
                        MaPhongBan = reader["MAPB"].ToString(),
                        MaBoPhan = reader["MABP"].ToString(),
                        MaChucVu = reader["MACV"].ToString(),
                        NgayQuyetDinh = Convert.ToDateTime(reader["NGAYQD"]),
                        TinhTrangCongTac = reader["TINHTRANG"].ToString()
                    });
                }
            }
            return list;
        }

        public async Task<List<CongTac>> SearchAsync(string maNhanVien, string maHD, string trangThai)
        {
            var list = new List<CongTac>();
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = @"
            SELECT * 
            FROM CONGTAC 
            WHERE (@MANV IS NULL OR MANV = @MANV) 
              AND (@MAHD IS NULL OR MAHD = @MAHD) 
              AND (@TINHTRANG IS NULL OR TINHTRANG = @TINHTRANG)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MANV", (object)maNhanVien ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MAHD", (object)maHD ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TINHTRANG", (object)trangThai ?? DBNull.Value);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new CongTac
                            {
                                MaNhanVien = reader["MANV"].ToString(),
                                MaHD = reader["MAHD"].ToString(),
                                MaPhongBan = reader["MAPB"].ToString(),
                                MaBoPhan = reader["MABP"].ToString(),
                                MaChucVu = reader["MACV"].ToString(),
                                NgayQuyetDinh = Convert.ToDateTime(reader["NGAYQD"]),
                                TinhTrangCongTac = reader["TINHTRANG"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

    }
}