using System;
using System.Collections.Generic;
using System.Text;

namespace MilkSalesManagementSystem.Modals
{
    class Item
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; }
        public int Soluong { get; set; }
        public Decimal Gia { get; set; }
        public Decimal TongTien => totalmoney();
        public Decimal totalmoney()
        {
            return Soluong * Gia ;
        }
        public override string ToString()
        {
            return $"{MaSp}\t\t{TenSp}\t{Soluong}\t\t{string.Format("{0:0,0}",Gia)}/cái\t\t{string.Format("{0:0,0}",TongTien)}";
        }
        public static void XemGioHang(List<Item> items)
        {
            Console.WriteLine("Mã Sản Phẩm\tTên Sản Phẩm\tSố Lượng\tĐơn Giá\t\tTổng Tiền Sản Phẩm");
            foreach (Item hang in items)
            {
                Console.WriteLine(hang.ToString());
            }
            decimal sum = 0;
            foreach (Item hang in items)
            {
                sum += hang.TongTien;
            }
            Console.WriteLine("Tổng Tiền Giỏ Hàng Là: " + string.Format("{0:0,0}", sum) + "vnd");
        }
    }
}
