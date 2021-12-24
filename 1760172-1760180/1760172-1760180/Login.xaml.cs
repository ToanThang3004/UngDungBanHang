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
    /// 
    public partial class MainWindow : Window
    {
        QLNONEntities db = new QLNONEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var Username = tbxUserName.Text;
            var Password = tbxPassword.Password;
            
            if (tbxUserName.Text == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập tài khoản");
            }
            if (tbxPassword.Password == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu");
            }

            var queryCheckTK = (from u in db.TAIKHOANs where u.TENTK == Username select u);
            if (queryCheckTK.Count() == 0)
            {
                MessageBox.Show("Tài khoản " + Username + " không tồn tại");
            } else
            {
                var queryCheckPass = (from p in db.TAIKHOANs where p.MATKHAU == Password select p);
                if (queryCheckPass.Count() == 0)
                {
                    MessageBox.Show("Mật khẩu không đúng");
                } else
                {
                    foreach (var i in queryCheckTK)
                    {
                        string tentk;
                        string hoten;
                   
                        tentk = i.TENTK;
                        hoten = i.HOTEN;
                        var scrMain = new Main(tentk, hoten);
                        scrMain.Show();
                        this.Close();
                    };
                }
            }
           
        }
    }
}
