using ConsoleExtension.Widgets;
using GuildOfHeroes.ConsoleInterface.Base;
using GuildOfHeroes.ConsoleInterface.MenuItems.Main;
using GuildOfHeroes.Core;
using GuildOfHeroes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuildOfHeroes.ConsoleInterface
{
    public class MainMenuScreen : ICommandExecuter, IDrawManager
    {
        private Frame rootFrame;
        private TextImage title;
        private SelectList<ISelectable> menu;

        public MainMenuScreen(int width, int height)
        {
            Console.WindowWidth = width;
            Console.WindowHeight = height;
            Console.CursorVisible = false;

            rootFrame = new Frame(0, 0, width, height);
            rootFrame.ForegroundColor = ConsoleColor.Blue;
        }
        public void Draw(World world)
        {
            rootFrame.Draw();
        }
        public void Setup()
        {
            BuildTitle();
            BuildMenu();
        }

        private void BuildMenu()
        {
            const int MenuWidthAddition = 10;

            ISelectable[] items = CreateMenuItems();
            int width = 
                items.Max(s => s.ToString().Length) + MenuWidthAddition;
            int titleEnd = title.Area.Line + title.Area.Height;
            int line = (Console.WindowHeight - titleEnd) / 2;
            int column = (Console.WindowWidth - width) / 2;

            menu = new SelectList<ISelectable>(
                line, 
                column, 
                width, 
                items.Length,
                rootFrame
            );
            menu.AddItems(items);
        }
        private static ISelectable[] CreateMenuItems()
        {
            return new ISelectable[] {
                new StartGameMenuItem(),
                new ContinueGameMenuItem(),
                new OptionsMenuItem(),
                new ExitMenuItem()
            };
        }
        private void BuildTitle()
        {
            const int TopOffset = 2;

            var text = ArtProvider.Title;
            int line = TopOffset;
            int column =
                (Console.WindowWidth - ArtProvider.Title[0].Length) / 2;

            title = new TextImage(ArtProvider.Title.ToArray(), line, column, rootFrame);
        }
    }
}
