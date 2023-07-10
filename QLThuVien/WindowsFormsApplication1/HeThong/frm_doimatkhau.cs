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

namespace WindowsFormsApplication1.HeThong
{
    public partial class frm_doimatkhau : Form
    {
        public frm_doimatkhau()
        {
            InitializeComponent();
        }
        KetNoiDT dt = new KetNoiDT();
        private void frm_doimatkhau_Load(object sender, EventArgs e)
        {
            lblTenTK.Text = frm_dangnhap.tk.ToString();
        }
        private bool Matkhau()
        {
            SqlParameter para1 = new SqlParameter("@matkhau", txtmkcu.Text);
            DataTable dulieu= dt.sqlLayDuLieu("PSP_NhanVien_test1",para1);
            if (dulieu.Rows.Count < 1)
                return true;
            else
                return false;
        }
        private void bntdoimatkhau_Click(object sender, EventArgs e)
        {
            if ( Matkhau() == true)
            {
                MessageBox.Show(" Mật khẩu cũ không đúng!");
            }else if(txtmkmoi.Text.Length<3)
                {
                    MessageBox.Show(" Mật khẩu mới phải có tối thiểu 3 kí tự!");
                }else  if (txtmkmoi.Text != txtnhaplaimkmoi.Text)
                    {
                        MessageBox.Show(" Mật khẩu mới không trùng khớp!");
                    }else if (txtmkmoi.Text == txtnhaplaimkmoi.Text && Matkhau() == false)
                        {
                            SqlParameter para1 = new SqlParameter("@TaiKhoan", lblTenTK.Text);
                            SqlParameter para2 = new SqlParameter("@MatKhau", txtmkmoi.Text);
                            dt.sqlThucThi("PSP_NhanVien_Update", para1, para2);
                            MessageBox.Show("Đổi mật khẩu thành công !");
                        }
        }

        private void bntthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
