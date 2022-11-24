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
        private class EmptyCommand : ICommand
        {
            public void Execute(ICommandExecuter commandExecuter)
            { }
        }
    }
}
