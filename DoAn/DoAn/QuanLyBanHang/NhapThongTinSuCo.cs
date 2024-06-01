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

namespace DoAn.QuanLyBanHang
{
    public partial class NhapThongTinSuCo : Form
    {
        SqlConnection cn;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public NhapThongTinSuCo()
        {
            InitializeComponent();
        }

        public void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = @"SELECT 
            MaSuCo AS [Mã Sự Cố],
            MaHoaDon AS [Mã Hóa Đơn],
            TenSuCo AS [Tên Sự Cố],
            ThoiGian AS [Thời Gian],
            NhanVienTiepNhan AS [Nhân Viên Tiếp Nhận],
            TrangThai AS [Trạng Thái]
            FROM ThongTinSuCo";
            dta.SelectCommand=cmd;
            dt.Clear();
            dta.Fill(dt);
            dgv.DataSource = dt;
        }
        private void NhapThongTinSuCo_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            if(cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            Random rd = new Random();
            //tạo số ngẫu nhiên từ 0 đến 99999
            int randomNumber1 = rd.Next(100000);
            txtmasc.Text = randomNumber1.ToString();
            txtthoigian.Text = DateTime.Now.ToString("dd/MM/yyyy");
            loaddata();
        }

        private void btthem_Click(object sender, EventArgs e)
        {
                cmd = new SqlCommand("INSERT INTO ThongTinSuCo VALUES (@masc, @mahd, @tensc, @thoigian, @nhanvien, @trangthai)", cn);
                cmd.Parameters.AddWithValue("@masc", txtmasc.Text);
                cmd.Parameters.AddWithValue("@mahd", txtmahd.Text);
                cmd.Parameters.AddWithValue("@tensc", txttensc.Text);
                DateTime thoigian = DateTime.ParseExact(txtthoigian.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("@thoigian", thoigian);
                cmd.Parameters.AddWithValue("@nhanvien", txtnhanvien.Text);
                cmd.Parameters.AddWithValue("@trangthai", cbtrangthai.Text);
                cmd.ExecuteNonQuery();
                loaddata();
            
        }

        private void btsua_Click(object sender, EventArgs e)
        {
                cmd = new SqlCommand("UPDATE ThongTinSuCo SET MaHoaDon=@mahd,TenSuCo=@tensc,ThoiGian=@thoigian,NhanVienTiepNhan=@nhanvien,TrangThai=@trangthai WHERE MaSuCo = @masc", cn);
                cmd.Parameters.AddWithValue("@masc", txtmasc.Text);
                cmd.Parameters.AddWithValue("@mahd", txtmahd.Text);
                cmd.Parameters.AddWithValue("@tensc", txttensc.Text);
                DateTime thoigian = DateTime.ParseExact(txtthoigian.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("@thoigian", thoigian);
                cmd.Parameters.AddWithValue("@nhanvien", txtnhanvien.Text);
                cmd.Parameters.AddWithValue("@trangthai", cbtrangthai.Text);
                cmd.ExecuteNonQuery();
                loaddata();
            
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem đã chọn một hàng hợp lệ
            {
                    txtmasc.ReadOnly = true;
                    DataGridViewRow row = dgv.Rows[e.RowIndex];
                    txtmasc.Text = row.Cells[0].Value.ToString();
                    txtmahd.Text = row.Cells[1].Value.ToString();
                    txttensc.Text = row.Cells[2].Value.ToString();
                    DateTime thoigian = Convert.ToDateTime(row.Cells[3].Value);
                    txtthoigian.Text = thoigian.ToString("dd/MM/yyyy");
                    txtnhanvien.Text = row.Cells[4].Value.ToString();
                    cbtrangthai.Text = row.Cells[5].Value.ToString();
                
            }
        }

        private void btxoa_Click(object sender, EventArgs e)
        {
                cmd = new SqlCommand("DELETE FROM ThongTinSuCo WHERE MaSuCo = '" + txtmasc.Text + "'", cn);
                cmd.ExecuteNonQuery();
                loaddata();
            
        }

        private void btdong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btlammoi_Click(object sender, EventArgs e)
        {
                Random rd = new Random();
                int randomNumber1 = rd.Next(100000);
                txtmasc.Text = randomNumber1.ToString();
                txtmahd.Text = "";
                txttensc.Text = "";
                txtnhanvien.Text = "";
                cbtrangthai.Text = "";
                txtthoigian.Text = DateTime.Now.ToString("dd/MM/yyyy");
                loaddata();
            
        }
        public void timkiem(String keywords, String searchText)
        {
            cmd = new SqlCommand(@"SELECT  
            MaSuCo AS [Mã Sự Cố],
            MaHoaDon AS [Mã Hóa Đơn],
            TenSuCo AS [Tên Sự Cố],
            ThoiGian AS [Thời Gian],
            NhanVienTiepNhan AS [Nhân Viên Tiếp Nhận],
            TrangThai AS [Trạng Thái]
            FROM ThongTinSuCo WHERE " + keywords+" like @searchText", cn);
            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            dgv.DataSource = dt;
        }
        private void bttimkiem_Click(object sender, EventArgs e)
        {
            String text = txttukhoa.Text;
            String keywords = cbtimtheo.Text;
            switch (keywords)
            {
                case "Tên sự cố":
                    keywords = "TenSuCo";
                    break;
                case "Thời gian":
                    keywords = "ThoiGian";
                    break;
                case "Nhân viên tiếp nhận":
                    keywords = "NhanVienTiepNhan";
                    break;
                case "Trạng thái":
                    keywords = "TrangThai";
                    break;

            }
            timkiem(keywords, text);
        }

        private void txttukhoa_TextChanged(object sender, EventArgs e)
        {
            String text = txttukhoa.Text;
            String keywords = cbtimtheo.Text;
            switch (keywords)
            {
                case "Tên sự cố":
                    keywords = "TenSuCo";
                    break;
                case "Thời gian":
                    keywords = "ThoiGian";
                    break;
                case "Nhân viên tiếp nhận":
                    keywords = "NhanVienTiepNhan";
                    break;
                case "Trạng thái":
                    keywords = "TrangThai";
                    break;

            }
            timkiem(keywords, text);
        }
    }
}
