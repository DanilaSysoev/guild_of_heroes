using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Widgets
{
    public class LineBorderDecorator : Widget
    {
        public char LeftDecor { get; set; }
        public char RightDecor { get; set; }

        public LineBorderDecorator(
            int line = 0,
            int column = 0,
            int width = 0,
            IWidget parent = null
        ) : base(line, column, width, 1, parent)
        {
            LeftDecor = DefaultLeftDecor;
            RightDecor = DefaultRightDecor;
        }

        protected override void DrawOwnBeforeChildren()
        {
            for(int i = Column + 1; i < Column + Width - 1; ++i)
            {
                Console.SetCursorPosition(
                    ConsoleColumn() + i, ConsoleLine()
                );
                Console.Write(' ');
            }
        }

        protected override void DrawOwnAfterChildren()
        {
            Console.SetCursorPosition(
                ConsoleColumn(), ConsoleLine()
            );
            Console.Write(LeftDecor);
            Console.SetCursorPosition(
                ConsoleColumn() + Width - 1, ConsoleLine()
            );
            Console.Write(RightDecor);
        }

        public const char DefaultLeftDecor = '<';
        public const char DefaultRightDecor = '>';
    }
}
