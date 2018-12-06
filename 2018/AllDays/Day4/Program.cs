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

        class Guard
        {
            public Dictionary<int, int> Times;
            public int TotalSleep;
            public int SleepyTime;
        }

        public static void Solution1()
        {
            string[] lines = File.ReadAllLines("TextFile1.txt");
            BubbleSort(lines);
            Regex r = new Regex("\\[\\d{4}-\\d{2}-\\d{2} (?'hour'\\d{2}):(?'min'\\d{2})\\] (?>(?'wake'wakes)|(?'fall'falls)|(?>Guard #)(?'guardid'\\d+))");
            Dictionary<string, Guard> guards = new Dictionary<string, Guard>();
            string currentGuard = null;
            string sleepyGuard = null;
            int lastTimeAwake = 0;
            bool isSleeping = false;

            for (int i = 0; i < lines.Length; i++)
            {
                Match match = r.Match(lines[i]);
                GroupCollection groups = match.Groups;

                if (groups["guardid"].Success)
                {
                    string nextGuard = groups["guardid"].Value;
                    if (!guards.ContainsKey(nextGuard))
                    {
                        Guard g = new Guard()
                        {
                            Times = new Dictionary<int, int>(),
                            SleepyTime = -1,
                            TotalSleep = 0
                        };
                        guards.Add(nextGuard, g);
                    }
                    int newTimeAwake = (groups["hour"].Value != "00") ? 0 : int.Parse(groups["min"].Value);

                    if (sleepyGuard == null)
                        sleepyGuard = nextGuard;
                    currentGuard = nextGuard;
                    lastTimeAwake = newTimeAwake;
                }

                if (groups["fall"].Success)
                {
                    lastTimeAwake = (groups["hour"].Value != "00") ? 0 : int.Parse(groups["min"].Value);
                    isSleeping = true;
                }

                if (groups["wake"].Success)
                {
                    if (isSleeping)
                    {
                        int newTimeAwake = (groups["hour"].Value != "00") ? 0 : int.Parse(groups["min"].Value);
                        Guard g = guards[currentGuard];
                        Guard sg = guards[sleepyGuard];

                        for (int j = lastTimeAwake; j != newTimeAwake; j = (j + 1) % 60)
                        {
                            if (!g.Times.ContainsKey(j))
                                g.Times.Add(j, 0);
                            int value = g.Times[j];
                            g.Times[j] = value + 1;

                            if (g.SleepyTime < 0 || g.Times[g.SleepyTime] < (value + 1))
                                g.SleepyTime = j;
                            g.TotalSleep++;
                        }

                        sleepyGuard = g.TotalSleep > sg.TotalSleep ? currentGuard : sleepyGuard;
                    }

                    isSleeping = false;
                }
            }

            Console.WriteLine(int.Parse(sleepyGuard) * guards[sleepyGuard].SleepyTime);
        }

        public static void BubbleSort(string[] array)
        {
            int n = array.Length;
            do
            {
                int newn = 0;
                for (int i = 1; i < n; i++)
                {
                    if (string.Compare(array[i].Substring(0, 18), array[i - 1].Substring(0, 18)) < 0)
                    {
                        string oldStr = array[i - 1];
                        string newStr = array[i];
                        array[i - 1] = newStr;
                        array[i] = oldStr;
                    }
                    newn = i;
                }
                n = newn;
            } while (n != 0);
        }

        public static void Solution2()
        {
            string[] lines = File.ReadAllLines("TextFile1.txt");
            BubbleSort(lines);
            Regex r = new Regex("\\[\\d{4}-\\d{2}-\\d{2} (?'hour'\\d{2}):(?'min'\\d{2})\\] (?>(?'wake'wakes)|(?'fall'falls)|(?>Guard #)(?'guardid'\\d+))");
            Dictionary<string, Guard> guards = new Dictionary<string, Guard>();
            string currentGuard = null;
            string sleepyGuard = null;
            int lastTimeAwake = 0;
            bool isSleeping = false;

            for (int i = 0; i < lines.Length; i++)
            {
                Match match = r.Match(lines[i]);
                GroupCollection groups = match.Groups;

                if (groups["guardid"].Success)
                {
                    string nextGuard = groups["guardid"].Value;
                    if (!guards.ContainsKey(nextGuard))
                    {
                        Guard g = new Guard()
                        {
                            Times = new Dictionary<int, int>(),
                            SleepyTime = -1,
                            TotalSleep = 0
                        };
                        guards.Add(nextGuard, g);
                    }
                    int newTimeAwake = (groups["hour"].Value != "00") ? 0 : int.Parse(groups["min"].Value);

                    if (sleepyGuard == null)
                        sleepyGuard = nextGuard;
                    currentGuard = nextGuard;
                    lastTimeAwake = newTimeAwake;
                }

                if (groups["fall"].Success)
                {
                    lastTimeAwake = (groups["hour"].Value != "00") ? 0 : int.Parse(groups["min"].Value);
                    isSleeping = true;
                }

                if (groups["wake"].Success)
                {
                    if (isSleeping)
                    {
                        int newTimeAwake = (groups["hour"].Value != "00") ? 0 : int.Parse(groups["min"].Value);
                        Guard g = guards[currentGuard];
                        Guard sg = guards[sleepyGuard];

                        for (int j = lastTimeAwake; j != newTimeAwake; j = (j + 1) % 60)
                        {
                            if (!g.Times.ContainsKey(j))
                                g.Times.Add(j, 0);
                            int value = g.Times[j];
                            g.Times[j] = value + 1;

                            if (g.SleepyTime < 0 || g.Times[g.SleepyTime] < (value + 1))
                                g.SleepyTime = j;
                            g.TotalSleep++;
                        }

                        sleepyGuard = g.Times[g.SleepyTime] > sg.Times[sg.SleepyTime] ? currentGuard : sleepyGuard;
                    }

                    isSleeping = false;
                }
            }

            Console.WriteLine(int.Parse(sleepyGuard) * guards[sleepyGuard].SleepyTime);
        }
    }
}
