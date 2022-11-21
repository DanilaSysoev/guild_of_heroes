using GuildOfHeroes.ConsoleInterface.Base;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    public class RightCommand : ICommand
    {
        public void Execute(ICommandExecuter commandExecuter)
        {
            commandExecuter.ExecuteRightCommand();
        }
    }
}
