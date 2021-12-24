using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for QLPhanMem.xaml
    /// </summary>
    public partial class QLPhanMem : Window
    {
        QLNONEntities db = new QLNONEntities();
        
        public QLPhanMem()
        {
            InitializeComponent();
        }
        
        void loadLVItem()
        {
            var query = from c in db.SANPHAMs
                        select c;
            lvSanPham.ItemsSource = query.ToList();
            
        }   

        private void btnThemLSP_Click(object sender, RoutedEventArgs e)
        {
            var screenThemLSP = new AddLSP();
            if (screenThemLSP.ShowDialog() == true)
            {
                string tlsp = screenThemLSP.TenLSP;
                // add no image
                string fileNoImage = AppDomain.CurrentDomain.BaseDirectory;
                fileNoImage = fileNoImage.Replace("bin\\Debug", "Images\\noimage.jpg");
                fileNoImage = fileNoImage.Replace("oimage.jpg\\", "oimage.jpg");

                var image = new BitmapImage(new Uri(fileNoImage, UriKind.Absolute));

                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);

                    var lsp = new LOAISP()
                    {
                        TENLOAISP = tlsp,
                        ANH = stream.ToArray()
                    };
                    db.LOAISPs.Add(lsp);
                    db.SaveChanges();
                }            
             
                MessageBox.Show("Thêm loại sản phẩm thành công");
                lvLoaiSP.ItemsSource = db.LOAISPs.ToList();
            } else
            {
                MessageBox.Show("Việc thêm sản phẩm mới bị hủy");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadLVItem();
            lvLoaiSP.ItemsSource = db.LOAISPs.ToList();
            lvThuongHieu.ItemsSource = db.NHANHANGs.ToList();
            cbxLoaiSP.ItemsSource = db.LOAISPs.ToList();
            cbxLoaiSP.DisplayMemberPath = "TENLOAISP";
            cbxLoaiSP.SelectedValuePath = "ID";
            cbxThuongHieu.ItemsSource = db.NHANHANGs.ToList();
            cbxThuongHieu.DisplayMemberPath = "TENNHANHANG";
            cbxThuongHieu.SelectedValuePath = "ID";
        }

        private void btnThemTH_Click(object sender, RoutedEventArgs e)
        {
            var screenThemTH = new AddTH();
            if (screenThemTH.ShowDialog() == true)
            {
                string th = screenThemTH.TenTH;
                // add no image
                string fileNoImage = AppDomain.CurrentDomain.BaseDirectory;
                fileNoImage = fileNoImage.Replace("bin\\Debug", "Images\\noimage.jpg");
                fileNoImage = fileNoImage.Replace("oimage.jpg\\", "oimage.jpg");

                var image = new BitmapImage(new Uri(fileNoImage, UriKind.Absolute));

                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);


                    var addth = new NHANHANG()
                    {
                        TENNHANHANG = th,
                        ANH = stream.ToArray()
                    };
                    db.NHANHANGs.Add(addth);
                    db.SaveChanges();
                }
               
                MessageBox.Show("Thêm thương hiệu thành công");
                lvThuongHieu.ItemsSource = db.NHANHANGs.ToList();
            }
            else
            {
                MessageBox.Show("Việc thêm thương hiệu mới bị hủy");
            }
        }

        private void btnThemSP_Click(object sender, RoutedEventArgs e)
        {
            var screenThemSP = new AddSP();
            if (screenThemSP.ShowDialog() == true)
            {
                var tensp = screenThemSP.TenSP;
                var loai = screenThemSP.Loai;
                var tth = screenThemSP.ThuongHieu;
                double GiaSP = screenThemSP.Gia;
                               
                // add no image
                string fileNoImage = AppDomain.CurrentDomain.BaseDirectory;
                fileNoImage = fileNoImage.Replace("bin\\Debug", "Images\\noimage.jpg");
                fileNoImage = fileNoImage.Replace("oimage.jpg\\", "oimage.jpg");

                var image = new BitmapImage(new Uri(fileNoImage, UriKind.Absolute));

                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);

                    var sp = new SANPHAM()
                    {
                        TENSP = tensp,
                        LOAISP = loai,
                        NHANHANG = tth,
                        GIA = GiaSP,
                        ANH = stream.ToArray()
                    };
                    db.SANPHAMs.Add(sp);
                    var spkho = new KHO()
                    {
                        SLTON = 0,
                        SLDABAN = 0
                    };
                    db.KHOes.Add(spkho);
                    db.SaveChanges();
                }
                MessageBox.Show("Thêm sản phẩm thành công");
                loadLVItem();
            }
            else
            {
                MessageBox.Show("Việc thêm sản phẩm mới bị hủy");
            }
        }

        private void deleteItem_Click(object sender, RoutedEventArgs e)
        {
            var sp = lvSanPham.SelectedItem as SANPHAM;
            db.SANPHAMs.Remove(sp);
            db.SaveChanges();
            loadLVItem();
            MessageBox.Show("Đã xóa sản phẩm thành công!");
        }

        private void editItem_Click(object sender, RoutedEventArgs e)
        {
            var sp = lvSanPham.SelectedItem as SANPHAM;
            var screenEditSP = new EditSP(sp.ID, sp.TENSP, sp.LOAISP, sp.NHANHANG, sp.GIA);
            if (screenEditSP.ShowDialog() == true)
            {
                var id = screenEditSP._id;
                var tensp = screenEditSP._ten;
                var loaisp = screenEditSP._loai;
                var thuonghieu = screenEditSP._thuonghieu;
                var giasp = screenEditSP._gia;

                SANPHAM editsp = db.SANPHAMs.Find(id);
                editsp.TENSP = tensp;
                editsp.LOAISP = loaisp;
                editsp.NHANHANG = thuonghieu;
                editsp.GIA = giasp;
                db.SaveChanges();
                MessageBox.Show("Chỉnh sửa sản phẩm thành công");
                loadLVItem();
            }
            else
            {
                MessageBox.Show("Việc chính sửa sản phẩm bị hủy");
            }
        }

        private void addImageItem_Click(object sender, RoutedEventArgs e)
        {
            var sp = lvSanPham.SelectedItem as SANPHAM;
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                var filename = screen.FileName;
                var image = new BitmapImage(new Uri(filename, UriKind.Absolute));

                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);

                    var spham = db.SANPHAMs.Find(sp.ID);
                    spham.ANH = stream.ToArray();
                    db.SaveChanges();
                }

                MessageBox.Show("Thêm ảnh thành công");
                loadLVItem();
            }
          
        }

        private void infoItem_Click(object sender, RoutedEventArgs e)
        {
            var sp = lvSanPham.SelectedItem as SANPHAM;
            var screen = new ChiTietSP(sp);
            
            if (screen.ShowDialog() == true)
            {
            }
        }

        private void deleteLSP_Click(object sender, RoutedEventArgs e)
        {
            var lsp = lvLoaiSP.SelectedItem as LOAISP;
            db.LOAISPs.Remove(lsp);
            db.SaveChanges();
            lvLoaiSP.ItemsSource = db.LOAISPs.ToList();
            MessageBox.Show("Đã xóa loại sản phẩm thành công!");
        }

        private void editLSP_Click(object sender, RoutedEventArgs e)
        {
            var lsp = lvLoaiSP.SelectedItem as LOAISP;
            var screen = new EditLSP(lsp.ID, lsp.TENLOAISP);
            if (screen.ShowDialog() == true)
            {
                var tenlsp = screen._ten;
                var _lsp = db.LOAISPs.Find(lsp.ID);
                _lsp.TENLOAISP = tenlsp;
                db.SaveChanges();
                MessageBox.Show("Chỉnh sửa loại sản phẩm thành công");
                lvLoaiSP.ItemsSource = db.LOAISPs.ToList();
            }
        }

        private void addImageLSP_Click(object sender, RoutedEventArgs e)
        {
            var lsp = lvLoaiSP.SelectedItem as LOAISP;
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                var filename = screen.FileName;
                var image = new BitmapImage(new Uri(filename, UriKind.Absolute));

                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);

                    var loaisp = db.LOAISPs.Find(lsp.ID);
                    loaisp.ANH = stream.ToArray();
                    db.SaveChanges();
                }

                MessageBox.Show("Thêm ảnh thành công");
                lvLoaiSP.ItemsSource = db.LOAISPs.ToList();
            }
        }

        private void deleteTH_Click(object sender, RoutedEventArgs e)
        {
            var th = lvThuongHieu.SelectedItem as NHANHANG;
            db.NHANHANGs.Remove(th);
            db.SaveChanges();
            lvThuongHieu.ItemsSource = db.NHANHANGs.ToList();
            MessageBox.Show("Đã xóa thương hiệu thành công!");
        }

        private void editTH_Click(object sender, RoutedEventArgs e)
        {
            var th = lvThuongHieu.SelectedItem as NHANHANG;
            var screen = new EditTH(th.ID, th.TENNHANHANG);
            if (screen.ShowDialog() == true)
            {
                var tenth = screen._ten;
                var _th = db.NHANHANGs.Find(th.ID);
                _th.TENNHANHANG = tenth;
                db.SaveChanges();
                MessageBox.Show("Chỉnh sửa thương hiệu thành công");
                lvThuongHieu.ItemsSource = db.NHANHANGs.ToList();
            }
        }

        private void addImageTH_Click(object sender, RoutedEventArgs e)
        {
            var th = lvThuongHieu.SelectedItem as NHANHANG;
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                var filename = screen.FileName;
                var image = new BitmapImage(new Uri(filename, UriKind.Absolute));

                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);

                    var thuonghieu = db.NHANHANGs.Find(th.ID);
                    thuonghieu.ANH = stream.ToArray();
                    db.SaveChanges();
                }

                MessageBox.Show("Thêm ảnh thành công");
                lvThuongHieu.ItemsSource = db.NHANHANGs.ToList();
            }
        }

        private void cbxLoaiSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = int.Parse(cbxLoaiSP.SelectedValue.ToString());
            if (i >= 0)
            {
                var query = from c in db.SANPHAMs
                            where c.LOAISP == i
                            select c;
                lvSanPham.ItemsSource = query.ToList();
            }
        }

        private void cbxThuongHieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = int.Parse(cbxLoaiSP.SelectedValue.ToString());
            if (i >= 0)
            {
                var query = from c in db.SANPHAMs
                            where c.LOAISP == i
                            select c;
                lvSanPham.ItemsSource = query.ToList();
            }
        }

        private void tbxTimKiemLSP_TextChanged(object sender, TextChangedEventArgs e)
        {
            var kw = tbxTimKiemLSP.Text;
            if (kw == "")
            {
                lvLoaiSP.ItemsSource = db.LOAISPs.ToList();
            } else
            {
                var query = from c in db.LOAISPs
                            where c.TENLOAISP.ToLower().Contains(kw.ToLower())
                            select c;
                lvLoaiSP.ItemsSource = query.ToList();
            }
        }

        private void tbxTimKiemTH_TextChanged(object sender, TextChangedEventArgs e)
        {
            var kw = tbxTimKiemTH.Text;
            if (kw == "")
            {
                lvThuongHieu.ItemsSource = db.NHANHANGs.ToList();
            } else
            {
                var query = from c in db.NHANHANGs
                            where c.TENNHANHANG.ToLower().Contains(kw.ToLower())
                            select c;
                lvThuongHieu.ItemsSource = query.ToList();
            }
           
           
        }
    }
}
