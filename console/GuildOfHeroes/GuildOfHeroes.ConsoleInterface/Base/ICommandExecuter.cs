namespace GuildOfHeroes.ConsoleInterface.Base
{
    public interface ICommandExecuter
    {
        void ExecuteUpCommand();
        void ExecuteDownCommand();
        void ExecuteLeftCommand();
        void ExecuteRightCommand();

        void ExecuteSelectCommand();
    }
}
