using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCodeAnalyser
{
    public class ProgressReporter
    {
        private static object _lock = new object();
        public void ShowProgress(int progress, int total)
        {
            lock (_lock)
            {
                ConsoleColor foreColor = Console.ForegroundColor;
                ConsoleColor backColor = Console.BackgroundColor;
                Console.ForegroundColor = ConsoleColor.White;

                Console.CursorLeft = 0;
                Console.Write("|");
                Console.CursorLeft = 32;
                Console.Write("|");
                Console.CursorLeft = 1;
                double onechunk = 30.0f/total;
                double percent = (100*progress)/total; 
                int position = 1;
                for (int i = 0; i < 31; i++)
                {
                    if (Math.Round(onechunk*progress )>= i)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }

                    Console.Write(getDisplayChar(i, percent));
                }

                Console.CursorLeft = 35;
                Console.ForegroundColor = foreColor;
                Console.BackgroundColor = backColor;
               
                Console.Write("{0} of {1}", progress, total);
                if (progress == total)
                {
                    Console.WriteLine();
                }
            }
           
        }

        public void ShowProgressMesage(int progress, int total, string message)
        {
            lock (_lock)
            {
                ConsoleColor foreColor = Console.ForegroundColor;
                ConsoleColor backColor = Console.BackgroundColor;
                Console.ForegroundColor = ConsoleColor.White;
                
                
                Console.CursorLeft = 0;
                Console.Write("|");
                Console.CursorLeft = 32;
                Console.Write("|");
                Console.CursorLeft = 1;
                double onechunk = 30.0f / total;
                double percent = (100 * progress) / total;
                int messagelength = Console.BufferWidth - (38 + total.ToString().Length + progress.ToString().Length );
                int position = 1;
                for (int i = 0; i < 31; i++)
                {
                    if (Math.Round(onechunk * progress) >= i)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }

                    Console.Write(getDisplayChar(i, percent));
                }

                Console.CursorLeft = 35;
                Console.ForegroundColor = foreColor;
                Console.BackgroundColor = backColor;
                
                Console.Write("\t{0} of {1}\t{2}", progress, total, progress == total ? (total.ToString().Length == 1  ?  "\tDone." : "Done.").PadRight(messagelength - 15, ' ') : message.PadRight(messagelength, ' ').Substring(0, messagelength-15));
                if (progress == total && total.ToString().Length >1)
                {
                    Console.WriteLine();
                }
            }

        }

        public void writeDescription(string desc)
        {
            lock (_lock)
            {
               // Console.WriteLine();
                Console.WriteLine(desc);
            }
        }
        private char getDisplayChar(int position, double percent)
        {
           char[] per = percent.ToString("000").ToCharArray();
            switch (position +1)
            {
                case 15:
                    return (per[0] == '1' ? '1' : ' ');
                case 16:
                    return (per[1] == '0' && per[0] == '0' ? ' ' : per[1]);
                case 17:
                    return (per[2]);
                case 18:
                    return '%';
                default:
                    return ' ';
            }
        }       
    }
}
