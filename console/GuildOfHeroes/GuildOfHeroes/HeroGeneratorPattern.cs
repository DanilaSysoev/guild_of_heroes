using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
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
            using (StreamReader reader = new StreamReader("Data/HeroPatterns.txt"))
            {
                var patternsTexts =
                    reader.ReadToEnd().Split(
                        new string[] { "\r\n\r\n" },
                        StringSplitOptions.RemoveEmptyEntries
                    );

                patterns = new Dictionary<string, HeroGeneratorPattern>();
                foreach (var text in patternsTexts)
                {
                    var pattern = FromText(text);
                    patterns.Add(pattern.Name, pattern);
                }
            }
        }
        private static HeroGeneratorPattern FromText(string text)
        {
            RandomRange baseSkillRange = null;
            RandomRange paymentRange = null;
            var modifiers              = new Dictionary<Skill, int>();
            var name                   = "";
            var skillsRange            = new Dictionary<Skill, RandomRange>();
            var racesWeights           = new Dictionary<Race, int>();
            var classesWeights         = new Dictionary<Class, int>();

            var tokens = text.Split(
                new string[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries
            );

            foreach (var token in tokens)
            {
                var keyValue = token.Split(':');
                ParseData(
                    ref name, 
                    ref baseSkillRange,
                    ref paymentRange,
                    skillsRange,
                    racesWeights,
                    classesWeights,
                    keyValue
                );
            }

            return new HeroGeneratorPattern(
                name,
                baseSkillRange,
                paymentRange,
                skillsRange,
                racesWeights,
                classesWeights
            );
        }

        private static void ParseData(
            ref string name, 
            ref RandomRange baseSkillRange,
            ref RandomRange paymentRange,
            Dictionary<Skill, RandomRange> skillsRange,
            Dictionary<Race, int> racesWeights, 
            Dictionary<Class, int> classesWeights,
            string[] keyValue)
        {
            switch (keyValue[0].Trim().ToLower())
            {
                case "name":
                    name = ParseName(keyValue[1]);
                    break;
                case "baseskillrange":
                    baseSkillRange = ParseRange(keyValue[1]);
                    break;
                case "paymentrange":
                    paymentRange = ParseRange(keyValue[1]);
                    break;
                case "skill":
                    ParseSkillRange(skillsRange, keyValue[1]);
                    break;
                case "class":
                    ParseClassWeight(classesWeights, keyValue[1]);
                    break;
                case "race":
                    ParseRaceWeight(racesWeights, keyValue[1]);
                    break;
            }
        }

        private static void ParseRaceWeight(
            Dictionary<Race, int> racesWeights, 
            string value
        )
        {
            var nameValue = value.Trim().Split(',');
            racesWeights.Add(
                Race.Get(nameValue[0].Trim()),
                int.Parse(nameValue[1])
            );
        }

        private static void ParseClassWeight(
            Dictionary<Class, int> classesWeights,
            string value
        )
        {
            var nameValue = value.Trim().Split(',');
            classesWeights.Add(
                Class.Get(nameValue[0].Trim()),
                int.Parse(nameValue[1])
            );
        }

        private static void ParseSkillRange(
            Dictionary<Skill, RandomRange> skillsRange,
            string value
        )
        {
            var nameValues = value.Trim().Split(',');
            skillsRange.Add(
                Skill.Get(nameValues[0].Trim()),
                new RandomRange(
                    int.Parse(nameValues[1]),
                    int.Parse(nameValues[2])
                )
            );
        }

        private static RandomRange ParseRange(
            string value            
        )
        {
            var minMax = value.Trim().Split(',');
            return new RandomRange(
                int.Parse(minMax[0]),
                int.Parse(minMax[1])
            );
        }

        private static string ParseName(
            string value
        )
        {
            return value.Trim();
        }
    }
}
