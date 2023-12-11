using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;

namespace AOC2023 {
    public class Day11 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day11 prog = new Day11("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day11( string file ) {
            puzzle = new Puzzle(file);
        }

        public override string Part1() {
            return puzzle.part1().ToString();
        }

        public override string Part2() {
            return puzzle.part2().ToString();
        }

        internal class Puzzle {

            List<List<char>> map = new();

            List<int> emptyX = new();
            List<int> emptyY = new();

            public Puzzle( string file ) {
                foreach ( var line in File.ReadAllLines(file) ) {
                    if ( !line.Contains('#') ) {
                        emptyX.Add(map.Count);
                    }
                    map.Add(line.ToCharArray().ToList());
                }
                for ( int i = 0; i < map[0].Count; i++ ) {
                    bool addCol = true;
                    for ( int j = 0; j < map.Count; j++ ) {
                        if ( map[j][i] == '#' ) {
                            addCol = false;
                            break;
                        }
                    }
                    if (addCol) {
                        emptyY.Add(i);
                    }
                }
            }

            public long part1() {
                long retVal = 0;

                List<(int x, int y)> coords = new();

                for ( int i = 0; i < map.Count; i++ ) {
                    for ( int j = 0; j < map[i].Count; j++ ) {
                        if ( map[i][j] == '#' ) {
                            coords.Add((i, j));
                        }
                    }
                }

                for ( int i = 0; i < coords.Count; i++ ) {
                    for ( int j = i + 1; j < coords.Count; j++ ) {
                        int dist = Math.Abs(coords[i].x - coords[j].x) + getEmptyXBetween(coords[i].x, coords[j].x) + Math.Abs(coords[i].y - coords[j].y) + getEmptyYBetween(coords[i].y, coords[j].y);
                        //Console.WriteLine($"{i} {j} {dist}");
                        retVal += dist;
                    }
                }

                return retVal;
            }

            public long part2() {
                long retVal = 0;

                List<(int x, int y)> coords = new();

                for (int i = 0; i < map.Count; i++) {
                    for (int j = 0; j < map[i].Count; j++) {
                        if (map[i][j] == '#') {
                            coords.Add((i, j));
                        }
                    }
                }

                for (int i = 0; i < coords.Count; i++) {
                    for (int j = i + 1; j < coords.Count; j++) {
                        int dist = Math.Abs(coords[i].x - coords[j].x) + (999999 * getEmptyXBetween(coords[i].x, coords[j].x)) + Math.Abs(coords[i].y - coords[j].y) + (999999 * getEmptyYBetween(coords[i].y, coords[j].y));
                        //Console.WriteLine($"{i} {j} {dist}");
                        retVal += dist;
                    }
                }

                return retVal;
            }

            public int getEmptyXBetween(int start, int stop) {
                int retVal = 0;
                for (int i = Math.Min(start, stop); i < Math.Max(start, stop); i++) {
                    if ( emptyX.Contains(i) ) {
                        retVal++;
                    }
                }
                return retVal;
            }

            public int getEmptyYBetween( int start, int stop ) {
                int retVal = 0;
                for (int i = Math.Min(start, stop); i < Math.Max(start, stop); i++) {
                    if (emptyY.Contains(i)) {
                        retVal++;
                    }
                }
                return retVal;
            }

        }
    }

}
