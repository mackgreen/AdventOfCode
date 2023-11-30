using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2022 {
    public class Day13 : DayN {

        //SignalPackets signalPackets;

        string[] lines;

        static void Main( string[] args ) {
            Day13 prog = new Day13("../../../example.txt");
            prog.Debug = false;
            prog.Run();
        }

        public Day13( string file ) {
            //signalPackets = new SignalPackets(file);
            lines = File.ReadAllLines(file);
        }

        public override string Part1() {
            int sum = 0;
            int idx = 1;

            for ( int i = 0; i < lines.Length; i += 2 ) {

                if ( checkCorrectNess(lines[i], lines[++i]) ) {
                    sum += idx;
                }

                //Console.WriteLine($"{idx} : {sum}");
                idx++;

            }

            return sum.ToString();
        }

        public override string Part2() {

            int two = 1;
            int six = 2;

            for (int i = 0; i < lines.Length; i++) {
                if (!String.IsNullOrWhiteSpace(lines[i])) {
                    if ( checkCorrectNess(lines[i], "[[2]]") ) {
                        two++;
                    }
                    if (checkCorrectNess(lines[i], "[[6]]")) {
                        six++;
                    }
                }

            }

            return (two * six).ToString();

        }

        public bool checkCorrectNess( string left, string right ) {

            //Console.WriteLine($"checkCorrectNess( {left}, {right})");

            if ( isInt(left[0]) && isInt(right[0]) ) {

                int l = Int32.Parse(getFullInt(ref left));
                int r = Int32.Parse(getFullInt(ref right));

                if ( l < r ) {
                    return true;
                }
                if ( l > r ) {
                    return false;
                }

                return checkCorrectNess(left, right);

            }

            else if (left[0] == ',' && right[0] == ',') {
                return checkCorrectNess(left.Substring(1), right.Substring(1));
            }

            else if ( left[0] == '[' && right[0] == '[' ) {
                return checkCorrectNess(left.Substring(1), right.Substring(1));
            }

            else if ( left[0] == ']' && right[0] == ']' ) {
                return checkCorrectNess(left.Substring(1), right.Substring(1));
            }

            else if ( left[0] == ']' ) {
                return true;
            }

            else if ( right[0] == ']' ) {
                return false;
            }

            else if ( isInt(left[0]) ) {
                string l = getFullInt(ref left);
                return checkCorrectNess('[' + l + ']' + left, right);
            }

            else {
                string r = getFullInt(ref right);
                return checkCorrectNess(left, '[' + r + ']' + right);
            }

        }

        public bool isInt( char val ) {
            if (val >= '0' && val <= '9') {
                return true;
            }
            return false;
        }

        public string getFullInt(ref string val) {
            string retVal = "";

            while ( isInt(val[0]) ) {
                retVal += val[0];
                val = val.Substring(1);
            }

            if ( val[0] == ',' ) {
                val = val.Substring(1);
            }

            return retVal;

        }

    }

}