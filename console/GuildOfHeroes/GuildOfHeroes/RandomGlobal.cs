using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
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
