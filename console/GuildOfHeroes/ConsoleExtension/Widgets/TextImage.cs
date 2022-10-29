using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Widgets
{
    public class TextImage : Widget
    {
        public TextImage(
            string[] image,
            int line = 0,
            int column = 0,
            IWidget parent = null
        ) : base(line, column, image[0].Length, image.Length, parent)
        {
            this.image = image;
        }

        protected override void DrawOwnBeforeChildren()
        {
            int startCharIndex = CalculateStartCharIndex();
            int startLine = CalculateStartLine();
            int endCharIndex = CalculateEndCharIndex(startCharIndex);
            int endLine = CalculateEndLine(startLine);

            for(int line = startLine, offsetLine = 0;
                line < endLine; 
                ++line, ++offsetLine)
            {
                for(int charIndex = startCharIndex, offsetColumn = 0; 
                    charIndex < endCharIndex; 
                    ++charIndex, ++offsetColumn)
                {
                    DrawSymbolIfPossible(
                        ParentConsoleLine() + offsetLine + Math.Max(0, Line),
                        ParentConsoleColumn() + offsetColumn + Math.Max(0, Column),
                        image[line][charIndex]
                    );
                }
            }
        }

        private int CalculateEndLine(int startLine)
        {
            int parentHeight =
                Parent == null ? Console.WindowHeight : Parent.Height;

            if (startLine > 0)
                return Math.Min(Height, startLine + parentHeight);
            else
                return Math.Min(Height, parentHeight - Line);
        }

        private int CalculateEndCharIndex(int startCharIndex)
        {
            int parentWidth =
                Parent == null ? Console.WindowWidth : Parent.Width;

            if (startCharIndex > 0)
                return Math.Min(Width, startCharIndex + parentWidth);
            else
                return Math.Min(Width, parentWidth - Column);
        }

        private int CalculateStartLine()
        {
            return Math.Max(0, -Line);
        }

        private int CalculateStartCharIndex()
        {
            return Math.Max(0, -Column);
        }

        private string[] image;
    }
}
