using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Service
{
    public class Rectangle
    {
        public int Line   { get; set; }
        public int Column { get; set; }
        public int Width  { get; set; }
        public int Height { get; set; }

        public Rectangle(
            int line = 0,
            int column = 0,
            int width = 0,
            int height = 0
        ) {
            Line = line;
            Column = column;
            Width = width;
            Height = height;
        }

        public Rectangle CutInsidePart(Rectangle insideRectangle)
        {
            return new Rectangle (
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
            if (insideRectangle.Line >= Height ||
                insideRectangle.Column >= Width)
                return 0;
            if (Column < 0)
                return Math.Min(
                    Width, 
                    insideRectangle.Width + insideRectangle.Column
                );
            return Width - insideRectangle.Column;
        }
        private int CalculateHeight(Rectangle insideRectangle)
        {
            if (insideRectangle.Line >= Height ||
                insideRectangle.Column >= Width)
                return 0;
            if (Line < 0)
                return Math.Min(
                    Height,
                    insideRectangle.Height + insideRectangle.Line
                );
            return Height - insideRectangle.Line;
        }
    }
}
