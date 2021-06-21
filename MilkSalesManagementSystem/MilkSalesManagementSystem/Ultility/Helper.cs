using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using MilkSalesManagementSystem.Modals;

namespace MilkSalesManagementSystem.Ultility
{
    class Helper<T> where T : class
    {
        public static T ReadFile(string filename)
        {
            var fullpath = Path.Combine(Common.FilePath, filename);
            var data = "";
            using (StreamReader sr = File.OpenText(fullpath))
            {
                data = sr.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void WriteFile(string filename, object data)
        {
            var serializeObject = JsonConvert.SerializeObject(data);
            var fullpath = Path.Combine(Common.FilePath, filename);
            using (StreamWriter sw = File.CreateText(fullpath))
            {
                sw.Write(serializeObject);
            }
        }
        public static void ThemHang(List<Item> items, out Decimal sum)
        {
            int count = 1;
            var result = Helper<Cart>.ReadFile("Order.json");
            string choice = "";
            bool check = false;
            do
            {
                Console.Write("Mời bạn nhập tên sản phẩm cần mua: ");
                string name = Console.ReadLine();

                foreach (Item item in items)
                {
                    if (item.TenSp.ToLower() == name.ToLower())
                    {
                        check = true;
                    }
                }
                Console.Write("Nhập số lượng cần mua: ");
                int sl = int.Parse(Console.ReadLine());
                for (int i = 0; i < result.items.Count; i++)
                {
                    if (result.items[i].TenSp.ToLower() == name.ToLower())
                    {
                        if (check)
                        {
                            foreach (Item item in items)
                            {
                                if (item.TenSp.ToLower() == name.ToLower())
                                {
                                    item.Soluong += sl;
                                }
                            }
                        }
                        else
                        {
                            items.Add(new Item()
                            {
                                MaSp = count++,
                                TenSp = name,
                                Soluong = sl,
                                Gia = result.items[i].Gia,
                            });
                        }
                    }
                }
                Console.WriteLine("Bạn có muốn mua thêm không?");
                Console.Write("Nhấn c để tiếp tục, k để thoát: ");
                choice = Console.ReadLine();
            }
            while (choice != "k");
            sum = 0;
            foreach (Item item in items)
            {
                sum += item.TongTien;
            }
        }
        public static void XoaMonHang(List<Item> items)
        {
            Item.XemGioHang(items);
            Console.Write("Nhập món hàng cần xóa: ");
            string name = Console.ReadLine();
            bool check = false;
            Decimal total = 0;
            foreach (Item hang in items)
            {
                if (hang.TenSp.ToLower() == name.ToLower())
                {
                    check = true;
                    break;
                }
            }
            if (check == true)
            {
                foreach (Item hang in items)
                {
                    if (hang.TenSp.ToLower() == name.ToLower())
                    {
                        items.Remove(hang);
                        Console.WriteLine("Sản phẩm đã xóa khỏi giỏ hàng.");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Sản phẩm không có trong giỏ hàng");
            }
            foreach (Item hang in items)
            {
                total += hang.TongTien;
            }
            Item.XemGioHang(items);
        }
    }
}
