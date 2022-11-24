using GuildOfHeroes.ConsoleInterface.Base;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    partial class Command
    {
        private class LeftCommand : ICommand
        {
            public void Execute(ICommandExecuter commandExecuter)
            {
                commandExecuter.ExecuteLeftCommand();
            }
        }
    }
}
