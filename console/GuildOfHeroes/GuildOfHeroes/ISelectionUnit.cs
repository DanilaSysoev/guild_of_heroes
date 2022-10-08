using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public interface ISelectionUnit
    {
        IGameDrawState GetNextState();

        string Text { get; }

        T GetData<T>();
    }
}
