using GuildOfHeroes.ConsoleInterface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.ConsoleInterface.MenuItems
{
    public abstract class MenuItem : ISelectable
    {
        public string Name { get; private set; }

        public MenuItem(string name)
        {
            Name = name;
        }

        public abstract void Select();

        public override string ToString()
        {
            return Name;
        }
    }
}
