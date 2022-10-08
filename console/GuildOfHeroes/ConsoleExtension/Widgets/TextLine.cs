using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Widgets
{
    public class TextLine : Widget, IText, IAlign, IColor
    {
        public static ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;
        public static ConsoleColor DefaultForegroundColor = ConsoleColor.Gray;

        public string Text { get; set; }
        public Alignment Alignment { get; set; }
        public override int Height 
        {
            get { return 1; }
            set { }
        }

        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public TextLine(
            IWidget parent = null,
            int line = 0,
            int column = 0,
            int width = 0, 
            int height = 0
        ) : base(parent, line, column, width, height)
        {
            BackgroundColor = DefaultBackgroundColor;
            ForegroundColor = DefaultForegroundColor;
        }

        protected override void DrawOwn()
        {
            PrepareTextLenAndOffsetPosition();
            int startPos = CalculateStartDrawPosition();
            int endPos = CalculateEndDrawPosition();
            int symPos = CalculateStartSymbolPosition();

            Console.SetCursorPosition(startPos, ConsoleLine());
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;

            for(int i = startPos; i < endPos; ++i, ++symPos)
                Console.Write(Text[symPos]);

            Console.ResetColor();
        }

        private void PrepareTextLenAndOffsetPosition()
        {
            textLength = Width < Text.Length ? Width : Text.Length;
            offsetPosition = OffsetDrawPosition();
        }

        private int CalculateStartSymbolPosition()
        {
            int startDrawPosition = ConsoleColumn();
            startDrawPosition += offsetPosition;
            return startDrawPosition < 0 ? -startDrawPosition : 0;
        }

        private int CalculateEndDrawPosition()
        {
            int endDrawPosition = ConsoleColumn() + textLength;
            endDrawPosition += offsetPosition;
            return endDrawPosition >= 0 ? endDrawPosition : 0;
        }

        private int CalculateStartDrawPosition()
        {            
            int startDrawPosition = ConsoleColumn();
            startDrawPosition += offsetPosition;
            return startDrawPosition >= 0 ? startDrawPosition : 0;
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
