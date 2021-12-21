#pragma once
#include <map>
#include "nameable.hpp"

namespace guild_of_heroes {
	class Hero : public Nameable {
	public:
		int getSkill(const std::string skillName) const;
		std::map<std::string, int> getSkills() const;
		int getSkillsCount() const;
		int getDailyFee() const;
		void setDailyFee(int fee);

		void addSkill(const std::string& skillName, int skillValue);

		static Hero* create(const std::string& name);


	private:
		Hero(const std::string& name);

		std::map<std::string, int> skills;
		int dailyFee;
	};
}