using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;



namespace BLL
{
    public class HopDongLaoDongBLL
    {
        private readonly HopDongLaoDongAccess hopDongAccess;
        private readonly NhanVienAccess nhanVienAccess;

        public HopDongLaoDongBLL(HopDongLaoDongAccess hopDongAccess, NhanVienAccess nhanVienAccess)
        {
            this.hopDongAccess = hopDongAccess;
            this.nhanVienAccess = nhanVienAccess;
        }

        public bool AddHopDong(HopDongLaoDong hopDong)
        {
            if (string.IsNullOrWhiteSpace(hopDong.MAHD) || string.IsNullOrWhiteSpace(hopDong.MANV))
                throw new Exception("Mã hợp đồng và mã nhân viên không được để trống.");

            if (hopDong.NGAYKYHD > hopDong.NGBD)
                throw new Exception("Ngày ký không được lớn hơn ngày bắt đầu.");

            var nhanVien = nhanVienAccess.GetNhanVienById(hopDong.MANV);
            if (nhanVien == null)
                throw new Exception("Nhân viên không tồn tại.");

            if (nhanVien.MAPB != hopDong.MAPB || nhanVien.MABP != hopDong.MABP || nhanVien.MACV != hopDong.MACV)
                throw new Exception("Phòng ban, bộ phận, hoặc chức vụ không khớp.");

            if (hopDong.NGKT > DateTime.Now)
            {
                hopDong.TINHTRANG = "Đang có hiệu lực";
            }
            else
            {
                hopDong.TINHTRANG = "Hết hiệu lực";
            }

            if (!CheckOverlappingContracts(hopDong))
            {
                throw new Exception("Khoảng thời gian hợp đồng bị chồng chéo với hợp đồng khác của nhân viên này.");
            }
            return hopDongAccess.InsertHopDong(hopDong);
        }
        public bool CheckOverlappingContracts(HopDongLaoDong newContract)
        {
            var existingContracts = hopDongAccess.GetContractsByNhanVien(newContract.MANV);

            foreach (var contract in existingContracts)
            {
                // Kiểm tra khoảng thời gian chồng chéo
                if ((newContract.NGBD <= contract.NGKT && newContract.NGBD >= contract.NGBD) ||
                    (newContract.NGKT >= contract.NGBD && newContract.NGKT <= contract.NGKT))
                {
                    return false; // Thời gian bị chồng chéo
                }
            }
            return true;
        }
        public bool CheckActiveContracts(string manv)
        {
            var contracts = hopDongAccess.GetContractsByNhanVien(manv);
            return contracts.Count(c => c.TINHTRANG == "Đang có hiệu lực") == 0;
        }


        public bool UpdateHopDong(HopDongLaoDong hopDong)
        {
            // Validate dữ liệu
            if (hopDong.NGAYKYHD > hopDong.NGBD || (hopDong.NGKT != null && hopDong.NGBD > hopDong.NGKT))
                throw new Exception("Ngày ký hoặc ngày kết thúc không hợp lệ.");

            // Cập nhật trạng thái hợp đồng dựa trên ngày kết thúc
            if (hopDong.NGKT == null || hopDong.NGKT > DateTime.Now)
            {
                hopDong.TINHTRANG = "Đang có hiệu lực";
            }
            else
            {
                hopDong.TINHTRANG = "Hết hiệu lực";
            }

            return hopDongAccess.UpdateHopDong(hopDong);
        }

        public bool DeleteHopDong(string mahd)
        {
            return hopDongAccess.DeleteHopDong(mahd);
        }

        public void UpdateHopDongTinhTrang()
        {
            var hopDongs = hopDongAccess.GetAllHopDongs();

            foreach (var hopDong in hopDongs)
            {
                string newTinhTrang = (hopDong.NGKT == null || hopDong.NGKT > DateTime.Now)
                    ? "Đang có hiệu lực"
                    : "Hết hiệu lực";

                hopDong.TINHTRANG = newTinhTrang;
                hopDongAccess.UpdateHopDong(hopDong); // Cần đảm bảo phương thức UpdateHopDong đã được định nghĩa
            }
        }
        public List<HopDongLaoDong> GetAllHopDongs()
        {
            return hopDongAccess.GetAllHopDongs(); // Gọi DAL để lấy danh sách hợp đồng
        }

        public List<Tuple<string, int>> GetNhanVienTheoViTri(string viTri, int year)
        {
            return hopDongAccess.GetNhanVienTheoViTri(viTri, year);
        }


    }
}
