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
namespace WindowsFormsApplication1.HeThong
{
    public partial class frm_dangnhap : Form
    {
        public static string quyen;
        public static string tk;
        public frm_dangnhap()
        {
            InitializeComponent();
        }
        KetNoiDT dt = new KetNoiDT();

        private void bntdangnhap_Click(object sender, EventArgs e)
        {
            SqlParameter para1 = new SqlParameter("@taikhoan", txtuser.Text);
            SqlParameter para2 = new SqlParameter("@matkhau", txtpass.Text);
            DataTable dulieu = dt.sqlLayDuLieu("PSP_NhanVien_test", para1, para2);
            SqlParameter para3 = new SqlParameter("@TaiKhoan", txtuser.Text);
            DataTable dulieu1 = dt.sqlLayDuLieu("PSP_NhanVien_Loai",para3);

            
            
            if (dulieu.Rows.Count < 1)
            {
                MessageBox.Show("Tài khoản không đúng !");
            }
            else
            {
                quyen = dulieu1.Rows[0]["LoaiTK"].ToString();
                tk = txtuser.Text;
                frm_Main frm = new frm_Main();
                frm.Show();
                this.Hide();
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frm_dangnhap_Load(object sender, EventArgs e)
        {
            txtuser.Focus();
        }
    }
}
