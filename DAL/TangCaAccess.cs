using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{


    public class TangCaAccess : DatabaseAccess
    {
        public List<TangCa> GetTangCaRecords()
        {
            List<TangCa> records = new List<TangCa>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT T.MaTangCa, T.NgayTangCa, T.SoGio, T.Luong1GioTangCa, L.TenLoaiCa, " +
                               "T.HeSoLoaiCa, (T.Luong1GioTangCa * T.HeSoLoaiCa * T.SoGio) AS LuongTangCa, " +
                               "T.MaNV FROM TANGCA T JOIN LOAICA L ON T.MALOAICA = L.MALOAICA";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        records.Add(new TangCa
                        {
                            MaTangCa = reader["MaTangCa"].ToString(),
                            NgayTangCa = Convert.ToDateTime(reader["NgayTangCa"]),
                            SoGio = Convert.ToInt32(reader["SoGio"]),
                            Luong1GioTangCa = Convert.ToInt32(reader["Luong1GioTangCa"]),
                            TenLoaiCa = reader["TenLoaiCa"].ToString(),
                            HeSoLoaiCa = Convert.ToDecimal(reader["HeSoLoaiCa"]),
                            LuongTangCa = Convert.ToDecimal(reader["LuongTangCa"]),
                            MaNhanVien = reader["MaNV"].ToString()
                        });
                    }
                }
            }

            return records;
        }

        // Lấy dữ liệu với bộ lọc
        public List<TangCa> GetTangCaRecords(string maNhanVien, string loaiCa)
        {
            List<TangCa> records = new List<TangCa>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT T.MaTangCa, T.NgayTangCa, T.SoGio, T.Luong1GioTangCa, L.TenLoaiCa, " +
                               "T.HeSoLoaiCa, (T.Luong1GioTangCa * T.HeSoLoaiCa * T.SoGio) AS LuongTangCa, " +
                               "T.MANV FROM TANGCA T JOIN LOAICA L ON T.MALOAICA = L.MALOAICA " +
                               "WHERE (@MaNV IS NULL OR T.MANV = @MaNV) " +  // Thêm lọc theo mã nhân viên
                               "AND (@LoaiCa IS NULL OR L.TenLoaiCa = @LoaiCa)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số vào câu truy vấn
                    cmd.Parameters.AddWithValue("@MaNV", string.IsNullOrEmpty(maNhanVien) ? (object)DBNull.Value : maNhanVien);
                    cmd.Parameters.AddWithValue("@LoaiCa", string.IsNullOrEmpty(loaiCa) ? (object)DBNull.Value : loaiCa);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        records.Add(new TangCa
                        {
                            MaTangCa = reader["MaTangCa"].ToString(),
                            NgayTangCa = Convert.ToDateTime(reader["NgayTangCa"]),
                            SoGio = Convert.ToInt32(reader["SoGio"]),
                            Luong1GioTangCa = Convert.ToInt32(reader["Luong1GioTangCa"]),
                            TenLoaiCa = reader["TenLoaiCa"].ToString(),
                            HeSoLoaiCa = Convert.ToDecimal(reader["HeSoLoaiCa"]),
                            LuongTangCa = Convert.ToDecimal(reader["LuongTangCa"]),
                            MaNhanVien = reader["MANV"].ToString()  // Đảm bảo lấy mã nhân viên
                        });
                    }
                }
            }

            return records;
        }

    }

}