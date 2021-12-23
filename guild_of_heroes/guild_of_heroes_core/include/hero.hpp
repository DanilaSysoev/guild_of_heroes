#pragma once
#include <map>
#include "nameable.hpp"

namespace guild_of_heroes {
	class Hero : public Nameable {
	public:
		Int getSkill(const std::string skillName) const;
		std::map<std::string, Int> getSkills() const;
		Int getSkillsCount() const;
		Int getDailyFee() const;
		void setDailyFee(Int fee);

		void addSkill(const std::string& skillName, Int skillValue);

		static Hero* create(const std::string& name);


	private:
		Hero(const std::string& name);

		std::map<std::string, Int> skills;
		Int dailyFee;
	};
}