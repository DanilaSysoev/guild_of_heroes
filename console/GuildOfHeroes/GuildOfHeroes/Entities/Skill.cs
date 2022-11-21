using GuildOfHeroes.Entities.Service;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuildOfHeroes.Entities
{
    public class Skill : ObjectWithName
    {
        private Skill(string name)
            : base(name)
        {
        }

        public override bool Equals(object obj)
        {
            var sObj = obj as Skill;
            if (sObj == null)
                return false;
            return Name == sObj.Name;
        }


        private static Dictionary<string, Skill> skills;

        public static Skill Get(string name)
        {
            return skills[name];
        }
        public static List<string> GetNames()
        {
            return skills.Keys.ToList();
        }
        public static List<Skill> GetAll()
        {
            return skills.Values.ToList();
        }
        public static void Load()
        {
            JArray skillsData = null;
            using (var reader = new StreamReader("Data/Skills.json"))
                skillsData = JArray.Parse(reader.ReadToEnd());

            skills = JsonBuilder.BuildKeyValueDictionary(
                skillsData,
                keyData => keyData.Value<string>(),
                valueData => new Skill(valueData.Value<string>())
            );
        }
    }
}