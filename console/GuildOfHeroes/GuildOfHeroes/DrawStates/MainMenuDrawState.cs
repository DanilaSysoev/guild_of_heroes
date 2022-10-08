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

        public MainMenuDrawState(
            List<List<ISelectionUnit>> selectionUnits
        ) : base(selectionUnits)
        {
            menuTexts = ExtractTexts();
        }

        public override void Draw()
        {
            Console.Clear();

            DrawTitle();
                        
            int startLine = CalcStartLine();
            DrawMenu(startLine);
            DrawSelectors(startLine);
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

        private void DrawMenu(int startLine)
        {
            for (int i = 0, line = startLine; i < menuTexts.Count; ++i, ++line)
            {
                int column = (Console.WindowWidth - menuTexts[i].Length) / 2;
                Console.SetCursorPosition(column, line);
                if (i == SelectionLine)
                    Console.ForegroundColor = SELECTED_FG_COLOR;
                Console.Write(menuTexts[i]);
                Console.ResetColor();
            }
        }
        private void DrawSelectors(int startLine)
        {
            int minColumn = menuTexts.Min(text => (Console.WindowWidth - text.Length) / 2);
            Console.SetCursorPosition(minColumn - 5, startLine + SelectionLine);
            Console.Write('[');
            Console.SetCursorPosition(Console.WindowWidth - minColumn + 5, startLine + SelectionLine);
            Console.Write(']');
        }
        private int CalcStartLine()
        {
            int freeSpace = Console.WindowHeight 
                         - menuTexts.Count 
                         - ArtProvider.Title.Count;
            return freeSpace / 2 + ArtProvider.Title.Count + TOP_BORDER;
        }
        private List<string> ExtractTexts()
        {
            var items = new List<string>();
            for(int i = 0; i < SelectionUnits.Count; ++i)
                items.Add(SelectionUnits[i][0].Text);
            return items;
        }

        public override IGameDrawState 
        ApplyChangePageAction(ChangePageAction action)
        {
            return this;
        }
    }
}
