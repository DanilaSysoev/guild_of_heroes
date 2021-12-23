#include <stdexcept>
#include "guild.hpp"

Int
guild_of_heroes::Guild::getHeroesCount() const
{
	return heroes.size();
}

std::vector<guild_of_heroes::Hero*>
guild_of_heroes::Guild::getHeroes() const
{
	return heroes;
}

void
guild_of_heroes::Guild::addHero(Hero* hero)
{
	if (hero == nullptr)
		throw std::invalid_argument("Hero can not be null");
	heroes.push_back(hero);
}

void
guild_of_heroes::Guild::removeHero(Hero* hero)
{
	auto item = std::find(heroes.begin(), heroes.end(), hero);
	if (item == heroes.end())
		throw std::logic_error("attempt to delete non-existent hero");
	heroes.erase(item);
}

guild_of_heroes::Guild*
guild_of_heroes::Guild::create(const std::string& name) {
	if (name.empty())
		throw std::invalid_argument("name can not be empty");
	return new Guild(name);
}

guild_of_heroes::Guild::Guild(const std::string& name)
	: Nameable(name), heroes()
{ }