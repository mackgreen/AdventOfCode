using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2023 {
    public class Day2 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day2 prog = new Day2("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day2( string file ) {
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

            public Puzzle(string file) {
                lines = File.ReadAllLines(file);
            }

            public int part1() {
                int retVal = 0;

                foreach ( var line in lines ) {
                    bool valid = true;
                    var game = line.Split(": ");
                    var draws = game[1].Split("; ");
                    foreach ( var draw in draws ) {
                        var cubes = draw.Split(", ");
                        foreach ( var cube in cubes ) {

                            var parts = cube.Split(" ");
                            var cnt = int.Parse(parts[0]);

                            if ( cube.Contains("red") && cnt > 12 ) {
                                valid = false;
                            }
                            else if ( cube.Contains("green") && cnt > 13 ) {
                                valid = false;
                            }
                            else if ( cube.Contains("blue") && cnt > 14 ) {
                                valid = false;
                            }
                        }
                    }

                    if ( valid ) {
                        var parts = game[0].Split(" ");
                        retVal += int.Parse(parts[1]);
                    }

                }

                return retVal;
            }

            public int part2() {
                int retVal = 0;

                foreach (var line in lines) {

                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    var game = line.Split(": ");
                    var draws = game[1].Split("; ");
                    foreach (var draw in draws) {
                        var cubes = draw.Split(", ");
                        foreach (var cube in cubes) {

                            var parts = cube.Split(" ");
                            var cnt = int.Parse(parts[0]);

                            if (cube.Contains("red")) {
                                red = Math.Max(red, cnt);
                            }
                            else if (cube.Contains("green")) {
                                green = Math.Max(green, cnt);
                            }
                            else if (cube.Contains("blue")) {
                                blue = Math.Max(blue, cnt);
                            }
                        }
                    }

                    retVal += (red * green * blue);

                }

                return retVal;
            }

        }
    }

}
