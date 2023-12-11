using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AOC2023 {
    public class Day9 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day9 prog = new Day9("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day9( string file ) {
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

                foreach ( var line in lines ) {
                    List<List<long>> steps = new List<List<long>>();
                    steps.Add(line.Split(' ').Select(long.Parse).ToList());
                    bool run = true;
                    while ( run ) {
                        run = false;
                        List<long> history = new List<long>();
                        for ( int i = 1; i < steps.Last().Count; i++ ) {
                            history.Add(steps.Last().ElementAt(i) - steps.Last().ElementAt(i - 1));
                            if ( history.Last() != 0 ) {
                                run = true;
                            }
                        }
                        steps.Add(history);
                    }

                    long val = 0;
                    for (int i = 0; i < steps.Count(); i++) {
                        val += steps[i].Last();
                    }
                    retVal += val;

                }
                return retVal;
            }

            public long part2() {
                long retVal = 0;

                foreach (var line in lines) {
                    List<List<long>> steps = new List<List<long>>();
                    steps.Add(line.Split(' ').Select(long.Parse).ToList());
                    bool run = true;
                    while (run) {
                        run = false;
                        List<long> history = new List<long>();
                        for (int i = 1; i < steps.Last().Count; i++) {
                            history.Add(steps.Last().ElementAt(i) - steps.Last().ElementAt(i - 1));
                            if (history.Last() != 0) {
                                run = true;
                            }
                        }
                        steps.Add(history);
                    }

                    long val = 0;
                    for (int i = steps.Count - 1; i >= 0; i--) {
                        val = steps[i][0] - val;
                    }

                    retVal += val;

                }
                return retVal;
            }

        }
    }

}
