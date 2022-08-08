using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuildOfHeroes
{
    public class Race : ObjectWithName
    {
        private Dictionary<Skill, int> skillModifiers;

        private Race(string name, Dictionary<Skill, int> skillModifiers)
            : base(name)
        {
            this.skillModifiers = skillModifiers;
        }

        public int GetSkillModifier(Skill skill)
        {
            if (skillModifiers.ContainsKey(skill))
                return skillModifiers[skill];
            return 0;
        }

        public override bool Equals(object obj)
        {
            var rObj = obj as Race;
            if (rObj == null)
                return false;
            return Name == rObj.Name;
        }


        private static Dictionary<string, Race> races;

        public static Race Get(string name)
        {
            return races[name];
        }
        public static List<string> GetNames()
        {
            return races.Keys.ToList();
        }
        public static void Load()
        {
            using (StreamReader reader = new StreamReader("Data/Races.txt"))
            {
                var racesTexts =
                    reader.ReadToEnd().Split(
                        new string[] { "\r\n\r\n" },
                        StringSplitOptions.RemoveEmptyEntries
                    );

                races = new Dictionary<string, Race>();
                foreach (var text in racesTexts)
                {
                    var race = FromText(text);
                    races.Add(race.Name, race);
                }
            }
        }
        private static Race FromText(string text)
        {
            var tokens = text.Split(
                new string[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries
            );
            Dictionary<Skill, int> modifiers = new Dictionary<Skill, int>();
            string name = "";
            foreach (var token in tokens)
            {
                var keyValue = token.Split(':');
                if (keyValue[0] == "name")
                {
                    name = token.Split(':')[1];
                }
                else if (keyValue[0] == "skill")
                {
                    var nameValue = keyValue[1].Split('=');
                    modifiers.Add(Skill.Get(nameValue[0]), int.Parse(nameValue[1]));
                }
            }
            return new Race(name, modifiers);
        }
    }
}