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
    /// Interaction logic for EditSP.xaml
    /// </summary>
    public partial class EditSP : Window
    {
        QLNONEntities db = new QLNONEntities();
        public int _id { get; set; } = 0;
        public string _ten { get; set; } = "";
        public int _loai { get; set; } = 0;
        public int _thuonghieu { get; set; } = 0;
        public double _gia { get; set; } = 0;

        public EditSP(int id, string ten, int loai, int thuonghieu, double gia)
        {
            InitializeComponent();
            tbxID.Text = id.ToString();
            tbxTenSP.Text = ten;
            cbxLoaiSP.ItemsSource = db.LOAISPs.ToList();
            cbxLoaiSP.DisplayMemberPath = "TENLOAISP";
            cbxLoaiSP.SelectedValuePath = "ID";
            cbxLoaiSP.SelectedIndex = loai-1;
            cbxThuongHieu.ItemsSource = db.NHANHANGs.ToList();
            cbxThuongHieu.DisplayMemberPath = "TENNHANHANG";
            cbxThuongHieu.SelectedValuePath = "ID";
            cbxThuongHieu.SelectedIndex = thuonghieu - 1;
            tbxGia.Text = gia.ToString();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            int idsp = int.Parse(tbxID.Text);
            string tensp = tbxTenSP.Text;
            int loaisp = int.Parse(cbxLoaiSP.SelectedValue.ToString());
            int th = int.Parse(cbxThuongHieu.SelectedValue.ToString());
            double giasp = double.Parse(tbxGia.Text);

            _id = idsp;
            _ten = tensp;
            _loai = loaisp;
            _thuonghieu = th;
            _gia = giasp;

            this.DialogResult = true;
        }
    }
}
