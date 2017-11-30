using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCloud.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Console.OutputEncoding = Encoding.Unicode;
            ConsoleHelper.ConsoleInfo("Welcome to yungoal word cloud generater.", ConsoleColor.Gray);
            Console.WriteLine(new string('=', 50));
            ConsoleHelper.ConsoleInfo("utf8 format file 'xxxx.txt' line format:", ConsoleColor.Gray);
            ConsoleHelper.ConsoleInfo(" TextA|Weight", ConsoleColor.Gray);
            ConsoleHelper.ConsoleInfo(" TextB|Weight:", ConsoleColor.Gray);
            while (true)
            {
                try
                {
                    Console.WriteLine(new string('=', 50));
                    ConsoleHelper.ConsoleInfo("Image width:[1400]", ConsoleColor.Green);
                    var width = int.Parse(ReadWithDefault("1400"));
                    ConsoleHelper.ConsoleInfo("Image height:[900]", ConsoleColor.Green);
                    var height = int.Parse(ReadWithDefault("900"));
                    ConsoleHelper.ConsoleInfo("Please input file path: like 'd:\\xxxx.txt':", ConsoleColor.Green);
                    var path = Console.ReadLine();
                    var input = File.ReadAllLines(path, Encoding.UTF8).ToList();
                    input.RemoveAll(s => string.IsNullOrWhiteSpace(s));
                    ConsoleHelper.ConsoleInfo("Drawing, please wait ...", ConsoleColor.Yellow);
                    var outputPath = DrawWordcloud(input, width, height);
                    ConsoleHelper.ConsoleInfo("Draw finish save to: " + outputPath, ConsoleColor.Yellow);
                    Process.Start(outputPath);
                }
                catch (Exception ex) { ConsoleHelper.ConsoleInfo("Error:" + ex.Message, ConsoleColor.Red); }
            }
        }
        static string DrawWordcloud(List<string> input, int width, int height)
        {
            List<string> lstWord = new List<string>();
            List<int> lstWeight = new List<int>();
            foreach (var item in input)
            {
                var arr = item.Split('|');
                lstWord.Add(arr[0].Trim());
                lstWeight.Add(int.Parse(arr[1].Trim()));
            }
            global::WordCloud.WordCloud word = new global::WordCloud.WordCloud(width, height, true);
            var img = word.Draw(lstWord, lstWeight);
            var tempDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
            if (!Directory.Exists(tempDir)) Directory.CreateDirectory(tempDir);
            var output = Path.Combine(tempDir, DateTime.Now.ToString("yyyy-MM-dd.HHmmss") + ".png");
            img.Save(output, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();
            return output;
        }
        static string ReadWithDefault(string defaultValue)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return defaultValue;
            }
            return input.Trim();
        }
    }
}
