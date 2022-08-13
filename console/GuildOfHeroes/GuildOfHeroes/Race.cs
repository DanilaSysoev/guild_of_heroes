using GuildOfHeroes.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuildOfHeroes
{
    public class Race : ObjectWithName
    {
        private Dictionary<Skill, int> skillModifiers;
        public int LoyaltyModifier { get; private set; }

        private Race(
            string name,
            int loyaltyModifier,
            Dictionary<Skill, int> skillModifiers
        )
            : base(name)
        {
            this.skillModifiers = skillModifiers;
            LoyaltyModifier = loyaltyModifier;
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
        public static List<Race> GetAll()
        {
            return races.Values.ToList();
        }
        public static void Load()
        {
            JArray racesData = null;
            using (var reader = new StreamReader("Data/Races.json"))
                racesData = JArray.Parse(reader.ReadToEnd());

            races = JsonBuilder.BuildKeyValueDictionary(
                racesData,
                nameData => nameData.Value<string>("name"),
                BuildRace
            );
        }

        private static Race BuildRace(JToken data)
        {
            return new Race(
                data.Value<string>("name"),
                data.Value<int>("loyalty"),
                JsonBuilder.BuildKeyValueDictionary(
                    data["skillModifiers"],
                    nameData => Skill.Get(nameData.Value<string>("name")),
                    token => token.Value<int>("value")
                )
            );
        }
    }
}