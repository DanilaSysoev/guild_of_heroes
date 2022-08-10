namespace GuildOfHeroes
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(100, 35);
            game.Run();
        }
    }
}
