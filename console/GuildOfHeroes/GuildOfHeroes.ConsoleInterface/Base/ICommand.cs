namespace GuildOfHeroes.ConsoleInterface.Base
{
    public interface ICommand
    {
        void Execute(ICommandExecuter commandExecuter);
    }
}
