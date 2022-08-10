using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class MenuGameDraw : GameDrawBase
    {
        public MenuGameDraw(IGameDrawState startState)
            : base(startState)
        {
        }

        public override void Redraw()
        {
            Console.Clear();

            List<string> items = ExtractTexts();
            int selectedIndex = ExtractSelectedIndex();

            int startLine = (Console.WindowHeight - items.Count) / 2;
            int minColumn = Console.WindowWidth + 1;
            for(int i = 0, line = startLine; i < items.Count; ++i, ++line)
            {
                int column = (Console.WindowWidth - items[i].Length) / 2;
                Console.SetCursorPosition(column, line);
                Console.Write(items[i]);
                if (column < minColumn)
                    minColumn = column;
            }
            Console.SetCursorPosition(minColumn - 5, startLine + selectedIndex);
            Console.Write('[');
            Console.SetCursorPosition(Console.WindowWidth - minColumn + 5, startLine + selectedIndex);
            Console.Write(']');
        }

        private int ExtractSelectedIndex()
        {
            var curentState = (DrawStateBasedOnSelectionUnit)CurrentState;
            var curItem = curentState.StartSelectionUnit;
            int selected = 0;
            while (curItem != curentState.CurrentSelectionUnit)
            {
                selected++;
                curItem = curItem.GetNeighbor(ChangeSelectionDirection.Down);
            }
            return selected;
        }

        private List<string> ExtractTexts()
        {
            var items = new List<string>();
            var curentState = (DrawStateBasedOnSelectionUnit)CurrentState;
            var curItem = curentState.StartSelectionUnit;
            do
            {
                items.Add(curItem.Text);
                curItem = curItem.GetNeighbor(ChangeSelectionDirection.Down);
            } while (curItem != curentState.StartSelectionUnit);
            return items;
        }
    }
}
