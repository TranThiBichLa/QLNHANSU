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
    public class BaoHiemAccess : DatabaseAccess
    {
        public List<BaoHiem> GetAllBaoHiem()
        {
            List<BaoHiem> dsBaoHiem = new List<BaoHiem>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT SOBHYT, NGAYCAP, GTSD, NGAYHETHAN, NOIKHAMBENH, MANV
            FROM BAOHIEM";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BaoHiem bh = new BaoHiem
                    {
                        SOBHYT = reader["SOBHYT"].ToString(),
                        NGAYCAP = Convert.ToDateTime(reader["NGAYCAP"]),
                        GTSD = Convert.ToDateTime(reader["GTSD"]),
                        NGAYHETHAN = reader["NGAYHETHAN"] as DateTime?,
                        NOIKHAMBENH = reader["NOIKHAMBENH"].ToString(),
                        MANV = reader["MANV"].ToString()
                    };

                    // Tính toán trạng thái
                    if (!bh.NGAYHETHAN.HasValue || bh.NGAYHETHAN.Value >= DateTime.Now)
                    {
                        bh.TRANGTHAI = "Còn hạn sử dụng";
                    }
                    else
                    {
                        bh.TRANGTHAI = "Hết hạn sử dụng";
                    }

                    dsBaoHiem.Add(bh);
                }

                reader.Close();
            }

            return dsBaoHiem;
        }
        public bool UpdateBaoHiem(BaoHiem baoHiem)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            UPDATE BAOHIEM
            SET NGAYCAP = @NgayCap,
                GTSD = @GTSD,
                NGAYHETHAN = @NgayHetHan,
                NOIKHAMBENH = @NoiKhamBenh
            WHERE SOBHYT = @SoBaoHiem";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayCap", baoHiem.NGAYCAP);
                cmd.Parameters.AddWithValue("@GTSD", baoHiem.GTSD);
                cmd.Parameters.AddWithValue("@NgayHetHan", baoHiem.NGAYHETHAN ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@NoiKhamBenh", baoHiem.NOIKHAMBENH);
                cmd.Parameters.AddWithValue("@SoBaoHiem", baoHiem.SOBHYT);

                return cmd.ExecuteNonQuery() > 0; // Trả về true nếu có dòng được cập nhật
            }
        }



    }
}