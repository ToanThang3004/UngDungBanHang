using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1760172_1760180
{
    public partial class GHang
    {
        public int ID { get; set; }
        public byte[] ANH { get; set; }
        public string TEN { get; set; }
        public string TENLOAISP { get; set; }
        public double GIA { get; set; }
        public int SoLuongMua { get; set; } = 0;
        public double THANHTIEN { get; set; }
    }
    public partial class SP_FormBanHang
    {
        public int ID { get; set; }
        public byte[] ANH { get; set; }
        public string TENSP { get; set; }
        public string TENLOAISP { get; set; }
        public string TENNHANHANG { get; set; }
        public double GIA { get; set; }
        public int SoLuongTon { get; set; }
        public string TinhTrang { get; set; }
    }
    public partial class SP_FormKhohang
    {
        public int ID { get; set; }
        public byte[] ANH { get; set; }
        public string TENSP { get; set; }
        public string TENLOAISP { get; set; }
        public string TENNHANHANG { get; set; }
        public double GIA { get; set; }
        public int SoLuongTon { get; set; }
    }
}
