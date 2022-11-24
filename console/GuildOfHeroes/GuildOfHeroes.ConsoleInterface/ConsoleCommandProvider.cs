using GuildOfHeroes.ConsoleInterface.Base;
using GuildOfHeroes.ConsoleInterface.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.ConsoleInterface
{
    class ConsoleCommandProvider : ICommandProvider
    {
        private Dictionary<ConsoleKey, ICommand> commands;

        public ConsoleCommandProvider()
        {
            commands = new Dictionary<ConsoleKey, ICommand>();
            AssignCommandsToKeys();
        }

        public ICommand GetNextCommand()
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            return commands.ContainsKey(key) ?
                   commands[key] :
                   Command.Enpty;
        }


        private void AssignCommandsToKeys()
        {
            commands.Add(ConsoleKey.W, Command.Up);
            commands.Add(ConsoleKey.S, Command.Down);
            commands.Add(ConsoleKey.A, Command.Left);
            commands.Add(ConsoleKey.D, Command.Right);

            commands.Add(ConsoleKey.UpArrow,    Command.Up);
            commands.Add(ConsoleKey.DownArrow,  Command.Down);
            commands.Add(ConsoleKey.LeftArrow,  Command.Left);
            commands.Add(ConsoleKey.RightArrow, Command.Right);

            commands.Add(ConsoleKey.Enter, Command.Select);
        }
    }
}
