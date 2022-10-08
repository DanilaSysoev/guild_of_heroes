using ConsoleExtension.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Widgets
{
    public interface IWidget : IPanel, IParent, IChild, IDrawer
    {
        int ConsoleLine();
        int ConsoleColumn();
    }
}
