﻿using GuildOfHeroes.ConsoleInterface.Base;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    partial class Command
    {
        private class UpCommand : ICommand
        {
            public void Execute(ICommandExecuter commandExecuter)
            {
                commandExecuter.ExecuteUpCommand();
            }
        }
    }
}
