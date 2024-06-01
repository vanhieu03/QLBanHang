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
    public partial class NhapHangNhaCungCap : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public NhapHangNhaCungCap()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM NhapHangNhaCungCap WHERE MaHoaDon = '"+ txt_mahoadon.Text+ "'";
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            tb_nh.DataSource = dt;
            
        }

        private void NhapHangNhaCungCap_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
            Random rd = new Random();
            txt_manh.Text = rd.Next(10000, 1000000).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO ThongTinSanPham(MaSanPham) VALUES (@MaSanPham)", cn);
            cmd1.Parameters.AddWithValue("@MaSanPham", txt_masp.Text);

            cmd1.ExecuteNonQuery();
            SqlCommand checkHoaDon = new SqlCommand("SELECT COUNT(*) FROM HoaDonNhaCungCap WHERE MaHoaDon = @mahd", cn);
            checkHoaDon.Parameters.AddWithValue("@mahd", txt_mahoadon.Text);
            int exists = (int)checkHoaDon.ExecuteScalar();
            if (exists == 0)
            {
                // Nếu mã hóa đơn chưa tồn tại, thêm vào cơ sở dữ liệu
                SqlCommand cmd3 = new SqlCommand("INSERT INTO HoaDonNhaCungCap (MaHoaDon) VALUES (@mahd)", cn);
                cmd3.Parameters.AddWithValue("@mahd", txt_mahoadon.Text);
                cmd3.ExecuteNonQuery();
            }
            // Thêm vào trong bảng DatHangNhaCungCap
            SqlCommand cmd = new SqlCommand("INSERT INTO NhapHangNhaCungCap (MaNhapHangNhaCungCap,MaNhaCungCap,MaHoaDon,NgayNhapHang,MaSanPham,TenSanPham,MauSac,KichThuoc,SoLuong,DonGia,PhuongThucTT,TongTien,GiaBan) VALUES (@MaNhapHangNhaCungCap,@MaNhaCungCap,@MaHoaDon,@NgayNhapHang,@MaSanPham,@TenSanPham,@MauSac,@KichThuoc,@SoLuong,@DonGia,@pttt,@tongtien,@GiaBan)", cn);
            cmd.Parameters.AddWithValue("@MaNhapHangNhaCungCap", txt_manh.Text);
            cmd.Parameters.AddWithValue("@MaNhaCungCap", txt_mancc.Text);
            cmd.Parameters.AddWithValue("@MaHoaDon", txt_mahoadon.Text);
            cmd.Parameters.AddWithValue("@NgayNhapHang", dateTimePicker1.Value.ToString("yyyy-MM-dd"));

            cmd.Parameters.AddWithValue("@MaSanPham", txt_masp.Text);
            cmd.Parameters.AddWithValue("@TenSanPham", txt_tensp.Text);
            cmd.Parameters.AddWithValue("@MauSac", txt_mausac.Text);
            cmd.Parameters.AddWithValue("@KichThuoc", txt_kt.Text);
            cmd.Parameters.AddWithValue("@SoLuong", txt_sl.Text);
            cmd.Parameters.AddWithValue("@DonGia", txt_gianhap.Text);
            cmd.Parameters.AddWithValue("@pttt", txt_pttt.Text);
            float tongtien = (int.Parse(txt_sl.Text) * float.Parse(txt_gianhap.Text));
            cmd.Parameters.AddWithValue("@tongtien", tongtien);
            cmd.Parameters.AddWithValue("@GiaBan", txt_giaban.Text);

            cmd.ExecuteNonQuery();
            loaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            txt_manh.Text = rd.Next(10000, 1000000).ToString();
            dateTimePicker1.Text = "";
            txt_masp.Text = "";
            txt_tensp.Text = "";
            txt_mausac.Text = "";
            txt_kt.Text = "";
            txt_sl.Text = "";
            txt_gianhap.Text = "";
            txt_giaban.Text = "";
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE NhapHangNhaCungCap SET MaNhapHangNhaCungCap=@MaNhapHangNhaCungCap,MaNhaCungCap=@MaNhaCungCap,MaHoaDon=@MaHoaDon,NgayNhapHang=@NgayNhapHang,MaSanPham=@MaSanPham,TenSanPham=@TenSanPham,MauSac=@MauSac,KichThuoc=@KichThuoc,SoLuong=@SoLuong,DonGia = @DonGia,PhuongThucTT = @pttt,GiaBan=@GiaBan WHERE MaNhapHangNhaCungCap=@MaNhapHangNhaCungCap", cn);

            cmd.Parameters.AddWithValue("@MaNhapHangNhaCungCap", txt_manh.Text);
            cmd.Parameters.AddWithValue("@MaNhaCungCap", txt_mancc.Text);
            cmd.Parameters.AddWithValue("@MaHoaDon", txt_mahoadon.Text);
            cmd.Parameters.AddWithValue("@NgayNhapHang", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@MaSanPham", txt_masp.Text);
            cmd.Parameters.AddWithValue("@TenSanPham", txt_tensp.Text);
            cmd.Parameters.AddWithValue("@MauSac", txt_mausac.Text);
            cmd.Parameters.AddWithValue("@KichThuoc", txt_kt.Text);
            cmd.Parameters.AddWithValue("@SoLuong", txt_sl.Text);
            cmd.Parameters.AddWithValue("@DonGia", txt_gianhap.Text);
            cmd.Parameters.AddWithValue("@pttt", txt_pttt.Text);
            cmd.Parameters.AddWithValue("@GiaBan", txt_giaban.Text);


            cmd.ExecuteNonQuery();

            //
            SqlCommand cmd2;
            cmd2 = new SqlCommand("UPDATE ThongTinNhaCungCap SET MaNhaCungCap=@mancc WHERE MaNhaCungCap = @mancc", cn);
            cmd2.Parameters.AddWithValue("@mancc", txt_mancc.Text);
            cmd2.ExecuteNonQuery();
            //
            SqlCommand cmd3 = new SqlCommand("UPDATE HoaDonNhaCungCap SET MaHoaDon=@MaHoaDon WHERE MaHoaDon=@MaHoaDon", cn);
            cmd3.Parameters.AddWithValue("@MaHoaDon", txt_mahoadon.Text);
            cmd3.ExecuteNonQuery();
            //
            SqlCommand cmd1 = new SqlCommand("UPDATE ThongTinSanPham SET TenSanPham=@TenSanPham, KichThuoc=@KichThuoc, MauSac=@MauSac,SoLuong= @SoLuong,DonGia= @DonGia WHERE MaSanPham=@MaSanPham", cn);
            cmd1.Parameters.AddWithValue("@MaSanPham", txt_masp.Text);
            cmd1.Parameters.AddWithValue("@TenSanPham", txt_tensp.Text);
            cmd1.Parameters.AddWithValue("@MauSac", txt_mausac.Text);
            cmd1.Parameters.AddWithValue("@KichThuoc", txt_kt.Text);
            cmd1.Parameters.AddWithValue("@SoLuong", txt_sl.Text);
            cmd1.Parameters.AddWithValue("@DonGia", txt_gianhap.Text);
            cmd1.ExecuteNonQuery();
            loaddata();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("DELETE FROM NhapHangNhaCungCap WHERE MaNhapHangNhaCungCap = '" + txt_manh.Text + "'", cn);
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("DELETE FROM ThongTinSanPham WHERE MaSanPham= '" + txt_masp.Text + "'", cn);
            cmd1.ExecuteNonQuery();


            loaddata();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_nh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = tb_nh.CurrentRow.Index;

            txt_manh.Text = tb_nh.Rows[i].Cells[0].Value.ToString();
            dateTimePicker1.Text = tb_nh.Rows[i].Cells[1].Value.ToString();
            txt_mancc.Text = tb_nh.Rows[i].Cells[2].Value.ToString();
            txt_mahoadon.Text = tb_nh.Rows[i].Cells[3].Value.ToString();

            txt_masp.Text = tb_nh.Rows[i].Cells[4].Value.ToString();
            txt_tensp.Text = tb_nh.Rows[i].Cells[5].Value.ToString();
            txt_mausac.Text = tb_nh.Rows[i].Cells[6].Value.ToString();

            txt_kt.Text = tb_nh.Rows[i].Cells[7].Value.ToString();

            txt_sl.Text = tb_nh.Rows[i].Cells[8].Value.ToString();
            txt_gianhap.Text = tb_nh.Rows[i].Cells[9].Value.ToString();
            txt_giaban.Text = tb_nh.Rows[i].Cells[12].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("UPDATE ThongTinSanPham SET TenSanPham = @TenSanPham, KichThuoc=@KichThuoc, MauSac=@MauSac, SoLuong =@SoLuong, DonGia=@DonGia WHERE MaSanPham = @MaSanPham", cn);
            cmd1.Parameters.AddWithValue("@MaSanPham", txt_masp.Text);
            cmd1.Parameters.AddWithValue("@TenSanPham", txt_tensp.Text);
            cmd1.Parameters.AddWithValue("@MauSac", txt_mausac.Text);
            cmd1.Parameters.AddWithValue("@KichThuoc", txt_kt.Text);
            cmd1.Parameters.AddWithValue("@SoLuong", txt_sl.Text);
            cmd1.Parameters.AddWithValue("@DonGia", txt_gianhap.Text);
            cmd1.ExecuteNonQuery();
            //Thêm vào bảng hóa đơn NCC
            SqlCommand cmd3 = new SqlCommand("UPDATE HoaDonNhaCungCap SET MaNhaCungCap = @manhacc, NgayDatHang=@ngaydathang,NgayGiao=@ngaygiao, PhuongThucTT=@phuongthuctt WHERE MaHoaDon =@mahd", cn);
            cmd3.Parameters.AddWithValue("@mahd", txt_mahoadon.Text);
            cmd3.Parameters.AddWithValue("@manhacc", txt_mancc.Text);
            cmd3.Parameters.AddWithValue("@ngaydathang", dateTimePicker2.Text);
            cmd3.Parameters.AddWithValue("@ngaygiao", dateTimePicker1.Text);
            cmd3.Parameters.AddWithValue("@phuongthuctt", txt_mahoadon.Text);
            cmd3.ExecuteNonQuery();
        }
    }
}
