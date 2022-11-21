using GuildOfHeroes.Entities;
using GuildOfHeroes.Entities.Service;

namespace GuildOfHeroes.Core
{
    public class Game
    {
        public static Game Instance { get; private set; }

        private IDrawManager drawManager;
        private IWorldUpdater worldUpdater;
        private World world;

        private bool gameActive;

        public Game(
            IDrawManager drawManager,
            IWorldUpdater worldUpdater,
            World world
        )
        {
            Instance = this;
            gameActive = true;
            this.drawManager = drawManager;
            this.worldUpdater = worldUpdater;
            this.world = world;
        }

        public void Run()
        {
            Load();
            while (gameActive)
                GameStep();
        }

        private void GameStep()
        {
            worldUpdater.Update(world);
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
