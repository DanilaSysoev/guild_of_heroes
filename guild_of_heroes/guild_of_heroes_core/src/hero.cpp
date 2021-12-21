#include <stdexcept>
#include "hero.hpp"

std::string
guild_of_heroes::Hero::getName() const {
	return name;
}
int
guild_of_heroes::Hero::getSkill(const std::string skillName) const
{
	auto skillPair = skills.find(skillName);
	if (skillPair == skills.end())
		return 0;
	return skillPair->second;
}
std::map<std::string, int>
guild_of_heroes::Hero::getSkills() const
{
	return skills;
}
int
guild_of_heroes::Hero::getSkillsCount() const {
	return skills.size();
}

int
guild_of_heroes::Hero::getDailyFee() const
{
	return dailyFee;
}

void
guild_of_heroes::Hero::setDailyFee(int fee)
{
	this->dailyFee = fee;
}

void 
guild_of_heroes::Hero::addSkill(const std::string& skillName, int skillValue)
{
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

guild_of_heroes::Hero::Hero(const std::string& name) {
	this->name = name;
}