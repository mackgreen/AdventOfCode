using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC2023 {
    public class Day7 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day7 prog = new Day7("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day7( string file ) {
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

                List<hand> hands = new List<hand>();

                foreach ( var line in lines ) {
                    string[] parts = line.Split(' ');
                    hands.Add(new hand(parts[0], long.Parse(parts[1])));
                }

                hands = hands.OrderBy(x => x.value).ToList();

                for ( int i = 0; i < hands.Count; i++ ) {
                    retVal += (i + 1) * hands[i].bid;
                }

                return retVal;
            }

            public long part2() {
                long retVal = 0;

                List<wildhand> hands = new List<wildhand>();

                foreach (var line in lines) {
                    string[] parts = line.Split(' ');
                    hands.Add(new wildhand(parts[0], long.Parse(parts[1])));
                }

                hands = hands.OrderBy(x => x.value).ToList();

                for (int i = 0; i < hands.Count; i++) {
                    retVal += (i + 1) * hands[i].bid;
                }

                return retVal;
            }

            internal class hand {
                public string cards;
                public long bid;
                public long value;

                public hand( string c, long b ) {
                    cards = c.Replace('T', (char)('9' + 1)).Replace('J', (char)('9' + 2)).Replace('Q', (char)('9' + 3)).Replace('K', (char)('9' + 4)).Replace('A', (char)('9' + 5));
                    bid = b;
                    var sorted = cards.GroupBy(x => x).OrderByDescending(x => x.Count());
                    string strVal = "1";
                    if (sorted.ElementAt(0).Count() == 5) {
                        strVal = "7";
                    }
                    else if (sorted.ElementAt(0).Count() == 4) {
                        strVal = "6";
                    }
                    else if (sorted.ElementAt(0).Count() == 3 && sorted.ElementAt(1).Count() == 2) {
                        strVal = "5";
                    }
                    else if (sorted.ElementAt(0).Count() == 3) {
                        strVal = "4";
                    }
                    else if (sorted.ElementAt(0).Count() == 2 && sorted.ElementAt(1).Count() == 2) {
                        strVal = "3";
                    }
                    else if (sorted.ElementAt(0).Count() == 2) {
                        strVal = "2";
                    }
                    strVal = $"{strVal}{(int)cards[0]}{(int)cards[1]}{(int)cards[2]}{(int)cards[3]}{(int)cards[4]}";
                    value = long.Parse(strVal);
                }

            }

            internal class wildhand {
                public string cards;
                public long bid;
                public long value;

                public wildhand( string c, long b ) {
                    cards = c.Replace('T', (char)('9' + 1)).Replace('J', '1').Replace('Q', (char)('9' + 3)).Replace('K', (char)('9' + 4)).Replace('A', (char)('9' + 5));
                    bid = b;
                    int wilds = cards.Count(x => x == '1');
                    string strVal = "1";
                    if (wilds == 5) {
                        strVal = "7";
                    }
                    else {
                        var sorted = Regex.Replace(cards, "1", "").GroupBy(x => x).OrderByDescending(x => x.Count());
                        if (sorted.ElementAt(0).Count() + wilds == 5) {
                            strVal = "7";
                        }
                        else if (sorted.ElementAt(0).Count() + wilds == 4) {
                            strVal = "6";
                        }
                        else if (sorted.ElementAt(0).Count() + wilds == 3 && sorted.ElementAt(1).Count() == 2) {
                            strVal = "5";
                        }
                        else if (sorted.ElementAt(0).Count() + wilds == 3) {
                            strVal = "4";
                        }
                        else if (sorted.ElementAt(0).Count() + wilds == 2 && sorted.ElementAt(1).Count() == 2) {
                            strVal = "3";
                        }
                        else if (sorted.ElementAt(0).Count() + wilds == 2) {
                            strVal = "2";
                        }
                    }
                    strVal = $"{strVal}{(int)cards[0]}{(int)cards[1]}{(int)cards[2]}{(int)cards[3]}{(int)cards[4]}";
                    value = long.Parse(strVal);
                }

            }

        }
    }

}
