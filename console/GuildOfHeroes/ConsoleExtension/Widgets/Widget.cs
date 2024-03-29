﻿using ConsoleExtension.Draw;
using ConsoleExtension.Service;
using System;
using System.Collections.Generic;

namespace ConsoleExtension.Widgets
{
    public abstract class Widget : IWidget
    {
        public virtual Rectangle Area { get; set; }

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

        public IConsole Console { get; private set; }

        public Widget(
            IConsole console,
            int line = 0,
            int column = 0,
            int width = 0,
            int height = 0,
            IWidget parent = null
        )
        {
            children = new List<IWidget>();

            Area = new Rectangle(line, column, width, height);
            Enable = true;

            BackgroundColor = DefaultBackgroundColor;
            ForegroundColor = DefaultForegroundColor;
            Console = console;

            if (parent != null)
                parent.AddChild(this);
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

        public virtual void Clear()
        {
            children.Clear();
        }

        public void Draw()
        {
            if (!Enable)
                return;

            SetupConsole();
            DrawOwnBeforeChildren();
            ResetConsole();

            foreach (var child in Children)
                child.Draw();

            SetupConsole();
            DrawOwnAfterChildren();
            ResetConsole();
        }

        public virtual int ConsoleLine()
        {
            if (Parent == null)
                return Area.Line;
            return Parent.ConsoleLine() + Area.Line;
        }
        public virtual int ConsoleColumn()
        {
            if (Parent == null)
                return Area.Column;
            return Parent.ConsoleColumn() + Area.Column;
        }

        public Rectangle RootArea()
        {
            return new Rectangle(
                0,
                0,
                Console.BufferWidth,
                Console.BufferHeight
            );
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
                    Parent.ConsoleColumn() + Parent.Area.Width
                );
            return Console.BufferWidth;
        }
        protected int ParentConsoleBottom()
        {
            if (Parent != null)
                return Math.Min(
                    Console.BufferHeight,
                    Parent.ConsoleLine() + Parent.Area.Height
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

        protected virtual void DrawOwnBeforeChildren() { }
        protected virtual void DrawOwnAfterChildren() { }

        protected void DrawSymbolIfPossible(int line, int column, char symbol)
        {
            if (line >= 0 && line < Console.BufferHeight &&
               column >= 0 && column < Console.BufferWidth)
            {
                Console.SetCursorPosition(column, line);
                Console.Write(symbol);
            }
        }

        private List<IWidget> children;
    }
}
