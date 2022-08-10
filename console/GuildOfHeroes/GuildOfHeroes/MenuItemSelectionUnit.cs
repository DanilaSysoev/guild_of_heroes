using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class MenuItemSelectionUnit : IDrawStateSelectionUnit
    {
        public IGameDrawState NextState { get; set; }
        public Dictionary<ChangeSelectionDirection, IDrawStateSelectionUnit>
        Neighbors { get; private set; }

        public string Text { get; set; }

        public MenuItemSelectionUnit(string text)
        {
            Neighbors = 
                new Dictionary<
                    ChangeSelectionDirection, IDrawStateSelectionUnit
                >();
            Neighbors.Add(ChangeSelectionDirection.Up, this);
            Neighbors.Add(ChangeSelectionDirection.Right, this);
            Neighbors.Add(ChangeSelectionDirection.Down, this);
            Neighbors.Add(ChangeSelectionDirection.Left, this);
            Text = text;
        }

        public IDrawStateSelectionUnit 
        GetNeighbor(ChangeSelectionDirection direction)
        {
            return Neighbors[direction];
        }

        public IGameDrawState GetNextState()
        {
            return NextState;
        }
    }
}
