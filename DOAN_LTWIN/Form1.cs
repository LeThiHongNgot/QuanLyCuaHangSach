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
using System.Globalization;
using System.Threading;
namespace DOAN_LTWIN
{

    public partial class Form1 : Form
    {
        SqlConnection _Connsql, connsql;
        DataSet ds_TTBan = new DataSet();
        SqlDataAdapter da_TTBan;
        public static DataSet ds_HDBan = new DataSet();
        SqlDataAdapter da_HDBan;
        DataColumn[] key = new DataColumn[1];
        DataColumn[] key1 = new DataColumn[1];
        DataSet ds_NV = new DataSet();
        SqlDataAdapter da_NV;
        DataSet ds_TC = new DataSet();
        DataSet ds_TCC = new DataSet();
        SqlDataAdapter da_TC;
        int count = 0;
        bool f = true, fm = true;
        string vet = "";
        public Form1()
        {
            InitializeComponent();
            _Connsql = new SqlConnection(@"Data Source=LAPTOP-QO933F2U;Initial Catalog=QUANLYCUAHANGSACH;Integrated Security=True");
            connsql = new SqlConnection(@"Data Source=LAPTOP-QO933F2U;Initial Catalog=QUANLYCUAHANGSACH;Integrated Security=True");
            rdoNhap.CheckedChanged += rdoMua_CheckedChanged;
            cboTenNXB.KeyPress += cboMaSach_KeyPress;
            cboTenTG.KeyPress += cboMaSach_KeyPress;
            cboTheLoai.KeyPress += cboMaSach_KeyPress;
            cboMaKH.KeyPress += cboMaNV_KeyPress;
            txtGiaBan.KeyPress += txtGiaNhap_KeyPress;
            rdoChi.CheckedChanged += rdoThu_CheckedChanged;

        }
        public void loaddgvTTBan()
        {

            string strsel = "select MASACH,TENSACH,TENTACGIA,SOLUONGCON,TENTHELOAI,TENNHAXUATBAN from KHOSACH" +
                " where TENSACH LIKE N'%" + txtTenSach.Text + "%'";
            da_TTBan = new SqlDataAdapter(strsel, _Connsql);
            ds_TTBan.Clear();
            da_TTBan.Fill(ds_TTBan, "TTBAN");
            dgvTTBan.DataSource = ds_TTBan.Tables["TTBAN"];
            Databingding(ds_TTBan.Tables["TTBAN"]);
            // dgvchinh.Columns[2].DefaultCellStyle.Format = "yyyy/MM/dd";
        }
        
        public void Databingding(DataTable pDT)
        {
            //pDT.Columns[2].DefaultCellStyle.Format = "yyyy/MM/dd";

            txtTenSach.DataBindings.Clear();
            cboMaSach.DataBindings.Clear();
            cboTenNXB.DataBindings.Clear();
            cboTenTG.DataBindings.Clear();
            cboTheLoai.DataBindings.Clear();
            //txtSoLuong.DataBindings.Clear();
            txtTenSach.DataBindings.Add("Text", pDT, "TENSACH");
            cboMaSach.DataBindings.Add("Text", pDT, "MASACH");
            cboTenTG.DataBindings.Add("Text", pDT, "TENTACGIA");
            cboTenNXB.DataBindings.Add("Text", pDT, "TENNHAXUATBAN");
            cboTheLoai.DataBindings.Add("Text", pDT, "TENTHELOAI");
            //txtSoLuong.DataBindings.Add("Text", pDT, "SOLUONGCON"); 
            
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTenSach.Text != "")
            {
                loaddgvTTBan();
                Databingding(ds_TTBan.Tables[0]);
            }
            else
            {
                MessageBox.Show("Mời bạn nhập tên sách");
            }
        }


        public void loadTongTien()
        {
            try
            {

                connsql.Open();
                string u = "CHITIETHOADON", v = "SOHOADON", x = "GIABAN", y = "SOLUONGBAN";
                if (rdoNhap.Checked)
                {
                    u = "CHITIETPHIEUNHAP";
                    v = "SOPHIEUNHAP";
                    x = "GIANHAP";
                    y = "SOLUONG";
                }
                string strsel = "select SUM (THANHTIEN) FROM ( SELECT " + y + " * " + x + " AS THANHTIEN from KHOSACH," + u +
                " where KHOSACH.MASACH = " + u + ".MASACH AND " + v + " = " + count.ToString() + ") AS R1";
                if (rdoNhap.Checked)
                    strsel = "select SUM (THANHTIEN) FROM ( SELECT GIANHAP * SOLUONG AS THANHTIEN from CHITIETPHIEUNHAP" +
                " where SOPHIEUNHAP = " + count.ToString() + ") AS R1";

                SqlCommand CMD = new SqlCommand(strsel, connsql);
                int tt = (int)CMD.ExecuteScalar();
                txtTongTien.Text = tt.ToString();
                string upd = "update HOADON SET TONGTIEN = " + txtTongTien.Text + " WHERE SOHOADON = " + count.ToString();
                if (rdoNhap.Checked)
                    upd = "update PHIEUNHAP SET TONGTIEN = " + txtTongTien.Text + " WHERE SOPHIEUNHAP = " + count.ToString();
                CMD = new SqlCommand(upd, connsql);
                CMD.ExecuteNonQuery();
                connsql.Close();
            }
            catch (Exception ex)
            {
                connsql.Close();
                MessageBox.Show("Tong tien" + ex.Message);
            }
        }
        public void loadcboNV()
        {
            connsql.Open();
            string selectString = "select MANHANVIEN from NHANVIEN";
            SqlCommand cmd = new SqlCommand(selectString, connsql);
            SqlDataReader rd = cmd.ExecuteReader();
            cboMNV.Items.Clear();
            cboMaNV.Items.Clear();
            cboMNV.Items.Add("Tất cả");
           
            while (rd.Read())
            {
                cboMNV.Items.Add(rd["MANHANVIEN"].ToString());
                cboMaNV.Items.Add(rd["MANHANVIEN"].ToString());
                cboMNVTC.Items.Add(rd["MANHANVIEN"].ToString());
            }
            rd.Close();
            connsql.Close();
            cboMaNV.SelectedIndex = 0;
            cboMNV.SelectedIndex = 0;
        }
        public void loadcboKH()
        {
            connsql.Open();
            string selectString = "select MAKHACHHANG from KHACHHANG";
            SqlCommand cmd = new SqlCommand(selectString, connsql);
            SqlDataReader rd = cmd.ExecuteReader();
            cboMaKH.Items.Clear();
            cboKH.Items.Clear();
            cboKH.Items.Add("Tất cả");
            while (rd.Read())
            {
                cboMaKH.Items.Add(rd["MAKHACHHANG"].ToString());
                cboMKHTC.Items.Add(rd["MAKHACHHANG"].ToString());
                cboKH.Items.Add(rd["MAKHACHHANG"].ToString());
            }
            rd.Close();
            connsql.Close();
            //cboMaNV.SelectedIndex = 0;
        }
        public void loadcboMS()
        {
            connsql.Open();
            string selectString = "select MASACH,TENTACGIA,TENTHELOAI,TENNHAXUATBAN from KHOSACH";
            SqlCommand cmd = new SqlCommand(selectString, connsql);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                cboMaSach.Items.Add(rd["MASACH"].ToString());
                cboTenTG.Items.Remove(rd["TENTACGIA"].ToString());
                cboTenTG.Items.Add(rd["TENTACGIA"].ToString());
                cboTheLoai.Items.Remove(rd["TENTHELOAI"].ToString());
                cboTheLoai.Items.Add(rd["TENTHELOAI"].ToString());
                cboTenNXB.Items.Remove(rd["TENNHAXUATBAN"].ToString());
                cboTenNXB.Items.Add(rd["TENNHAXUATBAN"].ToString());

                cboMSKS.Items.Add(rd["MASACH"].ToString());
                cboTGKS.Items.Remove(rd["TENTACGIA"].ToString());
                cboTGKS.Items.Add(rd["TENTACGIA"].ToString());
                cboTLKS.Items.Remove(rd["TENTHELOAI"].ToString());
                cboTLKS.Items.Add(rd["TENTHELOAI"].ToString());
                cboNXBKS.Items.Remove(rd["TENNHAXUATBAN"].ToString());
                cboNXBKS.Items.Add(rd["TENNHAXUATBAN"].ToString());

            }
            rd.Close();
            connsql.Close();
            //cboMaNV.SelectedIndex = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            culture.DateTimeFormat.LongDatePattern = "dd, MMMM, yyyy";
            Thread.CurrentThread.CurrentCulture = culture;
            loadcboNV();
            loadcboKH();
            loadcboMS();
            load_dt();
            loaddgvTTBan();
            rdoBan.Checked = true;
            rdoThu.Checked = true;
            mskKTTC.Text = mskBDTC.Text = DateTime.Now.ToString();
            cboKH.SelectedIndex = cboDT.SelectedIndex = 0;
        }

        public void loaddgvHD()
        {
            try
            {
                string u = "CHITIETHOADON", v = "SOHOADON", x = "GIABAN", y = "SOLUONGBAN";
                if (rdoNhap.Checked)
                {
                    u = "CHITIETPHIEUNHAP";
                    v = "SOPHIEUNHAP";
                    x = "GIANHAP";
                    y = "SOLUONG";
                }
                string strsel = "select KHOSACH.MASACH,TENSACH," + y + "," + x + "," + y + " * " + x + " AS THANHTIEN from KHOSACH," + u +
                 " where KHOSACH.MASACH = " + u + ".MASACH AND " + v + " = " + count.ToString();
                da_HDBan = new SqlDataAdapter(strsel, _Connsql);
                // ds_HDBan.Tables[0].Clear();
                da_HDBan.Fill(ds_HDBan, "HDBAN");
                dgvHDBan.DataSource = ds_HDBan.Tables[0];
                key[0] = ds_HDBan.Tables[0].Columns[0];
                ds_HDBan.Tables[0].PrimaryKey = key;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hoa don" + ex.Message);
            }

        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            if (f)
            {
                MessageBox.Show("Chưa có hóa đơn mới");
                return;
            }
            f = true;
            try 
            {
                connsql.Open();
                string vv = "Thời gian bán sách : ";

                if (rdoNhap.Checked)
                    vv = "Thời gian nhập sách : ";
                string hd = vv  + DateTime.Now.ToString() +"\n";
                hd += "Nhân viên : ";
                string nv = "select HOTEN from NHANVIEN where MANHANVIEN = '" + cboMaNV.Text + "'";
                string kh = "select TENKHACHHANG from KHACHHANG where MAKHACHHANG = '" + cboMaKH.Text + "'";
                if (rdoNhap.Checked)
                    kh = "select TENDOITAC from DOITAC where MADOITAC = '" + cboMaKH.Text + "'";
                SqlCommand cmm = new SqlCommand(nv, connsql);
                SqlDataReader rh = cmm.ExecuteReader();
                while (rh.Read())
                {
                    hd += rh["HOTEN"].ToString() + "\n";
                    break;
                }
                rh.Close();
                cmm = new SqlCommand(kh, connsql);
                SqlDataReader rr = cmm.ExecuteReader();
                while(rr.Read())
                {
                    if (rdoBan.Checked)
                        hd += "Khách hàng : " + rr["TENKHACHHANG"].ToString() + "\n\n";
                    else
                        hd += "Đối tác : " + rr["TENDOITAC"].ToString() + "\n\n";
                    break;
                }
                rr.Close();
                //MessageBox.Show(hd);
                string u = "CHITIETHOADON", v = "SOHOADON", x = "GIABAN", y = "SOLUONGBAN";
                if (rdoNhap.Checked)
                {
                    u = "CHITIETPHIEUNHAP";
                    v = "SOPHIEUNHAP";
                    x = "GIANHAP";
                    y = "SOLUONG";
                }
                string strsel = "select KHOSACH.MASACH,TENSACH," + y + "," + x + "," + y + " * " + x + " AS THANHTIEN from KHOSACH," + u +
                 " where KHOSACH.MASACH = " + u + ".MASACH AND " + v + " = " + count.ToString();
                SqlCommand cmd = new SqlCommand(strsel, connsql);
                SqlDataReader rd = cmd.ExecuteReader();
                 hd += "Tên sách\t\t\tSố lượng\t\tThành tiền\n";
                while(rd.Read())
                {
                    hd += rd["TENSACH"].ToString() + "\t\t" + rd[y].ToString() + "\t\t" + rd["THANHTIEN"].ToString()+ "\n";
                }
                rd.Close();
                hd += "\nTổng tiền\t\t\t\t" + txtTongTien.Text;
                MessageBox.Show(hd);
                connsql.Close();
                txtTongTien.Text = "";
            }
            catch(Exception ex)
            {
                connsql.Close();
               // MessageBox.Show(ex.Message);
            }
           ds_HDBan.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!checktrung(txtTenSach.Text))
            {
                MessageBox.Show("Bạn cần chọn đúng tên sách và mã sách");
                return;
            }
            if (cboMaSach.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn sách");
                return;
            }
            if (!f)
            {
                if (!checkMS(cboMaSach.Text, rdoBan.Checked))
                {

                    try
                    {
                        connsql.Open();
                        int slc = ktslKS(cboMaSach.Text) + ktslHD(cboMaSach.Text);
                        int sl = int.Parse(txtSoLuong.Text);
                        MessageBox.Show(slc.ToString());
                        if (slc < sl)
                        {
                            connsql.Close();
                            MessageBox.Show("Không đủ sách trong kho");
                            return;
                        }

                        updateCTHD(cboMaSach.Text, int.Parse(txtSoLuong.Text));

                        string updateString = "update HOADON set MAKHACHHANG = '" + cboMaKH.Text + "', MANHANVIEN = '" + cboMaNV.Text +
                                 "'where SOHOADON = " + count.ToString();
                        SqlCommand cmd = new SqlCommand(updateString, connsql);
                        cmd.ExecuteNonQuery();
                        updateKS(cboMaSach.Text, slc - sl);
                        connsql.Close();
                        MessageBox.Show("Sửa thành công!");
                        loaddgvHD();
                        ds_TTBan.Tables[0].Clear();
                        loaddgvTTBan();
                    }
                    catch (Exception ex)
                    {
                       // MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    themb();
                }
                loadTongTien();
            }

        }
        public int ktslPN(string MS)
        {
            string ss = "select MAX(SOLUONG) from CHITIETPHIEUNHAP where MASACH = '" + MS + "' AND SOPHIEUNHAP = " + count.ToString();
            SqlCommand cpp = new SqlCommand(ss, connsql);
            return (int)cpp.ExecuteScalar();
        }
        public void updateKS(string MS, int sl)
        {
            string s = "update KHOSACH set SOLUONGCON = " + sl.ToString() +
                        "where MASACH = '" + MS + "'";
            SqlCommand ckk = new SqlCommand(s, connsql);
            ckk.ExecuteNonQuery();
        }
        public void deleteCTHD(string MS)
        {
            string u = "CHITIETHOADON", v = "SOHOADON";
            if (rdoNhap.Checked)
            {
                u = "CHITIETPHIEUNHAP";
                v = "SOPHIEUNHAP";
            }
            string updateString = "delete " + u + " where MASACH ='" + MS + "' AND " + v + " = " + count.ToString();
            SqlCommand cmd = new SqlCommand(updateString, connsql);
            cmd.ExecuteNonQuery();
        }
        public void updateCTHD(string MS, int sl)
        {
            string updateString = "update CHITIETHOADON set SOLUONGBAN = " + sl.ToString() +
                                 " where MASACH = '" + MS + "' AND SOHOADON = " + count.ToString();
            SqlCommand cmd = new SqlCommand(updateString, connsql);
            cmd.ExecuteNonQuery();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!checktrung(txtTenSach.Text))
            {
                MessageBox.Show("Bạn cần chọn đúng tên sách và mã sách");
                return;
            }

            if (cboMaSach.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn sách");
                return;
            }

            if (!f)
            {
                if (!checkMS(cboMaSach.Text, rdoBan.Checked))
                {
                    DialogResult r = MessageBox.Show("Bạn có thực sự muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.Yes)
                    {

                        try
                        {
                            connsql.Open();
                            if (rdoBan.Checked)
                            {
                                updateKS(cboMaSach.Text, ktslKS(cboMaSach.Text) + ktslHD(cboMaSach.Text));
                            }
                            else
                            {
                                updateKS(cboMaSach.Text, ktslKS(cboMaSach.Text) - ktslPN(cboMaSach.Text));
                            }
                            deleteCTHD(cboMaSach.Text);
                            connsql.Close();
                            MessageBox.Show(" Đã xóa thành công! ");
                            ds_HDBan.Tables[0].Clear();
                            loaddgvHD();
                            ds_TTBan.Tables[0].Clear();
                            loaddgvTTBan();
                        }
                        catch (Exception ex)
                        {
                          //  MessageBox.Show(ex.Message);
                        }
                    }
                    loadTongTien();
                }
                else
                {
                    MessageBox.Show("Sách chưa được chọn");
                }
            }

        }
        public void huy()
        {
            if (!f)
            {
                DialogResult r = MessageBox.Show("Bạn có thực sự muốn hủy đơn hàng hiện tại ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button1);
                if (r == DialogResult.Yes)
                {
                    f = true;
                    try
                    {

                        connsql.Open();
                        string u = "CHITIETHOADON", v = "SOHOADON";
                        if (rdoNhap.Checked)
                        {
                            u = "CHITIETPHIEUNHAP";
                            v = "SOPHIEUNHAP";
                        }
                        string re = "select MASACH FROM " + u + " WHERE " + v + " = " + count.ToString();
                        SqlCommand smd = new SqlCommand(re, connsql);
                        SqlDataReader rd = smd.ExecuteReader();
                        List<string> s = new List<string>();
                        while (rd.Read())
                        {
                            s.Add(rd["MASACH"].ToString());
                        }
                        rd.Close();
                        foreach (string ss in s)
                        {
                            if (rdoBan.Checked)
                                updateKS(ss, ktslKS(ss) + ktslHD(ss));
                            else
                                updateKS(ss, ktslKS(ss) - ktslPN(ss));
                            deleteCTHD(ss);
                        }
                        string xoa = "delete HOADON where SOHOADON = " + count.ToString();
                        if (rdoNhap.Checked)
                            xoa = "delete PHIEUNHAP where SOPHIEUNHAP = " + count.ToString();
                        smd = new SqlCommand(xoa, connsql);
                        smd.ExecuteNonQuery();
                        connsql.Close();
                        MessageBox.Show(" Đã Hủy ");
                        ds_HDBan.Tables[0].Clear();
                        loaddgvHD();
                        ds_TTBan.Tables[0].Clear();
                        loaddgvTTBan();

                        txtTongTien.Text = "";
                    }
                    catch (Exception ex)
                    {
                        connsql.Close();
                        //MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            huy();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ctr = (TextBox)sender;
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
                e.Handled = false;
            else
                e.Handled = true;
        }
        public bool checkMS(string ms, bool kk)
        {

            try
            {
                connsql.Open();
                string u = "CHITIETHOADON", v = "SOHOADON";
                if (!kk)
                {
                    u = "CHITIETPHIEUNHAP"; v = "SOPHIEUNHAP";
                }
                string ss = "select count(*) from " + u + " WHERE MASACH = '" + ms + "' AND +" + v + "=" + count.ToString();
                SqlCommand cmd = new SqlCommand(ss, connsql);
                int sl = (int)cmd.ExecuteScalar();
                connsql.Close();
                return sl == 0;
            }
            catch (Exception ex)
            {
               // MessageBox.Show("NÈ " + ex.Message);
            }
            return false;
        }
        public int ktslKS(string MS)
        {
            string ss = "select MAX(SOLUONGCON) from KHOSACH where MASACH = '" + MS + "'";
            SqlCommand cpp = new SqlCommand(ss, connsql);
            return (int)cpp.ExecuteScalar();
        }
        public int ktslHD(string MS)
        {
            string ss = "select MAX(SOLUONGBAN) from CHITIETHOADON where MASACH = '" + MS + "' AND SOHOADON = " + count.ToString();
            SqlCommand cpp = new SqlCommand(ss, connsql);
            return (int)cpp.ExecuteScalar();
        }
        public bool checkMSKS(string MS)
        {
            connsql.Open();
            string ss = "select count(*) from KHOSACH WHERE MASACH = '" + MS + "'";
            SqlCommand cmd = new SqlCommand(ss, connsql);
            int sl = (int)cmd.ExecuteScalar();
            connsql.Close();
            if (sl > 0)
                txtGiaBan.Text = "";
            return sl == 0;
        }
        public void themb()
        {
            if (rdoBan.Checked && !checktrung(txtTenSach.Text))
            {
                MessageBox.Show("Bạn cần chọn đúng tên sách và mã sách");
                return;
            }
            if (txtSoLuong.Text.Length == 0)
            {
                MessageBox.Show("Mời bạn nhập số lượng sách");
                return;
            }
            if (cboMaSach.Text.Length == 0)
            {
                MessageBox.Show("Mời bạn chọn đúng sách trong danh sách hoặc nhập mã sách");
                return;
            }
            if (rdoNhap.Checked && txtGiaNhap.Text.Length == 0)
            {
                MessageBox.Show("Mời bạn điền giá nhập");
                txtGiaNhap.Focus();
                return;
            }
            try
            {
                if (checkMSKS(cboMaSach.Text) && rdoNhap.Checked)
                {
                   
                    try
                    {
                        string ss = "insert into KHOSACH (MASACH,TENSACH,SOLUONGCON,TENTACGIA,GIABAN,TENTHELOAI,TENNHAXUATBAN) values('" +
                        cboMaSach.Text + "',N'" + txtTenSach.Text + "'," + "0" + ",N'" + cboTenTG.Text + "'," + txtGiaBan.Text +
                        ",N'" + cboTheLoai.Text + "',N'" + cboTenNXB.Text + "')";
                        connsql.Open();
                        SqlCommand cmd = new SqlCommand(ss, connsql);
                        cmd.ExecuteNonQuery();
                        //MessageBox.Show("Thanh cong");
                        connsql.Close();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                    }
                }
                connsql.Open();
                int slc = ktslKS(cboMaSach.Text);
                int sl = int.Parse(txtSoLuong.Text);

                if (rdoBan.Checked)
                {
                   // MessageBox.Show(slc.ToString());

                    if (slc < sl)
                    {
                        MessageBox.Show("Số lượng sách trong kho không đủ!");
                        connsql.Close();
                        return;
                    }
                    updateKS(cboMaSach.Text, slc - sl);
                }
                else
                    updateKS(cboMaSach.Text, slc + sl);
                connsql.Close();
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("ơ day" + ex.Message);
            }

            if (f)
            {
                f = false;
                try
                {
                    if (connsql.State == ConnectionState.Open)
                    {
                        connsql.Close();
                    }
                    if (connsql.State == ConnectionState.Closed)
                    {
                        connsql.Open();
                    }

                    string insertString;
                    if (rdoBan.Checked)
                        insertString = "insert into HOADON (MAKHACHHANG,NGAYBAN,MANHANVIEN) values('" +
                        cboMaKH.Text + "','" + DateTime.Now.ToString() + "','" + cboMaNV.Text + "')";
                    else
                        insertString = "insert into PHIEUNHAP (MADOITAC,NGAYNHAP,MANHANVIEN) values('" +
                        cboMaKH.Text + "','" + DateTime.Now.ToString() + "','" + cboMaNV.Text + "')";
                    SqlCommand cmd = new SqlCommand(insertString, connsql);
                    cmd.ExecuteNonQuery();
                    string ss = "SOHOADON", s = "HOADON";
                    if (rdoNhap.Checked)
                    {
                        ss = "SOPHIEUNHAP";
                        s = "PHIEUNHAP";
                    }
                    string selectString = "select MAX(" + ss + ") from " + s;
                    cmd = new SqlCommand(selectString, connsql);
                   
                    count = (int)cmd.ExecuteScalar();
                    if (connsql.State == ConnectionState.Open)
                    {
                        connsql.Close();
                    }
                  // MessageBox.Show("Thanh cong " + count.ToString());
                }
                catch (Exception ex)
                {
                    connsql.Close();
                   // MessageBox.Show(ex.Message + " nè nè");
                }


            }

            if (checkMS(cboMaSach.Text, rdoBan.Checked))
            {
                try
                {
                    if (connsql.State == ConnectionState.Closed)
                    {
                        connsql.Open();
                    }
                    string u = "CHITIETHOADON", v = "";
                    if (rdoNhap.Checked)
                    {
                        u = "CHITIETPHIEUNHAP";
                        v = "," + txtGiaNhap.Text;
                    }
                    string insertString;
                    insertString = "insert into " + u + " values (" + count.ToString() + ",'" + cboMaSach.Text + "'," + txtSoLuong.Text + v + ")";
                    SqlCommand cmd = new SqlCommand(insertString, connsql);
                    cmd.ExecuteNonQuery();
                    if (connsql.State == ConnectionState.Open)
                    {
                        connsql.Close();
                    }
                    MessageBox.Show("Thêm sách thành công!");
                    loaddgvHD();
                }
                catch (Exception ex)
                {
                    connsql.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Bạn đã chọn sách này");
            }

            ds_TTBan.Clear();
            loaddgvTTBan();
            loadTongTien();
        }
        public bool checktrung(string TS)
        {
            try
            {
                connsql.Open();
                string selectString = "select MASACH from KHOSACH where TENSACH = N'" + TS + "'";
                SqlCommand cmd = new SqlCommand(selectString, connsql);
                SqlDataReader rd = cmd.ExecuteReader();
                string ss = "";
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (cboMaSach.Text == rd["MASACH"].ToString())
                        {
                            rd.Close();
                            connsql.Close();
                            return true;
                        }
                    }
                    rd.Close();
                }
                connsql.Close();
            }
            catch(Exception ex)
            {
                connsql.Close();
           //     MessageBox.Show(ex.Message);
            }
            return false;
        }

        private void cboMaSach_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = rdoBan.Checked;
        }

        private void cboMaNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public void loadcboDT()
        {
            connsql.Open();
            string selectString = "select MADOITAC from DOITAC";
            SqlCommand cmd = new SqlCommand(selectString, connsql);
            SqlDataReader rd = cmd.ExecuteReader();
            cboMaKH.Items.Clear();
            while (rd.Read())
            {
                cboMaKH.Items.Add(rd["MADOITAC"].ToString());
            }
            rd.Close();
            connsql.Close();
        }

        private void txtGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ctr = (TextBox)sender;
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8 || (e.KeyChar == 45 && ctr.Text.Length == 0))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void tabNV_Click(object sender, EventArgs e)
        {

        }

        private void rdoMua_CheckedChanged(object sender, EventArgs e)
        {
            huy();
            if (rdoNhap.Checked)
            {
                lblMaKH.Text = "Mã Đối Tác";
                // cboMaSach.;
                btnXuat.Text = "Xuất Phiếu Nhập";
                loadcboDT();


            }
            if (rdoBan.Checked)
            {
                loadcboKH();
                lblMaKH.Text = "Mã Khách Hàng";
                // cboMaSach.Handle = true;
                btnXuat.Text = "Xuất Hóa Đơn";
            }
            cboTheLoai.Visible = cboTenNXB.Visible = cboTenTG.Visible = txtGiaNhap.Visible = txtGiaBan.Visible = rdoNhap.Checked;
            lblTheloai.Visible = lblGiaBan.Visible = lblGiaNhap.Visible = lblTenTG.Visible = lblTenNXB.Visible = rdoNhap.Checked;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themb();

        }
        public void loadtabNV()
        {
            string strsel = "select * from NHANVIEN ";
            da_NV = new SqlDataAdapter(strsel, _Connsql);
            ds_NV.Clear();
            da_NV.Fill(ds_NV, "TTNV");
            dgvNV.DataSource = ds_NV.Tables["TTNV"];
            Databingdin(ds_NV.Tables[0]);
        }
        public void loadtabNV(string MS)
        {
            string strsel = "select * from NHANVIEN where MANHANVIEN = '" + MS + "'";
            da_NV = new SqlDataAdapter(strsel, _Connsql);
            ds_NV.Clear();
            da_NV.Fill(ds_NV, "TTNV");
            dgvNV.DataSource = ds_NV.Tables["TTNV"];
            Databingdin(ds_NV.Tables[0]);
        }
        private void cboMNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMNV.SelectedIndex == 0)
            {
                loadtabNV();
            }
            else
            {
                loadtabNV(cboMNV.SelectedItem.ToString());
            }

        }
        public void loaddgvTKNV()
        {

            string strsel = "select * from NHANVIEN where HOTEN like N'%" + txtHTNV.Text + "%'";
            da_NV = new SqlDataAdapter(strsel, _Connsql);
            ds_NV.Clear();
            da_NV.Fill(ds_NV, "TTNV");
            dgvNV.DataSource = ds_NV.Tables["TTNV"];
            Databingdin(ds_NV.Tables[0]);
        }
        private void btnTimNV_Click(object sender, EventArgs e)
        {
            if (txtHTNV.Text.Length != 0)
            {
                loaddgvTKNV();
            }
            else
            {
                MessageBox.Show("Mời nhập tên nhân viên");
            }
        }
        public bool checkGT(string gt)
        {
            return gt == "Nam" || gt == "Nữ";
        }
        private void btnThemNV_Click(object sender, EventArgs e)
        {

            if (!checkGT(txtGTNV.Text))
            {
                MessageBox.Show("Mời nhập chính xác thông tin giới tính");
                return;
            }
            foreach (string s in cboMNV.Items)
            {
                if (s == cboMNV.Text)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại");
                    return;
                }
            }
            try
            {
                connsql.Open();
                string insertString = "insert into NHANVIEN values('" +
                            cboMNV.Text + "','" + txtCCCD.Text + "',N'" + txtHTNV.Text + "',N'" + txtGTNV.Text + "','"
                            + mskNSNV.Text + "',N'" + txtDCNV.Text + "','" + txtDTNV.Text + "')";
                SqlCommand cmd = new SqlCommand(insertString, connsql);
                cmd.ExecuteNonQuery();
                cboMNV.Items.Add(cboMNV.Text);
                connsql.Close();
                MessageBox.Show("Thêm nhân viên thành công!");
            }
            catch (Exception ex)
            {
                connsql.Close();
                MessageBox.Show("Mời nhập đúng định dạng ngày sinh" + ex.Message);
            }
            loaddgvTKNV();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            if (!checkGT(txtGTNV.Text))
            {
                MessageBox.Show("Mời nhập chính xác thông tin giới tính");
                return;
            }
            foreach (string pp in cboMNV.Items)
            {
                if (pp == lblMNV.Text && pp != cboMNV.Items[0])
                {
                    try
                    {
                        connsql.Open();
                        string insertString = "update NHANVIEN set CCCD = '" + txtCCCD.Text + "',HOTEN  = N'" + txtHTNV.Text +
                            "', GIOITINH = N'" + txtGTNV.Text + "', NGAYSINH = '"
                                    + mskNSNV.Text + "',DIACHI = N'" + txtDCNV.Text + "', DIENTHOAI = '" + txtDTNV.Text
                                    + "' where MANHANVIEN = '" + pp + "'";
                        SqlCommand cmd = new SqlCommand(insertString, connsql);
                        cmd.ExecuteNonQuery();
                        // cboMNV.Items.Add(cboMNV.Text);
                        connsql.Close();
                        MessageBox.Show("Sửa thông tin thành công!");
                    }
                    catch (Exception ex)
                    {
                        connsql.Close();
                     //   MessageBox.Show("Đây " + ex.Message);
                    }
                    loaddgvTKNV();

                    return;
                }
            }
            MessageBox.Show("Mã nhân viên không tồn tại");
        }
        public bool checkMNV(string mnv)
        {
            try
            {
                connsql.Open();
                string ss = "select COUNT(*) FROM HOADON WHERE MANHANVIEN = '" + mnv + "'";
                SqlCommand CMD = new SqlCommand(ss, connsql);
                int dem = (int)CMD.ExecuteScalar();
                connsql.Close();
               // MessageBox.Show("GOOD");
                return dem > 0;
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
                connsql.Close();
                return false;
            }
        }
        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            foreach (string pp in cboMNV.Items)
            {
                if (pp == lblMNV.Text && pp != cboMNV.Items[0])
                {
                    if (checkMNV(pp))
                    {
                        MessageBox.Show("Không thể xóa vì nhân viên có liên quan đến các hóa đơn");
                        return;
                    }

                    try
                    {
                        DialogResult r = MessageBox.Show("Bạn có thực sự muốn xóa nhân viên?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.No)
                            return;
                        else
                        {
                            connsql.Open();
                            string insertString = "delete NHANVIEN where MANHANVIEN = '" + pp + "'";
                            SqlCommand cmd = new SqlCommand(insertString, connsql);
                            cmd.ExecuteNonQuery();
                            // cboMNV.Items.Add(cboMNV.Text);
                            connsql.Close();
                            cboMNV.Items.Remove(pp);
                            cboMNV.SelectedIndex = 0;
                            loadtabNV();

                            MessageBox.Show("Xóa nhân viên thành công!");
                        }
                     
                    }
                    catch (Exception ex)
                    {
                        connsql.Close();
                      //  MessageBox.Show("Đây " + ex.Message);
                    }
                    return;
                }
            }
            MessageBox.Show("Mã nhân viên không tồn tại!");
        }

        public void loaddgvTC()
        {
            try
            {
                string u = "HOADON", v = "NGAYBAN";
                if (rdoChi.Checked)
                {
                    u = "PHIEUNHAP";
                    v = "NGAYNHAP"; 
                }
                string x = "MAKHACHHANG = '", y = "MANHANVIEN = '",x1 = "'", y1 = "'", z = " AND ", y2 = " AND ";
                if(cboMNVTC.Text == "")
                {
                    y = "";
                    y1 = y2 =  "";
                }
                if (cboMKHTC.Text == "")
                {
                    x = x1 = "";
                    y2 = "";
                }
                if (cboMNVTC.Text == "" && cboMKHTC.Text == "")
                    z = "";
                string st = "select * from " + u + " WHERE " + x + cboMKHTC.Text + x1 + y2 + y + cboMNVTC.Text + y1 + z + v + " BETWEEN '" + mskBDTC.Text + "' AND '" + mskKTTC.Text + "'";
                da_TC = new SqlDataAdapter(st, _Connsql);
                ds_TC.Clear();
                ds_TCC.Clear();
                if (rdoThu.Checked)
                {
                    da_TC.Fill(ds_TC, "TTC");
                    dgvTC.DataSource = ds_TC.Tables["TTC"];
                }
                else
                {
                    da_TC.Fill(ds_TCC, "TTC");
                    dgvTC.DataSource = ds_TCC.Tables["TTC"];
                }    

                
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("TC " + ex.Message);
            }
        }
        public void loadTTCC()
        {
            try
            {
                connsql.Open();
                string u = "HOADON", v = "NGAYBAN";
                if (rdoChi.Checked)
                {
                    u = "PHIEUNHAP";
                    v = "NGAYNHAP";
                }
                string x = "MAKHACHHANG = '", y = "MANHANVIEN = '", x1 = "'", y1 = "'", z = " AND ", y2 = " AND ";
                if (cboMNVTC.Text == "")
                {
                    y = "";
                    y1 = y2 = "";
                }
                if (cboMKHTC.Text == "")
                {
                    x = x1 = "";
                    y2 = "";
                }
                if (cboMNVTC.Text == "" && cboMKHTC.Text == "")
                    z = "";
                string stt = "select SUM(TONGTIEN) from " + u + " WHERE " + x + cboMKHTC.Text + x1 + y2 + y + cboMNVTC.Text + y1 + z + v + " BETWEEN '" + mskBDTC.Text + "' AND '" + mskKTTC.Text + "'";
                string st = "select count(*) from " + u + " WHERE " + x + cboMKHTC.Text + x1 + y2 + y + cboMNVTC.Text + y1 + z + v + " BETWEEN '" + mskBDTC.Text + "' AND '" + mskKTTC.Text + "'";
                SqlCommand cmm = new SqlCommand(st, connsql);
                int t = (int)cmm.ExecuteScalar();
                if(t == 0)
                {
                    MessageBox.Show("Không có hóa đơn nào được tìm thấy");
                    connsql.Close();
                    txtTongTienTC.Text = "0";
                    return;
                }  
                SqlCommand cmd = new SqlCommand(stt, connsql);
                txtTongTienTC.Text = ((int)cmd.ExecuteScalar()).ToString();
                connsql.Close();
               // MessageBox.Show("tot roi");
            }
            catch (Exception ex)
            {
                connsql.Close();
                MessageBox.Show("TTTC " + ex.Message);
            }
        }
        private void btnTKTC_Click(object sender, EventArgs e)
        {
            loadTTCC();
            loaddgvTC();
        }

        private void rdoThu_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void tabKS_Click(object sender, EventArgs e)
        {

        }
        SqlDataAdapter da_KS;
        DataSet ds_KS = new DataSet();
        public void loadTKKS()
        {
             string strsel = "select MASACH,TENSACH,TENTACGIA,SOLUONGCON,TENTHELOAI,TENNHAXUATBAN,GIABAN from KHOSACH" +
                " where TENSACH LIKE N'%" + txtTSKS.Text + "%'";
            da_KS = new SqlDataAdapter(strsel, _Connsql);
            ds_KS.Clear();
            da_KS.Fill(ds_KS, "TKKS");
            dgvKS.DataSource = ds_KS.Tables[0];
            Databin(ds_KS.Tables[0]);
        }
        public void loadKS()
        {
            string strsel = "select MASACH,TENSACH,TENTACGIA,SOLUONGCON,TENTHELOAI,TENNHAXUATBAN,GIABAN from KHOSACH";
            da_KS = new SqlDataAdapter(strsel, _Connsql);
            ds_KS.Clear();
            da_KS.Fill(ds_KS, "TKKS");
            dgvKS.DataSource = ds_KS.Tables[0];
            Databin(ds_KS.Tables[0]);
        }
        private void btnTimkiemKS_Click(object sender, EventArgs e)
        {
            loadTKKS();
        }
        public void Databin(DataTable pDT)
        {
            //pDT.Columns[2].DefaultCellStyle.Format = "yyyy/MM/dd";

            txtTSKS.DataBindings.Clear();
            txtSLKS.DataBindings.Clear();
            cboNXBKS.DataBindings.Clear();
            cboTGKS.DataBindings.Clear();
            cboTLKS.DataBindings.Clear();
            txtGBKS.DataBindings.Clear();
            cboMSKS.DataBindings.Clear();
            //txtSoLuong.DataBindings.Clear();
            txtTSKS.DataBindings.Add("Text", pDT, "TENSACH");
            txtSLKS.DataBindings.Add("Text", pDT, "SOLUONGCON");
            cboTGKS.DataBindings.Add("Text", pDT, "TENTACGIA");
            cboNXBKS.DataBindings.Add("Text", pDT, "TENNHAXUATBAN");
            cboTLKS.DataBindings.Add("Text", pDT, "TENTHELOAI");
            txtGBKS.DataBindings.Add("Text", pDT, "GIABAN");
            cboMSKS.DataBindings.Add("Text", pDT, "MASACH");


            //txtSoLuong.DataBindings.Add("Text", pDT, "SOLUONGCON"); 
        }
        public bool checkMSKS()
        {
            if(cboMSKS.Text == "")
            {
                return true;
            }
            try
            {
                connsql.Open();
                string ss = "select count(*) from KHOSACH where MASACH ='" + cboMSKS.Text + "'";
                SqlCommand cmd = new SqlCommand(ss, connsql);
                int t = (int)cmd.ExecuteScalar();
               
                connsql.Close();
              //  MessageBox.Show("OK roi do");
                return t > 0;
            }catch(Exception ex)
            {
                connsql.Close();
               // MessageBox.Show(ex.Message);
                return true;
            }
        }
        private void btnThemKS_Click(object sender, EventArgs e)
        {
            if(cboMSKS.Text.Length == 0)
            {
                MessageBox.Show("Mời nhập mã sách");
                return;
            }
            if(checkMSKS())
            {
                MessageBox.Show("Mã sách đã tồn tại");
                return;
            }
            try
            {
                string ss = "insert into KHOSACH (MASACH,TENSACH,SOLUONGCON,TENTACGIA,GIABAN,TENTHELOAI,TENNHAXUATBAN) values('" +
                cboMSKS.Text + "',N'" + txtTSKS.Text + "'," + txtSLKS.Text + ",N'" + cboTGKS.Text + "'," + txtGBKS.Text +
                ",N'" + cboTLKS.Text + "',N'" + cboNXBKS.Text + "')";
                connsql.Open();
                SqlCommand cmd = new SqlCommand(ss, connsql);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thêm sách thành công!");
                connsql.Close();
                loadKS();
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
        }

        private void btnSuaKS_Click(object sender, EventArgs e)
        {
            if (cboMSKS.Text.Length == 0)
            {
                MessageBox.Show("Mời nhập mã sách");
                return;
            }
            if (!checkMSKS())
            {
                MessageBox.Show("Mã sách không tồn tại!");
                return;
            }
            try
            {
                string ss = "update KHOSACH set TENSACH = N'"+ txtTSKS.Text +"',SOLUONGCON = '"+ txtSLKS.Text+
                    "',TENTACGIA = N' "+ cboTGKS.Text +" ',GIABAN = " + txtGBKS.Text +",TENTHELOAI = N'"+cboTLKS.Text+
                    " ',TENNHAXUATBAN = '" + cboNXBKS.Text +"' WHERE MASACH = '" + cboMSKS.Text  + "'";
                connsql.Open();
                SqlCommand cmd = new SqlCommand(ss, connsql);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Sửa sách thành công!");
                connsql.Close();
                loadKS();
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
        }
        public bool checkMSHDPT()
        {
            try 
            {
                connsql.Open();
                string ss = "select count(*) from CHITIETHOADON WHERE MASACH = '" + cboMSKS.Text+ "'";
                string s = "select count(*) from CHITIETPHIEUNHAP WHERE MASACH = '" + cboMSKS.Text + "'";
                SqlCommand cmd = new SqlCommand(ss, connsql);
                int t = (int)cmd.ExecuteScalar();
                cmd = new SqlCommand(s, connsql);
                int tt = (int)cmd.ExecuteScalar();
                connsql.Close();
               // MessageBox.Show("Tam ");
                return (t + tt) > 0;
            }
            catch(Exception ex)
            {
                connsql.Close();
              //  MessageBox.Show(ex.Message);
                return true;
            }
        }
        private void btnXoaKS_Click(object sender, EventArgs e)
        {
               if (cboMSKS.Text.Length == 0)
            {
                MessageBox.Show("Mời nhập mã sách");
                return;
            }
            if (!checkMSKS())
            {
                MessageBox.Show("Mã sách không tồn tại!");
                return;
            }
            if(checkMSHDPT())
            {
                MessageBox.Show("Không thể xóa mã sách có liên quan đến các hóa đơn!");
                return;
            }
            try 
            {
                DialogResult r = MessageBox.Show("Bạn có thực sự muốn xóa sách ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button1);
                if (r == DialogResult.No)
                    return;
                else
                {
                    connsql.Open();
                    string de = "delete KHOSACH where MASACH = '" + cboMSKS.Text + "'";
                    SqlCommand CMD = new SqlCommand(de, connsql);
                    CMD.ExecuteNonQuery();
                    MessageBox.Show("Xóa sách thành công!");
                    connsql.Close();
                    loadKS();
                }
               
            }
            catch(Exception ex)
            {
                connsql.Close();
               // MessageBox.Show(ex.Message);
            }
        }
        public void XoaHDPT(string so)
        {
            try
            {
                connsql.Open();
                string s = "delete CHITIETHOADON WHERE SOHOADON = " + so;
                if(rdoChi.Checked)
                {
                    s = "delete CHITIETPHIEUNHAP WHERE SOPHIEUNHAP = " + so;
                }
                SqlCommand CMM = new SqlCommand(s, connsql);
                CMM.ExecuteNonQuery();
                connsql.Close();
            }
            catch(Exception ex)
            {
                connsql.Close();
                //MessageBox.Show(ex.Message);
            }
        }
        private void btnXoaTC_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có thực sự muốn xóa các hóa đơn?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                return;
            try
            {
                connsql.Open();
                string u = "HOADON", v = "NGAYBAN";
                if (rdoChi.Checked)
                {
                    u = "PHIEUNHAP";
                    v = "NGAYNHAP";
                }
                string x = "MAKHACHHANG = '", y = "MANHANVIEN = '", x1 = "'", y1 = "'", z = " AND ", y2 = " AND ";
                if (cboMNVTC.Text == "")
                {
                    y = "";
                    y1 = y2 = "";
                }
                if (cboMKHTC.Text == "")
                {
                    x = x1 = "";
                    y2 = "";
                }
                if (cboMNVTC.Text == "" && cboMKHTC.Text == "")
                    z = "";
                string st = "select SO" + u +" from " + u + " WHERE " + x + cboMKHTC.Text + x1 + y2 +
                  y + cboMNVTC.Text + y1 + z + v + " BETWEEN '" + mskBDTC.Text + "' AND '" + mskKTTC.Text + "'";
                SqlCommand cmd = new SqlCommand(st, connsql);
                SqlDataReader rd = cmd.ExecuteReader();
                List<string> luu = new List<string>();
                while(rd.Read())
                {
                    luu.Add(rd["SO" + u].ToString());
                }
                rd.Close();
                connsql.Close();
                foreach (string sss in luu)
                {
                    XoaHDPT(sss);
                    connsql.Open();
                    string ss = "delete " + u + " where SO" + u + " = " + sss;
                    cmd = new SqlCommand(ss, connsql);
                    cmd.ExecuteNonQuery();
                    connsql.Close();
                }
              
              
                MessageBox.Show("Xóa hóa đơn thành công!");
                loaddgvTC();
            }
            catch(Exception ex)
            {
                connsql.Close();
             //   MessageBox.Show(ex.Message);
            }
        }
        SqlDataAdapter da_KH;
        DataSet ds_KH = new DataSet();
        public void load_tabKH()
        {

            string strsel = "select * from KHACHHANG ";
            da_KH = new SqlDataAdapter(strsel, _Connsql);
            ds_KH.Clear();
            da_KH.Fill(ds_KH, "TTKH");
            dgvKH.DataSource = ds_KH.Tables["TTKH"];
            DatabingdinG(ds_KH.Tables[0]);
        }
        public void DatabingdinG(DataTable pDT)
        {
            txttenKH.DataBindings.Clear();
            txtdiachiKH.DataBindings.Clear();
            txtdienthoaiKH.DataBindings.Clear();
            lblKH.DataBindings.Clear();
            txttenKH.DataBindings.Add("Text", pDT, "TENKHACHHANG");
            txtdiachiKH.DataBindings.Add("Text", pDT, "DIACHI");
            txtdienthoaiKH.DataBindings.Add("Text", pDT, "DIENTHOAI");
            lblKH.DataBindings.Add("Text", pDT, "MAKHACHHANG");
        }
        public void load_tabKH(string MS)
        {
            string M = "select * from KHACHHANG where MAKHACHHANG = '" + MS + "'";
            da_KH = new SqlDataAdapter(M, _Connsql);
            ds_KH.Clear();
            da_KH.Fill(ds_KH, "TTKH");
            dgvKH.DataSource = ds_KH.Tables["TTKH"];
            DatabingdinG(ds_KH.Tables[0]);
        }

        private void cboKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKH.SelectedIndex == 0)
            {
                load_tabKH();
            }
            else
            {
                load_tabKH(cboKH.SelectedItem.ToString());
            }
        }

        public void Databingdin(DataTable pDT)
        {
            //pDT.Columns[2].DefaultCellStyle.Format = "yyyy/MM/dd";

            txtDTNV.DataBindings.Clear();
            txtDCNV.DataBindings.Clear();
            txtCCCD.DataBindings.Clear();
            txtGTNV.DataBindings.Clear();
            txtHTNV.DataBindings.Clear();
            lblMNV.DataBindings.Clear();
            mskNSNV.DataBindings.Clear();
            //txtSoLuong.DataBindings.Clear();
            txtDTNV.DataBindings.Add("Text", pDT, "DIENTHOAI");
            txtDCNV.DataBindings.Add("Text", pDT, "DIACHI");
            txtCCCD.DataBindings.Add("Text", pDT, "CCCD");
            txtGTNV.DataBindings.Add("Text", pDT, "GIOITINH");
            txtHTNV.DataBindings.Add("Text", pDT, "HOTEN");
            lblMNV.DataBindings.Add("Text", pDT, "MANHANVIEN");
            mskNSNV.DataBindings.Add("Text", pDT,"NGAYSINH");
            //txtSoLuong.DataBindings.Add("Text", pDT, "SOLUONGCON"); 
        }
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMain.SelectedIndex == 0)
            {
                ds_TTBan.Clear();
                loaddgvTTBan();
            }
            if (tabMain.SelectedIndex == 1)
            {
                loadtabNV();
            }
            if(tabMain.SelectedIndex == 4)
            {
                loadKS();
            }
          
            if (tabMain.SelectedIndex == 2)
            {
                load_tabKH();
            }
            if (tabMain.SelectedIndex == 3)
            {
                load_tabDT();
            }
            if (tabMain.SelectedIndex == 5)
            {
                loaddgvTC();
            }
        }
        public void DatabingdinGs(DataTable pDT)
        {
            txttenDT.DataBindings.Clear();
            txtdcDT.DataBindings.Clear();
            txtdtDT.DataBindings.Clear();
            lblDT.DataBindings.Clear();
            txttenDT.DataBindings.Add("Text", pDT, "TENDOITAC");
            txtdcDT.DataBindings.Add("Text", pDT, "DIACHI");
            txtdtDT.DataBindings.Add("Text", pDT, "DIENTHOAI");
            lblDT.DataBindings.Add("Text", pDT, "MADOITAC");
        }
        SqlDataAdapter da_DT;

        private void btnthemKH_Click(object sender, EventArgs e)
        {
            foreach (string s in cboKH.Items)
            {
                if (s == cboKH.Text)
                {
                    MessageBox.Show("Mã khách hàng đã tồn tại!");
                    return;
                }
            }
            try
            {
                connsql.Open();
                string i = "insert into KHACHHANG values('" +
                            cboKH.Text + "',N'" + txttenKH.Text + "',N'" + txtdiachiKH.Text + "',N'" + txtdienthoaiKH.Text + "')";
                SqlCommand cmd = new SqlCommand(i, connsql);
                cmd.ExecuteNonQuery();
                cboKH.Items.Add(cboKH.Text);
                connsql.Close();
                MessageBox.Show(" Thêm khách hàng thành công!");
            }
            catch (Exception ex)
            {
                connsql.Close();
                MessageBox.Show("Thêm khách hàng thất bại" + ex.Message);
            }
            load_tabKH();
        }
        public bool ktramakh(string makh)
        {
            try
            {
                connsql.Open();
                string s = "Select count (*) from HOADON where MAKHACHHANG='" + makh + "'";
                SqlCommand c = new SqlCommand(s, connsql);
                int t = (int)c.ExecuteScalar();
                connsql.Close();
                //MessageBox.Show("Có thể xóa");
                return t > 0;
            }
            catch (Exception ec)
            {
                connsql.Close();
              //  MessageBox.Show(ec.Message);
                return true;
            }
        }
        private void btxoaKH_Click(object sender, EventArgs e)
        {
            foreach (string ff in cboKH.Items)
            {
                if (ff == lblKH.Text && ff != cboKH.Items[0])
                {
                    if (ktramakh(ff))
                    {
                        MessageBox.Show("Không thể xóa khách hàng đã mua hóa đơn");
                        return;
                    }
                    try
                    {
                        DialogResult r = MessageBox.Show("Bạn có thực sự muốn xóa khách hàng?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
               MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.No)
                            return;
                        else
                        {
                            connsql.Open();
                            string insertString = "delete KHACHHANG where MAKHACHHANG = '" + ff + "'";
                            SqlCommand cmd = new SqlCommand(insertString, connsql);
                            cmd.ExecuteNonQuery();
                            //cboMNV.Items.Add(cboMNV.Text);
                            connsql.Close();
                            cboKH.Items.Remove(f);
                            cboKH.SelectedIndex = 0;
                            load_tabKH();

                            MessageBox.Show("Xóa khách hàng thành công!");

                        }
                    }
                    catch (Exception ex)
                    {
                        connsql.Close();
                    //    MessageBox.Show("Đây " + ex.Message);
                    }
                    return;
                }
            }
        }
        public void load_dgvKH()
        {
            string strsel = "select * from KHACHHANG where TENKHACHHANG like N'%" + txttenKH.Text + "%'";
            da_KH = new SqlDataAdapter(strsel, _Connsql);
            ds_KH.Clear();
            da_KH.Fill(ds_KH, "TTKH");
            dgvKH.DataSource = ds_KH.Tables["TTKH"];
            DatabingdinG(ds_KH.Tables[0]);
        }
        private void btnsuaKH_Click(object sender, EventArgs e)
        {
            foreach (string f in cboKH.Items)
            {
                if (f == lblKH.Text && f != cboKH.Items[0])
                {
                    try
                    {
                        connsql.Open();
                        string insertString = "update KHACHHANG set TENKHACHHANG =N'" + txttenKH.Text + "',DIACHI  = N'" + txtdiachiKH.Text +
                            "', DIENTHOAI = N'" + txtdienthoaiKH.Text + "' where MAKHACHHANG = '" + lblKH.Text + "'";
                        SqlCommand cmd = new SqlCommand(insertString, connsql);
                        cmd.ExecuteNonQuery();
                        connsql.Close();
                        MessageBox.Show("Sửa thông tin thành công!");
                    }
                    catch (Exception ex)
                    {
                        connsql.Close();
                    //    MessageBox.Show("Đây " + ex.Message);
                    }
                    load_dgvKH();

                    return;
                }
            }
        }

        private void btntimkiemKH_Click(object sender, EventArgs e)
        {
            load_dgvKH();
        }

        private void btnthemDT_Click(object sender, EventArgs e)
        {
            foreach (string s in cboDT.Items)
            {
                if (s == cboDT.Text)
                {
                    MessageBox.Show("Mã đối tác đã tồn tại!");
                    return;
                }
            }
            try
            {
                connsql.Open();
                string i = "insert into DOITAC values('" +
                            cboDT.Text + "',N'" + txttenDT.Text + "',N'" + txtdcDT.Text + "',N'" + txtdtDT.Text + "')";
                SqlCommand cmd = new SqlCommand(i, connsql);
                cmd.ExecuteNonQuery();
                cboDT.Items.Add(cboDT.Text);
                connsql.Close();
                MessageBox.Show(" Thêm đối tác thành công!");
            }
            catch (Exception ex)
            {
                connsql.Close();
               // MessageBox.Show("Thêm thất bại" + ex.Message);
            }
            load_tabDT();
        }
        public bool ktramaDT(string madt)
        {
            try
            {
                connsql.Open();
                string s = "Select count (*) from PHIEUNHAP where MADOITAC='" + madt + "'";
                SqlCommand c = new SqlCommand(s, connsql);
                int t = (int)c.ExecuteScalar();
                connsql.Close();
             //   MessageBox.Show("Có thể xóa");
                return t > 0;
            }
            catch (Exception ec)
            {
                connsql.Close();
               // MessageBox.Show(ec.Message);
                return true;
            }
        }
        private void btnxoaDT_Click(object sender, EventArgs e)
        {
            foreach (string ff in cboDT.Items)
            {
                if (ff == lblDT.Text && ff != cboDT.Items[0])
                {
                    if (ktramaDT(ff))
                    {
                        MessageBox.Show("Không được xóa mã đối tác vì có trong phiếu nhập");
                        return;
                    }
                    try
                    {
                        DialogResult r = MessageBox.Show("Bạn có thực sự muốn xóa đối tác ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button1);
                        if (r == DialogResult.No)
                            return;
                        else
                        {
                            connsql.Open();
                            string insertString = "delete DOITAC where MADOITAC = '" + ff + "'";
                            SqlCommand cmd = new SqlCommand(insertString, connsql);
                            cmd.ExecuteNonQuery();
                            connsql.Close();
                            cboDT.Items.Remove(ff);
                            cboDT.SelectedIndex = 0;
                            load_tabDT();
                            MessageBox.Show("Xóa đối tác thành công!");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        connsql.Close();
                      //  MessageBox.Show("Đây " + ex.Message);
                    }
                    return;
                }
            }
        }

        private void btnsuaDT_Click(object sender, EventArgs e)
        {
            foreach (string f in cboDT.Items)
            {
                if (f == lblDT.Text && f != cboDT.Items[0])
                {
                    try
                    {
                        connsql.Open();
                        string insertString = "update DOITAC set TENDOITAC =N'" + txttenDT.Text + "',DIACHI  = N'" + txtdcDT.Text +
                            "', DIENTHOAI = N'" + txtdtDT.Text + "' where MADOITAC = '" + lblDT.Text + "'";
                        SqlCommand cmd = new SqlCommand(insertString, connsql);
                        cmd.ExecuteNonQuery();
                        connsql.Close();
                        MessageBox.Show("Sửa đối tác thành công!");
                    }
                    catch (Exception ex)
                    {
                        connsql.Close();
                       // MessageBox.Show("Đây " + ex.Message);
                    }
                    load_dgvDT();

                    return;
                }
            }
        }

        private void btntimkiemDT_Click(object sender, EventArgs e)
        {
            load_dgvDT();
        }
        public void load_dgvDT()
        {
            string strsel = "select * from DOITAC where TENDOITAC like N'%" + txttenDT.Text + "%'";
            da_DT = new SqlDataAdapter(strsel, _Connsql);
            ds_DT.Clear();
            da_DT.Fill(ds_DT, "TTDT");
            dgvDT.DataSource = ds_DT.Tables["TTDT"];
            DatabingdinGs(ds_DT.Tables[0]);
        }
        DataSet ds_DT = new DataSet();
        public void load_tabDT(string MS)
        {
            try
            {
                string M = "select * from DOITAC where MADOITAC = '" + MS + "'";
                da_DT = new SqlDataAdapter(M, _Connsql);
                ds_DT.Clear();
                da_DT.Fill(ds_DT, "TTDT");
                dgvDT.DataSource = ds_DT.Tables["TTDT"];
                DatabingdinGs(ds_DT.Tables[0]);
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }
        private void cboDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDT.SelectedIndex == 0)
            {
                load_tabDT();
            }
            else
            {
                load_tabDT(cboDT.SelectedItem.ToString());
            }
        }

        private void btnXuatTC_Click(object sender, EventArgs e)
        {

        }

        public void load_tabDT()
        {
            string strsel = "select * from DOITAC ";
            da_DT = new SqlDataAdapter(strsel, _Connsql);
            ds_DT.Clear();
            da_DT.Fill(ds_DT, "TTDT");
            dgvDT.DataSource = ds_DT.Tables["TTDT"];
            DatabingdinGs(ds_DT.Tables[0]);
        }
        public void load_dt()
        {
            connsql.Open();
            string selectString = "select MADOITAC from DOITAC";
            SqlCommand cmd = new SqlCommand(selectString, connsql);
            SqlDataReader rd = cmd.ExecuteReader();
            cboDT.Items.Clear();
            cboDT.Items.Add("Tất cả");
            while (rd.Read())
            {
                cboDT.Items.Add(rd["MADOITAC"].ToString());
            }
            rd.Close();
            connsql.Close();
        }
    }
}
