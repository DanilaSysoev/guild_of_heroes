#include "..\include\nameable.hpp"

std::string
guild_of_heroes::Nameable::getName() const {
	return name;
}

guild_of_heroes::Nameable::~Nameable()
{ }

guild_of_heroes::Nameable::Nameable(const std::string& name) {
	this->name = name;
}
