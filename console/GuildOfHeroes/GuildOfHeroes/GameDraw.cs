using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class GameDraw : IGameDraw
    {
        public IGameDrawState CurrentState { get; private set; }

        public GameDraw(
            IGameDrawState startState,
            int winWidth, 
            int winHeight
        )
        {
            Console.SetWindowSize(winWidth, winHeight);
            Console.SetBufferSize(winWidth, winHeight);
            CurrentState = startState;
        }

        public void ChangeState(IInputAction inputAction)
        {
            var newState = inputAction.ApplyThis(CurrentState);
            if (newState != CurrentState) {
                CurrentState.OnExit();
                CurrentState = newState;
                CurrentState.OnEnter();
            }
        }

        public void Redraw()
        {
            CurrentState.Draw();
        }
    }
}
