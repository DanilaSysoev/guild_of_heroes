using ConsoleExtension.Widgets;
using System;

namespace ConsoleExtensionTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //TextLine wL = new TextLine();
            //wL.Alignment = Alignment.TopLeft;
            //wL.Line = 5;
            //wL.Column = -3;
            //wL.Width = 10;
            //wL.Text = "Left Alighnment";
            //wL.BackgroundColor = ConsoleColor.Gray;
            //wL.ForegroundColor = ConsoleColor.Black;

            //TextLine wC = new TextLine();
            //wC.Alignment = Alignment.MiddleCenter;
            //wC.Line = 6;
            //wC.Width = Console.WindowWidth;
            //wC.Text = "Center Alighnment";
            //wC.BackgroundColor = ConsoleColor.Gray;
            //wC.ForegroundColor = ConsoleColor.DarkBlue;

            //TextLine wR = new TextLine();
            //wR.Alignment = Alignment.BottomRight;
            //wR.Line = 7;
            //wR.Column = 5;
            //wR.Width = Console.WindowWidth;
            //wR.Text = "Right Alighnment";

            //wL.Draw();
            //wC.Draw();
            //wR.Draw();

            Rectangle r1 = new Rectangle(5, 8, 20, 10);
            Rectangle r2 = new Rectangle(3, 3, 5, 20);
            r1.Filled = true;


            TextLine wL = new TextLine();
            wL.Alignment = Alignment.TopLeft;
            wL.Line = 4;
            wL.Column = 5;
            wL.Width = 20;
            wL.Text = "Left Alighnment";
            wL.BackgroundColor = ConsoleColor.Gray;
            wL.ForegroundColor = ConsoleColor.Black;
            r1.AddChild(wL);
            r1.AddChild(r2);

            r1.Draw();

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
