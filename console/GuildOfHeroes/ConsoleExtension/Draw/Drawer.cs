using ConsoleExtension.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Draw
{
    public abstract class Drawer : IDrawer
    {
        public IDrawer Parent { get; private set; }

        public Drawer(IDrawer parent)
        {
            Parent = parent;
        }

        public void Draw()
        {
            if (Parent != null)
                Parent.Draw();
            DrawOwn();
        }

        public abstract void DrawOwn();
    }
}
