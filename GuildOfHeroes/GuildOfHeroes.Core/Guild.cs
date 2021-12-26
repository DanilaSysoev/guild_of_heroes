using GuildOfHeroes.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuildOfHeroes.Core
{
    public class Guild : NameableBase
    {
        public int HeroesCount => heroes.Count;
        public IReadOnlyList<Hero> Heroes => heroes;
        public int HeroesDailyPayment => heroes.Sum(h => h.DailyFee);

        public void AddHero(Hero hero)
        {
            if (hero == null)
                throw new ArgumentException("Hero can not be null");            
            if (heroes.Contains(hero))
                throw new ArgumentException("Hero already added");

            heroes.Add(hero);
        }
        public void RemoveHero(Hero hero)
        {
            if(!heroes.Remove(hero))
                throw new InvalidOperationException(
                    "Attempt to delete non-existent hero"
                );
        }
        public void AddResource(string name, int count)
        {

        }
        public int GetResourcesTypesCount()
        {
            return 0;
        }


        public static Guild Create(string name)
        {
            if (name.Length == 0)
                throw new ArgumentException("name can not be empty");
            return new Guild(name);
        }



        private Guild(string name)
            : base(name)
        {
            heroes = new List<Hero>();
        }

        private List<Hero> heroes;
    }
}
