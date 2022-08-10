using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.DrawStateBuilders
{
    public interface IDrawStateBuilder
    {
        IGameDrawState Build();
    }
}
