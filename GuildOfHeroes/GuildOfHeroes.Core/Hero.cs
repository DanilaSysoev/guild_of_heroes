using GuildOfHeroes.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildOfHeroes.Core
{
    public class Hero : NameableBase
    {
        /*
         * Int getSkill(const std::string skillName) const;
		std::map<std::string, Int> getSkills() const;
		Int getSkillsCount() const;
		Int getDailyFee() const;
		void setDailyFee(Int fee);

		void addSkill(const std::string& skillName, Int skillValue);

		static Hero* create(const std::string& name);


	private:
		Hero(const std::string& name);

		std::map<std::string, Int> skills;
         */

        public int DailyFee 
        {
            get { return dailyFee; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Fee can not be negative");
                dailyFee = value;
            }
        }
        public int SkillsCount => skills.Count;
        public IReadOnlyDictionary<string, int> Skills => skills;

        public int GetSkillValue(string skillName)
        {
            if (NotOwnSkill(skillName))
                return 0;
            return skills[skillName];
        }
        public void AddSkill(string skillName, int skillValue)
        {
            if (skillValue <= 0)
                throw new ArgumentException("SkillValue must be positive");
            if (skillName.Length == 0)
                throw new ArgumentException("SkillName can not be empty");
            if (NotOwnSkill(skillName))
                skills.Add(skillName, skillValue);
            else
                skills[skillName] = skillValue;
        }
        public bool OwnSkill(string skillName)
        {
            return skills.ContainsKey(skillName);
        }
        public bool NotOwnSkill(string skillName)
        {
            return !skills.ContainsKey(skillName);
        }


        public static Hero Create(string name)
        {
            return new Hero(name);
        }



        private Hero(string name)
            : base(name)
        {
            dailyFee = 0;
            skills = new Dictionary<string, int>();
        }

        private int dailyFee;
        private Dictionary<string, int> skills;
    }
}
