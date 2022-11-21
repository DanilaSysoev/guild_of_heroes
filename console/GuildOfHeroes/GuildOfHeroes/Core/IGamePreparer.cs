using GuildOfHeroes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.Core
{
    public interface IGamePreparer
    {
        void Prepare(World world);
    }
}
