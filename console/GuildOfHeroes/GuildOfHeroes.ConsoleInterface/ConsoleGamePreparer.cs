using GuildOfHeroes.Core;
using GuildOfHeroes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.ConsoleInterface
{
    public class ConsoleGamePreparer : IGamePreparer
    {
        public void Prepare(World world)
        {
            Console.CursorVisible = false;
            ArtProvider.Load();
        }
    }
}
