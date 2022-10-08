using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public abstract class DrawStateBasedOnSelectionUnit : DrawState
    {
        private List<List<ISelectionUnit>> selectionUnits;
        public int SelectionLine { get; private set; }
        public int SelectionColumn { get; private set; }

        public IReadOnlyList<IReadOnlyList<ISelectionUnit>> SelectionUnits 
        {
            get
            {
                return selectionUnits;
            } 
        }

        public ISelectionUnit SelectedUnit
        {
            get
            {
                return selectionUnits[SelectionLine][SelectionColumn];
            }
        }

        public DrawStateBasedOnSelectionUnit(
            List<List<ISelectionUnit>> selectionUnits,
            int selectionLine = 0,
            int selectionColumn = 0
        )
        {
            this.selectionUnits = selectionUnits;
            this.SelectionLine = selectionLine;
            this.SelectionColumn = selectionColumn;
        }

        public override IGameDrawState 
        ApplyChangeSelectionAction(ChangeSelectionAction action)
        {
            switch (action.Direction)
            {
                case Direction.Up:
                    CycledDecreaseSelectionLine();
                    break;
                case Direction.Down:
                    CycledIncreaseSelectionLine();
                    break;
                case Direction.Left:
                    CycledDecreaseSelectionColumn();
                    break;
                case Direction.Right:
                    CycledIncreaseSelectionColumn();
                    break;
            }
            return this;
        }

        private void CycledIncreaseSelectionColumn()
        {
            SelectionColumn =
                (SelectionColumn + 1) % selectionUnits[SelectionLine].Count;
        }

        private void CycledDecreaseSelectionColumn()
        {
            SelectionColumn =
                (SelectionColumn + selectionUnits[SelectionLine].Count - 1) %
                selectionUnits[SelectionLine].Count;
        }

        private void CycledIncreaseSelectionLine()
        {
            SelectionLine = 
                (SelectionLine + 1) % selectionUnits.Count;
        }

        private void CycledDecreaseSelectionLine()
        {
            SelectionLine = 
                (SelectionLine + selectionUnits.Count - 1) % 
                selectionUnits.Count;
        }

        public override IGameDrawState 
        ApplySelectAction(SelectAction action)
        {            
            var nextState = 
                selectionUnits[SelectionLine][SelectionColumn].GetNextState();
            SelectionLine = SelectionColumn = 0;
            return nextState;
        }
    }
}
