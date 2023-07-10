using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestThuVien.QLThuVienDTO;
using System.Data.SqlClient;
using WindowsFormsApplication1.HeThong;

namespace WindowsFormsApplication1
{
    public partial class frm_quanlysach : Form
    {
        public frm_quanlysach()
        {
            InitializeComponent();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frm_capnhat _them = new frm_capnhat();
            _them.them = true;
            _them.ShowDialog();
        }
        DTO.DTO_Sach sach;
        private void setToDTOSach(DataGridViewRow dt)
        {
            sach = new DTO.DTO_Sach();
            sach.maSach = dt.Cells["colMaSach"].Value.ToString();
            sach.maTL = dt.Cells["colMaTL"].Value.ToString();
            sach.tenSach = dt.Cells["colTenSach"].Value.ToString();
            sach.hoTenTG = dt.Cells["colHoTenTG"].Value.ToString();
            sach.soLuong = Convert.ToInt32(dt.Cells["colSoLuong"].Value.ToString());
            sach.giaSach = Convert.ToInt32(dt.Cells["colGiaSach"].Value.ToString());
            sach.ngayXuatBan = dt.Cells["colNgayXuatBan"].Value.ToString();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (sach != null)
            {
                frm_capnhat sua = new frm_capnhat();
                sua.them = false;
                sua.sach = this.sach;
                sua.ShowDialog();
                DataTable dulieu = dt.sqlLayDuLieu("PSP_ThongKeSachCon");
                dgvSach.DataSource = dulieu;
                this.sach = null;
            }
            else
                MessageBox.Show("Chưa chọn sách cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        KetNoiDT dt = new KetNoiDT();
        private void frm_quanlysach_Load(object sender, EventArgs e)
        {
            DataTable slc = dt.sqlLayDuLieu("PSP_ThongKeSachCon");
            dgvSach.DataSource = slc;
            if (frm_dangnhap.quyen.ToString() == "2")
            {

                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = btnXoa.Enabled = false;
                
            }
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            SqlParameter pa3 = new SqlParameter("@search", txtSeach.Text);
            DataTable seach = dt.sqlLayDuLieu("PSP_QuanLySach_Search", pa3);
            dgvSach.DataSource = seach;
            txtSeach.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dulieu = dt.sqlLayDuLieu("PSP_ThongKeSachCon");
            dgvSach.DataSource = dulieu;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlParameter masach = new SqlParameter("@MaSach", sach.maSach);
                SqlParameter sachma = new SqlParameter("@MaSach", sach.maSach);
                SqlParameter hoten = new SqlParameter("@HoTenTG", sach.hoTenTG);
                if (DialogResult.Yes == MessageBox.Show("Bạn có chắc muốn xóa?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    dt.sqlThucThi("PSP_SachTG_Delete", sachma, hoten);
                    dt.sqlThucThi("PSP_SachDelete", masach);
                    MessageBox.Show("Đã xoá");
                }
            }
            catch
            {
                MessageBox.Show("Sách đang mượn không thể xoá","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dgvSach_Click(object sender, EventArgs e)
        {
            setToDTOSach(dgvSach.CurrentRow);
        }

        private void btnInDS_Click(object sender, EventArgs e)
        {
          
        }
    }
}