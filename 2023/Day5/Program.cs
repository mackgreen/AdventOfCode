using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace AOC2023 {
    public class Day5 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day5 prog = new Day5("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day5( string file ) {
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

                List<long> nums = lines[0].Split(' ').Where(x => long.TryParse(x, out _)).Select(long.Parse).ToList();
                List<long> next = new List<long>();

                foreach ( var num in nums ) {
                    next.Add(0);
                }

                foreach ( var line in lines ) {
                    if ( string.IsNullOrEmpty(line) ) {
                        for ( int i = 0; i < nums.Count; i++ ) {
                            if ( next[i] != 0 ) {
                                nums[i] = next[i];
                            }
                            next[i] = 0;
                        }
                    }
                    else if ( line[0] >= '0' && line[0] <= '9' ) {
                        List<long> map = line.Split(' ').Where(x => long.TryParse(x, out _)).Select(long.Parse).ToList();

                        for ( int i = 0; i < nums.Count; i++ ) {


                            long delta = nums[i] - map[1];
                            if (next[i] == 0 && delta >= 0 && delta <= map[2]) {
                                next[i] = map[0] + delta; 
                            }
                        }
                    }
                }

                return nums.Min();                
            }

            public long part2() {
                long retVal = 0;

                List<long> nums = lines[0].Split(' ').Where(x => long.TryParse(x, out _)).Select(long.Parse).ToList();
                List<range> ranges = new List<range>();

                for ( int i = 0; i < nums.Count; i+= 2 ) {
                    range r = new range();
                    r.start = nums[i];
                    r.count = nums[i + 1];
                    r.isSet = false;
                    ranges.Add(r);
                } 

                foreach (var line in lines) {
                    if ( string.IsNullOrEmpty(line) ) {
                        foreach ( range r in ranges ) {
                            r.isSet = false;
                        }
                    }
                    else if (line[0] >= '0' && line[0] <= '9') {
                        Console.WriteLine(line);
                        List<long> map = line.Split(' ').Where(x => long.TryParse(x, out _)).Select(long.Parse).ToList();

                        List<range> newRanges = new List<range>();

                        foreach ( var r in ranges ) {
                            if ( r.isSet == false && map[1] <= r.start + r.count && r.start <= map[1] + map[2] ) { //the ranges overlap
                                if ( map[1] <= r.start && map[1] + map[2] < r.start + r.count ) {
                                    range n = new range();
                                    n.start = map[1] + map[2];
                                    n.count = r.count;
                                    r.count = map[1] + map[2] - r.start;
                                    n.count -= r.count;
                                    r.start -= map[1] - map[0];
                                    n.isSet = false;
                                    newRanges.Add(n);
                                    r.isSet = true;
                                }
                                else if ( map[1] >= r.start && map[1] + map[2] > r.start + r.count ) {
                                    range n = new range();
                                    n.start = r.start;
                                    n.count = map[1] - r.start;
                                    r.start = map[0];
                                    r.count -= n.count;
                                    n.isSet = false;
                                    newRanges.Add(n);
                                    r.isSet = true;
                                }
                                else {
                                    r.start -= map[1] - map[0];
                                    r.isSet = true;
                                }
                            }
                        }

                        foreach ( var n in newRanges ) {
                            ranges.Add(n);
                        }

                    }
                }

                return ranges.Min(x => x.start);
            }

            internal class range {
                public long start;
                public long count;
                public bool isSet;
            }

        }
    }

}
