using ConsoleExtension.Draw;

namespace ConsoleExtension.Widgets
{
    public interface IWidget : IPanel, IParent, IChild, IDrawer, IEnable, IColor
    {
        int ConsoleLine();
        int ConsoleColumn();
    }
}
