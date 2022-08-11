using GuildOfHeroes.Service;
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

            var attributes = new AttributeCollection();
            attributes.Add("modifiers", new Dictionary<Skill, int>());
            foreach (var attributeDeskription in tokens)
                ParseAttribute(attributeDeskription, attributes);

            return new Race(
                attributes.Get<string>("name"),
                attributes.Get<int>("loyalty"),
                attributes.Get<Dictionary<Skill, int>>("modifiers")
            );
        }

        private static void ParseAttribute(
            string attributeDeskription,
            AttributeCollection attributes
        )
        {
            var keyValue = attributeDeskription.Split(':');
            switch (keyValue[0].Trim().ToLower())
            {
                case "name":
                    attributes.Add("name", keyValue[1].Trim());
                    break;
                case "skill":
                    var nameValue = keyValue[1].Trim().Split(',');
                    attributes.Get<Dictionary<Skill, int>>("modifiers").
                        Add(
                            Skill.Get(nameValue[0].Trim()),
                            int.Parse(nameValue[1])
                        );
                    break;
                case "loyalty":
                    attributes.Add("loyalty", int.Parse(keyValue[1]));
                    break;
            }
        }
    }
}