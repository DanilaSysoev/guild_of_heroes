using System;

namespace GuildOfHeroes.Entities.Service
{
    public static class RandomGlobal
    {
        public static int Range(int min, int max)
        {
            return random.Next(min, max);
        }

        private static Random random;
        static RandomGlobal()
        {
            random = new Random();
        }
    }
}
