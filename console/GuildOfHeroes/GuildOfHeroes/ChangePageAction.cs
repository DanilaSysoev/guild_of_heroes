using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class ChangePageAction : IInputAction
    {
        public Direction Direction { get; private set; }

        private ChangePageAction(Direction direction)
        {
            Direction = direction;
        }

        public IGameDrawState ApplyThis(IGameDrawState currentState)
        {
            return currentState.ApplyChangePageAction(this);
        }


        public static readonly ChangePageAction UpAction =
            new ChangePageAction(Direction.Up);
        public static readonly ChangePageAction RightAction =
            new ChangePageAction(Direction.Right);
        public static readonly ChangePageAction DownAction =
            new ChangePageAction(Direction.Down);
        public static readonly ChangePageAction LeftAction =
            new ChangePageAction(Direction.Left);
    }
}
