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

namespace DoAn.QuanLyBanHang
{
    public partial class TimHoaDon : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public TimHoaDon()
        {
            InitializeComponent();
        }
        public void loaddata(String query)
        {
            using (SqlConnection cn = new SqlConnection(chuoiketnoi))
            {
                cn.Open();
                cmd = cn.CreateCommand();
                cmd.CommandText = query;
                dta.SelectCommand = cmd;
                dt.Clear();
                dta.Fill(dt);
                dgvhoadon.DataSource = dt;
            }

        }
        private void TimHoaDon_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection (chuoiketnoi);
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            loaddata("SELECT HD.MaHoaDon,HD.MaKhachHang,HD.TenKhachHang,HD.ThoiGian, SP.MaSanPhamBan, SP.MaSanPham, SP.TenSanPham, SP.KichThuoc, SP.MauSac, SP.SoLuong, SP.DonGia, SP.KhuyenMai,SP.TongTien FROM ThongTinHoaDon AS HD, DanhSachSanPhamBan AS SP");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void timkiem(String searchText)
        {
            
            cmd = new SqlCommand(@"SELECT HD.MaHoaDon,HD.MaKhachHang,HD.TenKhachHang,HD.ThoiGian, SP.MaSanPhamBan, SP.MaSanPham, SP.TenSanPham, SP.KichThuoc, SP.MauSac, SP.SoLuong, SP.DonGia, SP.KhuyenMai,SP.TongTien FROM ThongTinHoaDon AS HD, DanhSachSanPhamBan AS SP WHERE HD.MaHoaDon like @searchText ", cn);
            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
            cmd.ExecuteNonQuery();
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            dgvhoadon.DataSource = dt;
        }
        private void btntimkiem_Click(object sender, EventArgs e)
        {
            timkiem(txttukhoa.Text);
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("DELETE FROM ThongTinHoaDon WHERE MaHoaDon = '" + txttukhoa.Text + "'", cn);
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("DELETE FROM DanhSachSanPhamBan WHERE MaHoaDon = '" + txttukhoa.Text + "'", cn);
            cmd1.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("DELETE FROM ThongTinSuCo WHERE MaHoaDon = '" + txttukhoa.Text + "'", cn);
            cmd2.ExecuteNonQuery();
            txttukhoa.ReadOnly = false;
            loaddata("SELECT HD.MaHoaDon,HD.MaKhachHang,HD.TenKhachHang,HD.ThoiGian, SP.MaSanPhamBan, SP.MaSanPham, SP.TenSanPham, SP.KichThuoc, SP.MauSac, SP.SoLuong, SP.DonGia, SP.KhuyenMai,SP.TongTien FROM ThongTinHoaDon AS HD");

        }

        private void dgvhoadon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txttukhoa.TextChanged -= txttukhoa_TextChanged;
            int i;
            i = dgvhoadon.CurrentRow.Index;
            txttukhoa.Text = dgvhoadon.Rows[i].Cells[0].Value.ToString();
            cmd = new SqlCommand("SELECT ThanhToan FROM ThongTinHoaDon WHERE MaHoaDon like  @mahd", cn);
            cmd.Parameters.AddWithValue("@mahd", txttukhoa.Text);
            object result = cmd.ExecuteScalar();
            txtthanhtoan.Text = result.ToString();
            txttukhoa.TextChanged += txttukhoa_TextChanged;
        }

        private void txttukhoa_TextChanged(object sender, EventArgs e)
        {
            timkiem(txttukhoa.Text);
        }
    }
}
