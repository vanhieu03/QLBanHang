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
    public partial class NhapThongTinSuCo : Form
    {
        SqlConnection cn;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
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
            cmd.CommandText = "SELECT * FROM ThongTin";
        }
        private void NhapThongTinSuCo_Load(object sender, EventArgs e)
        {

        }
    }
}
