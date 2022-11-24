using GuildOfHeroes.Entities;
using GuildOfHeroes.Entities.Service;
using System;

namespace GuildOfHeroes.Core
{
    public class Game
    {
        public static Game Instance { get; private set; }

        private World world;
        private IDrawManager drawManager;
        private IWorldUpdater worldUpdater;
        private IGamePreparer gamePreparer;

        private bool gameActive;

        public Game(
            IDrawManager drawManager,
            IWorldUpdater worldUpdater,
            IGamePreparer gamePreparer
        )
        {
            Instance = this;
            gameActive = true;
            this.drawManager = drawManager;
            this.worldUpdater = worldUpdater;
            this.gamePreparer = gamePreparer;
        }

        public void Run()
        {
            PrepareGame();
            while (gameActive)
                GameStep();
        }

        private void PrepareGame()
        {
            LoadData();
            world = BuildWorld();
            gamePreparer.Prepare(world);
            drawManager.Setup();
        }

        private World BuildWorld()
        {
            return new World();
        }

        private void GameStep()
        {
            drawManager.Draw(world);
            worldUpdater.Update(world);
        }

        public void StopGame()
        {
            gameActive = false;
        }

        private void LoadData()
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
