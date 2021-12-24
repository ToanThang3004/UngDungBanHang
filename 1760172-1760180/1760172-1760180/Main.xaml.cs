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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public class user
        {
            public string tentk { get; set; }
            public string hoten { get; set; }
        }
        public Main(string ttk, string ten)
        {
            InitializeComponent();
            var _user = new user()
            {
                tentk = ttk,
                hoten = ten
            };
            this.DataContext = _user;
        }

        private void btnQLPhanMem_Click(object sender, RoutedEventArgs e)
        {
            var screenQLPhanMem = new QLPhanMem();
            screenQLPhanMem.Show();
           
        }

        private void btnQLKhoHang_Click(object sender, RoutedEventArgs e)
        {
            var screenQLPhanMem = new KhoHang();
            screenQLPhanMem.Show();
           
        }

        private void btnBanHang_Click(object sender, RoutedEventArgs e)
        {
            var screenQLPhanMem = new BanHang();
            screenQLPhanMem.Show();
           
        }
    }
}
