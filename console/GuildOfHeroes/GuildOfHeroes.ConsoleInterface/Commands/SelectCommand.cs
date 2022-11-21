using GuildOfHeroes.ConsoleInterface.Base;

namespace GuildOfHeroes.ConsoleInterface.Commands
{
    public class SelectCommand : ICommand
    {
        public void Execute(ICommandExecuter commandExecuter)
        {
            commandExecuter.ExecuteSelectCommand();
        }
    }
}
