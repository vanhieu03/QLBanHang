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
using System.Security.Cryptography;
using System.Collections;

namespace DoAn.QuanLyBanHang
{
    public partial class TongHopDuLieuBanHang : Form
    {
        SqlConnection cn;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public TongHopDuLieuBanHang()
        {
            InitializeComponent();
        }
        public void loaddata(string query,DataGridView tb)
        {
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                using (SqlDataAdapter dta = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    dta.Fill(dt);
                    tb.DataSource = dt;
                }
            }
        }
        private void TongHopDuLieuBanHang_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            try
            {
                cn.Open();
                loaddata("SELECT * FROM ThongTinSanPham", dgv1);
                loaddata("SELECT * FROM ThongTinSuCo", dgv2);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }
        public void timkiem(String table, String keywords, String searchText)
        {
            List<string> validColumns = new List<string> { "MaSanPham", "TenSanPham", "KichThuoc", "MauSac", "SoLuong", "DonGia", "MaSuCo", "MaHoaDon", "TenKhachHang", "TenSuCo", "ThoiGian", "NhanVienTiepNhan", "TrangThai" };

            if (!validColumns.Contains(keywords))
            {
                MessageBox.Show("Từ khóa tìm kiếm không hợp lệ.");
                return;
            }
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM {table} WHERE {keywords} LIKE @searchText", cn))
                {
                    cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                    using (SqlDataAdapter dta = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        dta.Fill(dt);
                        if (dt.Rows.Count == 0) // Nếu không có kết quả
                        {
                            if (table == "ThongTinSanPham")
                            {
                                dgv1.DataSource = null;
                            }
                            else if (table == "ThongTinSuCo")
                            {
                                dgv2.DataSource = null;
                            }
                        }

                        if (table == "ThongTinSanPham")
                        {
                            dgv1.DataSource = dt;
                        }
                        else if (table == "ThongTinSuCo")
                        {
                            dgv2.DataSource = dt;
                        }
                    }
                }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = txttimkiem1.Text;
            string keyword = cbtimkiem1.SelectedItem.ToString();
            Dictionary<string, string> keywordMapping = new Dictionary<string, string> {
            
            { "Tên sản phẩm", "TenSanPham" },
            { "Kích thước", "KichThuoc" },
            { "Màu sắc", "MauSac" },
            { "Số lượng", "SoLuong" },
            { "Đơn giá", "DonGia" }
            };
            if (keywordMapping.TryGetValue(keyword, out string mappedKeyword))
            {
                timkiem("ThongTinSanPham", mappedKeyword, text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = txttimkiem2.Text;
            string keyword = cbtimkiem2.SelectedItem.ToString();
            Dictionary<string, string> keywordMapping = new Dictionary<string, string> {
            
            { "Tên khách hàng", "TenKhachHang" },
            { "Tên sự cố", "TenSuCo" },
            { "Thời gian", "ThoiGian" },
            { "Nhân viên tiếp nhận", "NhanVienTiepNhan" },
            { "Trạng thái", "TrangThai" }
            };
            if (keywordMapping.TryGetValue(keyword, out string mappedKeyword))
            {
                timkiem("ThongTinSuCo", mappedKeyword, text);
            }
        }

        private void btnlammoi1_Click(object sender, EventArgs e)
        {
            txttimkiem1.Text = "";
        }

        private void btnlammoi2_Click(object sender, EventArgs e)
        {
            txttimkiem2.Text = "";
        }

        private void btndong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txttimkiem1_TextChanged(object sender, EventArgs e)
        {
            string text = txttimkiem1.Text;
            string keyword = cbtimkiem1.SelectedItem.ToString();
            Dictionary<string, string> keywordMapping = new Dictionary<string, string> {
            
            { "Tên sản phẩm", "TenSanPham" },
            { "Kích thước", "KichThuoc" },
            { "Màu sắc", "MauSac" },
            { "Số lượng", "SoLuong" },
            { "Đơn giá", "DonGia" }
            };
            if (keywordMapping.TryGetValue(keyword, out string mappedKeyword))
            {
                timkiem("ThongTinSanPham", mappedKeyword, text);
            }
        }

        private void txttimkiem2_TextChanged(object sender, EventArgs e)
        {
            string text = txttimkiem2.Text;
            string keyword = cbtimkiem2.SelectedItem.ToString();
            Dictionary<string, string> keywordMapping = new Dictionary<string, string> {
            
            { "Tên khách hàng", "TenKhachHang" },
            { "Tên sự cố", "TenSuCo" },
            { "Thời gian", "ThoiGian" },
            { "Nhân viên tiếp nhận", "NhanVienTiepNhan" },
            { "Trạng thái", "TrangThai" }
            };
            if (keywordMapping.TryGetValue(keyword, out string mappedKeyword))
            {
                timkiem("ThongTinSuCo", mappedKeyword, text);
            }
        }
    }
}
