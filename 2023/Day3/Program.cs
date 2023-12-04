using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace AOC2023 {
    public class Day3 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day3 prog = new Day3("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day3( string file ) {
            puzzle = new Puzzle(file);
        }

        public override string Part1() {
            return puzzle.part1().ToString();
        }

        public override string Part2() {
            return puzzle.part2().ToString();
        }

        internal class Puzzle {

            List<string> lines;

            public Puzzle( string file ) {
                lines = File.ReadAllLines(file).ToList();
            }

            public int part1() {
                int retVal = 0;

                Regex rx = new Regex(@"(\d+)", RegexOptions.Compiled);
                Regex rx2 = new Regex(@"([^\.\d])", RegexOptions.Compiled);

                for ( int i = 0; i < lines.Count; i++ ) {
                    foreach ( Match match in rx.Matches(lines[i]) ) {
                        int start = match.Index;
                        int length = match.Length;

                        if ( start + length < lines.Count ) {
                            length++;
                        }

                        if ( start > 0 ) {
                            start--;
                            length++;
                        }

                        string window = lines[i].Substring(start, length);
                        
                        if ( i > 0 ) {
                            window += lines[i - 1].Substring(start, length);
                        }

                        if ( i < lines.Count - 1 ) {
                            window += lines[i + 1].Substring(start, length);
                        }

                        if ( rx2.IsMatch(window) ) {
                            retVal += int.Parse(match.Value);
                        }
                    }
                }

                return retVal;
            }

            public long part2() {
                long retVal = 0;

                Regex rx = new Regex(@"(\*)", RegexOptions.Compiled);
                Regex rx2 = new Regex(@"(\d+)", RegexOptions.Compiled);

                for (int i = 0; i < lines.Count; i++) {
                    foreach (Match match in rx.Matches(lines[i])) {
                        List<int> vals = new List<int>();
                        foreach (Match digits in rx2.Matches(lines[i - 1])) {
                            int start = match.Index - 1;
                            int length = match.Length + 1;
                            if (match.Index >= (digits.Index - 1) && match.Index <= (digits.Index + digits.Length)) {
                                vals.Add(int.Parse(digits.Value));
                            }
                        }
                        foreach (Match digits in rx2.Matches(lines[i])) {
                            int start = match.Index - 1;
                            int length = match.Length + 1;
                            if (match.Index >= (digits.Index - 1) && match.Index <= (digits.Index + digits.Length)) {
                                vals.Add(int.Parse(digits.Value));
                            }
                        }
                        foreach (Match digits in rx2.Matches(lines[i + 1])) {
                            int start = match.Index - 1;
                            int length = match.Length + 1;
                            if (match.Index >= (digits.Index - 1) && match.Index <= (digits.Index + digits.Length)) {
                                vals.Add(int.Parse(digits.Value));
                            }
                        }
                        if ( vals.Count == 2 ) {
                            Console.WriteLine(i);
                            retVal += vals[0] * vals[1];
                        }
                    }
                }

                return retVal;
            }



        }
    }

}
