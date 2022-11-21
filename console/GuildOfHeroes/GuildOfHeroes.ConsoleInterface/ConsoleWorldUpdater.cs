using GuildOfHeroes.ConsoleInterface.Base;
using GuildOfHeroes.Core;
using GuildOfHeroes.Entities;

namespace GuildOfHeroes.ConsoleInterface
{
    public class ConsoleWorldUpdater : IWorldUpdater
    {
        private ICommandProvider commandProvider;
        private ICommandExecuter commandExecuter;

        public ConsoleWorldUpdater(
            ICommandProvider commandProvider,
            ICommandExecuter commandExecuter
        )
        {
            this.commandProvider = commandProvider;
            this.commandExecuter = commandExecuter;
        }

        public void Update(World world)
        {
            ICommand command = commandProvider.GetNextCommand();
            command.Execute(commandExecuter);
        }
    }
}
