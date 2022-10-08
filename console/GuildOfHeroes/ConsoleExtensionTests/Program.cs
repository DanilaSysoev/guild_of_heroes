using ConsoleExtension.Widgets;
using System;

namespace ConsoleExtensionTests
{
    class Program
    {
        static void Main(string[] args)
        {
            TextLine wL = new TextLine();
            wL.Alignment = Alignment.TopLeft;
            wL.Line = 5;
            wL.Width = Console.WindowWidth;
            wL.Text = "Left Alighnment";
            wL.BackgroundColor = ConsoleColor.Gray;
            wL.ForegroundColor = ConsoleColor.Black;

            TextLine wC = new TextLine();
            wC.Alignment = Alignment.MiddleCenter;
            wC.Line = 6;
            wC.Width = Console.WindowWidth;
            wC.Text = "Center Alighnment";
            wC.BackgroundColor = ConsoleColor.Gray;
            wC.ForegroundColor = ConsoleColor.DarkBlue;
            
            TextLine wR = new TextLine();
            wR.Alignment = Alignment.BottomRight;
            wR.Line = 7;
            wR.Width = Console.WindowWidth;
            wR.Text = "Right Alighnment";

            wL.Draw();
            wC.Draw();
            wR.Draw();

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
