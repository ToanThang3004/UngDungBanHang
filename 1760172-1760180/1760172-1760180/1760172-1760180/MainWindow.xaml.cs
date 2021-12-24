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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1760172_1760180
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_DN(object sender, RoutedEventArgs e)
        {
            string tendn = this.tenDN.Text;
            string mk = this.MatKhau.Password;
            if (tendn == "Thang123" && mk == "123")
            {

                var screen = new GiaoDien();
                if (screen.ShowDialog() == true)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                }
            }

            else
                MessageBox.Show("Đăng nhập thất bại!");
        }

        private void button_Thoat(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
