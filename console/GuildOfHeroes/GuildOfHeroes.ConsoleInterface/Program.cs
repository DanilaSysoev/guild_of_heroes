using GuildOfHeroes.ConsoleInterface.Screens;
using GuildOfHeroes.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.ConsoleInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = CreateGame();
            game.Run();
        }

        private static Game CreateGame()
        {
            var startScreen = new MainMenuScreen(120, 40);
            Game game = new Game(
                startScreen,
                new ConsoleWorldUpdater(
                    new ConsoleCommandProvider(), startScreen
                ),
                new ConsoleGamePreparer()
            );
            return game;
        }
    }
}
