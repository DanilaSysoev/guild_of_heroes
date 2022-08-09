using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuildOfHeroes
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
            skills = new Dictionary<string, Skill>();
            using (StreamReader reader = new StreamReader("Data/Skills.txt"))
            {
                while (!reader.EndOfStream)
                {
                    var name = reader.ReadLine();
                    skills.Add(name, new Skill(name));
                }
            }
        }
    }
}