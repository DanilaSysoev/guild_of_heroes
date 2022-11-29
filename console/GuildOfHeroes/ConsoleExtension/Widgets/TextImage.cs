using ConsoleExtension.Draw;
using ConsoleExtension.Service;
using System;

namespace ConsoleExtension.Widgets
{
    public class TextImage : Widget
    {
        public TextImage(
            IConsole console,
            string[] image,
            int line = 0,
            int column = 0,
            IWidget parent = null
        ) : base(console, line, column, image[0].Length, image.Length, parent)
        {
            this.image = image;
        }

        protected override void DrawOwnBeforeChildren()
        {
            Rectangle drawingRect =
                Parent == null ?
                RootArea().CutInsidePart(Area) :
                Parent.Area.CutInsidePart(Area);

            for (int offsetLine = 0;
                offsetLine < drawingRect.Height;
                ++offsetLine)
            {
                int line = drawingRect.Line + offsetLine;
                for (int offsetColumn = 0;
                    offsetColumn < drawingRect.Width;
                    ++offsetColumn)
                {
                    int column = drawingRect.Column + offsetColumn;
                    DrawSymbolIfPossible(
                        ParentConsoleLine() + offsetLine + Math.Max(0, Area.Line),
                        ParentConsoleColumn() + offsetColumn + Math.Max(0, Area.Column),
                        image[line][column]
                    );
                }
            }
        }

        private string[] image;
    }
}
