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
    public class TrinhDoAccess : DatabaseAccess
    {


        public List<TrinhDo1> GetTrinhDoNhanVien()
        {
            List<TrinhDo1> list = new List<TrinhDo1>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT 
        nv.MANV, nv.HOTEN, td.MATD, td.TENTD, td.TGHOANTHANH, nvtd.NGAYHETHAN, nvtd.TRANGTHAI
    FROM NV_TD nvtd
    INNER JOIN NHANVIEN nv ON nvtd.MANV = nv.MANV
    INNER JOIN TRINHDO td ON nvtd.MATD = td.MATD";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new TrinhDo1
                    {
                        MANV = reader["MANV"].ToString(),
                        HOTEN = reader["HOTEN"].ToString(),
                        MATD = reader["MATD"].ToString(),
                        TENTD = reader["TENTD"].ToString(),
                        TGHOANTHANH = reader["TGHOANTHANH"] as int?,
                        NGAYHETHAN = reader["NGAYHETHAN"] as DateTime?,
                        TRANGTHAI = reader["TRANGTHAI"].ToString()
                    });
                }
            }


            return list;
        }
        public bool UpdateTrinhDo(TrinhDo1 trinhDo)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            UPDATE TRINHDO
            SET 
                TENTD = @TenTD,
                TGHOANTHANH = @ThoiGianHoanThanh
            WHERE MATD = @MaTD;

            UPDATE NV_TD
            SET 
                NGAYHETHAN = @NgayHetHan
            WHERE MANV = @MaNV AND MATD = @MaTD;
        ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTD", trinhDo.MATD);
                cmd.Parameters.AddWithValue("@TenTD", trinhDo.TENTD ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ThoiGianHoanThanh", trinhDo.TGHOANTHANH ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayHetHan", trinhDo.NGAYHETHAN ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MaNV", trinhDo.MANV);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
}