#include <stdexcept>
#include "guild.hpp"

std::string
guild_of_heroes::Guild::getName() const {
	return name;
}

guild_of_heroes::Guild*
guild_of_heroes::Guild::create(const std::string& name) {
	if (name.empty())
		throw std::invalid_argument("name can not be empty");
	return new Guild(name);
}

guild_of_heroes::Guild::Guild(const std::string& name) {
	this->name = name;
}