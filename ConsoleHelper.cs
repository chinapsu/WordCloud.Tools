using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCloud.Tools
{
    class ConsoleHelper
    {
        public static void ConsoleInfo(string info, ConsoleColor? foreColor = null)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = foreColor ?? color;
            Console.WriteLine(info);
            Console.ForegroundColor = color;
        }

        public static bool AskYesOrNot(string question)
        {
            Console.Write(question + "[Y/N]：");
            var isYes = new[] { "yes", "y" }.Contains(Console.ReadLine()?.ToLower());
            return isYes;
        }
    }
}
