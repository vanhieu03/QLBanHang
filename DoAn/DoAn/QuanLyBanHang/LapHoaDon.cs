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
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
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
            if(cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            Random rd = new Random();
            int rd_mahd = rd.Next(1000,10000);
            int rd_maspban = rd.Next(1000, 100000);
            txtmaspban.Text = rd_maspban.ToString();
            txtmahoadon.Text = rd_mahd.ToString();
            txtthoigian.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //Thêm MaHoaDon vào bảng ThongTinHoaDon trước
            SqlCommand cmd1 = new SqlCommand("INSERT INTO ThongTinHoaDon(MaHoaDon) VALUES(@mahd)", cn);
            cmd1.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
            cmd1.ExecuteNonQuery();
            loaddata(@"SELECT MaSanPhamBan AS [Mã Sản Phẩm Bán],
            MaHoaDon AS [Mã Hóa Đơn],
            MaSanPham AS [Mã Sản Phẩm],
            TenSanPham AS [Tên Sản Phẩm],
            KichThuoc AS [Kích Thước],
            MauSac AS [Màu Sắc],
            SoLuong AS [Số Lượng],
            DonGia AS [Đơn Giá],
            KhuyenMai AS [Khuyến Mãi],
            TongTien AS [Tổng Tiền] FROM DanhSachSanPhamBan WHERE MaHoaDon = '" + txtmahoadon.Text+"'");
        }
        private void btthem_Click(object sender, EventArgs e)
        {
                //Thêm vào bảng DanhSachSanPhamBan
                cmd = new SqlCommand("INSERT INTO DanhSachSanPhamBan(MaSanPhamBan,MaHoaDon, MaSanPham, TenSanPham, KichThuoc, MauSac, SoLuong, DonGia, KhuyenMai,TongTien) VALUES(@maspban, @mahd,@masp, @tensp,@kichthuoc, @mausac, @soluong, @dongia, @khuyenmai, @tongtien )", cn);
                cmd.Parameters.AddWithValue("@maspban", txtmaspban.Text);
                cmd.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                cmd.Parameters.AddWithValue("@masp", txtmasp.Text);
                cmd.Parameters.AddWithValue("@tensp", txttensp.Text);
                cmd.Parameters.AddWithValue("@kichthuoc", txtkichthuoc.Text);
                cmd.Parameters.AddWithValue("@mausac", txtmausac.Text);
                cmd.Parameters.AddWithValue("@soluong", int.Parse(txtsoluong.Text));
                cmd.Parameters.AddWithValue("@dongia", float.Parse(txtdongia.Text));
                cmd.Parameters.AddWithValue("@khuyenmai", float.Parse(txtkhuyenmai.Text));
                float tongtien = (int.Parse(txtsoluong.Text) * float.Parse(txtdongia.Text)) - float.Parse(txtkhuyenmai.Text);
                cmd.Parameters.AddWithValue("@tongtien", tongtien);
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("UPDATE ThongTinHoaDon SET MaKhachHang=@makh, TenKhachHang=@tenkh, ThoiGian=@thoigian WHERE MaHoaDon = @mahd", cn);
                cmd1.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                cmd1.Parameters.AddWithValue("@makh", txtmakh.Text);
                cmd1.Parameters.AddWithValue("@tenkh", txttenkhachhang.Text);
                DateTime thoigian = DateTime.ParseExact(txtthoigian.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                cmd1.Parameters.AddWithValue("@thoigian", thoigian);
                cmd1.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT SUM(TongTien) FROM DanhSachSanPhamBan WHERE MaHoaDon = @mahd", cn);
                cmd.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                object result = cmd.ExecuteScalar(); // Trả về giá trị đầu tiên của kết quả truy vấn
                txtthanhtoan.Text = result.ToString(); // Hiển thị tổng tiền trong TextBox thanh toán
                //Gán vào cột ThanhToan trong bảng ThongTinHoaDon
                cmd1 = new SqlCommand("UPDATE ThongTinHoaDon SET ThanhToan = @thanhtoan WHERE MaHoaDon = @mahd", cn);
                cmd1.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                cmd1.Parameters.AddWithValue("@thanhtoan", txtthanhtoan.Text);
                cmd1.ExecuteNonQuery();
                loaddata(@"SELECT MaSanPhamBan AS [Mã Sản Phẩm Bán],
                MaHoaDon AS [Mã Hóa Đơn],
                MaSanPham AS [Mã Sản Phẩm],
                TenSanPham AS [Tên Sản Phẩm],
                KichThuoc AS [Kích Thước],
                MauSac AS [Màu Sắc],
                SoLuong AS [Số Lượng],
                DonGia AS [Đơn Giá],
                KhuyenMai AS [Khuyến Mãi],
                TongTien AS [Tổng Tiền] FROM DanhSachSanPhamBan WHERE MaHoaDon = '" + txtmahoadon.Text + "'");

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
                    cmd = new SqlCommand("SELECT TenSanPham, DonGia FROM ThongTinSanPham WHERE MaSanPham = @MaSP", cn);
                    cmd.Parameters.AddWithValue("@MaSP", txtmasp.Text.Trim());

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txttensp.Text = dr["TenSanPham"].ToString();
                        txtdongia.Text = dr["DonGia"].ToString();
                    }
                    else
                    {
                        txttensp.Text = ""; // Clear khi không tìm thấy sản phẩm
                    }
                    dr.Close();
                
            }
            else
            {
                txttensp.Text = "";
                txtdongia.Text = "";
            }
        }

        private void btsua_Click(object sender, EventArgs e)
        {
                cmd = new SqlCommand("UPDATE DanhSachSanPhamBan SET MaHoaDon = @mahd, MaSanPham = @masp, TenSanPham = @tensp, KichThuoc =@kichthuoc, MauSac = @mausac, DonGia = @dongia, KhuyenMai = @khuyenmai,TongTien = @tongtien WHERE MaSanPhamBan = @maspban", cn);
                cmd.Parameters.AddWithValue("@maspban", txtmaspban.Text);
                cmd.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                cmd.Parameters.AddWithValue("@masp", txtmasp.Text);
                cmd.Parameters.AddWithValue("@tensp", txttensp.Text);
                cmd.Parameters.AddWithValue("@kichthuoc", txtkichthuoc.Text);
                cmd.Parameters.AddWithValue("@mausac", txtmausac.Text);
                cmd.Parameters.AddWithValue("@soluong", int.Parse(txtsoluong.Text));
                cmd.Parameters.AddWithValue("@dongia", float.Parse(txtdongia.Text));
                cmd.Parameters.AddWithValue("@khuyenmai", float.Parse(txtkhuyenmai.Text));
                float tongtien = (int.Parse(txtsoluong.Text) * float.Parse(txtdongia.Text)) - float.Parse(txtkhuyenmai.Text);
                cmd.Parameters.AddWithValue("@tongtien", tongtien);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("SELECT SUM(TongTien) FROM DanhSachSanPhamBan WHERE MaHoaDon = @mahd", cn);
                cmd.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                object result = cmd.ExecuteScalar(); // Trả về giá trị đầu tiên của kết quả truy vấn
                txtthanhtoan.Text = result.ToString(); // Hiển thị tổng tiền trong TextBox thanh toán
                                                       //Gán vào cột ThanhToan trong bảng ThongTinHoaDon
                SqlCommand cmd1 = new SqlCommand("UPDATE ThongTinHoaDon SET ThanhToan = @thanhtoan WHERE MaHoaDon = @mahd", cn);
                cmd1.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                cmd1.Parameters.AddWithValue("@thanhtoan", txtthanhtoan.Text);
                cmd1.ExecuteNonQuery();
                loaddata(@"SELECT MaSanPhamBan AS [Mã Sản Phẩm Bán],
                MaHoaDon AS [Mã Hóa Đơn],
                MaSanPham AS [Mã Sản Phẩm],
                TenSanPham AS [Tên Sản Phẩm],
                KichThuoc AS [Kích Thước],
                MauSac AS [Màu Sắc],
                SoLuong AS [Số Lượng],
                DonGia AS [Đơn Giá],
                KhuyenMai AS [Khuyến Mãi],
                TongTien AS [Tổng Tiền] FROM DanhSachSanPhamBan WHERE MaHoaDon = '" + txtmahoadon.Text + "'");

        }

        private void dgvhoadon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem đã chọn một hàng hợp lệ
            {
                    txtmahoadon.ReadOnly = true;
                    DataGridViewRow row = dgvhoadon.Rows[e.RowIndex];
                    txtmaspban.Text = row.Cells[0].Value.ToString();
                    txtmahoadon.Text = row.Cells[1].Value.ToString();
                    txtmasp.Text = row.Cells[2].Value.ToString();
                    txttensp.Text = row.Cells[3].Value.ToString();
                    txtkichthuoc.Text = row.Cells[4].Value.ToString();
                    txtmausac.Text = row.Cells[5].Value.ToString();
                    txtsoluong.Text = row.Cells[6].Value.ToString();
                    txtdongia.Text = row.Cells[7].Value.ToString();
                    txtkhuyenmai.Text = row.Cells[8].Value.ToString();
                
            }
        }

        private void btxoa_Click(object sender, EventArgs e)
        {
                SqlCommand cmd2 = new SqlCommand("DELETE FROM DanhSachSanPhamBan WHERE MaSanPhamBan = '" + txtmaspban.Text + "'", cn);
                cmd2.ExecuteNonQuery();
                cmd.CommandText = "SELECT COUNT(*) FROM DanhSachSanPhamBan WHERE MaBanHang = '" + txtmaspban.Text + "'";
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    if(count == 0)
                    {
                        cmd = new SqlCommand("DELETE FROM ThongTinHoaDon WHERE MaHoaDon = '" + txtmahoadon.Text + "'", cn);
                        cmd.ExecuteNonQuery();
                        SqlCommand cmd1 = new SqlCommand("DELETE FROM ThongTinSuCo WHERE MaHoaDon = '" + txtmahoadon.Text + "'", cn);
                        cmd1.ExecuteNonQuery();
                    }
                }
                cmd = new SqlCommand("SELECT SUM(TongTien) FROM DanhSachSanPhamBan WHERE MaHoaDon = @mahd", cn);
                cmd.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                object result = cmd.ExecuteScalar(); // Trả về giá trị đầu tiên của kết quả truy vấn
                txtthanhtoan.Text = result.ToString(); // Hiển thị tổng tiền trong TextBox thanh toán
                                                       //Gán vào cột ThanhToan trong bảng ThongTinHoaDon
                SqlCommand cmd3 = new SqlCommand("UPDATE ThongTinHoaDon SET ThanhToan = @thanhtoan WHERE MaHoaDon = @mahd", cn);
                cmd3.Parameters.AddWithValue("@mahd", txtmahoadon.Text);
                cmd3.Parameters.AddWithValue("@thanhtoan", txtthanhtoan.Text);
                cmd3.ExecuteNonQuery();
                loaddata(@"SELECT MaSanPhamBan AS [Mã Sản Phẩm Bán],
                MaHoaDon AS [Mã Hóa Đơn],
                MaSanPham AS [Mã Sản Phẩm],
                TenSanPham AS [Tên Sản Phẩm],
                KichThuoc AS [Kích Thước],
                MauSac AS [Màu Sắc],
                SoLuong AS [Số Lượng],
                DonGia AS [Đơn Giá],
                KhuyenMai AS [Khuyến Mãi],
                TongTien AS [Tổng Tiền] FROM DanhSachSanPhamBan WHERE MaHoaDon = '" + txtmahoadon.Text + "'");
                txtmahoadon.ReadOnly = false;
                txtmaspban.Text = "";
                txtmasp.Text = "";
                txttensp.Text = "";
                txtkichthuoc.Text = "";
                txtmausac.Text = "";
                txtsoluong.Text = "";
                txtdongia.Text = "";
                txtthoigian.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtkhuyenmai.Text = "";
            
        }

        private void btlammoi_Click(object sender, EventArgs e)
        {
                Random rd = new Random();
                int rd_maspban = rd.Next(1000, 100000);
                txtmaspban.Text = rd_maspban.ToString();
                //txtmakh.Text = "";
                //txttenkhachhang.Text = "";
                //txtsdt.Text = "";
                txtmasp.Text = "";
                txttensp.Text = "";
                txtkichthuoc.Text = "";
                txtmausac.Text = "";
                txtsoluong.Text = "";
                txtdongia.Text = "";
                txtthoigian.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtkhuyenmai.Text = "";
                loaddata(@"SELECT MaSanPhamBan AS [Mã Sản Phẩm Bán],
                MaHoaDon AS [Mã Hóa Đơn],
                MaSanPham AS [Mã Sản Phẩm],
                TenSanPham AS [Tên Sản Phẩm],
                KichThuoc AS [Kích Thước],
                MauSac AS [Màu Sắc],
                SoLuong AS [Số Lượng],
                DonGia AS [Đơn Giá],
                KhuyenMai AS [Khuyến Mãi],
                TongTien AS [Tổng Tiền] FROM DanhSachSanPhamBan WHERE MaHoaDon = '" + txtmahoadon.Text + "'");

        }

        private void btdong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
