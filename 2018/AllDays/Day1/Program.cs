using System;
using System.Collections.Generic;
using System.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution1();
            Console.ReadKey();
            Solution2();
            Console.ReadKey();
        }

        public static void Solution1()
        {
            string[] lines = File.ReadAllLines("TextFile1.txt");
            int sum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                sum += int.Parse(lines[i]);
            }

            Console.WriteLine(sum);
        }

        public static void Solution2()
        {
            string[] lines = File.ReadAllLines("TextFile2.txt");
            int sum = 0;
            int i = 0;
            HashSet<int> set = new HashSet<int>();

            do
            {
                sum += int.Parse(lines[i++ % lines.Length]);
            }
            while (set.Add(sum));

            Console.WriteLine(sum);
        }
    }
}
