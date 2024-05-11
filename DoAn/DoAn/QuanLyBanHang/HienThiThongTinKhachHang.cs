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
using System.Data.Common;

namespace DoAn.QuanLyBanHang
{
    public partial class HienThiThongTinKhachHang : Form
    {
        SqlConnection cn;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        SqlCommand cmd;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();

        public HienThiThongTinKhachHang()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM ThongTinKhachHang";
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            dgv.DataSource = dt;
        }
        private void HienThiThongTinKhachHang_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            cn.Open();
            loaddata();
        }

    }
}
