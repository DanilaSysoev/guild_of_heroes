using GuildOfHeroes.DrawStateBuilders;
using System;

namespace GuildOfHeroes
{
    public class Game
    {
        public static Game Instance { get; private set; }

        private IInputManager inputManager;
        private IGameDraw gameDraw;
        private bool gameActive;

        public Game()
        {
            Instance = this;
            gameActive = true;
            gameDraw = new MenuGameDraw(
                new MainMenuDrawStateBuilder().Build()
            );
            inputManager = new ConsoleInputManager();
        }

        public void Run()
        {
            Load();
            while(gameActive)
            {
                gameDraw.Redraw();
                var action = inputManager.GetNextAction();
                gameDraw.ChangeState(action);
            }
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
            HeroGeneratorPattern.Load();
        }
    }
}
