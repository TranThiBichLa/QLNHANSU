using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class DangKiBLL
    {
        private DangKiAccess dangKiAccess = new DangKiAccess();

        public string GetNhanVienByTaiKhoan(string taiKhoan, string matKhau)
        {
            return dangKiAccess.GetNhanVienByTaiKhoan(taiKhoan, matKhau);
        }
        private XttTangCaBLL tangCaBLL = new XttTangCaBLL(); // Đảm bảo khởi tạo đúng lớp BLL

        public List<DTO.DangKi.TangCaDTO> GetTCByTaiKhoan(string taiKhoan)
        {
            return tangCaBLL.GetTangCaByTaiKhoan(taiKhoan); // Gọi phương thức trong DAL để lấy dữ liệu
        }

        // Đăng ký bảo hiểm
        public bool DangKyBaoHiem(string maNhanVien, DangKi.BaoHiemDTO baoHiem)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(baoHiem.SoBHYT))
                {
                    throw new ArgumentException("Số BHYT không được để trống.");
                }
                if (baoHiem.NgayCap == DateTime.MinValue)
                {
                    throw new ArgumentException("Ngày cấp không hợp lệ.");
                }
                if (baoHiem.GiaTriSuDung == DateTime.MinValue)
                {
                    throw new ArgumentException("Giá trị sử dụng không hợp lệ.");
                }
                if (string.IsNullOrEmpty(baoHiem.NoiKhamBenh))
                {
                    throw new ArgumentException("Nơi khám bệnh không được để trống.");
                }
                if (string.IsNullOrEmpty(maNhanVien))
                {
                    throw new ArgumentException("Mã nhân viên không hợp lệ.");
                }

                // Gọi DAL để thêm bảo hiểm
                return dangKiAccess.InsertBaoHiem(baoHiem);
            }
            catch (ArgumentException ex)
            {
                // Xử lý lỗi kiểm tra đầu vào
                throw new Exception("Dữ liệu đầu vào không hợp lệ: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi DAL
                throw new Exception("Lỗi trong quá trình đăng ký bảo hiểm: " + ex.Message);
            }
        }

        // Đăng ký trình độ
        public bool DangKyTrinhDo(string maNhanVien, DangKi.TrinhDoDTO trinhDo)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(maNhanVien))
                {
                    throw new ArgumentException("Mã nhân viên không hợp lệ.");
                }
                if (string.IsNullOrEmpty(trinhDo.MaTrinhDo))
                {
                    throw new ArgumentException("Mã trình độ không được để trống.");
                }
                if (string.IsNullOrEmpty(trinhDo.TenTrinhDo))
                {
                    throw new ArgumentException("Tên trình độ không được để trống.");
                }

                // Gọi DAL để thêm trình độ
                return dangKiAccess.InsertTrinhDo(trinhDo);
            }
            catch (ArgumentException ex)
            {
                // Xử lý lỗi kiểm tra đầu vào
                throw new Exception("Dữ liệu đầu vào không hợp lệ: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi DAL
                throw new Exception("Lỗi trong quá trình đăng ký trình độ: " + ex.Message);
            }
        }

        // Đăng ký tăng ca
        public bool DangKyTangCa(string maNhanVien, DangKi.TangCaDTO tangCa)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty( maNhanVien))
                {
                    throw new ArgumentException("Mã nhân viên không hợp lệ.");
                }
                if (string.IsNullOrEmpty(tangCa.MaTangCa))
                {
                    throw new ArgumentException("Mã tăng ca không được để trống.");
                }
                if (string.IsNullOrEmpty(tangCa.MaLoaiCa))
                {
                    throw new ArgumentException("Mã loại ca không được để trống.");
                }
                if (tangCa.NgayTangCa == DateTime.MinValue)
                {
                    throw new ArgumentException("Ngày tăng ca không hợp lệ.");
                }
                if (tangCa.SoGioTangCa <= 0)
                {
                    throw new ArgumentException("Số giờ tăng ca phải lớn hơn 0.");
                }

                // Gọi DAL để thêm tăng ca
                return dangKiAccess.InsertTangCa(tangCa);
            }
            catch (ArgumentException ex)
            {
                // Xử lý lỗi kiểm tra đầu vào
                throw new Exception("Dữ liệu đầu vào không hợp lệ: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi DAL
                throw new Exception("Lỗi trong quá trình đăng ký tăng ca: " + ex.Message);
            }
        }
    }
}
