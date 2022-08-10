using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public interface IGameDraw
    {
        IGameDrawState CurrentState { get; }
        void ChangeState(IInputAction inputAction);
        void Redraw();
    }
}
