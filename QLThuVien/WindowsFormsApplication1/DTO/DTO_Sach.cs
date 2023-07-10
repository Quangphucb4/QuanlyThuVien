using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.DTO
{
    public class DTO_Sach
    {
        string MaSach, TenSach, HoTenTG, NgayXuatBan, MaTL;

        public string maTL
        {
            get { return MaTL; }
            set { MaTL = value; }
        }

        public string ngayXuatBan
        {
            get { return NgayXuatBan; }
            set { NgayXuatBan = value; }
        }

        public string hoTenTG
        {
            get { return HoTenTG; }
            set { HoTenTG = value; }
        }

        public string tenSach
        {
            get { return TenSach; }
            set { TenSach = value; }
        }

        public string maSach
        {
            get { return MaSach; }
            set { MaSach = value; }
        }
        int SoLuong, GiaSach;

        public int giaSach
        {
            get { return GiaSach; }
            set { GiaSach = value; }
        }

        public int soLuong
        {
            get { return SoLuong; }
            set { SoLuong = value; }
        }
    }
}
