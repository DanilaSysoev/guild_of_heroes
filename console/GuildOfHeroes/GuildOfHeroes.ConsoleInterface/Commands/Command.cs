using GuildOfHeroes.ConsoleInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    partial class Command
    {
        public static ICommand Up     { get; private set; }
        public static ICommand Down   { get; private set; }
        public static ICommand Left   { get; private set; }
        public static ICommand Right  { get; private set; }
        public static ICommand Select { get; private set; }
        public static ICommand Enpty  { get; private set; }

        static Command()
        {
            Up     = new UpCommand();
            Down   = new DownCommand();
            Left   = new LeftCommand();
            Right  = new RightCommand();
            Select = new SelectCommand();
            Enpty  = new EmptyCommand();
        }
    }
}
