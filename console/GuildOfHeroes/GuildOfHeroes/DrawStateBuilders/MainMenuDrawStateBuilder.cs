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

            List<List<ISelectionUnit>> units = new List<List<ISelectionUnit>>();
            units.Add(new List<ISelectionUnit> { startMenuItem });
            units.Add(new List<ISelectionUnit> { continueMenuItem });
            units.Add(new List<ISelectionUnit> { optionsMenuItem });
            units.Add(new List<ISelectionUnit> { exitMenuItem });

            MainMenuDrawState drawState = new MainMenuDrawState(units);
            startMenuItem.NextState = drawState;
            continueMenuItem.NextState = drawState;
            optionsMenuItem.NextState = drawState;
            exitMenuItem.NextState = ExitDrawState.Instance;

            return drawState;
        }
    }
}
