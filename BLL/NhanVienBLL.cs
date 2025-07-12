using System;
using System.Collections.Generic;

using DTO; 
using DAL; 

namespace BLL
{
    public class NhanVienBLL
    {
        private readonly NhanVienAccess nhanVienAccess;
        private readonly HopDongLaoDongBLL hopDongLaoDongBLL;

        public NhanVienBLL(NhanVienAccess nhanVienAccess, HopDongLaoDongBLL hopDongLaoDongBLL)
        {
            this.nhanVienAccess = nhanVienAccess;
            this.hopDongLaoDongBLL = hopDongLaoDongBLL;
        }
        

        public bool AddNhanVien(NhanVien nv)
        {
            // Validate dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(nv.MANV) || string.IsNullOrWhiteSpace(nv.HOTEN))
                throw new Exception("Mã nhân viên và họ tên không được để trống.");

            if (nv.NTNS >= DateTime.Today)
                throw new Exception("Ngày sinh không được lớn hơn hoặc bằng ngày hiện tại.");
            int tuoi = DateTime.Today.Year - nv.NTNS.Year;
            if (nv.NTNS.Date > DateTime.Today.AddYears(-tuoi))
                tuoi--;

            if (tuoi < 18)
                throw new Exception("Nhân viên phải đủ 18 tuổi trở lên.");

            if (!IsValidEmail(nv.EMAIL))
                throw new Exception("Email không hợp lệ.");

            //CHECK
            if (!nhanVienAccess.IsValidMaPB(nv.MAPB))
                throw new Exception("Mã phòng ban không tồn tại.");

            if (!nhanVienAccess.IsValidMaBP(nv.MABP, nv.MAPB))
                throw new Exception("Mã bộ phận không hợp lệ hoặc không liên kết với mã phòng ban.");


            if (!nhanVienAccess.IsValidMaCV(nv.MACV))
                throw new Exception("Mã chức vụ không tồn tại.");

            return nhanVienAccess.InsertNhanVien(nv);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public List<NhanVien> GetAllNhanVien()
        {
            return nhanVienAccess.GetAllNhanVien();
        }

        public bool UpdateTinhTrangNhanVien(string maNV, string tinhTrang)
        {
            return nhanVienAccess.UpdateNhanVienTinhTrang(maNV, tinhTrang);
        }
        public bool IsNhanVienExists(string manv)
        {
            return nhanVienAccess.GetNhanVienById(manv) != null;
        }

        public bool UpdateNhanVien(NhanVien nv)
        {
            return nhanVienAccess.UpdateNhanVien(nv);
        }
        public bool DeleteNhanVien(string maNV)
        {
            return nhanVienAccess.DeleteNhanVien(maNV);
        }
        

    }
}
