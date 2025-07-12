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
    //public class SqlConnectionData
    //{//tạo chuỗi kết nối với cơ sở dữ liệu
    //    public static SqlConnection Connect()
    //    {
    //        string strcon = @"Data Source = DESKTOP - VTBENSB\MSSQLK2023; Initial Catalog = QUANLYNHANSU; Integrated Security = True; Trust Server Certificate = True";
    //        SqlConnection conn = new SqlConnection(strcon); //khởi tạo connect
    //        return conn;
    //    }

    //}
    public class DatabaseAccess
    {
        protected readonly string connectionString = @"Data Source=DESKTOP-VTBENSB\MSSQLK2023;Initial Catalog=QUANLYNHANSU;Integrated Security=True;TrustServerCertificate=True";

        protected IDbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
        public DataTable GetData(string query)
            {
                DataTable data = new DataTable();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(data);
                }
                return data;
            }
  
    }
}
