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
    /// Interaction logic for KhoHang.xaml
    /// </summary>
    public partial class KhoHang : Window
    {
        public class ThongTinKho
        {
            public int TongSP { get; set; }
            public int TongSLHH { get; set; }
            public int TongSLDaBan { get; set; }
        }
        QLNONEntities db = new QLNONEntities();
        public KhoHang()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tongsp = (from c in db.KHOes select c).Count();
            int tongslhh = 0;
            var query1 = (from c in db.KHOes select c);
            foreach (var i in query1)
            {
                tongslhh = tongslhh + i.SLTON;
            }
            int tongsldb = 0;
            var query3 = (from c in db.KHOes select c);
            foreach (var a in query3)
            {
                tongsldb = tongsldb + a.SLDABAN;
            }
            ThongTinKho TTKho = new ThongTinKho()
            {
                TongSP = tongsp,
                TongSLHH = tongslhh,
                TongSLDaBan = tongsldb
            };
            this.DataContext = TTKho;
            var query2 = from c in db.KHOes
                         from d in db.SANPHAMs
                         from a in db.LOAISPs
                         from b in db.NHANHANGs
                         where d.ID == c.ID & c.SLTON > 0 & d.LOAISP == a.ID & d.NHANHANG == b.ID
                         select new { d.ID, d.TENSP, d.ANH, d.GIA, c.SLTON, a.TENLOAISP, b.TENNHANHANG };
            lvSPTrongKho.ItemsSource = query2.ToList();
            var query = from c in db.KHOes
                        from d in db.SANPHAMs
                        where d.ID == c.ID & c.SLTON == 0
                        select d;
            lvSPHetHang.ItemsSource = query.ToList();
            var hd = from c in db.SANPHAMs
                     from d in db.HOATDONGKHOes
                     where c.ID == d.IDSP
                     select new { c.ID, c.ANH, c.TENSP, d.SOLUONG, d.NGAYHD, d.HOATDONG };
            lvLichSuNX.ItemsSource = hd.ToList();
        }   

        private void infoItem_Click(object sender, RoutedEventArgs e)
        {

        }   

        private void nhapHang_Click(object sender, RoutedEventArgs e)
        {           
            var sp = lvSPHetHang.SelectedItem as SANPHAM;
            var screen = new NhapHang(sp.ID);
            if (screen.ShowDialog() == true)
            {
                var idsp = screen._idsp;
                var sl = screen._sl;

                var query = from c in db.KHOes
                            where c.ID == idsp
                            select c;
                foreach (var i in query)
                {
                    var spkho = db.KHOes.Find(i.ID);
                    spkho.SLTON = spkho.SLTON + sl;
                }
                // 1 nhap, 2 xuat
                var hdk = new HOATDONGKHO()
                {
                    IDSP = idsp,
                    HOATDONG = "Xuất",
                    SOLUONG = sl,
                    NGAYHD = DateTime.Now
                };
                db.HOATDONGKHOes.Add(hdk);
                db.SaveChanges();
                MessageBox.Show("Nhập hàng thành công");
                var query1 = from c in db.KHOes
                            from d in db.SANPHAMs
                            where d.ID == c.ID & c.SLTON == 0
                            select d;
                lvSPHetHang.ItemsSource = query1.ToList();
                var query2 = from c in db.KHOes
                             from d in db.SANPHAMs
                             from a in db.NHANHANGs
                             from b in db.LOAISPs
                             where d.ID == c.ID & c.SLTON > 0 & d.LOAISP == b.ID & d.NHANHANG == a.ID
                             select new { d.ID, d.TENSP, a.TENNHANHANG, b.TENLOAISP, d.ANH, d.GIA, c.SLTON };
                lvSPTrongKho.ItemsSource = query2.ToList();
                var hd = from c in db.SANPHAMs
                         from d in db.HOATDONGKHOes
                         where c.ID == d.IDSP
                         select new { c.ID, c.ANH, c.TENSP, d.SOLUONG, d.NGAYHD, d.HOATDONG };
                lvLichSuNX.ItemsSource = hd.ToList();
            }
        }

        private void infoNhapHang_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void nhapThemHang_Click(object sender, RoutedEventArgs e)
        {
           
            
        }
    }
}
