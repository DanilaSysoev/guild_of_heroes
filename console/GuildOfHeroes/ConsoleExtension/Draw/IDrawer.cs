using ConsoleExtension.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Draw
{
    public interface IDrawer
    {
        IDrawer Parent { get; }

        void Draw();
    }
}
