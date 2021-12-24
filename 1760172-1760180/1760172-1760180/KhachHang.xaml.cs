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
    /// Interaction logic for KhachHang.xaml
    /// </summary>
    /// 
    public partial class KhachHang : Window
    {
        public string _TenKH { get; set; }
        public string _SDT { get; set; }
        public string _DiaChi { get; set; }
        public string _GhiChu { get; set; }
        public KhachHang()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            var Ten = tbxTenKH.Text;
            var DT = tbxSDT.Text;
            var DC = tbxDiaChi.Text;
            var note = tbxGhiChu.Text;

            _TenKH = Ten;
            _SDT = DT;
            _DiaChi = DC;
            _GhiChu = note;
            this.DialogResult = true;
        }
    }
}
