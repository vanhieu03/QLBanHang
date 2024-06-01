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

namespace DoAn.BaoCaoThongKe
{
    public partial class ThongKeKho : Form
    {
        SqlConnection cn;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public ThongKeKho()
        {
            InitializeComponent();
        }
        public void loaddata()
        {
            cmd = cn.CreateCommand();
            cmd.CommandText = "SELECT * FROM ThongTinKhoHang";
            dta.SelectCommand = cmd;
            dt.Clear();
            dta.Fill(dt);
            dgv.DataSource = dt;
        }
        private void ThongKeKho_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(chuoiketnoi);
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            loaddata();
        }
    }
}
