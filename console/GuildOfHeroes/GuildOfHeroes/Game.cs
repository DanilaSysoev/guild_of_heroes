using System;

namespace GuildOfHeroes
{
    public class Game
    {
        public static Game Instance { get; private set; }

        public Game()
        {
            Instance = this;
        }

        public void Run()
        {
            Load();
        }

        private void Load()
        {
            Skill.Load();
            Race.Load();
            Class.Load();
        }
    }
}
