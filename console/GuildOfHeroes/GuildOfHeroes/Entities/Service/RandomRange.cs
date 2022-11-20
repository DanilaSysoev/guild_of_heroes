using System;

namespace GuildOfHeroes
{
    public class RandomRange
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public RandomRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Get()
        {
            return RandomGlobal.Range(Min, Max);
        }
    }
}