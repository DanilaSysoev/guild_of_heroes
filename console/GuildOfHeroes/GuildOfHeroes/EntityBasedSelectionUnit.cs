using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class EntityBasedSelectionUnit : SelectionUnitBase
    {
        private object entity;
        public override string Text => entity.ToString();

        public EntityBasedSelectionUnit(object entity)
        {
            this.entity = entity;
        }

        public override T GetData<T>()
        {
            return (T)entity;
        }
    }
}
