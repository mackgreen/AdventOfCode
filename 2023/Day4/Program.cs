using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.X86;

namespace AOC2023 {
    public class Day4 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day4 prog = new Day4("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day4( string file ) {
            puzzle = new Puzzle(file);
        }

        public override string Part1() {
            return puzzle.part1().ToString();
        }

        public override string Part2() {
            return puzzle.part2().ToString();
        }

        internal class Puzzle {

            string[] lines;

            public Puzzle( string file ) {
                lines = File.ReadAllLines(file);
            }

            public long part1() {
                long retVal = 0;

                foreach (var line in lines) {
                    int cnt = 0;
                    string[] nums = line.Split(new char[] { ':', '|' }, StringSplitOptions.RemoveEmptyEntries);
                    List<int> winningNums = new List<int>();
                    foreach (var n in nums[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
                        winningNums.Add(int.Parse(n));
                    }
                    foreach (var n in nums[2].Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
                        if (winningNums.Contains(int.Parse(n))) {
                            cnt++;
                        }
                    }
                    if (cnt > 0) {
                        retVal += (long)Math.Pow(2, cnt - 1);
                    }
                }

                return retVal;
            }

            public long part2() {

                long retVal = 0;
                int[] cardCnt = new int[lines.Length];

                for ( int i = 0; i < lines.Length; i++ ) {
                    cardCnt[i]++;
                    int j = i + 1;
                    string[] nums = lines[i].Split(new char[] { ':', '|' }, StringSplitOptions.RemoveEmptyEntries);
                    List<int> winningNums = new List<int>();
                    foreach (var n in nums[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
                        winningNums.Add(int.Parse(n));
                    }
                    foreach (var n in nums[2].Split(' ', StringSplitOptions.RemoveEmptyEntries)) {
                        if (winningNums.Contains(int.Parse(n))) {
                            cardCnt[j++] += cardCnt[i];
                        }
                    }
                }

                for ( int i = 0; i < cardCnt.Length; i++ ) {
                    retVal += cardCnt[i];
                }

                return retVal;
            }

        }
    }

}
