using GuildOfHeroes.ConsoleInterface.Base;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    public class UpCommand : ICommand
    {
        public void Execute(ICommandExecuter commandExecuter)
        {
            commandExecuter.ExecuteUpCommand();
        }
    }
}
