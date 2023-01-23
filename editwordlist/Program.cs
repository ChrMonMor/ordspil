using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace editwordlist
{
    public class Program
    {
        private static readonly string path = @"C:\Users\chri615w\source\repos\ordspil\editwordlist\oldlist\2012dkk.txt";
        static void Main(string[] args)
        {
            
            foreach (string line in WordListDansk(0, 255))
            {
                Console.WriteLine(line);
            }
            Console.ReadLine();

        }
        // virker kun men 2012dansk 
        private static string[] RemoveFlawsFromArray(string[] readText)
        {
            Regex r = new Regex("[0-9]$");
            Array.Sort(readText);
            for (int i = 0; i < readText.Length; i++)
            {
                readText[i] = readText[i].ToLower();
                int j = readText[i].IndexOf(';');
                if (readText[i][j - 1] == '.')
                {
                    j--;
                }
                readText[i] = readText[i].Remove(j, readText[i].Length - j);
                readText[i] = readText[i].Replace("-", "");
                if (readText[i].Any(char.IsDigit))
                {
                    readText[i] = readText[i].Remove(0, 3);
                }
            }
            readText = readText.Distinct().ToArray();
            return readText = readText.Where(x => !x.Contains(" ")).ToArray();
        }
        public static string[] WordListDansk(int min, int max)
        {
            string[] readText = File.ReadAllLines(path);
            readText = RemoveFlawsFromArray(readText);
            return readText = readText.Where(x=>x.Length>min && x.Length<max).ToArray();
        }

    }
}
