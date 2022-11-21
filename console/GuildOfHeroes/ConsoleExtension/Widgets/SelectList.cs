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
        public bool SelectedNumberDisplay { get; set; }

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
           int line = 0,
           int column = 0,
           int width = 0,
           int height = 0,
           IWidget parent = null
        ) : base(line, column, width, height, parent)
        {
            items = new List<T>();
            lines = new List<IWidget>();
            selectedIndex = -1;
            SelectionBackgroundColor = DefaultBackgroundColor;
            SelectionForegroundColor = DefaultForegroundColor;

            visibleAreaBeginLine = 0;
            visibleAreaEndLine = 0;

            SelectedNumberDisplay = false;
        }

        public void AddItem(T item)
        {
            var newTextLine = new TextLine(items.Count, 1, Area.Width - 2, this);
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
                int newSelected = (SelectedIndex + 1) % lines.Count;
                RemoveSelection();
                SetSelection(newSelected);
            }
        }
        public void MoveSelectionOnPrevious()
        {
            if (lines.Count == 0)
                return;

            if (SelectedIndex < 0)
                SetSelection(lines.Count - 1);
            else
            {
                int newSelected =
                    (SelectedIndex + lines.Count - 1) % lines.Count;
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
            for (int i = visibleAreaBeginLine; i < visibleAreaEndLine; ++i)
            {
                lines[i].Area.Line -= visibleAreaBeginLine;
                lines[i].Draw();
                lines[i].Area.Line += visibleAreaBeginLine;
            }
            if (SelectedNumberDisplay && SelectedIndex >= 0)
                DrawSelectedNumber();
        }

        private List<T> items;
        private List<IWidget> lines;
        private int selectedIndex;
        private Alignment itemsAlignment;
        private int visibleAreaBeginLine;
        private int visibleAreaEndLine;

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
            int additionalOffset = SelectedNumberDisplay ? 1 : 0;

            if (SelectedIndex < 0)
                return;
            if (SelectedIndex < visibleAreaBeginLine)
                MoveUpToSelected(additionalOffset);
            else if (SelectedIndex >= visibleAreaEndLine)
                MovedownToSelected(additionalOffset);
            else
                CorrectionWithSelectedVisual();
        }

        private void CorrectionWithSelectedVisual()
        {
            if (SelectedNumberDisplay &&
               visibleAreaEndLine - visibleAreaBeginLine == Area.Height)
                --visibleAreaEndLine;
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
                    line.Area.Line,
                    line.Area.Column - 1,
                    line.Area.Width + 2,
                    this
                );
            decorator.BackgroundColor = SelectionBackgroundColor;
            decorator.ForegroundColor = SelectionForegroundColor;
            decorator.AddChild(line);
            return decorator;
        }
        private void DrawSelectedNumber()
        {
            TextLine line = new TextLine(Area.Height - 1, 0, Area.Width, this);
            line.Text = string.Format("{0}/{1}", SelectedIndex + 1, lines.Count);
            line.Alignment = Alignment.BottomRight;
            line.BackgroundColor = SelectionBackgroundColor;
            line.ForegroundColor = SelectionForegroundColor;
            line.Draw();
        }
    }
}
