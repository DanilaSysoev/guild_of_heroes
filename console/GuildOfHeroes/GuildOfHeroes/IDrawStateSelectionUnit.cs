using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public interface IDrawStateSelectionUnit
    {
        IDrawStateSelectionUnit 
        GetNeighbor(ChangeSelectionDirection direction);

        IGameDrawState GetNextState();

        string Text { get; }
    }
}
