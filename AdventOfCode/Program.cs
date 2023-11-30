using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AOC2022 {

    public class UI {

        static void Main( string[] args ) {

            int maxDayVal = 0;
            DirectoryInfo[] yrDirs = new DirectoryInfo("../../../../").GetDirectories();

            foreach ( var y in yrDirs ) {
                if ( int.TryParse(y.Name, out int yrVal) ) {
                    DirectoryInfo[] dayDirs = new DirectoryInfo(y.FullName).GetDirectories();
                    foreach ( var d in dayDirs ) {
                        if ( d.Name.StartsWith("Day") ) {
                            int dayVal = yrVal * 100 + int.Parse(d.Name.Substring(3));
                            maxDayVal = Math.Max(maxDayVal, dayVal);
                        }
                    }
                }
            }

            Console.Write($"Day to execute (enter for {maxDayVal}): ");

            string input = Console.ReadLine();

            if ( !String.IsNullOrEmpty(input) ) {
                maxDayVal = int.Parse(input); 
            }

            int day = maxDayVal % 100;
            int year = maxDayVal / 100;

            #if DEBUG

            Assembly asm = Assembly.LoadFrom($"../../../../{year}/Day{day}/bin/Debug/net6.0/Day{day}.dll");
            Type objType = asm.GetType($"AOC{year}.Day{day}");
            DayN exProg = (DayN)Activator.CreateInstance(objType, $"../../../../{year}/Day{day}/example.txt");
            exProg.Debug = true;
            exProg.Run();

            #else

            Assembly asm = Assembly.LoadFrom($"../../../../{year}/Day{day}/bin/Release/net6.0/Day{day}.dll");
            Type objType = asm.GetType($"AOC{year}.Day{day}");
            DayN prodProg = (DayN)Activator.CreateInstance(objType, $"../../../../{year}/Day{day}/input.txt");
            prodProg.Debug = false;
            prodProg.Run();

            #endif

        }

    }

}
