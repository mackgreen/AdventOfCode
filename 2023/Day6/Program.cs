using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AOC2023 {
    public class Day6 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day6 prog = new Day6("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day6( string file ) {
            puzzle = new Puzzle(file);
        }

        public override string Part1() {
            return puzzle.part1().ToString();
        }

        public override string Part2() {
            return puzzle.part2().ToString();
        }

        internal class Puzzle {

            List<long> times;
            List<long> distances;

            long time;
            long distance;

            public Puzzle( string file ) {
                string[] lines = File.ReadAllLines(file);
                times = lines[0].Split(' ').Where(x => long.TryParse(x, out _)).Select(long.Parse).ToList();
                distances = lines[1].Split(' ').Where(x => long.TryParse(x, out _)).Select(long.Parse).ToList();

                string t = lines[0].Split(": ")[1];
                t = Regex.Replace(t, @"\s+", "");
                time = long.Parse(t);

                string d = lines[1].Split(": ")[1];
                d = Regex.Replace(d, @"\s+", "");
                distance = long.Parse(d);

            }

            public long part1() {
                long retVal = 1;

                for ( int i = 0; i < times.Count; i++ ) {
                    int cnt = 0;
                    for ( int j = 1; j < times[i]; j++ ) {
                        if ( j * (times[i] - j) > distances[i] ) {
                            cnt++;
                        }
                    }
                    retVal *= cnt;
                }

                return retVal;
            }

            public long part2() {
                long retVal = 0;
                Console.WriteLine($"{time} : {distance}");
                for ( int i = 1; i < time; i++ ) {
                    if ( i * (time - i) > distance ) {
                        retVal++;
                    }
                    else if ( retVal > 0 ) {
                        break;
                    }
                }
                return retVal;
            }

        }
    }

}
