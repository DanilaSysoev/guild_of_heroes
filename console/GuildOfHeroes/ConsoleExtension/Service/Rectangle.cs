using System;

namespace ConsoleExtension.Service
{
    public class Rectangle
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int RightBorder { get { return Column + Width - 1; } }
        public int BottomBorder { get { return Line + Height - 1; } }

        public Rectangle(
            int line = 0,
            int column = 0,
            int width = 0,
            int height = 0
        )
        {
            Line = line;
            Column = column;
            Width = width;
            Height = height;
        }

        public Rectangle CutInsidePart(Rectangle insideRectangle)
        {
            return new Rectangle(
                CalculateLine(insideRectangle),
                CalculateColumn(insideRectangle),
                CalculateWidth(insideRectangle),
                CalculateHeight(insideRectangle)
            );
        }

        private int CalculateLine(Rectangle insideRectangle)
        {
            if (insideRectangle.Line < 0)
                return -insideRectangle.Line;
            return 0;
        }
        private int CalculateColumn(Rectangle insideRectangle)
        {
            if (insideRectangle.Column < 0)
                return -insideRectangle.Column;
            return 0;
        }
        private int CalculateWidth(Rectangle insideRectangle)
        {
            if (NotIntersect(insideRectangle))
                return 0;
            int leftCut = insideRectangle.Column < 0 ? -Column : 0;
            int rightCut =
                insideRectangle.RightBorder >= Width ?
                insideRectangle.RightBorder - Width + 1 : 0;
            return insideRectangle.Width - leftCut - rightCut;
        }

        private bool NotIntersect(Rectangle insideRectangle)
        {
            return insideRectangle.Line >= Height ||
                   insideRectangle.Column >= Width ||
                   insideRectangle.RightBorder < 0 ||
                   insideRectangle.BottomBorder < 0;
        }

        private int CalculateHeight(Rectangle insideRectangle)
        {
            if (NotIntersect(insideRectangle))
                return 0;
            int topCut = insideRectangle.Line < 0 ? -Line : 0;
            int bottomCut =
                insideRectangle.BottomBorder >= Height ?
                insideRectangle.BottomBorder - Height + 1 : 0;
            return insideRectangle.Height - topCut - bottomCut;
        }
    }
}
