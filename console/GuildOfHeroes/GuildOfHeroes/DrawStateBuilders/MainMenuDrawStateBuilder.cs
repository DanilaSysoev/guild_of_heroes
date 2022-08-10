using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.DrawStateBuilders
{
    public class MainMenuDrawStateBuilder : IDrawStateBuilder
    {
        public IGameDrawState Build()
        {
            var startMenuItem = new MenuItemSelectionUnit("Start game");
            var continueMenuItem = new MenuItemSelectionUnit("Continue");
            var optionsMenuItem = new MenuItemSelectionUnit("Options");
            var exitMenuItem = new MenuItemSelectionUnit("Exit");

            startMenuItem.
                Neighbors[ChangeSelectionDirection.Up] = exitMenuItem;
            startMenuItem.
                Neighbors[ChangeSelectionDirection.Down] = continueMenuItem;

            continueMenuItem.
                Neighbors[ChangeSelectionDirection.Up] = startMenuItem;
            continueMenuItem.
                Neighbors[ChangeSelectionDirection.Down] = optionsMenuItem;

            optionsMenuItem.
                Neighbors[ChangeSelectionDirection.Up] = continueMenuItem;
            optionsMenuItem.
                Neighbors[ChangeSelectionDirection.Down] = exitMenuItem;

            exitMenuItem.
                Neighbors[ChangeSelectionDirection.Up] = optionsMenuItem;
            exitMenuItem.
                Neighbors[ChangeSelectionDirection.Down] = startMenuItem;

            DrawStateBasedOnSelectionUnit drawState = new DrawStateBasedOnSelectionUnit(startMenuItem);
            startMenuItem.NextState = drawState;
            continueMenuItem.NextState = drawState;
            optionsMenuItem.NextState = drawState;
            exitMenuItem.NextState = drawState;

            return drawState;
        }
    }
}
