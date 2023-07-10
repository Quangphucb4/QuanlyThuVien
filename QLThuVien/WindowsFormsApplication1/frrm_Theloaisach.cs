using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestThuVien.QLThuVienDTO;
using System.Data.SqlClient;
namespace WindowsFormsApplication1
{
    public partial class frrm_Theloaisach : Form
    {
        public frrm_Theloaisach()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        KetNoiDT dt = new KetNoiDT();
        private void frrm_Theloaisach_Load(object sender, EventArgs e)
        {
            DataTable dulieu = dt.sqlLayDuLieu("PSP_TheLoaiSach_Select");
            dtgvtheloaisach.DataSource = dulieu;

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
      
        private void btnthem_Click(object sender, EventArgs e)
        {
            SqlParameter para1 = new SqlParameter("@matl", txtmatl.Text);
            SqlParameter para2 = new SqlParameter("@tentl", txttentl.Text);
            dt.sqlThucThi("PSP_TheLoaiSach_Insert", para1, para2);
            DataTable dulieu = dt.sqlLayDuLieu("PSP_TheLoaiSach_Select");
            dtgvtheloaisach.DataSource = dulieu;
            txtmatl.Text = "";
            txttentl.Text = "";
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            SqlParameter para1 = new SqlParameter("@matl", txtmatl.Text);
            dt.sqlThucThi("PSP_TheLoaiSach_Delete", para1);
            DataTable dulieu = dt.sqlLayDuLieu("PSP_TheLoaiSach_Select");
            dtgvtheloaisach.DataSource = dulieu;
            txtmatl.Text = "";
            txttentl.Text = "";
        }

        private void dtgvtheloaisach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmatl.Text = dtgvtheloaisach.Rows[e.RowIndex].Cells[0].Value.ToString();
            txttentl.Text = dtgvtheloaisach.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            SqlParameter para1 = new SqlParameter("@matl", txtmatl.Text);
            SqlParameter para2 = new SqlParameter("@tentl", txttentl.Text);
            dt.sqlThucThi("PSP_TheLoaiSach_Update", para1, para2);
            DataTable dulieu = dt.sqlLayDuLieu("PSP_TheLoaiSach_Select");
            dtgvtheloaisach.DataSource = dulieu;
            txtmatl.Text = "";
            txttentl.Text = "";
            
        }
    }
}
