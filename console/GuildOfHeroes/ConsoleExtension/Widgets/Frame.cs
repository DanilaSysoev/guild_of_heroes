using System;

namespace ConsoleExtension.Widgets
{
    public class Frame : Widget, IColor
    {
        public bool Filled { get; set; }

        public bool Bordered { get; set; }

        public char TopLeftSymbol { get; set; }
        public char TopRightSymbol { get; set; }
        public char BottomLeftSymbol { get; set; }
        public char BottomRightSymbol { get; set; }
        public char HorizontalSymbol { get; set; }
        public char VerticalSymbol { get; set; }

        public Frame(
            int line = 0,
            int column = 0,
            int width = 0,
            int height = 0,
            IWidget parent = null
        ) : base(line, column, width, height, parent)
        {
            TopLeftSymbol = DefaultTopLeftSymbol;
            TopRightSymbol = DefaultTopRightSymbol;
            BottomLeftSymbol = DefaultBottomLeftSymbol;
            BottomRightSymbol = DefaultBottomRightSymbol;
            HorizontalSymbol = DefaultHorizontalSymbol;
            VerticalSymbol = DefaultVerticalSymbol;

            Bordered = true;
        }

        protected override void DrawOwnBeforeChildren()
        {
            if (Filled)
                Fill();
        }
        protected override void DrawOwnAfterChildren()
        {
            if (Bordered)
            {
                DrawCorners();
                DrawHorizontals();
                DrawVerticals();
            }
        }

        private void DrawCorners()
        {
            DrawSymbol(ConsoleLine(), ConsoleColumn(), TopLeftSymbol);
            DrawSymbol(ConsoleLine() + Area.Height - 1, ConsoleColumn(), BottomLeftSymbol);
            DrawSymbol(ConsoleLine(), ConsoleColumn() + Area.Width - 1, TopRightSymbol);
            DrawSymbol(ConsoleLine() + Area.Height - 1, ConsoleColumn() + Area.Width - 1, BottomRightSymbol);
        }

        private void DrawSymbol(int line, int column, char symbol)
        {
            if (PositionInsideParent(line, column))
            {
                Console.SetCursorPosition(column, line);
                Console.Write(symbol);
            }
        }

        private void DrawHorizontals()
        {
            int start = Math.Max(ConsoleColumn() + 1, ParentConsoleColumn());
            int end = Math.Min(ConsoleColumn() + Area.Width - 1, ParentConsoleRight());

            if (LineInsideParent(ConsoleLine()))
                DrawHorizintalLine(ConsoleLine(), start, end);

            if (LineInsideParent(ConsoleLine() + Area.Height - 1))
                DrawHorizintalLine(ConsoleLine() + Area.Height - 1, start, end);
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
            int end = Math.Min(ConsoleLine() + Area.Height - 1, ParentConsoleBottom());

            if (ColumnInsideParent(ConsoleColumn()))
                DrawVerticalLine(start, ConsoleColumn(), end);

            if (ColumnInsideParent(ConsoleColumn() + Area.Width - 1))
                DrawVerticalLine(start, ConsoleColumn() + Area.Width - 1, end);
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
            int fillBorder = Bordered ? 0 : 1;

            int startLine = Math.Max(ConsoleLine() + 1, ParentConsoleLine()) - fillBorder;
            int endLine = Math.Min(ConsoleLine() + Area.Height - 1, ParentConsoleBottom()) + fillBorder;
            int startColumn = Math.Max(ConsoleColumn() + 1, ParentConsoleColumn()) - fillBorder;
            int endColumn = Math.Min(ConsoleColumn() + Area.Width - 1, ParentConsoleRight()) + fillBorder;

            for (int line = startLine; line < endLine; ++line)
            {
                Console.SetCursorPosition(startColumn, line);
                for (int column = startColumn; column < endColumn; ++column)
                {
                    Console.Write(' ');
                }
            }
        }

        public const char DefaultTopLeftSymbol = '+';
        public const char DefaultTopRightSymbol = '+';
        public const char DefaultBottomLeftSymbol = '+';
        public const char DefaultBottomRightSymbol = '+';
        public const char DefaultHorizontalSymbol = '-';
        public const char DefaultVerticalSymbol = '|';

    }
}
