using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        struct BookList
        {
            public string title;
            public int price;
            public int amount;
        }
        static void Main(string[] args)
        {
            int num = 0;
            bool isOK_num = false;
            while (!isOK_num)//輸入筆數防呆
            {
                Console.Write("請輸入欲鍵入的筆數(至少三筆):");
                isOK_num = InputNum(int.Parse(Console.ReadLine()), ref num);

            }

            BookList[] bookList = new BookList[0];
            string[] content;

            bool isOK_Item = false;
            while (!isOK_Item)
            {
                isOK_Item = GetItem(num, out bookList, out content);//輸入所有item
            }

            StringBuilder builder = GetBuilder(bookList);//將結果存入StringBuilder

            try
            {
                SaveFile(builder);//存入檔案
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void SaveFile(StringBuilder builder)
        {
            string path = $"content{DateTime.Now.ToString("MMdd")}.html";//存成HTML檔
            File.WriteAllText(path, builder.ToString(), Encoding.UTF8);//因為有中文，使用UTF8編碼
            Process.Start(path);
        }

        private static StringBuilder GetBuilder(BookList[] bookList)
        {
            StringBuilder builder = new StringBuilder();//建立StringBuilder
            builder.Append(@"<!DOCTYPE html><html lang=""en""><head><meta charset=""UTF - 8""><title>Document</title><style>table,th,td{border:1px solid;border-collapse:collapse;}</style></head><body><table>");
            builder.AppendLine("<tr><th>標題</th><th>價錢</th><th>數量</th></tr>");
            for (int i = 0; i < bookList.Length; i++)//將內容加入StringBuilder
            {
                builder.AppendLine($"<tr><td>{bookList[i].title}</td><td>{bookList[i].price}</td><td>{bookList[i].amount}</td></tr>");
            }
            builder.Append("</table></body></html>");
            return builder;
        }

        private static bool GetItem(int num, out BookList[] bookList, out string[] content)
        {
            bookList = new BookList[num];
            content = new string[num];
            Console.WriteLine("請輸入標題、價錢、數量(用逗號隔開):");
            for (int i = 0; i < content.Length; i++)//存入輸入的內容至字串陣列中
            {

                content[i] = Console.ReadLine();
                string[] item = content[i].Split(',');
                if (item.Length != 3)
                {
                    Console.WriteLine("Enter Again!");
                    return false;
                }
                if (!(int.TryParse(item[2], out bookList[i].amount) && int.TryParse(item[1], out bookList[i].price)))
                {
                    Console.WriteLine("Enter Again!");
                    return false;
                }
                
                bookList[i].title = item[0];
                bookList[i].price = int.Parse(item[1]);
                bookList[i].amount = int.Parse(item[2]);
            }
            return true;
        }

        private static bool InputNum(int num, ref int num1)
        {
            if (num < 3)
            {
                Console.WriteLine("請輸入至少3筆");
                return false;
            }
            num1 = num;
            return true;
        }
    }
}
