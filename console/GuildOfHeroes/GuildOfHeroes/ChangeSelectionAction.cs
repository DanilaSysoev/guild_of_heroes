using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class ChangeSelectionAction : IInputAction
    {
        public ChangeSelectionDirection Direction { get; private set; }

        private ChangeSelectionAction(ChangeSelectionDirection direction)
        {
            Direction = direction;
        }

        public IGameDrawState ApplyThis(IGameDrawState currentState)
        {
            return currentState.ApplyChangeSelectionAction(this);
        }


        public static readonly ChangeSelectionAction UpAction =
            new ChangeSelectionAction(ChangeSelectionDirection.Up);
        public static readonly ChangeSelectionAction RightAction =
            new ChangeSelectionAction(ChangeSelectionDirection.Right);
        public static readonly ChangeSelectionAction DownAction =
            new ChangeSelectionAction(ChangeSelectionDirection.Down);
        public static readonly ChangeSelectionAction LeftAction =
            new ChangeSelectionAction(ChangeSelectionDirection.Left);
    }
}
