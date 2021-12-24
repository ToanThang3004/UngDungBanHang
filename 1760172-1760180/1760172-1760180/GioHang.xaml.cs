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
    /// Interaction logic for GioHang.xaml
    /// </summary>
    public partial class GioHang : Window
    {
        QLNONEntities db = new QLNONEntities();
        public class ThongTinGH
        {
            public double TongThanhTien { get; set; }
        }
        List<GHang> _listSPGH = new List<GHang>();
        public GioHang(List<GHang> listSPGH)
        {
            InitializeComponent();
            _listSPGH = listSPGH.ToList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lvGioHang.ItemsSource = _listSPGH.ToList();
            double tongtt = 0;
            foreach (var i in _listSPGH)
            {
                tongtt = tongtt + i.THANHTIEN;
            }
            var TT = new ThongTinGH()
            {
                TongThanhTien = tongtt
            };
            this.DataContext = TT;
        }       

        private void themSoLuong_Click(object sender, RoutedEventArgs e)
        {
            var sp = lvGioHang.SelectedItem as GHang;
            var spk = from c in db.KHOes
                          where c.ID == sp.ID
                          select c;
            int slsptrongkho = 0;
            foreach (var a in spk)
            {
                slsptrongkho = a.SLTON;
            }
            var screen = new ThemSL(sp.ID, sp.SoLuongMua,slsptrongkho);
            if (screen.ShowDialog() == true)
            {
                var slm = screen._SoLuongMua;

                foreach (var i in _listSPGH)
                {
                    if (i.ID == sp.ID)
                    {
                        int temp = 0;
                        if (i.SoLuongMua > slm)
                        {
                            temp = i.SoLuongMua - slm;
                        } else
                        {
                            temp = slm - i.SoLuongMua;
                        }
                        i.SoLuongMua = slm;
                        i.THANHTIEN = i.THANHTIEN + (temp * sp.GIA);
                    }
                }
                lvGioHang.ItemsSource = _listSPGH.ToList();
            }
        }

        private void btnThanhToan_Click(object sender, RoutedEventArgs e)
        {
            var screen = new KhachHang();
            if (screen.ShowDialog() == true)
            {
                var tenkh = screen._TenKH;
                var sdt = screen._SDT;
                var diachi = screen._DiaChi;
                var ghichu = screen._GhiChu;

                double tongtt = 0;
                foreach (var i in _listSPGH)
                {
                    tongtt = tongtt + i.THANHTIEN;
                }

                var query1 = from c in db.KHACHHANGs
                             where c.SDT == sdt
                             select c;
                if (query1.Count() == 0)
                {
                    var KHang = new KHACHHANG()
                    {
                        TEN = tenkh,
                        DIACHI = diachi,
                        SDT = sdt
                    };
                    db.KHACHHANGs.Add(KHang);
                    db.SaveChanges();
                    if (db.SaveChanges() == 0)
                    {
                        var idkh = db.KHACHHANGs.OrderByDescending(s => s.ID).First();
                        var HD = new HOADON()
                        {
                            ID_KHACHHANG = idkh.ID,
                            THANHTIEN = tongtt,
                            GHICHU = ghichu,
                            TINHTRANG = "Chưa thanh toán",
                            NGAYTHANHTOAN = DateTime.Now
                        };
                        db.HOADONs.Add(HD);
                        db.SaveChanges();
                        if (db.SaveChanges() == 0)
                        {
                            var idhd = db.HOADONs.OrderByDescending(s => s.ID).First();
                            foreach (var i in _listSPGH)
                            {
                                var cthd = new CT_HOADON()
                                {
                                    ID_HOADON = idhd.ID,
                                    ID_SANPHAM = i.ID,
                                    SOLUONG = i.SoLuongMua,
                                    GIA = i.GIA,
                                    THANHTIEN = i.THANHTIEN
                                };
                                db.CT_HOADON.Add(cthd);
                                db.SaveChanges();
                            }
                            var screenHD = new HoaDon(idhd.ID);
                            screenHD.Show();
                            this.Close();
                        }
                    }
                } else
                {
                    int _idkh = 0;
                    foreach (var i in query1)
                    {
                        _idkh = i.ID;
                    }
                    var HD = new HOADON()
                    {
                        ID_KHACHHANG = _idkh,
                        THANHTIEN = tongtt,
                        GHICHU = ghichu,
                        TINHTRANG = "Chưa thanh toán",
                        NGAYTHANHTOAN = DateTime.Now
                    };
                    db.HOADONs.Add(HD);
                    db.SaveChanges();
                    if (db.SaveChanges() == 0)
                    {
                        var idhd = db.HOADONs.OrderByDescending(s => s.ID).First();
                        foreach (var i in _listSPGH)
                        {
                            var cthd = new CT_HOADON()
                            {
                                ID_HOADON = idhd.ID,
                                ID_SANPHAM = i.ID,
                                SOLUONG = i.SoLuongMua,
                                GIA = i.GIA,
                                THANHTIEN = i.THANHTIEN
                            };
                            db.CT_HOADON.Add(cthd);
                            db.SaveChanges();
                        }
                        var screenHD = new HoaDon(idhd.ID);
                        screenHD.Show();
                        this.Close();
                    }
                } 
            } else
            {
                MessageBox.Show("Hủy thanh toán");
            }
        }
    }
}
