using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class MainMenuDrawState : DrawStateBasedOnSelectionUnit
    {
        private const int TOP_BORDER = 2;
        private const ConsoleColor SELECTED_FG_COLOR = ConsoleColor.Green;
        private List<string> menuTexts;

        public MainMenuDrawState(IDrawStateSelectionUnit startSelectionUnit)
            : base(startSelectionUnit)
        {
            menuTexts = ExtractTexts();
        }

        public override void Draw()
        {
            Console.Clear();

            DrawTitle();

            int selectedIndex = ExtractSelectedIndex();
            int startLine = CalcStartLine();
            DrawMenu(selectedIndex, startLine);
            DrawSelectors(selectedIndex, startLine);
        }

        private void DrawTitle()
        {
            var title = ArtProvider.Title;
            for (int i = 0, line = TOP_BORDER; i < title.Count; ++i, ++line)
            {
                int column = (Console.WindowWidth - title[i].Length) / 2;
                Console.SetCursorPosition(column, line);
                Console.Write(title[i]);
            }
        }

        private void DrawMenu(int selectedIndex, int startLine)
        {
            for (int i = 0, line = startLine; i < menuTexts.Count; ++i, ++line)
            {
                int column = (Console.WindowWidth - menuTexts[i].Length) / 2;
                Console.SetCursorPosition(column, line);
                if (i == selectedIndex)
                    Console.ForegroundColor = SELECTED_FG_COLOR;
                Console.Write(menuTexts[i]);
                Console.ResetColor();
            }
        }
        private void DrawSelectors(int selectedIndex, int startLine)
        {
            int minColumn = menuTexts.Min(text => (Console.WindowWidth - text.Length) / 2);
            Console.SetCursorPosition(minColumn - 5, startLine + selectedIndex);
            Console.Write('[');
            Console.SetCursorPosition(Console.WindowWidth - minColumn + 5, startLine + selectedIndex);
            Console.Write(']');
        }
        private int CalcStartLine()
        {
            int freeSpace = Console.WindowHeight 
                         - menuTexts.Count 
                         - ArtProvider.Title.Count;
            return freeSpace / 2 + ArtProvider.Title.Count + TOP_BORDER;
        }
        private int ExtractSelectedIndex()
        {
            var curItem = StartSelectionUnit;
            int selected = 0;
            while (curItem != CurrentSelectionUnit)
            {
                selected++;
                curItem = curItem.GetNeighbor(ChangeSelectionDirection.Down);
            }
            return selected;
        }
        private List<string> ExtractTexts()
        {
            var items = new List<string>();
            var curItem = StartSelectionUnit;
            do
            {
                items.Add(curItem.Text);
                curItem = curItem.GetNeighbor(ChangeSelectionDirection.Down);
            } while (curItem != StartSelectionUnit);
            return items;
        }
    }
}
