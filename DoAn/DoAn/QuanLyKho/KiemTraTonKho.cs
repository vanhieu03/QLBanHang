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

namespace DoAn.QuanLyKho
{
    
    public partial class KiemTraTonKho : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM ThongTinSanPham";
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            dgv.DataSource = dt;

        }
        public KiemTraTonKho()
        {
            InitializeComponent();
        }

        private void KiemTraTonKho_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }

        public void timkiem()
        {
            cmd = new SqlCommand($"SELECT* FROM ThongTinSanPham WHERE SoLuong <= '"+txtsoluong.Text+"'", cn);
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            dgv.DataSource = dt;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            timkiem();        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtsoluong.Text = "";
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }
    }
}
