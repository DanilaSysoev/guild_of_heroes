using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class MenuItemSelectionUnit : SelectionUnitBase
    {
        private string text;
        public override string Text => text;

        public MenuItemSelectionUnit(string text)
        {
            this.text = text;
        }

        public override T GetData<T>()
        {
            return default(T);
        }
    }
}
