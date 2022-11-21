using GuildOfHeroes.ConsoleInterface.Base;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    public class DownCommand : ICommand
    {
        public void Execute(ICommandExecuter commandExecuter)
        {
            commandExecuter.ExecuteDownCommand();
        }
    }
}
