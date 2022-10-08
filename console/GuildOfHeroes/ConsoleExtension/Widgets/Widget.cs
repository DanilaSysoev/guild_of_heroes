using System.Collections.Generic;

namespace ConsoleExtension.Widgets
{
    public abstract class Widget : IWidget
    {
        public virtual int Line { get; set; }
        public virtual int Column { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public IReadOnlyList<IWidget> Children 
        { 
            get 
            {
                return children;
            }
        }

        public IWidget Parent { get; set; }

        public Widget(
            IWidget parent = null,
            int line = 0,
            int column = 0,
            int width = 0,
            int height = 0
        )
        {
            Parent = parent;
            Line = line;
            Column = column;
            Width = width;
            Height = height;
        }

        public void AddChild(IWidget child)
        {
            children.Add(child);
        }

        public bool RemoveChild(IWidget child)
        {
            return children.Remove(child);
        }

        public void Clear()
        {
            children.Clear();
        }

        public void Draw()
        {
            if (Parent != null)
                Parent.Draw();
            DrawOwn();
        }

        public int ConsoleLine() 
        {
            if (Parent == null)
                return Line;
            return Parent.ConsoleLine() + Line;
        }
        public int ConsoleColumn() 
        {
            if (Parent == null)
                return Column;
            return Parent.ConsoleColumn() + Column;
        }

        protected abstract void DrawOwn();

        private List<IWidget> children;
    }
}
