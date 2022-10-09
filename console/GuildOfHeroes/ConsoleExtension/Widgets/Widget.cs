﻿using System;
using System.Collections.Generic;

namespace ConsoleExtension.Widgets
{
    public abstract class Widget : IWidget
    {
        public virtual int Line { get; set; }
        public virtual int Column { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public bool Enable { get; set; }

        public IReadOnlyList<IWidget> Children 
        { 
            get 
            {
                return children;
            }
        }

        public IWidget Parent { get; set; }

        public Widget(
            int line = 0,
            int column = 0,
            int width = 0,
            int height = 0,
            IWidget parent = null
        )
        {
            children = new List<IWidget>();

            Parent = parent;
            Line = line;
            Column = column;
            Width = width;
            Height = height;
            Enable = true;

            BackgroundColor = DefaultBackgroundColor;
            ForegroundColor = DefaultForegroundColor;
        }

        public void AddChild(IWidget child)
        {
            children.Add(child);
            child.Parent = this;
        }

        public bool RemoveChild(IWidget child)
        {
            bool exist = children.Remove(child);
            if (exist)
                child.Parent = null;
            return exist;
        }

        public void Clear()
        {
            children.Clear();
        }

        public void Draw()
        {
            if (!Enable)
                return;

            foreach (var child in Children)
                child.Draw();

            SetupConsole();
            DrawOwn();
            ResetConsole();
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

        protected int ParentConsoleLine()
        {
            if (Parent != null)
                return Math.Max(0, Parent.ConsoleLine());
            return 0;
        }
        protected int ParentConsoleColumn()
        {
            if (Parent != null)
                return Math.Max(0, Parent.ConsoleColumn());
            return 0;
        }
        protected int ParentConsoleRight()
        {
            if (Parent != null)
                return Math.Min(
                    Console.BufferWidth,
                    Parent.ConsoleColumn() + Parent.Width
                );
            return Console.BufferWidth;
        }
        protected int ParentConsoleBottom()
        {
            if (Parent != null)
                return Math.Min(
                    Console.BufferHeight, 
                    Parent.ConsoleLine() + Parent.Height
                );
            return Console.BufferHeight;
        }
        protected bool PositionInsideParent(int line, int column)
        {
            return LineInsideParent(line) &&
                   ColumnInsideParent(column);
        }
        protected bool LineInsideParent(int line)
        {
            return line >= ParentConsoleLine() &&
                   line < ParentConsoleBottom();
        }
        protected bool ColumnInsideParent(int column)
        {
            return column >= ParentConsoleColumn() &&
                   column < ParentConsoleRight();
        }

        public static ConsoleColor 
        DefaultBackgroundColor = ConsoleColor.Black;

        public static ConsoleColor 
        DefaultForegroundColor = ConsoleColor.Gray;


        private void SetupConsole()
        {
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
        }
        private void ResetConsole()
        {
            Console.ResetColor();
        }

        protected abstract void DrawOwn();

        private List<IWidget> children;
    }
}