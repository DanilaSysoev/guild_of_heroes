namespace ConsoleExtension.Widgets
{
    public interface IChild
    {
        IWidget Parent { get; set; }
    }
}
