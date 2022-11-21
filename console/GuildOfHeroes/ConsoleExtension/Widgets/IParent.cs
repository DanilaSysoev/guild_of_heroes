using System.Collections.Generic;

namespace ConsoleExtension.Widgets
{
    public interface IParent
    {
        IReadOnlyList<IWidget> Children { get; }

        void AddChild(IWidget child);
        bool RemoveChild(IWidget child);
        void Clear();
    }
}
