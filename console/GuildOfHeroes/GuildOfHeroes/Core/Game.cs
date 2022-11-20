using GuildOfHeroes.Entities;

namespace GuildOfHeroes.Core
{
    public class Game
    {
        public static Game Instance { get; private set; }

        private ICommandProvider commandProvider;
        private IDrawManager drawManager;
        private ICommandExecuter commandExecuter;
        private World world;

        private bool gameActive;

        public Game(
            ICommandProvider commandProvider,
            IDrawManager drawManager,
            ICommandExecuter commandExecuter,
            World world
        ) {
            Instance = this;
            gameActive = true;
            this.commandProvider = commandProvider;
            this.drawManager = drawManager;
            this.commandExecuter = commandExecuter;
            this.world = world;
        }

        public void Run()
        {
            Load();
            while(gameActive)
                GameStep();
        }

        private void GameStep()
        {
            var command = commandProvider.GetNextCommand();
            command.Execute(commandExecuter);
            drawManager.Draw(world);
        }

        public void StopGame()
        {
            gameActive = false;
        }

        private void Load()
        {
            Skill.Load();
            Race.Load();
            Class.Load();
            NameGenerator.Load();
            GuildMaster.Load();
            HeroGeneratorPattern.Load();
        }
    }
}
