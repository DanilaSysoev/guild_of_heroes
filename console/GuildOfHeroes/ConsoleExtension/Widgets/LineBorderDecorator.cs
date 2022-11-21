namespace ConsoleExtension.Widgets
{
    public class LineBorderDecorator : Widget
    {
        public char LeftDecor { get; set; }
        public char RightDecor { get; set; }

        public LineBorderDecorator(
            int line = 0,
            int column = 0,
            int width = 0,
            IWidget parent = null
        ) : base(line, column, width, 1, parent)
        {
            LeftDecor = DefaultLeftDecor;
            RightDecor = DefaultRightDecor;
        }

        protected override void DrawOwnBeforeChildren()
        {
            for (int i = Area.Column + 1; i < Area.Column + Area.Width - 1; ++i)
                DrawSymbolIfPossible(ConsoleLine(), ConsoleColumn() + i, ' ');
        }

        protected override void DrawOwnAfterChildren()
        {
            DrawSymbolIfPossible(
                ConsoleLine(),
                ConsoleColumn(),
                LeftDecor
            );
            DrawSymbolIfPossible(
                ConsoleLine(),
                ConsoleColumn() + Area.Width - 1,
                RightDecor
            );
        }

        public const char DefaultLeftDecor = '<';
        public const char DefaultRightDecor = '>';
    }
}
