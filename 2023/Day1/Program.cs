using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2023 {

    public class Day1 : AdventOfCode.DayN {

        Trebuchet trebuchet;

        static void Main( string[] args ) {
            Day1 prog = new Day1("../../../input.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day1( string file ) {
            trebuchet = new Trebuchet(file);
        }


        public override string Part1() {
            return trebuchet.calcCalibrationValue().ToString();
        }

        public override string Part2() {
            trebuchet.convertWordsToInt();
            return trebuchet.calcCalibrationValue().ToString();

        }

        internal class Trebuchet {

            string[] lines;

            public Trebuchet( string file ) {
                lines = File.ReadAllLines(file);
            }

            public int calcCalibrationValue() {
                int retVal = 0;
                
                foreach ( var line in lines ) {
                    int first = -1;
                    int last = 0;
                    foreach ( char c in line ) {
                        if ( c >= '0' && c <= '9' ) {
                            if ( first == -1 ) {
                                first = (int)(c - '0') * 10;
                                last = (int)(c - '0');
                            }
                            else {
                                last = (int)(c - '0');
                            }
                        }
                    }
                    Console.WriteLine($"Value: {first + last}");
                    retVal += first + last;
                }

                return retVal;
            }

            public void convertWordsToInt() {

                var charMap = new Dictionary<string, string>();
                charMap.Add("one", "1");
                charMap.Add("two", "2");
                charMap.Add("three", "3");
                charMap.Add("four", "4");
                charMap.Add("five", "5");
                charMap.Add("six", "6");
                charMap.Add("seven", "7");
                charMap.Add("eight", "8");
                charMap.Add("nine", "9");

                Regex rx = new Regex(@"(one|two|three|four|five|six|seven|eight|nine)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                for (uint i = 0; i < lines.Length; i++) {
                    MatchCollection matches = rx.Matches(lines[i]);
                    if ( matches.Count > 0 ) {
                        int place = lines[i].IndexOf(matches[0].Value);
                        lines[i] = lines[i].Remove(place, matches[0].Value.Length).Insert(place, charMap[matches[0].Value]);
                    }
                    if ( matches.Count > 1 ) {
                        int place = lines[i].LastIndexOf(matches[matches.Count - 1].Value);
                        lines[i] = lines[i].Remove(place, matches[matches.Count - 1].Value.Length).Insert(place, charMap[matches[matches.Count - 1].Value]);
                    }
                }

            }

        }
    }

}
