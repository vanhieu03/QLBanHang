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
using System.Globalization;
using System.Collections;

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
        private void LapHoaDon_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            if(cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            Random rd = new Random();
            int rd_mahd = rd.Next(1000,10000);
            txtmahoadon.Text = rd_mahd.ToString();
            txtthoigian.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void btthem_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(chuoiketnoi))
            {
                cn.Open();
                cmd = new SqlCommand("INSERT INTO ThongTinHoaDon(MaHoaDon, MaKhachHang, MaSanPham,TenSanPham,SoLuong,DonGia,TongSoTienThanhToan,ThoiGian,KhuyenMai) VALUES(@mahd, @makh, @masp, @tensp, @soluong, @dongia, @tongtien, @thoigian, @khuyenmai )", cn);
                cmd.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                cmd.Parameters.AddWithValue("@makh", txtmakh.Text);
                cmd.Parameters.AddWithValue("@masp", txtmasp.Text);
                cmd.Parameters.AddWithValue("@tensp", txttensp.Text);
                cmd.Parameters.AddWithValue("@soluong", int.Parse(txtsoluong.Text));
                cmd.Parameters.AddWithValue("@dongia", float.Parse(txtdg.Text));
                float tongtien = int.Parse(txtsoluong.Text) * float.Parse(txtdg.Text);
                cmd.Parameters.AddWithValue("@tongtien", tongtien);
                DateTime thoigian = DateTime.ParseExact(txtthoigian.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("@thoigian", thoigian);
                cmd.Parameters.AddWithValue("@khuyenmai", txtkm.Text);
                cmd.ExecuteNonQuery();
                loaddata("SELECT KH.MaKhachHang, HD.* FROM ThongTinKhachHang AS KH JOIN ThongTinHoaDon AS HD ON KH.MaKhachHang = HD.MaKhachHang");
            }
            
        }

        private void txtsdt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtsdt.SelectedItem != null)
            {
                // Lấy số điện thoại đã chọn từ ComboBox
                string selectedSDT = txtsdt.SelectedItem.ToString();

                string query = "SELECT MaKhachHang, TenKhachHang FROM ThongTinKhachHang WHERE SDT = @SDT";

                using (cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@SDT", selectedSDT);

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // Hiển thị thông tin mã khách hàng và tên khách hàng tương ứng
                            txtmakh.Text = reader["MaKhachHang"].ToString();
                            txttenkhachhang.Text = reader["TenKhachHang"].ToString();
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void txtsdt_TextChanged(object sender, EventArgs e)
        {
            string sdt = txtsdt.Text.Trim(); // Lấy số điện thoại từ ComboBox

            // Kiểm tra nếu số điện thoại không rỗng
            if (!string.IsNullOrEmpty(sdt))
            {
                string query = "SELECT SDT FROM ThongTinKhachHang WHERE SDT LIKE @SDT + '%'";
                // Tìm kiếm các số điện thoại bắt đầu bằng số điện thoại người dùng nhập vào
                using (cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@SDT", sdt);

                    try
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            // Thêm số điện thoại vào ComboBox
                            txtsdt.Items.Add(reader["SDT"].ToString());
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
                
            }
            else
            {
                txtmakh.Text = "";
                txttenkhachhang.Text = "";
                txtsdt.Items.Clear();
            }
        }

        private void txtmasp_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtmasp.Text))
            {
                using (cn = new SqlConnection(chuoiketnoi))
                {
                    cn.Open();
                    cmd = new SqlCommand("SELECT TenSanPham, DonGia FROM ThongTinSanPham WHERE MaSanPham = @MaSP", cn);
                    cmd.Parameters.AddWithValue("@MaSP", txtmasp.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txttensp.Text = dr["TenSanPham"].ToString();
                        txtdg.Text = dr["DonGia"].ToString();
                    }
                    else
                    {
                        txttensp.Text = ""; // Clear khi không tìm thấy sản phẩm
                    }
                    dr.Close();
                }
            }
            else
            {
                txttensp.Text = "";
                txtdg.Text = "";
            }
        }
    }
}
