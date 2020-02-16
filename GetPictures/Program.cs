using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace GetPictures
{
    class Program
    {
        static void Main(string[] args)
        {
            //GeneratePack();
            GenerateCaptions();
            Console.WriteLine("Hello World!");
        }

        // Генерация кода метода генерации колоды
        private static void GeneratePack()
        {
            string path = @"C:\Project\Dev\Src\Dobble\GetPictures\Files";
            string fileName = path + @"\Dobble.xlsx";
            string arrayString = string.Empty;
            string arrayAllStrings = string.Empty;
            FileInfo fileInfo = new FileInfo(fileName);
            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            for (int col = 4; col <= 60; col++)
            {
                arrayString = string.Empty;
                for (int row = 2; row <= 58; row++)
                {
                    if (worksheet.Cells[row, col].Value != null)
                    {
                        if (worksheet.Cells[row, col].Value.ToString() == "X")
                        {
                            arrayString += arrayString.Length > 0 ? "," : string.Empty;
                            arrayString += (row - 1).ToString();
                        }
                    }
                }
                arrayAllStrings += "new DobbleCard() {Counter = 8, Pictures = new int[] { " + arrayString + "} }";
                arrayAllStrings += col < 60 ? @",
" : string.Empty;
            }

            string outputFileName = path + @"\DobblePack.txt";
            using (FileStream outputFileStream = File.OpenWrite(outputFileName))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(arrayAllStrings);
                outputFileStream.Write(array, 0, array.Length);
            }
        }

        // Загрузка файлов с картинками
        private void DoanloadFiles()
        {
            string path = @"C:\Project\Dev\Git\Dobble22\GetPictures\Files";
            string fileName = path + @"\Dobble.xlsx";
            string content;
            string fileName2;
            FileInfo fileInfo = new FileInfo(fileName);
            ExcelPackage package = new ExcelPackage(fileInfo);
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
            var wc = new WebClient();
            for (int i = 2; i <= 58; i++)
            {
                content = worksheet.Cells[i, 3].Formula;
                content = content.Replace(@"IMAGE(""", string.Empty).Replace(@""")", string.Empty);
                fileName2 = path + @"\" + (i - 1).ToString() + ".png";
                wc.DownloadFile(content, fileName2);
            }
        }

        // Генерация файлов с надписями
        private static void GenerateCaptions()
        {
            string path = @"C:\Project\Dev\Git\Dobble22\GetPictures\Files";
            var font = new Font(new FontFamily("Trebuchet MS"), 20.0f);
            string phrase = "якорь, яблоко, птичка, бомба, бутылочка, лампочка, кактус, свеча, машина, морковь, кот, ромашка, сыр, часы, клевер, клоун, динозавр, доббль, собака, дельфин, дракон, капля, восклицательный знак, глаз, огонь, привидение, очки, молоток, сердце, конь, человек, лёд, иглу, ключ, божья коровка, кленовый лист, молния, губы, замок, луна, карандаш, вопросительный знак, ножницы, снеговик, череп, снежинка, паук, клякса, знак стоп, солнце, мишень, скрипичный ключ, дерево, черепаха, паутина, знак инь-янь, зебра";
            string[] words = phrase.Split(", ");
            int i = 0;
            foreach (var word in words)
            {
                i++;
                String2Img.DrawText(word,font,Color.Black,200, path + @"\" + i.ToString() + ".png");
            }
        }
    }
}
