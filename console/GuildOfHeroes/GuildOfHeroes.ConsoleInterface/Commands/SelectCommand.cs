using GuildOfHeroes.ConsoleInterface.Base;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    partial class Command
    {
        private class SelectCommand : ICommand
        {
            public void Execute(ICommandExecuter commandExecuter)
            {
                commandExecuter.ExecuteSelectCommand();
            }
        }
    }
}
