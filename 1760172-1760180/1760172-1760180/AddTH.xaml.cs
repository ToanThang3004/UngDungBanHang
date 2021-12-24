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
    /// Interaction logic for AddTH.xaml
    /// </summary>
    public partial class AddTH : Window
    {
        public string TenTH { get; set; } = "";
        public AddTH()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string data = tbxTenTH.Text;
            TenTH = data;

            this.DialogResult = true;
        }
    }
}
