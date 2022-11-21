using GuildOfHeroes.Entities.Service;
using System.Collections.Generic;
using System.Linq;

namespace GuildOfHeroes.Entities
{
    public class Hero
    {
        public const int BASE_LOYALTY = 0;

        public string Name { get; private set; }

        public int Level { get; private set; }
        public int Experience { get; private set; }

        public Race Race { get; private set; }
        public Class Class { get; private set; }
        public Dictionary<Skill, int> Skills { get; private set; }

        public int Payment { get; private set; }

        public int AccumulatedLoyalty { get; private set; }
        public int Loyalty
        {
            get
            {
                return BASE_LOYALTY +
                       AccumulatedLoyalty +
                       Race.LoyaltyModifier +
                       Class.LoyaltyModifier;
            }
        }

        public int GetSkill(Skill skill)
        {
            return
                Skills[skill] +
                Race.GetSkillModifier(skill) +
                Class.GetSkillModifier(skill);
        }

        private Hero()
        {
            Level = 1;
            Experience = 0;
            AccumulatedLoyalty = 0;
            Skills = new Dictionary<Skill, int>();
        }


        public static Hero Create(HeroGeneratorPattern pattern)
        {
            Hero hero = new Hero();
            hero.Race = SelectWithWeights(pattern.RacesWeights);
            hero.Class = SelectWithWeights(pattern.ClassesWeights);
            foreach (var skill in Skill.GetAll())
            {
                if (pattern.SkillsRange.ContainsKey(skill))
                    hero.Skills.Add(skill, pattern.SkillsRange[skill].Get());
                else
                    hero.Skills.Add(skill, pattern.BaseSkillsRange.Get());
            }
            hero.Payment = pattern.PaymentRange.Get();

            hero.Name = NameGenerator.Generate(hero);

            return hero;
        }

        private static T SelectWithWeights<T>(
            IReadOnlyDictionary<T, int> entityWeights
        )
        {
            int sum = entityWeights.Sum(kvp => kvp.Value);
            int v = RandomGlobal.Range(0, sum);
            int curSum = 0;
            foreach (var entityWeight in entityWeights)
            {
                if (v >= curSum && v < curSum + entityWeight.Value)
                    return entityWeight.Key;
                curSum += entityWeight.Value;
            }
            return default(T);
        }
    }
}
