using DoAn.QuanLyBanHang;
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
using DoAn.QuanLyKho;

namespace DoAn
{
    public partial class HeThongQuanLyBanHang : Form
    {
        public HeThongQuanLyBanHang()
        {
            InitializeComponent();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hiệnThịThôngTinKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HienThiThongTinKhachHang hienthi = new HienThiThongTinKhachHang();
            hienthi.MdiParent = this;
            hienthi.Show();
        }

        private void quảnLýThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLySanPham hienthi = new QuanLySanPham();
            hienthi.MdiParent = this;
            hienthi.Show();
        }

        private void lậpHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LapHoaDon hienthi = new LapHoaDon();
            hienthi.MdiParent = this;
            hienthi.Show();
        }

        private void nhậpThôngTinXuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhapThongTinSuCo hienthi = new NhapThongTinSuCo();
            hienthi.MdiParent = this;
            hienthi.Show();
        }

        private void tìmKiếmHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimHoaDon hienthi = new TimHoaDon();
            hienthi.MdiParent = this;
            hienthi.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TongHopDuLieuBanHang hienthi = new TongHopDuLieuBanHang();
            hienthi.MdiParent = this;
            hienthi.Show();
        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _1 hienthi = new _1();
            hienthi.MdiParent = this;
            hienthi.Show();
        }

        private void kiểmTraTồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KiemTraTonKho hienthi = new KiemTraTonKho();
            hienthi.MdiParent = this;
            hienthi.Show();
        }
    }
}
