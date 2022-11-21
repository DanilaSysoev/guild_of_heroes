using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuildOfHeroes.Entities.Service
{
    public class HeroGeneratorPattern : ObjectWithName
    {
        public RandomRange BaseSkillsRange { get; private set; }
        public RandomRange PaymentRange { get; private set; }
        public IReadOnlyDictionary<Skill, RandomRange> SkillsRange { get; private set; }
        public IReadOnlyDictionary<Race, int> RacesWeights { get; private set; }
        public IReadOnlyDictionary<Class, int> ClassesWeights { get; private set; }

        private HeroGeneratorPattern(
            string name,
            RandomRange baseSkillsRange,
            RandomRange paymentRange,
            Dictionary<Skill, RandomRange> skillsRange,
            Dictionary<Race, int> racesWeights,
            Dictionary<Class, int> classesWeights
        ) : base(name)
        {
            BaseSkillsRange = baseSkillsRange;
            PaymentRange = paymentRange;
            SkillsRange = skillsRange;

            if (racesWeights.Count == 0)
            {
                racesWeights = new Dictionary<Race, int>();
                foreach (var race in Race.GetAll())
                    racesWeights.Add(race, 1);
            }
            RacesWeights = racesWeights;

            if (classesWeights.Count == 0)
            {
                classesWeights = new Dictionary<Class, int>();
                foreach (var c in Class.GetAll())
                    classesWeights.Add(c, 1);
            }
            ClassesWeights = classesWeights;
        }


        public static HeroGeneratorPattern Get(string name)
        {
            return patterns[name];
        }
        public static List<string> GetNames()
        {
            return patterns.Keys.ToList();
        }
        public static List<HeroGeneratorPattern> GetAll()
        {
            return patterns.Values.ToList();
        }

        private static Dictionary<string, HeroGeneratorPattern> patterns;

        public static void Load()
        {
            JArray heroPatternsData = null;
            using (var reader = new StreamReader("Data/HeroPatterns.json"))
                heroPatternsData = JArray.Parse(reader.ReadToEnd());

            patterns = JsonBuilder.BuildKeyValueDictionary(
                heroPatternsData,
                nameData => nameData.Value<string>("name"),
                BuildHeroPattern
            );
        }

        private static HeroGeneratorPattern BuildHeroPattern(JToken data)
        {
            return new HeroGeneratorPattern(
                data.Value<string>("name"),
                new RandomRange(
                    data["baseSkillRange"].Value<int>("min"),
                    data["baseSkillRange"].Value<int>("max")
                ),
                new RandomRange(
                    data["paymentRange"].Value<int>("min"),
                    data["paymentRange"].Value<int>("max")
                ),
                JsonBuilder.BuildKeyValueDictionary(
                    data["skillModifiers"],
                    nameData => Skill.Get(nameData.Value<string>("name")),
                    valueData => new RandomRange(
                        valueData["value"].Value<int>("min"),
                        valueData["value"].Value<int>("max")
                    )
                ),
                JsonBuilder.BuildKeyValueDictionary(
                    data["raceWeights"],
                    nameData => Race.Get(nameData.Value<string>("name")),
                    valueData => valueData.Value<int>("value")
                ),
                JsonBuilder.BuildKeyValueDictionary(
                    data["classWeights"],
                    nameData => Class.Get(nameData.Value<string>("name")),
                    valueData => valueData.Value<int>("value")
                )
            );
        }
    }
}
