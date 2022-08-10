using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public abstract class GameDrawBase : IGameDraw
    {
        public IGameDrawState CurrentState { get; private set; }

        public GameDrawBase(IGameDrawState startState)
        {
            CurrentState = startState;
        }

        public void ChangeState(IInputAction inputAction)
        {
            CurrentState = inputAction.ApplyThis(CurrentState);
        }

        public abstract void Redraw();
    }
}
