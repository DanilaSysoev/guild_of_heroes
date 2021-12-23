#include <stdexcept>
#include "guild.hpp"

Int
guild_of_heroes::Guild::getHeroesCount() const
{
	return 0;
}

guild_of_heroes::Guild*
guild_of_heroes::Guild::create(const std::string& name) {
	if (name.empty())
		throw std::invalid_argument("name can not be empty");
	return new Guild(name);
}

guild_of_heroes::Guild::Guild(const std::string& name)
	: Nameable(name) 
{ }