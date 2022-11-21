using GuildOfHeroes.ConsoleInterface.Base;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    public class LeftCommand : ICommand
    {
        public void Execute(ICommandExecuter commandExecuter)
        {
            commandExecuter.ExecuteLeftCommand();
        }
    }
}
