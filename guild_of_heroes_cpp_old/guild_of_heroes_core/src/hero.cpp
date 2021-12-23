#include <stdexcept>
#include "hero.hpp"

Int
guild_of_heroes::Hero::getSkill(const std::string skillName) const
{
	auto skillPair = skills.find(skillName);
	if (skillPair == skills.end())
		return 0;
	return skillPair->second;
}
std::map<std::string, Int>
guild_of_heroes::Hero::getSkills() const {
	return skills;
}
Int
guild_of_heroes::Hero::getSkillsCount() const {
	return skills.size();
}

Int
guild_of_heroes::Hero::getDailyFee() const {
	return dailyFee;
}

void
guild_of_heroes::Hero::setDailyFee(Int fee) {
	if (fee < 0)
		throw std::invalid_argument("fee can not be negative");
	this->dailyFee = fee;
}

void 
guild_of_heroes::Hero::addSkill(const std::string& skillName, Int skillValue) {
	if(skillValue <= 0) 
		throw std::invalid_argument("skillValue must be positive");
	if(skillName.empty())
		throw std::invalid_argument("skillName can not be empty");
	skills[skillName] = skillValue;
}

guild_of_heroes::Hero*
guild_of_heroes::Hero::create(const std::string& name) {
	if (name.empty())
		throw std::invalid_argument("name can not be empty");
	return new Hero(name);
}

guild_of_heroes::Hero::Hero(const std::string& name)
	: Nameable(name), dailyFee(0)
{ }