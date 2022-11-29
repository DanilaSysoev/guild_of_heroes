using ConsoleExtension;
using ConsoleExtension.Draw;
using ConsoleExtension.Widgets;
using GuildOfHeroes.ConsoleInterface.Base;
using GuildOfHeroes.ConsoleInterface.MenuItems.Main;
using GuildOfHeroes.Core;
using GuildOfHeroes.Entities;
using System;
using System.Linq;

namespace GuildOfHeroes.ConsoleInterface.Screens
{
    public class MainMenuScreen : ICommandExecuter, IDrawManager
    {
        private const ConsoleColor MenuSelectionForeground = ConsoleColor.Yellow;

        private Frame rootFrame;
        private TextImage title;
        private SelectList<ISelectable> menu;
        private IConsole console;

        public MainMenuScreen(int width, int height)
        {
            Console.WindowWidth = width;
            Console.WindowHeight = height;
            Console.CursorVisible = false;

            console = ConsoleBuilder.Build(width, height);
            rootFrame = new Frame(console, 0, 0, width, height);
            rootFrame.ForegroundColor = ConsoleColor.Blue;
        }
        public void Draw(World world)
        {
            rootFrame.Draw();

            console.Draw();
        }
        public void Setup()
        {
            BuildTitle(console);
            BuildMenu(console);
        }

        private void BuildMenu(IConsole console)
        {
            const int MenuWidthAddition = 10;

            ISelectable[] items = CreateMenuItems();
            int width = 
                items.Max(s => s.ToString().Length) + MenuWidthAddition;
            int titleEnd = title.Area.Line + title.Area.Height;
            int line = (Console.WindowHeight + titleEnd) / 2;
            int column = (Console.WindowWidth - width) / 2;

            menu = new SelectList<ISelectable>(
                console,
                line,
                column,
                width,
                items.Length,
                rootFrame
            );
            menu.SelectionForegroundColor = MenuSelectionForeground;
            menu.AddItems(items);
            menu.Select(0);
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
        private void BuildTitle(IConsole console)
        {
            const int TopOffset = 2;

            var text = ArtProvider.Title;
            int line = TopOffset;
            int column =
                (Console.WindowWidth - ArtProvider.Title[0].Length) / 2;

            title = new TextImage(
                console, ArtProvider.Title.ToArray(), line, column, rootFrame
            );
        }

        public void ExecuteUpCommand()
        {
            menu.MoveSelectionOnPrevious();
        }

        public void ExecuteDownCommand()
        {
            menu.MoveSelectionOnNext();
        }

        public void ExecuteLeftCommand()
        {}

        public void ExecuteRightCommand()
        {}

        public void ExecuteSelectCommand()
        {
            menu.SelectedItem.Select();
        }
    }
}
