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
    /// Interaction logic for BanHang.xaml
    /// </summary>
    /// 

    public partial class BanHang : Window
    {
        public class TTGH
        {
            public int TongSP { get; set; }
        }
        public class cbxItem_LoaiSP
        {
            public int ID { get; set; }
            public string TENLOAISP { get; set; }
        }
        public class cbxItem_ThuongHieu
        {
            public int ID { get; set; }
            public string TENNHANHANG { get; set; }
        }
        List<GHang> listSPGH = new List<GHang>();
        List<SP_FormBanHang> listBanHang = new List<SP_FormBanHang>();
        List<cbxItem_LoaiSP> listcbxItem_LoaiSP = new List<cbxItem_LoaiSP>();
        List<cbxItem_ThuongHieu> listcbxItem_ThuongHieu = new List<cbxItem_ThuongHieu>();
        QLNONEntities db = new QLNONEntities();
        public BanHang()
        {
            InitializeComponent();
        }
        void ShowCbxLoaiSP()
        {
            listcbxItem_LoaiSP.Add(new cbxItem_LoaiSP()
            {
                ID = 0,
                TENLOAISP = "Mặc định"
            });
            foreach (var i in db.LOAISPs)
            {
                listcbxItem_LoaiSP.Add(new cbxItem_LoaiSP()
                {
                    ID = i.ID,
                    TENLOAISP = i.TENLOAISP
                });
            }
            cbxLoaiSP.ItemsSource = listcbxItem_LoaiSP;
            cbxLoaiSP.DisplayMemberPath = "TENLOAISP";
            cbxLoaiSP.SelectedValuePath = "ID";
        }
        void ShowCbxThuongHieu()
        {
            listcbxItem_ThuongHieu.Add(new cbxItem_ThuongHieu()
            {
                ID = 0,
                TENNHANHANG = "Mặc định"
            });
            foreach (var i in db.NHANHANGs)
            {
                listcbxItem_ThuongHieu.Add(new cbxItem_ThuongHieu()
                {
                    ID = i.ID,
                    TENNHANHANG = i.TENNHANHANG
                });
            }
            cbxThuongHieu.ItemsSource = listcbxItem_ThuongHieu;
            cbxThuongHieu.DisplayMemberPath = "TENNHANHANG";
            cbxThuongHieu.SelectedValuePath = "ID";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowCbxLoaiSP();
            ShowCbxThuongHieu();
            var query = from c in db.SANPHAMs
                        from d in db.KHOes
                        from a in db.LOAISPs
                        from b in db.NHANHANGs
                        where c.ID == d.ID & c.LOAISP == a.ID & c.NHANHANG == b.ID
                        select new { c.ID, c.ANH, c.TENSP, a.TENLOAISP, b.TENNHANHANG, c.GIA, d.SLTON };
            foreach (var i in query)
            {
                string ttrang;
                if (i.SLTON == 0)
                {
                    ttrang = "Hết hàng";
                } else
                {
                    ttrang = "Còn hàng";
                }
                listBanHang.Add(new SP_FormBanHang()
                {
                    ID = i.ID,
                    ANH = i.ANH,
                    TENSP = i.TENSP,
                    TENLOAISP = i.TENLOAISP,
                    TENNHANHANG = i.TENNHANHANG,
                    GIA = i.GIA,
                    SoLuongTon = i.SLTON,
                    TinhTrang = ttrang
                });
            }
            lvSanPham.ItemsSource = listBanHang;
            var ttg = new TTGH()
            {
                TongSP = listSPGH.Count()
            };
            this.DataContext = ttg;
           
        }

        private void cbxLoaiSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = int.Parse(cbxLoaiSP.SelectedValue.ToString());
            var lsp = db.LOAISPs.Find(id);
            if (id == 0)
            {
                lvSanPham.ItemsSource = listBanHang;
            } else
            {
                List<SP_FormBanHang> _listsp = new List<SP_FormBanHang>();
                foreach (var i in listBanHang)
                {
                        if (i.TENLOAISP == lsp.TENLOAISP)
                        {
                            _listsp.Add(new SP_FormBanHang()
                            {
                                ID = i.ID,
                                ANH = i.ANH,
                                TENSP = i.TENSP,
                                TENLOAISP = i.TENLOAISP,
                                TENNHANHANG = i.TENNHANHANG,
                                GIA = i.GIA,
                                SoLuongTon = i.SoLuongTon,
                                TinhTrang = i.TinhTrang
                            });
                        }
                }
                lvSanPham.ItemsSource = _listsp;
            }
        }

        private void cbxThuongHieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = int.Parse(cbxThuongHieu.SelectedValue.ToString());
            var th = db.NHANHANGs.Find(id);
            if (id == 0)
            {
                lvSanPham.ItemsSource = listBanHang;
            }
            else
            {
                List<SP_FormBanHang> _listsp = new List<SP_FormBanHang>();
                foreach (var i in listBanHang)
                {
                    if (i.TENNHANHANG == th.TENNHANHANG)
                    {
                        _listsp.Add(new SP_FormBanHang()
                        {
                            ID = i.ID,
                            ANH = i.ANH,
                            TENSP = i.TENSP,
                            TENLOAISP = i.TENLOAISP,
                            TENNHANHANG = i.TENNHANHANG,
                            GIA = i.GIA,
                            SoLuongTon = i.SoLuongTon,
                            TinhTrang = i.TinhTrang
                        });
                    }
                }
                lvSanPham.ItemsSource = _listsp;
            }
        }

        private void themVaoGioHang_Click(object sender, RoutedEventArgs e)
        {
            var sp = lvSanPham.SelectedItem as SP_FormBanHang;
            if (sp.SoLuongTon == 0)
            {
                MessageBox.Show("Sản phẩm này đã hết hàng");
                return;
            }
            foreach (var x in listSPGH)
            {
                if (x.ID == sp.ID)
                {
                    if (x.SoLuongMua == sp.SoLuongTon)
                    {
                        MessageBox.Show("Số hàng trong kho không đủ đáp ứng");
                        return;
                    }
                }
            }
            if (listSPGH.Count() < 1)
            {
                listSPGH.Add(new GHang()
                {
                    ID = sp.ID,
                    TEN = sp.TENSP,
                    ANH = sp.ANH,
                    TENLOAISP = sp.TENLOAISP,
                    GIA = sp.GIA,
                    SoLuongMua = 1,
                    THANHTIEN = sp.GIA
                });
            } else
            {
                int count = 0;
                foreach (var i in listSPGH)                   
                {
                    if (sp.ID == i.ID)
                    {
                        count += 1;
                    }                     
                }
                if (count > 0)
                {
                    foreach (var i in listSPGH)
                    {
                        if (i.ID == sp.ID)
                        {
                            i.SoLuongMua = i.SoLuongMua + 1;
                            i.THANHTIEN = i.THANHTIEN + sp.GIA;
                        }
                    }
                } else
                {
                    listSPGH.Add(new GHang()
                    {
                        ID = sp.ID,
                        TEN = sp.TENSP,
                        ANH = sp.ANH,
                        TENLOAISP = sp.TENLOAISP,
                        GIA = sp.GIA,
                        SoLuongMua = 1,
                        THANHTIEN = sp.GIA
                    });
                }
            }
       
          
            var ttg = new TTGH()
            {
                TongSP = listSPGH.Count()
            };
            this.DataContext = ttg;
        }

        private void btnGioHang_Click(object sender, RoutedEventArgs e)
        {
            var screen = new GioHang(listSPGH);
            if (screen.ShowDialog() == true)
            {
            }
        }

        private void tbxTenSP_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = tbxTenSP.Text;

            List<SP_FormBanHang> _listsp = new List<SP_FormBanHang>();
            foreach (var i in listBanHang)
            {
                if (i.TENSP.ToLower().Contains(keyword.ToLower()))
                {
                    _listsp.Add(new SP_FormBanHang()
                    {
                        ID = i.ID,
                        ANH = i.ANH,
                        TENSP = i.TENSP,
                        TENLOAISP = i.TENLOAISP,
                        TENNHANHANG = i.TENNHANHANG,
                        GIA = i.GIA,
                        SoLuongTon = i.SoLuongTon,
                        TinhTrang = i.TinhTrang
                    });
                }
            }
            lvSanPham.ItemsSource = _listsp;
        }
    }
}
