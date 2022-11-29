using ConsoleExtension.Draw;
using GuildOfHeroes.Entities;

namespace GuildOfHeroes.Core
{
    public interface IDrawManager
    {
        void Setup();
        void Draw(World world);
    }
}
