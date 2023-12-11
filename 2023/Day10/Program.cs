using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2023 {
    public class Day10 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day10 prog = new Day10("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day10( string file ) {
            puzzle = new Puzzle(file);
        }

        public override string Part1() {
            return puzzle.part1().ToString();
        }

        public override string Part2() {
            return puzzle.part2().ToString();
        }

        internal class Puzzle {

            List<List<char>> map = new List<List<char>>();
            List<List<char>> path = new List<List<char>>();
            int x = 0;
            int y = 0;
            long area = 0;
            char dir;

            public Puzzle( string file ) {
                foreach ( var line in File.ReadAllLines(file) ) {
                    if ( line.Contains('S') ) {
                        y = map.Count;
                        x = line.IndexOf('S');
                    }
                    map.Add(line.ToCharArray().ToList());
                    path.Add(new string('.', line.Length).ToCharArray().ToList());
                }
            }

            public long part1() {
                long retVal = 0;

                //find first connector to start
                if ( map[y - 1][x] == '|' || map[y-1][x] == '7' || map[y-1][x] == 'F' ) {
                    dir = 'N';
                }
                else if ( map[y][x + 1] == '-' || map[y][x+1] == '7' || map[y][x + 1] == 'J' ) {
                    dir = 'E';
                }
                else if ( map[y + 1][x] == '|' || map[y + 1][x] == 'L' || map[y + 1][x] == 'J' ) {
                    dir = 'S';
                }
                else if ( map[y][x + 1] == '-' || map[y][x + 1] == 'L' || map[y][x + 1] == 'F' ) {
                    dir = 'W';
                }

                path[y][x] = map[y][x];
                int lastRow = y;
                updatePositionAndDirection();
                retVal++;

                while ( map[y][x] != 'S' ) {
                    area += x * (y - lastRow);
                    lastRow = y;
                    path[y][x] = map[y][x];
                    updatePositionAndDirection();
                    retVal++;
                }

                area += x * (y - lastRow);
                area = Math.Abs(area) - (retVal / 2) + 1;

                return retVal / 2;
            }

            public long part2() {
                return area;
            }

            private void updatePositionAndDirection() {
                if ( map[y][x] == 'L' ) {
                    dir = dir == 'S' ? 'E' : 'N';
                }
                else if ( map[y][x] == 'J' ) {
                    dir = dir == 'S' ? 'W' : 'N'; 
                }
                else if ( map[y][x] == '7' ) {
                    dir = dir == 'N' ? 'W' : 'S';
                }
                else if ( map[y][x] == 'F' ) {
                    dir = dir == 'N' ? 'E' : 'S'; 
                }

                if (dir == 'N') {
                    y--;
                }
                else if (dir == 'E') {
                    x++;
                }
                else if ( dir == 'S') {
                    y++;
                } 
                else {
                    x--;
                }
            }

        }
    }

}
