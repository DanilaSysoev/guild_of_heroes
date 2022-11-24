using GuildOfHeroes.ConsoleInterface.Base;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    partial class Command
    {
        private class DownCommand : ICommand
        {
            public void Execute(ICommandExecuter commandExecuter)
            {
                commandExecuter.ExecuteDownCommand();
            }
        }
    }
}
