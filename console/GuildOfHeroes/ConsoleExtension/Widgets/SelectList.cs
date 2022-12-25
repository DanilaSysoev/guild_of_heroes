using ConsoleExtension.Draw;
using System;
using System.Collections.Generic;

namespace ConsoleExtension.Widgets
{
    public class SelectList<T> : Widget
    {
        public IReadOnlyList<T> Items { get { return items; } }
        public T SelectedItem { get { return items[selectedIndex]; } }
        public int SelectedIndex { get { return selectedIndex; } }
        public ConsoleColor SelectionBackgroundColor { get; set; }
        public ConsoleColor SelectionForegroundColor { get; set; }
        public bool SelectedNumberDisplay 
        {
            get { return selectedNumberDisplay; }
            set 
            {
                selectedNumberDisplay = value;
                SetupVisibleArea();
            }
        }
        public bool IsCycled { get; set; }
        public string Title { get; set; }
        public bool TitleDisplay { get; set; }
        public Alignment ItemsAlignment
        {
            get { return itemsAlignment; }
            set
            {
                itemsAlignment = value;
                var selected = SelectedIndex;
                SetItems(items);
                SetSelection(selected);
            }
        }

        public SelectList(
            IConsole console,
            int line = 0,
            int column = 0,
            int width = 0,
            int height = 0,
            IWidget parent = null
        ) : base(console, line, column, width, height, parent)
        {
            items = new List<T>();
            lines = new List<IWidget>();
            selectedIndex = -1;
            SelectionBackgroundColor = DefaultBackgroundColor;
            SelectionForegroundColor = DefaultForegroundColor;

            visibleAreaBeginLine = 0;
            visibleAreaEndLine = 0;

            SelectedNumberDisplay = false;
            IsCycled = true;

            Title = "";
            TitleDisplay = false;
        }

        public void AddItem(T item)
        {
            var newTextLine = 
                new TextLine(Console, items.Count, 1, Area.Width - 2);
            newTextLine.Parent = this;
            newTextLine.Text = item.ToString();
            newTextLine.Alignment = ItemsAlignment;
            lines.Add(newTextLine);
            items.Add(item);

            visibleAreaEndLine =
                Math.Min(visibleAreaBeginLine + Area.Height, lines.Count);
        }
        public void ClearItems()
        {
            items = new List<T>();
            lines = new List<IWidget>();
            selectedIndex = -1;
        }
        public void RemoveItem(T item)
        {
            RemoveAt(items.IndexOf(item));
        }
        public void RemoveAt(int position)
        {
            if (position < 0 || position >= lines.Count)
                return;

            int newSelectedIndex = CalcNewSelectedIndexOnRemove(position);
            RemoveSelection();
            items.RemoveAt(position);
            lines.RemoveAt(position);
            SetSelection(newSelectedIndex);
        }

        public void AddItems(IEnumerable<T> items)
        {
            foreach (var item in items)
                AddItem(item);
        }
        public void SetItems(IEnumerable<T> items)
        {
            ClearItems();
            AddItems(items);
        }

        public void MoveSelectionOnNext()
        {
            if (lines.Count == 0)
                return;
            if (SelectedIndex < 0)
                SetSelection(0);
            else
            {
                int newSelected = SelectedIndex + 1;
                if (newSelected >= lines.Count && !IsCycled)
                    return;
                if(newSelected >= lines.Count)
                    newSelected = 0;
                RemoveSelection();
                SetSelection(newSelected);
            }
        }
        public void MoveSelectionOnPrevious()
        {
            if (lines.Count == 0)
                return;
            if (SelectedIndex < 0 && IsCycled)
                SetSelection(lines.Count - 1);
            else
            {
                int newSelected = SelectedIndex - 1;
                if (newSelected < 0 && !IsCycled)
                    return;
                if (newSelected < 0)
                    newSelected = lines.Count - 1;
                RemoveSelection();
                SetSelection(newSelected);
            }
        }
        public void Select(int index)
        {
            if (lines.Count == 0)
                return;
            RemoveSelection();
            SetSelection(index);
        }

        protected override void DrawOwnBeforeChildren()
        {
            int titleOffset = TitleDisplay ? 1 : 0;

            for (int i = visibleAreaBeginLine; i < visibleAreaEndLine; ++i)
            {
                lines[i].Area.Line -= visibleAreaBeginLine - titleOffset;
                lines[i].Draw();
                lines[i].Area.Line += visibleAreaBeginLine - titleOffset;
            }
            if (TitleDisplay)
                DrawTitle();
            if (SelectedNumberDisplay && SelectedIndex >= 0)
                DrawSelectedNumber();
        }

        private List<T> items;
        private List<IWidget> lines;
        private int selectedIndex;
        private Alignment itemsAlignment;
        private int visibleAreaBeginLine;
        private int visibleAreaEndLine;
        private bool selectedNumberDisplay;

        private int CalcNewSelectedIndexOnRemove(int position)
        {
            if (SelectedIndex < 0)
                return SelectedIndex;
            if (SelectedIndex == position)
                return -1;
            if (SelectedIndex < position)
                return SelectedIndex - 1;
            return SelectedIndex;
        }
        private void RemoveSelection()
        {
            if (SelectedIndex < 0)
                return;
            var decorator = lines[SelectedIndex];
            lines[SelectedIndex] = decorator.Children[0];
            decorator.RemoveChild(lines[SelectedIndex]);
            lines[SelectedIndex].Area.Line = decorator.Area.Line;
            lines[SelectedIndex].Parent = this;
            decorator.Clear();
            selectedIndex = -1;
        }
        private void SetSelection(int index)
        {
            if (index < 0)
                return;
            var line = lines[index];
            var decorator = BuildDecorator(line);
            line.Area.Line = 0;
            lines[index] = decorator;
            selectedIndex = index;
            SetupVisibleArea();
        }
        private void SetupVisibleArea()
        {
            int additionalOffset = (SelectedNumberDisplay ? 1 : 0) + 
                                   (TitleDisplay ? 1 : 0);
            if (SelectedIndex < 0)
                return;
            if (SelectedIndex < visibleAreaBeginLine)
                MoveUpToSelected(additionalOffset);
            else if (SelectedIndex >= visibleAreaEndLine)
                MovedownToSelected(additionalOffset);
            else
                CorrectionWithTitleAndSelectedVisual(additionalOffset);
        }

        private void CorrectionWithTitleAndSelectedVisual(int additionalOffset)
        {
            if (visibleAreaEndLine - visibleAreaBeginLine + additionalOffset >= Area.Height)
                visibleAreaEndLine = visibleAreaBeginLine + Area.Height - additionalOffset;
        }

        private void MovedownToSelected(int additionalOffset)
        {
            visibleAreaEndLine = SelectedIndex + 1;
            visibleAreaBeginLine =
                Math.Max(visibleAreaEndLine - Area.Height + additionalOffset,
                0
            );
        }
        private void MoveUpToSelected(int additionalOffset)
        {
            visibleAreaBeginLine = SelectedIndex;
            visibleAreaEndLine =
                Math.Min(
                    visibleAreaBeginLine + Area.Height - additionalOffset,
                    lines.Count
                );
        }
        private LineBorderDecorator BuildDecorator(IWidget line)
        {
            var decorator =
                new LineBorderDecorator(
                    Console,
                    line.Area.Line,
                    line.Area.Column - 1,
                    line.Area.Width + 2                    
                );
            decorator.Parent = this;
            decorator.BackgroundColor = SelectionBackgroundColor;
            decorator.ForegroundColor = SelectionForegroundColor;
            decorator.AddChild(line);
            return decorator;
        }
        private void DrawSelectedNumber()
        {
            TextLine line = 
                new TextLine(Console, Area.Height - 1, 0, Area.Width, null);
            line.Text =
                string.Format("{0}/{1}", SelectedIndex + 1, lines.Count);
            line.Alignment = Alignment.BottomRight;
            line.BackgroundColor = SelectionBackgroundColor;
            line.ForegroundColor = SelectionForegroundColor;
            line.Parent = this;
            line.Draw();
        }
        private void DrawTitle()
        {
            TextLine line =
                new TextLine(Console, 0, 0, Area.Width, null);
            line.Text = Title;
            line.Alignment = Alignment.TopLeft;
            line.BackgroundColor = SelectionBackgroundColor;
            line.ForegroundColor = SelectionForegroundColor;
            line.Parent = this;
            line.Draw();
        }
    }
}
