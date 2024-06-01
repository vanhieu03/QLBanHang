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
    public partial class NhapThongTinNhaCungCap : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public NhapThongTinNhaCungCap()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM ThongTinNhaCungCap";
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            tb_ncc.DataSource = dt;

        }
        private void NhapThongTinNhaCungCap_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd1 = new SqlCommand("INSERT INTO ThongTinLienHe VALUES (@TTlienhe, @DanhTinh, @TenKH, @SDT, @DiaChi)", cn);
            cmd1.Parameters.AddWithValue("@TTlienhe", txt_ttlh.Text);
            cmd1.Parameters.AddWithValue("@DanhTinh", txt_dt.Text);
            cmd1.Parameters.AddWithValue("@TenKH", txt_ten.Text);
            cmd1.Parameters.AddWithValue("@SDT", txt_sdt.Text);
            cmd1.Parameters.AddWithValue("@DiaChi", txt_dc.Text);
            cmd1.ExecuteNonQuery();

            SqlCommand cmd = new SqlCommand("INSERT INTO ThongTinNhaCungCap VALUES (@MaNCC, @TenNCC, @DiaChi, @SDT,  @Email,@TTlienhe, @DanhTinh)", cn);
            cmd.Parameters.AddWithValue("@MaNCC", txt_ma.Text);
            cmd.Parameters.AddWithValue("@TenNCC", txt_ten.Text);
            cmd.Parameters.AddWithValue("@DiaChi", txt_dc.Text);
            cmd.Parameters.AddWithValue("@SDT", txt_sdt.Text);

            cmd.Parameters.AddWithValue("@Email", txt_email.Text);
            cmd.Parameters.AddWithValue("@TTlienhe", txt_ttlh.Text);
            cmd.Parameters.AddWithValue("@DanhTinh", txt_dt.Text);
            cmd.ExecuteNonQuery();
            loaddata();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("UPDATE ThongTinNhaCungCap SET TenNhaCungCap=@ten, DiaChi=@diachi, SDT=@sdt, Email=@email,MaThongTinLienHeNhaCungCap=@ttlienhe, DanhTinh=@danhtinh WHERE MaNhaCungCap = @mancc", cn);
            cmd.Parameters.AddWithValue("@mancc", txt_ma.Text);
            cmd.Parameters.AddWithValue("@ten", txt_ten.Text);
            cmd.Parameters.AddWithValue("@diachi", txt_dc.Text);
            cmd.Parameters.AddWithValue("@sdt", txt_sdt.Text);
            cmd.Parameters.AddWithValue("@email", txt_email.Text);
            cmd.Parameters.AddWithValue("@ttlienhe", txt_ttlh.Text);

            cmd.Parameters.AddWithValue("@danhtinh", txt_dt.Text);
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("UPDATE ThongTinLienHe SET DanhTinh=@danhtinh, Ten=@ten, SDT=@sdt, Diachi=@diachi WHERE MaThongTinLienHe =@malienhe", cn);
            cmd1.Parameters.AddWithValue("@malienhe", txt_email.Text);
            cmd1.Parameters.AddWithValue("@danhtinh", txt_dt.Text);
            cmd1.Parameters.AddWithValue("@ten", txt_ten.Text);
            cmd1.Parameters.AddWithValue("@sdt", txt_sdt.Text);
            cmd1.Parameters.AddWithValue("@diachi", txt_dc.Text);
            cmd1.ExecuteNonQuery();
            loaddata();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("DELETE FROM ThongTinNhaCungCap WHERE MaNhaCungCap = '" + txt_ma.Text + "'", cn);
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("DELETE FROM ThongTinLienHe WHERE MaThongTinLienHe = '" + txt_ttlh.Text + "'", cn);
            cmd1.ExecuteNonQuery();
            loaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txt_ma.ReadOnly = false;
            txt_ttlh.ReadOnly = false;
            txt_ma.Text = "";
            txt_ten.Text = "";
            txt_dc.Text = " ";
            txt_sdt.Text = "";

            txt_ttlh.Text = "";
            txt_email.Text = "";
            txt_dt.Text = " ";
            txttimkiem.Text = "";

            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void timkiem(String keywords, String searchText)
        {
            cmd = new SqlCommand($"SELECT* FROM ThongTinNhaCungCap WHERE {keywords} like @searchText", cn);
            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            tb_ncc.DataSource = dt;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            String text = txttimkiem.Text;
            String keywords = cbtimkiem.Text;
            switch (keywords)
            {
                case "Tên nhà cung cấp":
                    keywords = "TenNhaCungCap";
                    break;
                case "Địa chỉ":
                    keywords = "DiaChi";
                    break;
                case "Số điện thoại":
                    keywords = "SDT";
                    break;
                case "Email":
                    keywords = "Email";
                    break;
                case "Mã thông tin liên hệ":
                    keywords = "MaThongTinLienHe";
                    break;
                case "Mã thẻ thành viên":
                    keywords = "MaTheThanhVien";
                    break;
                case "Danh tính":
                    keywords = "DanhTinh";
                    break;

            }
            timkiem(keywords, text);
        }

        private void tb_ncc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_ma.ReadOnly = true;
            txt_ttlh.ReadOnly = true;
            int i;
            i = tb_ncc.CurrentRow.Index;
            txt_ma.Text = tb_ncc.Rows[i].Cells[0].Value.ToString();
            txt_ten.Text = tb_ncc.Rows[i].Cells[1].Value.ToString();
            txt_dc.Text = tb_ncc.Rows[i].Cells[2].Value.ToString();
            txt_sdt.Text = tb_ncc.Rows[i].Cells[3].Value.ToString();
            txt_email.Text = tb_ncc.Rows[i].Cells[4].Value.ToString();
            txt_ttlh.Text = tb_ncc.Rows[i].Cells[5].Value.ToString();
            txt_dt.Text = tb_ncc.Rows[i].Cells[6].Value.ToString();
        }
    }
}
