#include "guild.hpp"

guild_of_heroes::Guild*
guild_of_heroes::Guild::create(const std::string& name) {
	return new Guild(name);
}

guild_of_heroes::Guild::Guild(const std::string& name) {
	this->name = name;
}