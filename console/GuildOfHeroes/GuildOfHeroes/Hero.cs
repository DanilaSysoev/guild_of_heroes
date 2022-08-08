using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class Hero
    {
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public Race Race { get; private set; }
        public Class Class { get; private set; }
        public Dictionary<Skill, int> Skills { get; private set; }
    }
}
