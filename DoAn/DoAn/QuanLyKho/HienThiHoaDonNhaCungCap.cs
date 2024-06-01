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

namespace DoAn.QuanLyKho
{
    public partial class HienThiHoaDonNhaCungCap : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();

        public void loaddata(string v)
        {

            using (SqlConnection cn = new SqlConnection(chuoiketnoi))
            {
                cn.Open();
                cmd = cn.CreateCommand();
                cmd.CommandText = v;
                dta.SelectCommand = cmd;
                dt.Clear();
                dta.Fill(dt);
                tb_hd.DataSource = dt;
            }

        }
        public HienThiHoaDonNhaCungCap()
        {
            InitializeComponent();
        }

        private void HienThiHoaDonNhaCungCap_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            loaddata(@"SELECT HD.MaHoaDon, HD.MaNhaCungCap, HD.NgayDatHang, HD.NgayGiao, SP.MaNhapHangNhaCungCap, SP.MaSanPham, SP.TenSanPham, SP.MauSac, SP.KichThuoc, SP.SoLuong, SP.DonGia, SP.PhuongthucTT, SP.TongTien FROM HoaDonNhaCungCap AS HD INNER JOIN NhapHangNhaCungCap AS SP ON HD.MaHoaDon = SP.MaHoaDon");
        }
        public void timkiem(String searchText)
        {
            cmd = new SqlCommand(@"SELECT HD.*, SP.MaNhapHangNhaCungcap, SP.MaSanPham, SP.TenSanPham,  SP.MauSac,SP.KichThuoc, SP.SoLuong, SP.DonGia, SP.PhuongThucTT,SP.TongTien FROM HoaDonNhaCungCap AS HD, NhapHangNhaCungCap AS SP WHERE HD.MaHoaDon like @searchText AND SP.MaHoaDon like @searchText AND HD.MaHoaDon = SP.MaHoaDon", cn);
            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
            cmd.ExecuteNonQuery();
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            tb_hd.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timkiem(txt_tk.Text);
        }

        private void tb_hd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_tk.ReadOnly = true;
            int i;
            i = tb_hd.CurrentRow.Index;
            txt_tk.Text = tb_hd.Rows[i].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("DELETE FROM NhapHangNhaCungCap WHERE MaHoaDon = '" + txt_tk.Text + "'", cn);
            cmd1.ExecuteNonQuery();


            txt_tk.ReadOnly = false;
            loaddata("SELECT HD.MaHoaDon, HD.MaNhaCungCap, HD.NgayDatHang, HD.NgayGiao,SP.NhapHangNhaCungCap, SP.MaSanPham, SP.TenSanPham, SP.MauSac, SP.KichThuoc, SP.SoLuong, SP.DonGia, SP.PhuongthucTT, SP.TongTien FROM HoaDonNhaCungCap AS HD INNER JOIN NhapHnagNhaCungCap AS SP ON HD.MaHoaDon = SP.MaHoaDon");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
