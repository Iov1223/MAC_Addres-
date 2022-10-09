using System;
using System.Text.RegularExpressions;

namespace MAC_Address
{
    public class MACAddress
    {
        private string macWindows;
        private Regex regex;
        public bool CheckMAC(string macCisco)
        {
            regex = new Regex(@"(\b[0-9A-Fa-f]{4}[.][0-9A-Fa-f]{4}[.][0-9A-Fa-f]{4})\z");
            MatchCollection matches = regex.Matches(macCisco);
            if (matches.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void macCiscoToMacWindows(string macCisco)
        {
            Console.WriteLine($"Введённый MAC адрес Cisco - {macCisco}");
            regex = new Regex(@"[.]");
            MatchCollection matches = regex.Matches(macCisco);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                    macCisco = macCisco.Replace(match.Value, "");
            }
            else
            {
                Console.WriteLine("Совпадений не найдено");
            }
            macCisco = macCisco.ToUpper();
            macWindows = macCisco;
            string substring = "-";
            for (int i = 2; i < macWindows.Length; i += 3)
            {
                macWindows = macWindows.Insert(i, substring);
            }

            Console.WriteLine($"Преобразованный MAC адрес Cisco в MAC адрес Windows - {macWindows}");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            MACAddress myMAC = new MACAddress();
            try
            {
                if (myMAC.CheckMAC(args[0]))
                {
                    myMAC.macCiscoToMacWindows(args[0]);
                }
                else
                {
                    Console.WriteLine("Неверно введённый аргумент. Пример: 901b.0e94.83a8");
                }
            }
            catch
            {
                Console.Write("Программа требует ввода аргумента командной строки.\n" +
                    "Введите аргумент (MAC address Cisco): ");
                string address = Console.ReadLine();
                if (myMAC.CheckMAC(address))
                {
                    myMAC.macCiscoToMacWindows(address);
                }
                else
                {
                    Console.WriteLine("Неверно введённый аргумент. Пример: 901b.0e94.83a8");
                }
            }
        }
    }
}