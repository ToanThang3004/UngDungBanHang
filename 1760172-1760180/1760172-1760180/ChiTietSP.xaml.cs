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
    /// Interaction logic for ChiTietSP.xaml
    /// </summary>
    public partial class ChiTietSP : Window
    {
        QLNONEntities db = new QLNONEntities();
        public ChiTietSP(SANPHAM _sp)
        {
            InitializeComponent();
            tbxID.Text = _sp.ID.ToString();
            tbxTenSP.Text = _sp.TENSP.ToString();
            var lsp = db.LOAISPs.Find(_sp.LOAISP);
            tbxLoaiSP.Text = lsp.TENLOAISP;
            var th = db.NHANHANGs.Find(_sp.NHANHANG);
            tbxThuongHieu.Text = th.TENNHANHANG;
            tbxGiaSP.Text = _sp.GIA.ToString();
            var query = from c in db.SANPHAMs
                        where c.ID == _sp.ID
                        select c;
            ImageItem.ItemsSource = query.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
