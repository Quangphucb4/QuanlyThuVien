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
    public partial class Frm_MuonTraSach : Form
    {
        public Frm_MuonTraSach()
        {
            InitializeComponent();
        }

        KetNoiDT dt = new KetNoiDT();
        private void enable(bool a)
        {
            cmbHoTenHS.Enabled = dtpTra.Enabled = txtSoLuongMuon.Enabled = btnChoMuon.Enabled = a;
        }
        private void lockTra()
        {
           
        }
        private void btnThoat1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThoat2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_MuonTraSach_Load(object sender, EventArgs e)
        {
            enable(false);
            DataTable sach = dt.sqlLayDuLieu("PSP_ShowTenSach");
            DataTable chitiet = dt.sqlLayDuLieu("PSP_CTMuonSach_Select");
            dgvChiTietMuon.DataSource = chitiet;
            dgvChitietMuon1.DataSource = chitiet;
            for(int i=1;i<=sach.Rows.Count;i++)
            {
                cmbTenSach.Items.Add(sach.Rows[sach.Rows.Count - i]["TenSach"].ToString());
            }
            txtMaHS.Enabled = txtMaSach.Enabled = txtMaMuon.Enabled = dtpNgayTra.Enabled = dtpNgayMuon.Enabled = dtpNgayHenTra.Enabled = false;
            frm_dangnhap _login=new frm_dangnhap();

            if(frm_dangnhap.quyen.ToString() == "2")
            {
                btnMoi.Enabled = btnTraSach.Enabled = false;
                btnChoMuon.Enabled = btnTraSach.Enabled = false;
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            enable(true);
            DataTable hs = dt.sqlLayDuLieu("PSP_HocSinh_Show");
            for(int i=1;i<=hs.Rows.Count;i++)
            {
                cmbHoTenHS.Items.Add(hs.Rows[hs.Rows.Count - i]["HoTenHS"].ToString());
            }
            DataTable id = dt.sqlLayDuLieu("SP_IDCTMuon_Max");
            lblMaMuon.Text = id.Rows[0]["MaMuonSach"].ToString();
            btnMoi.Enabled = false;
        }

        
        private void btnChoMuon_Click(object sender, EventArgs e)
        {
            //try
            //{
                SqlParameter pa=new SqlParameter("@MaSach", lblMaSach.Text);
                DataTable sls=dt.sqlLayDuLieu("PSP_SLConLai",pa);
                if (Convert.ToInt32(txtSoLuongMuon.Text) <= Convert.ToInt32(sls.Rows[0]["SLConLai"].ToString()))
                {
                    SqlParameter HoTen = new SqlParameter("@HoTenHS", cmbHoTenHS.Text);
                    DataTable mshs = dt.sqlLayDuLieu("PsP_MaHS_Select", HoTen);
                    lblMaHS.Text = mshs.Rows[0]["MaHS"].ToString();
                    SqlParameter MaMuonSach = new SqlParameter("@MaMuonSach", lblMaMuon.Text);
                    SqlParameter MaSach = new SqlParameter("@MaSach", lblMaSach.Text);
                    SqlParameter MaHS = new SqlParameter("@MaHS", lblMaHS.Text);
                    SqlParameter SLMuon = new SqlParameter("@SLMuon", txtSoLuongMuon.Text);
                    SqlParameter Muon = new SqlParameter("@NgayMuon", Convert.ToDateTime(dtpMuon.Text));
                    SqlParameter HenTra = new SqlParameter("@NgayHenTra", Convert.ToDateTime(dtpTra.Text));
                    SqlParameter Tra = new SqlParameter("@NgayTra", "");
                    dt.sqlThucThi("PSP_ChiTietMuon_Insert", MaMuonSach, MaSach, MaHS, SLMuon, Muon, HenTra, Tra);

                    enable(false);
                    cmbTenSach.Text = "";
                    lblMaSach.Text = "";
                    lblGia.Text = "";
                    lblSLuong.Text = "";
                    lblNgayXB.Text = "";
                    lblTacGia.Text = "";
                    cmbHoTenHS.Text = "";
                    txtSoLuongMuon.Text = "";
                }
                else
                    MessageBox.Show("Vượt quá số lượng sách có trong kho.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch
            //{
            //    MessageBox.Show("Chưa đủ thông tin mượn sách", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
          
        }

        private void btnXemSach_Click(object sender, EventArgs e)
        {
            try
            {
                lblMaSach.Text = "";
                lblGia.Text = "";
                lblSLuong.Text = "";
                lblNgayXB.Text = "";
                lblTacGia.Text = "";
                SqlParameter Tensach = new SqlParameter("@TenSach", cmbTenSach.Text);
                DataTable show = dt.sqlLayDuLieu("PSP_Sach_Show", Tensach);
                lblMaSach.Text = show.Rows[0]["MaSach"].ToString();
                lblGia.Text = show.Rows[0]["GiaSach"].ToString();
                lblSLuong.Text = show.Rows[0]["SoLuong"].ToString();
                lblNgayXB.Text = string.Format("{0:d}", (show.Rows[0]["NgayXuatBan"]));
                lblTacGia.Text = show.Rows[0]["HoTenTG"].ToString();
            }
            catch
            {
                MessageBox.Show("Bạn chưa chọn sách muốn xem", "Lỗi");
            }
        }

        private void btnLoad1_Click(object sender, EventArgs e)
        {
            DataTable chitiet = dt.sqlLayDuLieu("PSP_CTMuonSach_Select");
            dgvChiTietMuon.DataSource = chitiet;
        }

        private void btnLoad2_Click(object sender, EventArgs e)
        {
            DataTable chitiet = dt.sqlLayDuLieu("PSP_CTMuonSach_Select");
            dgvChitietMuon1.DataSource = chitiet;
        }

        private void dgvChitietMuon1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaMuon.Text = dgvChitietMuon1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMaHS.Text = dgvChitietMuon1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMaSach.Text = dgvChitietMuon1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtpNgayMuon.Text = dgvChitietMuon1.Rows[e.RowIndex].Cells[4].Value.ToString();
            dtpNgayHenTra.Text = dgvChitietMuon1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            SqlParameter pa = new SqlParameter("@MaMuon",txtMaMuon.Text);
            DataTable t = dt.sqlLayDuLieu("PSP_SLTra",pa);
            if (Convert.ToInt32(txtSoLuong.Text) <= Convert.ToInt32(t.Rows[0]["SLMuon"].ToString()))
            {
                SqlParameter mamuon = new SqlParameter("@MaMuon", txtMaMuon.Text);
                SqlParameter sltra = new SqlParameter("@SLTra", txtSoLuong.Text);
                SqlParameter ngay = new SqlParameter("@NgayTra", dtpNgayTra.Text);
                dt.sqlThucThi("PSP_Update_TraSach", mamuon, sltra, ngay);
                btnLoad2_Click(sender, e);
                txtSoLuong.Text = "";
            }
            else
            {
                MessageBox.Show("Số sách vượt quá số lượng đã mượn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Text = "";
            }
        }

        private void btnChiTietMuonTra_Click(object sender, EventArgs e)
        {
           
        }

        private void btnQuaHan_Click(object sender, EventArgs e)
        {
            
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
