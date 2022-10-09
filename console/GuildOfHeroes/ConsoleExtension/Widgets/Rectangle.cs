﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Widgets
{
    public class Rectangle : Widget, IColor
    {
        public bool Filled { get; set; }

        public char TopLeftSymbol     { get; set; }
        public char TopRightSymbol    { get; set; }
        public char BottomLeftSymbol  { get; set; }
        public char BottomRightSymbol { get; set; }
        public char HorizontalSymbol  { get; set; }
        public char VerticalSymbol    { get; set; }

        public Rectangle(
            int line = 0,
            int column = 0,
            int width = 0,
            int height = 0,
            IWidget parent = null
        ) : base(line, column, width, height, parent)
        {
            TopLeftSymbol     = DefaultTopLeftSymbol;
            TopRightSymbol    = DefaultTopRightSymbol;
            BottomLeftSymbol  = DefaultBottomLeftSymbol;
            BottomRightSymbol = DefaultBottomRightSymbol;
            HorizontalSymbol  = DefaultHorizontalSymbol;
            VerticalSymbol    = DefaultVerticalSymbol;
        }

        protected override void DrawOwn()
        {
            DrawCorners();
            DrawHorizontals();
            DrawVerticals();
            if (Filled)
                Fill();
        }

        private void DrawCorners()
        {
            DrawSymbol(ConsoleLine(), ConsoleColumn(), TopLeftSymbol);
            DrawSymbol(ConsoleLine() + Height - 1, ConsoleColumn(), BottomLeftSymbol);
            DrawSymbol(ConsoleLine(), ConsoleColumn() + Width - 1, TopRightSymbol);
            DrawSymbol(ConsoleLine() + Height - 1, ConsoleColumn() + Width - 1, BottomRightSymbol);
        }

        private void DrawSymbol(int line, int column, char symbol)
        {
            if(PositionInsideParent(line, column))
            {
                Console.SetCursorPosition(column, line);
                Console.Write(symbol);
            }
        }

        private void DrawHorizontals()
        {
            int start = Math.Max(ConsoleColumn() + 1, ParentConsoleColumn());
            int end = Math.Min(ConsoleColumn() + Width - 1, ParentConsoleRight());

            if (LineInsideParent(ConsoleLine()))
                DrawHorizintalLine(ConsoleLine(), start, end);

            if (LineInsideParent(ConsoleLine() + Height - 1))
                DrawHorizintalLine(ConsoleLine() + Height - 1, start, end);
        }
        private void DrawHorizintalLine(int line, int column, int end)
        {
            Console.SetCursorPosition(column, line);
            for (int pos = column; pos < end; ++pos)
                Console.Write(HorizontalSymbol);
        }

        private void DrawVerticals()
        {
            int start = Math.Max(ConsoleLine() + 1, ParentConsoleLine());
            int end = Math.Min(ConsoleLine() + Height - 1, ParentConsoleBottom());

            if (ColumnInsideParent(ConsoleColumn()))
                DrawVerticalLine(start, ConsoleColumn(), end);

            if (ColumnInsideParent(ConsoleColumn() + Width - 1))
                DrawVerticalLine(start, ConsoleColumn() + Width - 1, end);
        }
        private void DrawVerticalLine(int line, int column, int end)
        {
            for (int pos = line; pos < end; ++pos)
            {
                Console.SetCursorPosition(column, pos);
                Console.Write(VerticalSymbol);
            }
        }

        private void Fill()
        {
            int startLine = Math.Max(ConsoleLine() + 1, ParentConsoleLine());
            int endLine = Math.Min(ConsoleLine() + Height - 1, ParentConsoleBottom());
            int startColumn = Math.Max(ConsoleColumn() + 1, ParentConsoleColumn());
            int endColumn = Math.Min(ConsoleColumn() + Width - 1, ParentConsoleRight());

            for(int line = startLine; line < endLine; ++line)
            {
                Console.SetCursorPosition(startColumn, line);
                for (int column = startColumn; column < endColumn; ++column)
                {
                    Console.Write(' ');
                }
            }
        }

        public const char DefaultTopLeftSymbol     = '+';
        public const char DefaultTopRightSymbol    = '+';
        public const char DefaultBottomLeftSymbol  = '+';
        public const char DefaultBottomRightSymbol = '+';
        public const char DefaultHorizontalSymbol  = '-';
        public const char DefaultVerticalSymbol    = '|';

    }
}
