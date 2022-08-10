using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public abstract class DrawStateBasedOnSelectionUnit : IGameDrawState
    {
        public IDrawStateSelectionUnit 
        CurrentSelectionUnit { get; private set; }
        public IDrawStateSelectionUnit
        StartSelectionUnit { get; private set; }

        public DrawStateBasedOnSelectionUnit(
            IDrawStateSelectionUnit startSelectionUnit
        )
        {
            CurrentSelectionUnit = startSelectionUnit;
            StartSelectionUnit = startSelectionUnit;
        }

        public IGameDrawState 
        ApplyChangeSelectionAction(ChangeSelectionAction action)
        {
            CurrentSelectionUnit = 
                CurrentSelectionUnit.GetNeighbor(action.Direction);
            return this;
        }

        public IGameDrawState 
        ApplySelectAction(SelectAction action)
        {
            CurrentSelectionUnit = StartSelectionUnit;
            return CurrentSelectionUnit.GetNextState();
        }

        public abstract void Draw();        
    }
}
