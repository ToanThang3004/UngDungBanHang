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
    /// Interaction logic for ThemSL.xaml
    /// </summary>
    public partial class ThemSL : Window
    {
        public int _SoLuongMua { get; set; } = 0;
        public int _SoLuongTon { get; set; } = 0;
        public ThemSL(int ID, int SoLuongMua, int SLT) 
        {
            InitializeComponent();
            tbxSoLuong.Text = SoLuongMua.ToString();
            _SoLuongTon = SLT;
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            int soluong = int.Parse(tbxSoLuong.Text);
            if (soluong > _SoLuongTon)
            {
                MessageBox.Show("Số lượng yêu cầu vượt quá số lượng sản phẩm có trong kho. Vui lòng nhập lại");
            } else
            {
                _SoLuongMua = soluong;
                this.DialogResult = true;
            }
        }
    }
}
