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
    public partial class ThongKeBanHang : Form
    {
        SqlConnection cn;
        String chuoiketnoi = @"Data Source=DESKTOP-NQC7O0B;Initial Catalog=QLBanHang;Integrated Security=True";
        //String chuoiketnoi = System.Configuration.ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
        SqlDataAdapter dta = new SqlDataAdapter();
        DataTable dt = new DataTable();
        public ThongKeBanHang()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            cn = new SqlConnection(chuoiketnoi);
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
            string query = @"
                        WITH DateRange AS (
                SELECT CONVERT(DATE, @StartDate) AS ""Thời gian""
                UNION ALL
                SELECT DATEADD(DAY, 1, ""Thời gian"")
                FROM DateRange
                WHERE DATEADD(DAY, 1, ""Thời gian"") <= CONVERT(DATE, @EndDate)
            ),
            KhachHangMoi AS (
                SELECT MaKhachHang, MIN(CONVERT(DATE, ThoiGian)) AS ThoiGianDauTien
                FROM ThongTinHoaDon
                GROUP BY MaKhachHang
            ),
            AggregatedData AS (
                SELECT 
                    CONVERT(DATE, TT.ThoiGian) AS ""Thời gian"",
                    COUNT(DISTINCT CASE WHEN CONVERT(DATE, TT.ThoiGian) = KHM.ThoiGianDauTien THEN TT.MaKhachHang ELSE NULL END) AS ""Khách hàng mới"",
                    COUNT(DISTINCT CASE WHEN CONVERT(DATE, TT.ThoiGian) <> KHM.ThoiGianDauTien THEN TT.MaKhachHang ELSE NULL END) AS ""Khách hàng quay lại"",
                    COUNT(DISTINCT TT.MaKhachHang) AS ""Tổng khách hàng"",
                    SUM(TT.ThanhToan) AS ""Tổng doanh thu"",
                    (SELECT COUNT(*) FROM ThongTinSuCo SC WHERE CONVERT(DATE, SC.ThoiGian) = CONVERT(DATE, TT.ThoiGian)) AS ""Số sự cố""
                FROM ThongTinHoaDon TT
                JOIN KhachHangMoi KHM ON TT.MaKhachHang = KHM.MaKhachHang
                GROUP BY CONVERT(DATE, TT.ThoiGian)
            )
            SELECT 
                DR.""Thời gian"",
                COALESCE(AD.""Khách hàng mới"", 0) AS ""Khách hàng mới"",
                COALESCE(AD.""Khách hàng quay lại"", 0) AS ""Khách hàng quay lại"",
                COALESCE(AD.""Tổng khách hàng"", 0) AS ""Tổng khách hàng"",
                COALESCE(AD.""Tổng doanh thu"", 0) AS ""Tổng doanh thu"",
                COALESCE(AD.""Số sự cố"", 0) AS ""Số sự cố""
            FROM DateRange DR
            LEFT JOIN AggregatedData AD ON DR.""Thời gian"" = AD.""Thời gian""
            ORDER BY DR.""Thời gian""
            OPTION (MAXRECURSION 0);


        ";

            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // Thiết lập các tham số cho câu truy vấn
                cmd.Parameters.AddWithValue("@StartDate", datetu.Value.Date);
                cmd.Parameters.AddWithValue("@EndDate", dateden.Value.Date);

                // Mở kết nối và thực thi truy vấn
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt; // Hiển thị kết quả lên DataGridView
                }
            }

            using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", datetu.Value.Date);
                    cmd.Parameters.AddWithValue("@EndDate", dateden.Value.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgv.DataSource = dt; // Displaying the results in a DataGridView
                    }
                }
            
        }

        private void ThongKeBanHang_Load(object sender, EventArgs e)
        {
           
            LoadData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
