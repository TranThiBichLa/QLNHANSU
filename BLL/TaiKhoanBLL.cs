using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class TaiKhoanBLL
    {
        TaiKhoanAccess tkAccess = new TaiKhoanAccess();
        public string CheckLogin(TaiKhoan taikhoan)
        {
            //kiểm tra nhiệm vụ 
            if (taikhoan.sTaiKhoan == "")
            {
                return "request_taikhoan";
            }
            if (taikhoan.sMatKhau == "")
            {
                return "request_password";
            }
            string info = tkAccess.CheckLogin(taikhoan);
            return info;
        }
    }
}
