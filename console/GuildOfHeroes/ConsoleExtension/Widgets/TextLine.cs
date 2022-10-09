using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Widgets
{
    public class TextLine : Widget, IText, IAlign, IColor
    {
        public string Text { get; set; }
        public Alignment Alignment { get; set; }
        public override int Height 
        {
            get { return 1; }
            set { }
        }

        public TextLine(
            int line = 0,
            int column = 0,
            int width = 0, 
            int height = 0,
            IWidget parent = null
        ) : base(line, column, width, height, parent)
        {
        }

        protected override void DrawOwn()
        {
            PrepareTextLenAndOffsetPosition();

            int startPos = CalculateStartDrawPosition();
            int endPos = CalculateEndDrawPosition();
            int symPos = CalculateStartSymbolPosition();

            if (LineInsideParent(ConsoleLine()))
            {
                Console.SetCursorPosition(startPos, ConsoleLine());
                for (int i = startPos; i < endPos; ++i, ++symPos)
                    Console.Write(Text[symPos]);
            }
        }

        private void PrepareTextLenAndOffsetPosition()
        {
            textLength = Width < Text.Length ? Width : Text.Length;
            offsetPosition = OffsetDrawPosition();
        }

        private int CalculateStartSymbolPosition()
        {
            int startDrawPosition = ConsoleColumn() - ParentConsoleColumn();
            startDrawPosition += offsetPosition;
            return Math.Max(0, -startDrawPosition);
        }

        private int CalculateEndDrawPosition()
        {
            int endDrawPosition = ConsoleColumn() + textLength;
            endDrawPosition += offsetPosition;
            endDrawPosition = Math.Min(endDrawPosition, ParentConsoleRight());
            return Math.Max(ParentConsoleColumn(), endDrawPosition);
        }

        private int CalculateStartDrawPosition()
        {            
            int startDrawPosition = ConsoleColumn();
            startDrawPosition += offsetPosition;
            return Math.Max(ParentConsoleColumn(), startDrawPosition);
        }

        private int OffsetDrawPosition()
        {
            switch (Alignment)
            {
                case Alignment.TopCenter:
                case Alignment.MiddleCenter:
                case Alignment.BottomCenter:
                    return (Width - textLength) / 2;

                case Alignment.TopRight:
                case Alignment.MiddleRight:
                case Alignment.BottomRight:
                    return Width - textLength;
            }

            return 0;
        }

        private int textLength;
        private int offsetPosition;
    }
}
