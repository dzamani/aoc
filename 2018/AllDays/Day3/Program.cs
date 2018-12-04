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
            Solution2();
            Console.ReadKey();
        }

        public static void Solution1()
        {
            string[] lines = File.ReadAllLines("TextFile1.txt");
            const int size = 1000;
            Regex r = new Regex("#(?'id'\\d+) @ (?'left'\\d+),(?'top'\\d+): (?'width'\\d+)x(?'height'\\d+)");
            int[] array = new int[size * size];
            HashSet<int> overlappingSquares = new HashSet<int>();

            for (int i = 0; i < lines.Length; i++)
            {
                Match match = r.Match(lines[i]);
                GroupCollection groups = match.Groups;
                int id = int.Parse(groups[1].Value);
                int leftOffset = int.Parse(groups[2].Value);
                int topOffset = int.Parse(groups[3].Value);
                int width = int.Parse(groups[4].Value);
                int height = int.Parse(groups[5].Value);
                int offset = topOffset * size + leftOffset;

                for (int j = 0; j < width * height; j++)
                {
                    array[offset + (j % width)] += 1;
                    if (array[offset + (j % width)] > 1)
                        overlappingSquares.Add(offset + (j % width));
                    offset = (topOffset + (j + 1) / width) * size + leftOffset;
                }
            }

            Console.WriteLine(overlappingSquares.Count);
        }

        public static void Solution2()
        {
            string[] lines = File.ReadAllLines("TextFile1.txt");
            const int size = 1000;
            Regex r = new Regex("#(?'id'\\d+) @ (?'left'\\d+),(?'top'\\d+): (?'width'\\d+)x(?'height'\\d+)");
            int[] array = new int[size * size];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = -1;
            }

            List<int> notOverlappingIds = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {
                Match match = r.Match(lines[i]);
                GroupCollection groups = match.Groups;
                int id = int.Parse(groups[1].Value);
                int leftOffset = int.Parse(groups[2].Value);
                int topOffset = int.Parse(groups[3].Value);
                int width = int.Parse(groups[4].Value);
                int height = int.Parse(groups[5].Value);
                int offset = topOffset * size + leftOffset;
                notOverlappingIds.Add(id);

                for (int j = 0; j < width * height; j++)
                {
                    if (array[offset + (j % width)] > 0)
                    {
                        notOverlappingIds.Remove(array[offset + (j % width)]);
                        notOverlappingIds.Remove(id);
                    }
                    array[offset + (j % width)] = id;
                    offset = (topOffset + (j + 1) / width) * size + leftOffset;
                }
            }

            Console.WriteLine(notOverlappingIds[0]);
        }
    }
}
