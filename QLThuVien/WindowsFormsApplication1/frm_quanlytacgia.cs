using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestThuVien.QLThuVienDTO;
using WindowsFormsApplication1.HeThong;



namespace WindowsFormsApplication1
{
    public partial class frm_quanlytacgia : Form
    {
       
        public frm_quanlytacgia()
        {
            InitializeComponent();
        }
       
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        KetNoiDT dt = new KetNoiDT();
        private void frm_quanlytacgia_Load(object sender, EventArgs e)
        {
            txtMaTG.Enabled = txtTenTG.Enabled = false;
            DataTable dulieu = dt.sqlLayDuLieu("PSP_TacGia_Select");
            dgvTacGia.DataSource = dulieu;
            if (frm_dangnhap.quyen.ToString() == "2")
            {
                btnThem.Enabled = false;
                btnCapNhat.Enabled = false;
                bntsua.Enabled = false;
                bntxoa.Enabled = false;
            }     
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaTG.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập mã tác giả.", "Chưa đủ dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMaTG.Focus();
                    return;
                }
                if (txtTenTG.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập họ tên tác giả.", "Chưa đủ dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenTG.Focus();
                    return;
                }
                    SqlParameter para1 = new SqlParameter("@MaTG", txtMaTG.Text);
                    SqlParameter para2 = new SqlParameter("@HoTenTG", txtTenTG.Text);
                    dt.sqlThucThi("PSP_TacGia_Insert", para1, para2);
                    MessageBox.Show("Thêm thành công.");
                    DataTable dulieu = dt.sqlLayDuLieu("PSP_TacGia_Select");
                    dgvTacGia.DataSource = dulieu;
                    btnThem.Visible = true;
                    txtTenTG.Text = "";
                    txtMaTG.Text = "";
                    txtMaTG.Enabled = txtTenTG.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Mã tác giả đã bị trùng lặp. Mời nhập lại.");
                txtMaTG.Focus();
            }
        }

        private void bntxoa_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn có chắc muốn xóa?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                SqlParameter para1 = new SqlParameter("@MaTG", txtMaTG.Text);
                dt.sqlThucThi("PSP_TacGia_Delete", para1);
                DataTable dulieu = dt.sqlLayDuLieu("PSP_TacGia_Select");
                dgvTacGia.DataSource = dulieu;
                txtTenTG.Text = "";
                txtMaTG.Text = "";
            }
        }

        private void dgvTacGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTenTG.Text = dgvTacGia.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMaTG.Text = dgvTacGia.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void bntsua_Click(object sender, EventArgs e)
        {
            SqlParameter para1 = new SqlParameter("@MaTG", txtMaTG.Text);
            SqlParameter para2 = new SqlParameter("@HoTenTG", txtTenTG.Text);
            dt.sqlThucThi("PSP_TacGia_Update", para1, para2);
            MessageBox.Show("Sửa thành công");
            btnEdit.Visible = true;
            DataTable dulieu = dt.sqlLayDuLieu("PSP_TacGia_Select");
            dgvTacGia.DataSource = dulieu;
            txtTenTG.Text = "";
            txtMaTG.Text = "";
            txtMaTG.Enabled = txtTenTG.Enabled = false;
        }

        private void txtMaTG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Visible = false;
            txtMaTG.Enabled = txtTenTG.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            txtTenTG.Enabled = true;
        }
       

    }
}
