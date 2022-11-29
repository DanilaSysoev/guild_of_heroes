using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Draw
{
    public interface IConsole
    {
        void Write(char symbol);
        void SetCursorPosition(int left, int top);
        ConsoleColor ForegroundColor { get; set; }
        ConsoleColor BackgroundColor { get; set; }
        int BufferWidth { get; }
        int BufferHeight { get; }
        void ResetColor();

        void Draw();
    }
}
