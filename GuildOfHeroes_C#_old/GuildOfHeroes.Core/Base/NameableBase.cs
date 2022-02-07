using GuildOfHeroes.Core.Interfaces;

namespace GuildOfHeroes.Core.Base
{
    public class NameableBase : INameable
    {
        public string Name { get; private set; }
        protected NameableBase(string name)
        {
            Name = name;
        }
    }
}
