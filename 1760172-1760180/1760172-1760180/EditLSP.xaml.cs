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
    /// Interaction logic for EditLSP.xaml
    /// </summary>
    public partial class EditLSP : Window
    {
        public string _ten { get; set; } = "";

        public EditLSP(int id, string ten)
        {
            InitializeComponent();
            tbxID.Text = id.ToString();
            tbxTenLSP.Text = ten;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            string tensp = tbxTenLSP.Text;

            _ten = tensp;
            this.DialogResult = true;
        }
    }
}
