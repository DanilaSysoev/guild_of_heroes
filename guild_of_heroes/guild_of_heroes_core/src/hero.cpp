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
int
guild_of_heroes::Hero::getSkillsCount() const {
	return skills.size();
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