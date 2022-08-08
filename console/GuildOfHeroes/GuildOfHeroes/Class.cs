using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuildOfHeroes
{
    public class Class : ObjectWithName
    {
        private Dictionary<Skill, int> skillModifiers;

        private Class(string name, Dictionary<Skill, int> skillModifiers) 
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
            Dictionary<Skill, int> modifiers = new Dictionary<Skill, int>();
            string name = "";
            foreach(var token in tokens)
            {
                var keyValue = token.Split(':');
                if(keyValue[0] == "name")
                {
                    name = token.Split(':')[1];
                }
                else if (keyValue[0] == "skill")
                {
                    var nameValue = keyValue[1].Split('=');
                    modifiers.Add(Skill.Get(nameValue[0]), int.Parse(nameValue[1]));
                }
            }
            return new Class(name, modifiers);
        }
    }
}