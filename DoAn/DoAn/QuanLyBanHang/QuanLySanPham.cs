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

namespace DoAn.QuanLyBanHang
{
    public partial class QuanLySanPham : Form
    {
        SqlConnection cn;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        SqlCommand cmd;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public QuanLySanPham()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM ThongTinSanPham";
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            dgvsp.DataSource = dt;
        }
        private void QuanLySanPham_Load(object sender, EventArgs e)
        {
            cbtimkiem.Text = "Mã sản phẩm";
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }
        public void timkiem(String keywords, String searchText)
        {
            cmd = new SqlCommand($"SELECT* FROM ThongTinSanPham WHERE {keywords} like @searchText", cn);
            cmd.Parameters.AddWithValue("@searchText", "%"+ searchText +"%");
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            dgvsp.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String text = txttimkiem.Text;
            String keywords = cbtimkiem.Text;
            switch (keywords)
            {
                case "Mã sản phẩm":
                    keywords = "MaSanPham";
                    break;
                case "Tên sản phẩm":
                    keywords = "TenSanPham";
                    break;
                case "Màu sắc":
                    keywords = "MauSac";
                    break;
                case "Kích thước":
                    keywords = "KichThuoc";
                    break;
                case "Số lượng":
                    keywords = "SoLuong";
                    break;
                case "Đơn giá":
                    keywords = "DonGia";
                    break;
            }
            timkiem(keywords, text);    
            
        }

        private void txtlammoi_Click(object sender, EventArgs e)
        {
            txttimkiem.Text = "";
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
