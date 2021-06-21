using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using MilkSalesManagementSystem.Modals;
using MilkSalesManagementSystem.Ultility;


namespace MilkSalesManagementSystem
{
    class Program
    {
        const int min = 1;
        const int max = 5;
        const int exitCode = 5;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Process();
        }

        public static void BuildMenu(out int option)
        {
            do
            {
                Console.WriteLine("----Hệ Thống Quản Lý Bán Sữa----");
                Console.WriteLine("1. Thêm vào giỏ hàng");
                Console.WriteLine("2. Xem giỏ hàng");
                Console.WriteLine("3. Xóa mặt hàng");
                Console.WriteLine("4. Tính tiền và in bill");
                Console.WriteLine("5. Exit");
                Console.Write($"Vui lòng chọn một số ({min},{max}):");
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    option = 0;
                }
            }
            while (option < min || option > max);
        }

        public static void Process()
        {
            List<Item> list = new List<Item>();
            Decimal total = 0;
            var selected = 0;
            do
            {
                BuildMenu(out selected);
                Console.Clear();
                switch (selected)
                {
                    case 1:
                        {
                            Helper<Cart>.ThemHang(list, out Decimal sum);
                            var Item_data = Helper<Cart>.ReadFile("Order.json");
                            total = sum;
                            break;
                        }
                    case 2:
                        {
                            Item.XemGioHang(list);
                            break;
                        }
                    case 3:
                        {
                            Helper<Cart>.XoaMonHang(list);
                            break;
                        }
                    case 4:
                        {
                            Helper<Cart>.WriteFile("Bill.json", list);
                            Console.WriteLine("Đã đặt hàng thành công");
                            break;
                        }
                    case 5:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            } while (selected != exitCode);
        }
    }
}
