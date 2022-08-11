using GuildOfHeroes.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuildOfHeroes
{
    public class Class : ObjectWithName
    {
        private Dictionary<Skill, int> skillModifiers;
        public int LoyaltyModifier { get; private set; }

        private Class(
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
            var cObj = obj as Class;
            if (cObj == null)
                return false;
            return Name == cObj.Name;
        }


        private static Dictionary<string, Class> classes;

        public static Class Get(string name)
        {
            return classes[name];
        }
        public static List<string> GetNames()
        {
            return classes.Keys.ToList();
        }
        public static List<Class> GetAll()
        {
            return classes.Values.ToList();
        }
        public static void Load()
        {
            using(StreamReader reader = new StreamReader("Data/Classes.txt"))
            {
                var classesTexts =
                    reader.ReadToEnd().Split(
                        new string[] { "\r\n\r\n" },
                        StringSplitOptions.RemoveEmptyEntries
                    );

                classes = new Dictionary<string, Class>();
                foreach(var text in classesTexts)
                {
                    var c = FromText(text);
                    classes.Add(c.Name, c);
                }
            }
        }
        private static Class FromText(string text)
        {
            var tokens = text.Split(
                new string[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries
            );
            
            var attributes = new AttributeCollection();
            attributes.Add("modifiers", new Dictionary<Skill, int>());
            foreach (var attributeDeskription in tokens)
                ParseAttribute(attributeDeskription, attributes);

            return new Class(
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