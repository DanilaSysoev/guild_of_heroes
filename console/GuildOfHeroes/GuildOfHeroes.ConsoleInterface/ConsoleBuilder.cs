using ConsoleExtension;
using ConsoleExtension.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.ConsoleInterface
{
    public class ConsoleBuilder
    {
        public static IConsole Build(int width, int height)
        {
            return new DoubleBufferConsole(width, height);
        }
    }
}
