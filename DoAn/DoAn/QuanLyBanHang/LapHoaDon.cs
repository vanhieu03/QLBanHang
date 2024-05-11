using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DoAn.QuanLyBanHang
{
    public partial class LapHoaDon : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public LapHoaDon()
        {
            InitializeComponent();
        }
        public void loaddata(String query)
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = query;
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            dgvhoadon.DataSource = dt;
        }
        private void LapHoaDon_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            Random rd = new Random();
            int rd_mahd = rd.Next(1000,10000);
            txtmahoadon.Text = rd_mahd.ToString();
            txtthoigian.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void btthem_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("INSERT INTO ThongTinHoaDon VALUES(@mahd, @makh, @masp, @tensp, @soluong, @dongia, @tongtien, @thoigian, @khuyenmai )",cn);
            cmd.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
            cmd.Parameters.AddWithValue("@makh", txtmahoadon.Text);
            cmd.Parameters.AddWithValue("@masp", txtmasp.Text);
            cmd.Parameters.AddWithValue("@tensp", txttensp.Text);
            cmd.Parameters.AddWithValue("@soluong", int.Parse(txtsoluong.Text));
            cmd.Parameters.AddWithValue("@dongia", float.Parse(txtdg.Text));
            float tongtien = int.Parse(txtsoluong.Text) * float.Parse(txtdg.Text);
            cmd.Parameters.AddWithValue("@tongtien", tongtien);
            cmd.Parameters.AddWithValue("@thoigian", txtthoigian.Text);
            cmd.Parameters.AddWithValue("@khuyenmai", txtkm.Text);
            cmd.ExecuteNonQuery();
            loaddata("SELECT * FORM ThongTinHoaDon");
        }
    }
}
