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
    /// Interaction logic for AddSP.xaml
    /// </summary>
    public partial class AddSP : Window
    {
        QLNONEntities db = new QLNONEntities();
        public string TenSP { get; set; } = "";
        public int Loai { get; set; } = 0;
        public int ThuongHieu { get; set; } = 0;
        public double Gia { get; set; } = 0;
        public AddSP()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            
            string ten = tbxTenSP.Text;
            int lo = int.Parse(cbxLoaiSP.SelectedValue.ToString());
            int th = int.Parse(cbxThuongHieu.SelectedValue.ToString());
            double gi = double.Parse(tbxGia.Text);

            TenSP = ten;
            Loai = lo;
            ThuongHieu = th;
            Gia = gi;

            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbxLoaiSP.ItemsSource = db.LOAISPs.ToList();
            cbxLoaiSP.DisplayMemberPath = "TENLOAISP";
            cbxLoaiSP.SelectedValuePath = "ID";
            cbxLoaiSP.SelectedIndex = 0;
            cbxThuongHieu.ItemsSource = db.NHANHANGs.ToList();
            cbxThuongHieu.DisplayMemberPath = "TENNHANHANG";
            cbxThuongHieu.SelectedValuePath = "ID";
            cbxThuongHieu.SelectedIndex = 0;
        }
    }
}
