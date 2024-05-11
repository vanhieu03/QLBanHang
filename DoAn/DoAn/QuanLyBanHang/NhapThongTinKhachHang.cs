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
using System.Collections;

namespace DoAn.QuanLyBanHang
{
    public partial class _1 : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM ThongTinKhachHang";
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            tb_kh.DataSource = dt;

        }
        public _1()
        {
            InitializeComponent();
        }
        private void _1_Load(object sender, EventArgs e)
        {
            cbtimkiem.Text = "Mã khách hàng";
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            //Tạo ra đối tượng random
            Random rd = new Random();
            //tạo số ngẫu nhiên từ 0 đến 99999
            int randomNumber1 = rd.Next(100000);
            int randomNumber2 = rd.Next(100000,1000000);
            txtttlienhe.Text = randomNumber1.ToString();
            txttthanhvien.Text = randomNumber2.ToString();
            loaddata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Thêm vào trong bảng ThongTinLienHe
            SqlCommand cmd1 = new SqlCommand("INSERT INTO ThongTinLienHe VALUES (@TTlienhe, @DanhTinh, @TenKH, @SDT, @DiaChi)", cn);
            cmd1.Parameters.AddWithValue("@TTlienhe", txtttlienhe.Text);
            cmd1.Parameters.AddWithValue("@DanhTinh", txtdanhtinh.Text);
            cmd1.Parameters.AddWithValue("@TenKH", txttenkh.Text);
            cmd1.Parameters.AddWithValue("@SDT", txtsdt.Text);
            cmd1.Parameters.AddWithValue("@DiaChi", txtdiachi.Text);
            cmd1.ExecuteNonQuery();

            // Thêm vào trong bảng ThongTinKhachHang
            SqlCommand cmd = new SqlCommand("INSERT INTO ThongTinKhachHang VALUES (@MaKH, @TenKH, @NgaySinh, @SDT, @DiaChi, @TTlienhe, @TTthanhvien, @DanhTinh)", cn);
            cmd.Parameters.AddWithValue("@MaKH", txtmakh.Text);
            cmd.Parameters.AddWithValue("@TenKH", txttenkh.Text);
            cmd.Parameters.AddWithValue("@NgaySinh", txt_ngaysinh.Text); 
            cmd.Parameters.AddWithValue("@SDT", txtsdt.Text);
            cmd.Parameters.AddWithValue("@DiaChi", txtdiachi.Text);
            cmd.Parameters.AddWithValue("@TTlienhe", txtttlienhe.Text);
            cmd.Parameters.AddWithValue("@TTthanhvien", txttthanhvien.Text);
            cmd.Parameters.AddWithValue("@DanhTinh",txtdanhtinh.Text);
            cmd.ExecuteNonQuery();
            loaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtmakh.Text = "";
            txttenkh.Text = "";
            txt_ngaysinh.Text = "1/1/1990";
            txtsdt.Text = "";
            txtdiachi.Text = "";
            txtdanhtinh.Text = "";
            txttimkiem.Text = "";
            Random rd = new Random();
            int randomNumber1 = rd.Next(100000);
            int randomNumber2 = rd.Next(100000, 1000000);
            txtttlienhe.Text = randomNumber1.ToString();
            txttthanhvien.Text = randomNumber2.ToString();
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("UPDATE ThongTinKhachHang SET TenKhachHang=@ten, NgaySinh=@ngaysinh, SDT=@sdt, DiaChi=@diachi,MaTheThanhVien=@mathe, DanhTinh=@danhtinh WHERE MaKhachHang = @makh",cn);
            cmd.Parameters.AddWithValue("@makh", txtmakh.Text);
            cmd.Parameters.AddWithValue("@ten",txttenkh.Text);
            cmd.Parameters.AddWithValue("@ngaysinh", txt_ngaysinh.Text);
            cmd.Parameters.AddWithValue("@sdt", txtsdt.Text);
            cmd.Parameters.AddWithValue("@diachi", txtdiachi.Text);
            cmd.Parameters.AddWithValue("@mathe", txttthanhvien.Text);
            cmd.Parameters.AddWithValue("@danhtinh", txtdanhtinh.Text);
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("UPDATE ThongTinLienHe SET DanhTinh=@danhtinh, Ten=@ten, SDT=@sdt, Diachi=@diachi WHERE MaThongTinLienHe =@malienhe", cn);
            cmd1.Parameters.AddWithValue("@malienhe", txtttlienhe.Text);
            cmd1.Parameters.AddWithValue("@danhtinh", txtdanhtinh.Text);
            cmd1.Parameters.AddWithValue("@ten", txttenkh.Text);
            cmd1.Parameters.AddWithValue("@sdt", txtsdt.Text);
            cmd1.Parameters.AddWithValue("@diachi", txtdiachi.Text);
            cmd1.ExecuteNonQuery();
            loaddata();
        }

        private void tb_kh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmakh.ReadOnly = true;
            txtttlienhe.ReadOnly = true;
            int i;
            i = tb_kh.CurrentRow.Index;
            txtmakh.Text = tb_kh.Rows[i].Cells[0].Value.ToString();
            txttenkh.Text = tb_kh.Rows[i].Cells[1].Value.ToString();
            txt_ngaysinh.Text = tb_kh.Rows[i].Cells[2].Value.ToString();
            txtsdt.Text = tb_kh.Rows[i].Cells[3].Value.ToString();
            txtdiachi.Text = tb_kh.Rows[i].Cells[4].Value.ToString();
            txtttlienhe.Text = tb_kh.Rows[i].Cells[5].Value.ToString();
            txttthanhvien.Text = tb_kh.Rows[i].Cells[6].Value.ToString();
            txtdanhtinh.Text = tb_kh.Rows[i].Cells[7].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("DELETE FROM ThongTinKhachHang WHERE MaKhachHang = '"+txtmakh.Text+"'",cn);
            cmd.ExecuteNonQuery();
            SqlCommand cmd1 = new SqlCommand("DELETE FROM ThongTinLienHe WHERE MaThongTinLienHe = '"+txtttlienhe.Text+"'", cn);
            cmd1.ExecuteNonQuery();
            loaddata();
        }
        public void timkiem(String keywords, String searchText)
        {
            cmd = new SqlCommand($"SELECT* FROM ThongTinKhachHang WHERE {keywords} like @searchText", cn);
            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            tb_kh.DataSource = dt;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            String text = txttimkiem.Text;
            String keywords = cbtimkiem.Text;
            switch (keywords)
            {
                case "Mã khách hàng":
                    keywords = "MaKhachHang";
                    break;
                case "Tên khách hàng":
                    keywords = "TenKhachHang";
                    break;
                case "Ngày sinh":
                    keywords = "NgaySinh";
                    break;
                case "Số điện thoại":
                    keywords = "SDT";
                    break;
                case "Địa chỉ":
                    keywords = "DiaChi";
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
    }
}
