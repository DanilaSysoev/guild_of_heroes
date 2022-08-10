using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public interface IInputAction
    {
        IGameDrawState ApplyThis(IGameDrawState currentState);
    }
}
