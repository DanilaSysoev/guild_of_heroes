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

        public Game(int winWidth, int winHeight)
        {
            Instance = this;
            gameActive = true;
            gameDraw = new GameDraw(
                new MainMenuDrawStateBuilder().Build(),
                winWidth,
                winHeight
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
