using GuildOfHeroes.Entities;

namespace GuildOfHeroes.Core
{
    public interface IWorldUpdater
    {
        void Setup();
        void Update(World world);
    }
}
