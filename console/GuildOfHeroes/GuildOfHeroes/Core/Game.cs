using System;

namespace GuildOfHeroes
{
    public class Game
    {
        public static Game Instance { get; private set; }

        private bool gameActive;

        public Game(int winWidth, int winHeight)
        {
            Instance = this;
            gameActive = true;
        }

        public void Run()
        {
            Load();
            while(gameActive)
                GameStep();
        }

        private void GameStep()
        {
            gameActive = false;
        }

        public void StopGame()
        {
            gameActive = false;
        }

        private void Load()
        {
            ArtProvider.Load();
            Skill.Load();
            Race.Load();
            Class.Load();
            NameGenerator.Load();
            GuildMaster.Load();
            HeroGeneratorPattern.Load();
        }
    }
}
