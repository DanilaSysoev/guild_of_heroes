#include <stdexcept>
#include "hero.hpp"

std::string
guild_of_heroes::Hero::getName() const {
	return name;
}
int
guild_of_heroes::Hero::getSkillsCount() const {
	return 0;
}

guild_of_heroes::Hero*
guild_of_heroes::Hero::create(const std::string& name) {
	if (name.empty())
		throw std::invalid_argument("name");
	return new Hero(name);
}

guild_of_heroes::Hero::Hero(const std::string& name) {
	this->name = name;
}