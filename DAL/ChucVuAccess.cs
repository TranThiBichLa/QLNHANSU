using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class ChucVuAccess : DatabaseAccess
    {
        public ChucVu GetChucVuById(string macv)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CHUCVU WHERE MACV = @MACV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MACV", macv);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new ChucVu
                    {
                        MACV = reader["MACV"].ToString(),
                        TENCV = reader["TENCV"].ToString(),
                        PHUCAP = Convert.ToInt32(reader["PHUCAP"]),
                        HESOLUONG = Convert.ToDecimal(reader["HESOLUONG"])
                    };
                }

                return null;
            }
        }
    }
}
