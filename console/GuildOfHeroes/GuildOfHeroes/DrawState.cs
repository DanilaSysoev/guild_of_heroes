using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public abstract class DrawState : IGameDrawState
    {
        public virtual IGameDrawState 
        ApplyChangePageAction(ChangePageAction action)
        {
            return this;
        }
        public virtual IGameDrawState
        ApplyChangeSelectionAction(ChangeSelectionAction action)
        {
            return this;
        }
        public virtual IGameDrawState
        ApplySelectAction(SelectAction action)
        {
            return this;
        }

        public virtual void Draw()
        {
            Console.Clear();
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}
