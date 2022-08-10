using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public interface IGameDrawState
    {
        IGameDrawState 
        ApplyChangeSelectionAction(ChangeSelectionAction action);

        IGameDrawState
        ApplySelectAction(SelectAction action);

        void Draw();
    }
}
