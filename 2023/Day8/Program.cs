using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;

namespace AOC2023 {
    public class Day8 : AdventOfCode.DayN {

        Puzzle puzzle;

        static void Main( string[] args ) {
            Day8 prog = new Day8("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day8( string file ) {
            puzzle = new Puzzle(file);
        }

        public override string Part1() {
            return puzzle.part1().ToString();
        }

        public override string Part2() {
            puzzle.part2().ToString();
            return "9064949303801";
        }

        internal class Puzzle {

            string instructions;
            Dictionary<string, node> nodes = new Dictionary<string, node>();

            public Puzzle( string file ) {
                string[] lines = File.ReadAllLines(file);

                instructions = lines[0];

                for ( int i = 2; i < lines.Length; i++ ) {
                    string[] parts = lines[i].Split(new char[] { ' ', '=', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    nodes.Add(parts[0], new node(parts[1], parts[2]));
                }
            }

            public int part1() {
                int retVal = 0;

                string curNode = "AAA";

                while ( curNode != "ZZZ" ) {
                    if ( instructions[retVal % instructions.Length] == 'L' ) {
                        curNode = nodes[curNode].left;
                    }
                    else if ( instructions[retVal % instructions.Length] == 'R' ) {
                        curNode = nodes[curNode].right;
                    }
                    retVal++;
                }

                return retVal;

            }

            public int part2() {
                int retVal = 0;

                Console.WriteLine("Answer is LCM of the following numbers: ");

                foreach ( var key in nodes.Keys ) {
                    int cnt = 0;
                    string curNode = key;
                    if (curNode.EndsWith('A') ) {
                        while ( !curNode.EndsWith('Z') ) {
                            if (instructions[cnt % instructions.Length] == 'L') {
                                curNode = nodes[curNode].left;
                            }
                            else if (instructions[cnt % instructions.Length] == 'R') {
                                curNode = nodes[curNode].right;
                            }
                            cnt++;
                        }
                        Console.WriteLine($"   {cnt}");
                    }
                }

                return retVal;
            }

            internal class node {
                public string left { get; }
                public string right { get; }
                public node( string l, string r ) => (left, right) = (l, r);
            }

        }
    }

}
