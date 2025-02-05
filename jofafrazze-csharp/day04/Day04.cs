﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day04
{
    public class Day04
    {
        readonly static string nsname = typeof(Day04).Namespace;
        readonly static string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\" + nsname + "\\input.txt");

        // Day 04: Play bingo with giant squid (no diagonals, RTFM!)

        static List<List<int>> ReadBoards()
        {
            StreamReader reader = File.OpenText(inputPath);
            var list = new List<List<int>>();
            var bingo = new List<int>();
            string line;
            reader.ReadLine();
            reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                var ls = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                bingo.AddRange(ls);
                if (ls.Count == 0)
                {
                    list.Add(bingo);
                    bingo = new List<int>();
                }
            }
            return list;
        }

        static List<int> ReadInts()
        {
            StreamReader reader = File.OpenText(inputPath);
            List<int> list = new List<int>();
            string line = reader.ReadLine();
            list.AddRange(line.Split(',').Select(int.Parse).ToList());
            return list;
        }

        static bool CheckStride(int pos, int stride, List<int> board, List<int> drawn)
        {
            for (int i = 0; i < 5; i++)
                if (!drawn.Contains(board[pos + i * stride]))
                    return false;
            return true;
        }

        static bool CheckBingo(List<int> board, List<int> drawn)
        {
            return CheckStride(0, 5, board, drawn) 
                || CheckStride(1, 5, board, drawn) 
                || CheckStride(2, 5, board, drawn) 
                || CheckStride(3, 5, board, drawn) 
                || CheckStride(4, 5, board, drawn) 
                || CheckStride(0, 1, board, drawn) 
                || CheckStride(5, 1, board, drawn) 
                || CheckStride(10, 1, board, drawn) 
                || CheckStride(15, 1, board, drawn) 
                || CheckStride(20, 1, board, drawn);
        }

        static int Calc(List<int> board, List<int> drawn, int last)
        {
            int a = 0;
            foreach (int i in board)
                if (!drawn.Contains(i))
                    a += i;
            return a * last;
        }

        static Object PartA()
        {
            var boards = ReadBoards();
            var nums = ReadInts();
            var drawn = new List<int>();
            int ans = 0;
            foreach (int i in nums)
            {
                drawn.Add(i);
                foreach (var b in boards)
                {
                    if (CheckBingo(b, drawn))
                    {
                        ans = Calc(b, drawn, i);
                        goto Found;
                    }
                }
            }
            Found:
            Console.WriteLine("Part A: Result is {0}", ans);
            return ans;
        }

        static Object PartB()
        {
            var boards = ReadBoards();
            var nums = ReadInts();
            var drawn = new List<int>();
            int ans = 0;
            foreach (int i in nums)
            {
                var nextBoards = new List<List<int>>();
                drawn.Add(i);
                foreach (var b in boards)
                {
                    if (CheckBingo(b, drawn))
                        ans = Calc(b, drawn, i);
                    else
                        nextBoards.Add(b);
                }
                boards = nextBoards;
                if (boards.Count == 0)
                    break;
            }
            Console.WriteLine("Part B: Result is {0}", ans);
            return ans;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("AoC 2021 - " + nsname + ":");
            var w = System.Diagnostics.Stopwatch.StartNew();
            PartA();
            PartB();
            w.Stop();
            Console.WriteLine("[Execution took {0} ms]", w.ElapsedMilliseconds);
        }

        public static bool MainTest()
        {
            int a = 29440;
            int b = 13884;
            return (PartA().Equals(a)) && (PartB().Equals(b));
        }
    }
}
