using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class ConsoleInputManager : IInputManager
    {
        public ConsoleInputManager()
        {
            Console.CursorVisible = false;
        }

        public IInputAction GetNextAction()
        {
            var key = Console.ReadKey();

            while (IncorrectInput(key.Key))
                key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    return ChangeSelectionAction.LeftAction;
                case ConsoleKey.RightArrow:
                    return ChangeSelectionAction.RightAction;
                case ConsoleKey.UpArrow:
                    return ChangeSelectionAction.UpAction;
                case ConsoleKey.DownArrow:
                    return ChangeSelectionAction.DownAction;
                case ConsoleKey.Enter:
                    return SelectAction.Instance;
                case ConsoleKey.PageUp:
                    return ChangePageAction.UpAction;
                case ConsoleKey.PageDown:
                    return ChangePageAction.DownAction;
                case ConsoleKey.Home:
                    return ChangePageAction.LeftAction;
                case ConsoleKey.End:
                    return ChangePageAction.RightAction;
            }

            throw new NotImplementedException("Impossible code");
        }

        private bool IncorrectInput(ConsoleKey key)
        {
            return key != ConsoleKey.LeftArrow &&
                   key != ConsoleKey.RightArrow &&
                   key != ConsoleKey.DownArrow &&
                   key != ConsoleKey.UpArrow &&
                   key != ConsoleKey.Enter &&
                   key != ConsoleKey.PageDown &&
                   key != ConsoleKey.PageUp &&
                   key != ConsoleKey.Home &&
                   key != ConsoleKey.End;
        }
    }
}
