using System.Collections.Generic;

namespace GuildOfHeroes
{
    public abstract class SelectionUnitBase : ISelectionUnit
    {
        protected SelectionUnitBase()
        {
        }

        public IGameDrawState NextState { get; set; }

        public abstract string Text { get; }
        public abstract T GetData<T>();

        public IGameDrawState GetNextState()
        {
            return NextState;
        }
    }
}