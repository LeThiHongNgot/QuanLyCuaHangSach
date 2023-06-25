using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DOAN_LTWIN
{
    public partial class Form2 : Form
    {
        SqlConnection conn;
        bool f = true;
        public Form2()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=LAPTOP-QO933F2U;Initial Catalog=QUANLYCUAHANGSACH;Integrated Security=True");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            rdoS.Checked = true;
        }
        public bool kt(string m, string n)
        {
            try
            {
                conn.Open();
                string ss = "select COUNT(*) FROM DANGNHAP WHERE TENTAIKHOAN = '" + m + "'AND MATKHAU = '" + n + "'";
                SqlCommand CMD = new SqlCommand(ss, conn);
                int dem = (int)CMD.ExecuteScalar();
                conn.Close();
                return dem > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
                return false;
            }
        }
        public bool checkTK()
        {
            try
            {
                conn.Open();
                string ss = "select COUNT(*) FROM DANGNHAP WHERE TENTAIKHOAN = '" + txtTK.Text + "'";
                SqlCommand CMD = new SqlCommand(ss, conn);
                int dem = (int)CMD.ExecuteScalar();
                conn.Close();
                return dem > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
                return false;
            }
        }

        private void btnthem_Click_1(object sender, EventArgs e)
        {
            if (checkTK())
            {
                MessageBox.Show("Tài Khoản đã tồn tại");
                return;
            }
            if (txtxn.Text != txtMK.Text || txtxn.Text.Length == 0 || txtMK.Text.Length == 0)
            {
                MessageBox.Show("Mật khẩu chưa khớp với xác nhận hoặc bị bỏ trống");
                return;
            }
            try
            {
                conn.Open();
                string s = "insert into DANGNHAP values ('" + txtTK.Text + "','" + txtMK.Text + "')";
                SqlCommand cmd = new SqlCommand(s, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã thêm.");
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnxoa_Click_1(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn thực sự muốn xóa tài khoản ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                return;
            if (!checkTK())
            {
                MessageBox.Show("Tài Khoản không tồn tại");
                return;
            }
            if (txtTK.Text == "ADMIN")
            {
                MessageBox.Show("Không thể xóa tài khoản ADMIN");
                return;
            }
            try
            {
                conn.Open();
                string s = "delete DANGNHAP where tentaikhoan = '" + txtTK.Text + "'";
                SqlCommand cmd = new SqlCommand(s, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã xóa");
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuayLai_Click_1(object sender, EventArgs e)
        {
            f = true;
            rdoS.Visible = rdoTK.Visible = true;
            btnDN.Text = "Đăng Nhập";
            btnthem.Visible = btnxoa.Visible = false;
            lblxn.Visible = txtxn.Visible = false;
            txtMK.Text = "";
            lblPass.Text = "Mật Khẩu";
            lblTK.Enabled = txtTK.Enabled = true;
            btnQuayLai.Visible = false;
            btnthem.Enabled = btnxoa.Enabled = false;
        }

    

        private void btnDN_Click(object sender, EventArgs e)
        {
            if (f)
            {

                if (kt(txtTK.Text, txtMK.Text))
                {
                    f = false;
                    if (rdoTK.Checked)
                    {
                        btnQuayLai.Visible = true;
                        rdoS.Visible = rdoTK.Visible = false;
                        btnDN.Text = "Sửa";
                        btnthem.Visible = btnxoa.Visible = true;
                        lblxn.Visible = txtxn.Visible = true;
                        txtMK.Text = "";
                        lblPass.Text = "Mật Khẩu Mới";
                        if (txtTK.Text == "ADMIN")
                            btnthem.Enabled = btnxoa.Enabled = true;
                        else
                            lblTK.Enabled = txtTK.Enabled = false;
                        

                    }
                    else
                    {
                        this.Visible = false;
                        Form1 f = new Form1();
                        f.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác");
                }
            }
            else
            {

                if (txtxn.Text != txtMK.Text || txtxn.Text.Length == 0 || txtMK.Text.Length == 0)
                {
                    MessageBox.Show("Mật khẩu chưa khớp với xác nhận hoặc bị bỏ trống");
                    return;
                }
                if (!checkTK())
                {
                    MessageBox.Show("Tài Khoản không tồn tại");
                    return;
                }

                try
                {
                    conn.Open();
                    string s = "update DANGNHAP set matkhau = '" + txtMK.Text + "' where tentaikhoan = '" + txtTK.Text + "'";
                    SqlCommand cmd = new SqlCommand(s, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã đổi mật khẩu.");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
