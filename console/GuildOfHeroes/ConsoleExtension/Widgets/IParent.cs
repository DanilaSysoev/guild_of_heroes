using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleExtension.Widgets
{
    public interface IParent
    {
        IReadOnlyList<IPanel> Children { get; }

        void AddChild(IPanel child);
        bool RemoveChild(IPanel child);
    }
}
