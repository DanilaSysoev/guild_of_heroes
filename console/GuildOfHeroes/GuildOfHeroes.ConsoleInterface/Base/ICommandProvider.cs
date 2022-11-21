namespace GuildOfHeroes.ConsoleInterface.Base
{
    public interface ICommandProvider
    {
        ICommand GetNextCommand();
    }
}
