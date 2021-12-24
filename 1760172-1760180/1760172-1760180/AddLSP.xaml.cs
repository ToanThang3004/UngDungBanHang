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
    /// Interaction logic for AddLSP.xaml
    /// </summary>
    public partial class AddLSP : Window
    {
        public string TenLSP { get; set; } = "";
        public AddLSP()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            string data = tbxTenLSP.Text;
            TenLSP = data;

            this.DialogResult = true;
        }
    }
}
