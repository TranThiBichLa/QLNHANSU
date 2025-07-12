using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using static DTO.DangKi;

namespace DAL
{
    public class DangKiAccess : DatabaseAccess
    {
        public string GetNhanVienByTaiKhoan(string taiKhoan, string matKhau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT MaNV 
                FROM NHANVIEN
                INNER JOIN TAIKHOAN ON NHANVIEN.MANV = TAIKHOAN.SMATKHAU
                WHERE TAIKHOAN.sTaiKhoan = @TaiKhoan AND TAIKHOAN.sMatKhau = @MatKhau";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TaiKhoan", taiKhoan); // Đưa tham số taiKhoan vào
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);   // Đưa tham số matKhau vào

                    var result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi lấy mã nhân viên từ tài khoản: " + ex.Message);
                }
            }
        }



        // Insert BaoHiem record
        public bool InsertBaoHiem(BaoHiemDTO baoHiem)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Bắt đầu giao dịch
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string query = "INSERT INTO BAOHIEM (SOBHYT, NGAYCAP, GTSD, NGAYHETHAN, NOIKHAMBENH, MANV) " +
                                           "VALUES (@SoBHYT, @NgayCap, @GiaTriSuDung, @NgayHetHan, @NoiKhamBenh, @MaNV)";

                            SqlCommand cmd = new SqlCommand(query, conn, transaction); // Chỉ định giao dịch cho SqlCommand
                            cmd.Parameters.AddWithValue("@SoBHYT", baoHiem.SoBHYT);
                            cmd.Parameters.AddWithValue("@NgayCap", baoHiem.NgayCap);
                            cmd.Parameters.AddWithValue("@GiaTriSuDung", baoHiem.GiaTriSuDung);
                            cmd.Parameters.AddWithValue("@NgayHetHan", baoHiem.NgayHetHan ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@NoiKhamBenh", baoHiem.NoiKhamBenh);
                            cmd.Parameters.AddWithValue("@MaNV", baoHiem.MaNV);

                            int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL

                            if (rowsAffected > 0)
                            {   
                                // Commit giao dịch nếu lệnh thành công
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                // Rollback giao dịch nếu không có bản ghi nào được chèn
                                transaction.Rollback();
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            // Nếu có lỗi, rollback giao dịch
                            transaction.Rollback();
                            throw new Exception("Error inserting BaoHiem record: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Database connection error: " + ex.Message);
                }
            }
        }


        // Insert TrinhDo record
        public bool InsertTrinhDo(TrinhDoDTO trinhDo)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Bắt đầu giao dịch
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Chèn vào bảng TRINHDO
                            string queryTrinhDo = "INSERT INTO TRINHDO (MATD, TENTD, TGHOANTHANH) VALUES (@MaTrinhDo, @TenTrinhDo, @ThoiGianHoanThanh)";
                            SqlCommand cmdTrinhDo = new SqlCommand(queryTrinhDo, conn, transaction);
                            cmdTrinhDo.Parameters.AddWithValue("@MaTrinhDo", trinhDo.MaTrinhDo);
                            cmdTrinhDo.Parameters.AddWithValue("@TenTrinhDo", trinhDo.TenTrinhDo);
                            cmdTrinhDo.Parameters.AddWithValue("@ThoiGianHoanThanh", trinhDo.ThoiGianHoanThanh ?? (object)DBNull.Value);

                            int rowsAffectedTrinhDo = cmdTrinhDo.ExecuteNonQuery();
                            if (rowsAffectedTrinhDo <= 0)
                            {
                                throw new Exception("Không thể chèn vào bảng TRINHDO");
                            }

                            // Chèn vào bảng NV_TD
                            string queryNV_TD = "INSERT INTO NV_TD (MANV, MATD, NGAYHETHAN) VALUES (@MaNV, @MaTrinhDo, @NgayHetHan)";
                            SqlCommand cmdNV_TD = new SqlCommand(queryNV_TD, conn, transaction);
                            cmdNV_TD.Parameters.AddWithValue("@MaNV", trinhDo.MaNV);
                            cmdNV_TD.Parameters.AddWithValue("@MaTrinhDo", trinhDo.MaTrinhDo);
                            cmdNV_TD.Parameters.AddWithValue("@NgayHetHan", trinhDo.NgayHetHan ?? (object)DBNull.Value);

                            int rowsAffectedNV_TD = cmdNV_TD.ExecuteNonQuery();
                            if (rowsAffectedNV_TD <= 0)
                            {
                                throw new Exception("Không thể chèn vào bảng NV_TD");
                            }

                            // Nếu cả hai thao tác chèn đều thành công, commit giao dịch
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            // Nếu có lỗi, rollback giao dịch
                            transaction.Rollback();
                            throw new Exception("Error inserting TrinhDo record: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Database connection error: " + ex.Message);
                }
            }
        }

        // Insert TangCa record
        public bool InsertTangCa(TangCaDTO tangCa)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Lấy HESOLOAICA từ bảng LOAICA dựa trên MALOAICA
                    string queryGetHeso = "SELECT HESOLOAICA FROM LOAICA WHERE MALOAICA = @MaLoaiCa";
                    SqlCommand cmdGetHeso = new SqlCommand(queryGetHeso, conn);
                    cmdGetHeso.Parameters.AddWithValue("@MaLoaiCa", tangCa.MaLoaiCa);
                    var result = cmdGetHeso.ExecuteScalar();

                    // Nếu không tìm thấy hệ số lương loại ca, báo lỗi
                    if (result == null)
                    {
                        throw new Exception("Không tìm thấy hệ số lương loại ca cho mã loại ca " + tangCa.MaLoaiCa);
                    }

                    // Lấy giá trị hệ số lương loại ca từ kết quả
                    decimal heSoLoaiCa = Convert.ToDecimal(result);

                    // Chèn vào bảng TANGCA (không tính lương tăng ca nữa)
                    string query = "INSERT INTO TANGCA (MATANGCA, NGAYTANGCA, SOGIO, MALOAICA, HESOLOAICA, MANV) " +
                                   "VALUES (@MaTangCa, @NgayTangCa, @SoGioTangCa, @MaLoaiCa, @HesoLoaiCa, @MaNhanVien)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaTangCa", tangCa.MaTangCa);
                    cmd.Parameters.AddWithValue("@NgayTangCa", tangCa.NgayTangCa);
                    cmd.Parameters.AddWithValue("@SoGioTangCa", tangCa.SoGioTangCa);
                    cmd.Parameters.AddWithValue("@MaLoaiCa", tangCa.MaLoaiCa);
                    cmd.Parameters.AddWithValue("@HesoLoaiCa", heSoLoaiCa);
                    cmd.Parameters.AddWithValue("@MaNhanVien", tangCa.MaNhanVien);

                    return cmd.ExecuteNonQuery() > 0; // Chèn thành công
                }
                catch (Exception ex)
                {
                    throw new Exception("Error inserting TangCa record: " + ex.Message);
                }
            }
        }
    }
}
