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
    /// Interaction logic for HoaDon.xaml
    /// </summary>
    public partial class HoaDon : Window
    {
        QLNONEntities db = new QLNONEntities();
        public int _ID_HD { get; set; }
        public class TTHoaDon
        {
            public int ID { get; set; }
            public string TinhTrang { get; set; }
            public string NgayThanhToan { get; set; }
            public string TenKH { get; set; }
            public string DienThoai { get; set; }
            public string DiaChi { get; set; }
            public double TongCong { get; set; }
        }
        public HoaDon(int ID)
        {
            InitializeComponent();
            _ID_HD = ID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var HDon = db.HOADONs.Find(_ID_HD);
            var KHang = db.KHACHHANGs.Find(HDon.ID_KHACHHANG);
            var tthdon = new TTHoaDon()
            {
                ID = _ID_HD,
                TinhTrang = HDon.TINHTRANG,
                NgayThanhToan = HDon.NGAYTHANHTOAN.ToString(),
                TenKH = KHang.TEN,
                DienThoai = KHang.SDT,
                DiaChi = KHang.DIACHI,
                TongCong = HDon.THANHTIEN
            };
            this.DataContext = tthdon;
            var query = from c in db.CT_HOADON
                        from d in db.SANPHAMs
                        from a in db.LOAISPs
                        from b in db.NHANHANGs
                        where c.ID_HOADON == _ID_HD & c.ID_SANPHAM == d.ID & d.LOAISP == a.ID & d.NHANHANG == b.ID
                        select new { d.ID, d.TENSP, a.TENLOAISP, b.TENNHANHANG, c.GIA, c.SOLUONG, d.ANH, c.THANHTIEN };
            lvct_hoadon.ItemsSource = query.ToList();
        }
        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            var HDon = db.HOADONs.Find(_ID_HD);
            if (HDon.TINHTRANG == "Đã Thanh Toán")
            {
                MessageBox.Show("Hóa đơn này đã thanh toán rồi.");
                return;
            }
            HDon.TINHTRANG = "Đã Thanh Toán";
            

            var query = from c in db.CT_HOADON where c.ID_HOADON == _ID_HD select c;
            foreach (var i in query)
            {
                var spkho = db.KHOes.Find(i.ID_SANPHAM);
                spkho.SLTON = spkho.SLTON - i.SOLUONG;
                spkho.SLDABAN = spkho.SLDABAN + i.SOLUONG;
                var hdban = new HOATDONGKHO()
                {
                    IDSP = i.ID_SANPHAM,
                    HOATDONG = "Nhập",
                    SOLUONG = i.SOLUONG,
                    NGAYHD = DateTime.Now
                };
                db.HOATDONGKHOes.Add(hdban);
            }
            db.SaveChanges();
            MessageBox.Show("Thanh toán thành công đơn hàng");
            if (db.SaveChanges() == 0)
            {
                var _HDon = db.HOADONs.Find(_ID_HD);
                var _KHang = db.KHACHHANGs.Find(_HDon.ID_KHACHHANG);
                var tthdon = new TTHoaDon()
                {
                    ID = _ID_HD,
                    TinhTrang = _HDon.TINHTRANG,
                    NgayThanhToan = _HDon.NGAYTHANHTOAN.ToString(),
                    TenKH = _KHang.TEN,
                    DienThoai = _KHang.SDT,
                    DiaChi = _KHang.DIACHI,
                    TongCong = _HDon.THANHTIEN
                };
                this.DataContext = tthdon;
                btnThanhToan.Content = "Đã thanh toán";
            }
        }
    }
}
