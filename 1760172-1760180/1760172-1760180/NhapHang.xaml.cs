using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _1760172_1760180
{
    /// <summary>
    /// Interaction logic for NhapHang.xaml
    /// </summary>
    public partial class NhapHang : Window
    {
        QLNONEntities db = new QLNONEntities();
        public int _idsp { get; set; } = 0;
        public int _sl { get; set; } = 0;
        public NhapHang(int id)
        {
            InitializeComponent();
            var sp = db.SANPHAMs.Find(id);
            ImageItem.ItemsSource = (from c in db.SANPHAMs where c.ID == sp.ID select c).ToList();
            tbxID.Text = sp.ID.ToString();
            tbxTenSP.Text = sp.TENSP;
            tbxLoaiSP.Text = db.LOAISPs.Find(sp.LOAISP).TENLOAISP;
            tbxThuongHieu.Text = db.NHANHANGs.Find(sp.NHANHANG).TENNHANHANG;
            tbxGiaSP.Text = sp.GIA.ToString();
            var query = from d in db.KHOes where d.ID == sp.ID select d;
            foreach (var i in query)
            {
                tbxSLTK.Text = i.SLTON.ToString();
            }
        }

        private void btnNhap_Click(object sender, RoutedEventArgs e)
        {
            var idsp = int.Parse(tbxID.Text);
            var sl = int.Parse(tbxSLNhap.Text);

            _idsp = idsp;
            _sl = sl;
            this.DialogResult = true;
        }
    }
}
