using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class ChangeSelectionAction : IInputAction
    {
        public Direction Direction { get; private set; }

        private ChangeSelectionAction(Direction direction)
        {
            Direction = direction;
        }

        public IGameDrawState ApplyThis(IGameDrawState currentState)
        {
            return currentState.ApplyChangeSelectionAction(this);
        }


        public static readonly ChangeSelectionAction UpAction =
            new ChangeSelectionAction(Direction.Up);
        public static readonly ChangeSelectionAction RightAction =
            new ChangeSelectionAction(Direction.Right);
        public static readonly ChangeSelectionAction DownAction =
            new ChangeSelectionAction(Direction.Down);
        public static readonly ChangeSelectionAction LeftAction =
            new ChangeSelectionAction(Direction.Left);
    }
}
