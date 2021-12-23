using GuildOfHeroes.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildOfHeroes.Core
{
    public class Settlement : NameableBase
    {
        public int  Size
        {
            get { return size; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Settlement size can not be negative");
                size = value;
                if (value > MaxSizeInPast)
                    MaxSizeInPast = value;
            }
        }
        public int  MaxSizeInPast { get; private set; }
        public bool IsAbandoned => Size == 0;
        public bool MonoRacial => racesWeights.Count == 1;

        public int  GetRaceWeight(string race)
        {
            if (racesWeights.Count == 0)
                throw new InvalidOperationException("Settlement state error: races not setupped");
            if (race.Length == 0)
                throw new ArgumentException("Race name can not be empty");
                        
            if (RaceNotExist(race))
                return 0;
            return racesWeights[race];
        }
        public void SetRaceWeight(string race, int weight)
        {
            if (weight < 0)
                throw new ArgumentException("Race weight can not be negative");
            if (race.Length == 0)
                throw new ArgumentException("Race name can not be empty");

            if (weight == 0 && RaceExist(race))
            {
                if (MonoRacial)
                    throw new InvalidOperationException("Impossible remove last race");
                racesWeights.Remove(race);
            }
            else if (weight != 0)
            {
                if (RaceNotExist(race))
                    racesWeights.Add(race, 0);
                racesWeights[race] = weight;
            }
        }
        public bool RaceExist(string race)
        {
            return racesWeights.ContainsKey(race);
        }
        public bool RaceNotExist(string race)
        {
            return !racesWeights.ContainsKey(race);
        }


        public static Settlement Create(string name)
        {
            return new Settlement(name);
        }



        private Settlement(string name)
            : base(name)
        {
            MaxSizeInPast = 1;
            size = 1;
            racesWeights = new Dictionary<string, int>();
        }

        private int size;
        private Dictionary<string, int> racesWeights;

    }
}
