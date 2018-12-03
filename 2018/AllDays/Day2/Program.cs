using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution1();
            Console.ReadKey();
            //Solution2();
            Console.ReadKey();
        }

        public static void Solution1()
        {
            string[] lines = File.ReadAllLines("TextFile1.txt");
            int twice = 0;
            int thrice  = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                bool hasTwice = false;
                bool hasThrice = false;

                string str = lines[i];

                while (!string.IsNullOrEmpty(str))
                {
                    int length = str.Length;
                    str = str.Replace(str[0] + "", "");
                    hasTwice |= (length - str.Length) == 2;
                    hasThrice |= (length - str.Length) == 3;
                }

                if (hasTwice)
                    twice++;
                if (hasThrice)
                    thrice++;
            }

            Console.WriteLine("" + twice + " * " + thrice + " = " + (twice * thrice));
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
