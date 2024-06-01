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
    public partial class DatHangNhaCungCap : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public DatHangNhaCungCap()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM DatHangNhaCungCap";
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            tb_dh.DataSource = dt;

        }
        private int lastCode = 0;
        private void DatHangNhaCungCap_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            Random rd = new Random();
            int randomCode;
            do
            {
                randomCode = rd.Next(100000, 1000000); // Tạo mã từ 100000 đến 999999
            } while (randomCode == lastCode); // Đảm bảo mã mới không trùng với mã cuối cùng

            lastCode = randomCode; // Lưu trữ mã này để kiểm tra lần sau
            txt_madathang.Text = randomCode.ToString();
            loaddata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmdCheck1 = new SqlCommand("SELECT COUNT(*) FROM ThongTinNhaCungCap WHERE MaNhaCungCap = @MaNhaCungCap", cn);
            cmdCheck1.Parameters.AddWithValue("@MaNhaCungCap", txt_mancc.Text);

            int exists = (int)cmdCheck1.ExecuteScalar();
            if (exists > 0)
            {
                // Nhà cung cấp tồn tại, bạn có thể thực hiện các thao tác tiếp theo nếu cần
                Console.WriteLine("Mã nhà cung cấp đã tồn tại: " + txt_mancc.Text);
            }
            else
            {
                // Mã nhà cung cấp không tồn tại, thông báo hoặc xử lý tiếp
                Console.WriteLine("Mã nhà cung cấp không tồn tại, bạn có thể thêm vào: " + txt_mancc.Text);
            }

            // Thêm vào trong bảng DatHangNhaCungCap
            SqlCommand cmd = new SqlCommand("INSERT INTO DatHangNhaCungCap ( MaDatHangNhaCungCap, MaNhaCungCap,NgayDatHang, NgayGiaoDuKien, TenSanPham, KichThuoc, MauSac, SoLuong, DonGia) VALUES (@MaDathangNhaCungCap, @MaNhaCungCap, @NgayDatHang, @NgayGiaoDuKien, @TenSanPham,  @KichThuoc, @MauSac, @SoLuong, @DonGia)", cn);
            cmd.Parameters.AddWithValue("@MaDatHangNhaCungCap", txt_madathang.Text);
            cmd.Parameters.AddWithValue("@MaNhaCungCap", txt_mancc.Text);
            cmd.Parameters.AddWithValue("@NgayDatHang", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@NgayGiaoDuKien", dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@TenSanPham", txt_tensp.Text);

            cmd.Parameters.AddWithValue("@MauSac", txt_mausac.Text);
            cmd.Parameters.AddWithValue("@KichThuoc", txt_kt.Text);
            cmd.Parameters.AddWithValue("@SoLuong", txt_sl.Text);
            cmd.Parameters.AddWithValue("@DonGia", txt_dg.Text);
            cmd.ExecuteNonQuery();
            loaddata();
        }

        private void tb_dh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = tb_dh.CurrentRow.Index;

            txt_madathang.Text = tb_dh.Rows[i].Cells[0].Value.ToString();
            txt_mancc.Text = tb_dh.Rows[i].Cells[1].Value.ToString();
            dateTimePicker1.Text = tb_dh.Rows[i].Cells[2].Value.ToString();
            dateTimePicker2.Text = tb_dh.Rows[i].Cells[3].Value.ToString();
            txt_tensp.Text = tb_dh.Rows[i].Cells[4].Value.ToString();

            txt_kt.Text = tb_dh.Rows[i].Cells[5].Value.ToString();
            txt_mausac.Text = tb_dh.Rows[i].Cells[6].Value.ToString();
            txt_sl.Text = tb_dh.Rows[i].Cells[7].Value.ToString();
            txt_dg.Text = tb_dh.Rows[i].Cells[8].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Text = "";
            dateTimePicker2.Text = "";

            txt_tensp.Text = "";
            txt_madathang.Text = "";
            txt_mancc.Text = "";
            txt_mausac.Text = "";
            txt_kt.Text = "";
            txt_sl.Text = "";
            txt_dg.Text = "";
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE DatHangNhaCungCap SET MaDatHangNhaCungCap = @MaDatHangNhaCungCap, MaNhaCungCap = @MaNhaCungCap,NgayDatHang=@NgayDatHang, NgayGiaoDuKien=@NgayGiaoDuKien, TenSanPham=@TenSanPham,  KichThuoc=@KichThuoc, MauSac=@MauSac, SoLuong=@SoLuong, DonGia=@DonGia WHERE MaDatHangNhaCungCap=@MaDatHangNhaCungCap", cn);

            cmd.Parameters.AddWithValue("@NgayDatHang", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@NgayGiaoDuKien", dateTimePicker2.Value.ToString("yyyy-MM-dd"));


            cmd.Parameters.AddWithValue("@TenSanPham", txt_tensp.Text);
            cmd.Parameters.AddWithValue("@MaDatHangNhaCungCap", txt_madathang.Text);
            cmd.Parameters.AddWithValue("@MaNhaCungCap", txt_mancc.Text);
            cmd.Parameters.AddWithValue("@MauSac", txt_mausac.Text);
            cmd.Parameters.AddWithValue("@KichThuoc", txt_kt.Text);
            cmd.Parameters.AddWithValue("@SoLuong", txt_sl.Text);
            cmd.Parameters.AddWithValue("@DonGia", txt_dg.Text);

            cmd.ExecuteNonQuery();


            SqlCommand cmd2;
            cmd2 = new SqlCommand("UPDATE ThongTinNhaCungCap SET MaNhaCungCap=@mancc WHERE MaNhaCungCap = @mancc", cn);
            cmd2.Parameters.AddWithValue("@mancc", txt_mancc.Text);
            cmd2.ExecuteNonQuery();


            loaddata();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("DELETE FROM DatHangNhaCungCap WHERE MaDatHangNhaCungCap = '" + txt_madathang.Text + "'", cn);
            cmd.ExecuteNonQuery();


            loaddata();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
