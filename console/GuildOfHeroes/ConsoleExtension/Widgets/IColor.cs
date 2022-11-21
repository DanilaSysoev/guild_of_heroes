using System;

namespace ConsoleExtension.Widgets
{
    public interface IColor
    {
        ConsoleColor BackgroundColor { get; set; }
        ConsoleColor ForegroundColor { get; set; }
    }
}
