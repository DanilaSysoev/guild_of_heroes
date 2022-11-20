using GuildOfHeroes.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public override int GetHashCode()
        {
            int hashCode = -1300808280;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<Skill, int>>.Default.GetHashCode(skillModifiers);
            hashCode = hashCode * -1521134295 + LoyaltyModifier.GetHashCode();
            return hashCode;
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
            JArray classesData = null;
            using (var reader = new StreamReader("Data/Classes.json"))
                classesData = JArray.Parse(reader.ReadToEnd());

            classes = JsonBuilder.BuildKeyValueDictionary(
                classesData,
                nameData => nameData.Value<string>("name"),
                BuildCalss
            );
        }

        private static Class BuildCalss(JToken classData)
        {
            return new Class(
                classData.Value<string>("name"),
                classData.Value<int>("loyalty"),
                JsonBuilder.BuildKeyValueDictionary(
                    classData["skillModifiers"],
                    nameData => Skill.Get(nameData.Value<string>("name")),
                    token => token.Value<int>("value")
                )
            );
        }
    }
}