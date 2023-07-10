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
    public partial class frm_dangky : Form
    {
        public frm_dangky()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        KetNoiDT dt = new KetNoiDT();
        private void button1_Click(object sender, EventArgs e)
        {
            if (txthoten.Text.Length < 1)
            {
                MessageBox.Show("Tên tài khoản không được để trống !", "Complete", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            else
            {
                if (txttaikhoan.Text.Length < 3)
                {
                    MessageBox.Show("Tài khoản phải tối thiểu có 3 kí tự !", "Complete", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else if(txtmatkhau.Text.Length<3)
                    {
                        MessageBox.Show("Mật khẩu phải tối thiểu có 3 kí tự !", "Complete", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if (cmbloaitk.Text=="")
                        {
                            MessageBox.Show("Chưa chọn quyền !", "Complete", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                                else
                                {
                                    SqlParameter chk = new SqlParameter("@TaiKhoan", txttaikhoan.Text);
                                    DataTable check = dt.sqlLayDuLieu("PSP_CheckTaiKhoanTonTai", chk);
                                    if (check.Rows[0]["TT"].ToString() == "0")
                                    {

                                        SqlParameter para1 = new SqlParameter("@HoTen", txthoten.Text);
                                        SqlParameter para2 = new SqlParameter("@TaiKhoan", txttaikhoan.Text);
                                        SqlParameter para3 = new SqlParameter("@MatKhau", txtmatkhau.Text);
                                        SqlParameter para4 = new SqlParameter("@LoaiTK", int.Parse(cmbloaitk.Text));
                                        dt.sqlThucThi("PSP_NhanVien_Insert", para1, para3, para2, para4);
                                        MessageBox.Show("Đăng ký thành công.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.None);
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Tài khoản đã tồn tại!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txttaikhoan.Focus();
                                        txttaikhoan.Text = "";
                                    }
                                   }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
