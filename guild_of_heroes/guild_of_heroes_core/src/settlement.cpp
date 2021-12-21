#include <stdexcept>
#include "settlement.hpp"

int
guild_of_heroes::Settlement::getSize() const {
	return size;
}

void
guild_of_heroes::Settlement::setSize(int size) {
	if (size < 0)
		throw std::invalid_argument("Settlement size can not be negative");
	this->size = size;
}

guild_of_heroes::Settlement*
guild_of_heroes::Settlement::create(const std::string& name) {
	if (name.empty())
		throw std::invalid_argument("name can not be empty");
	return new Settlement(name);
}

guild_of_heroes::Settlement::Settlement(const std::string& name)
	: Nameable(name) {
	this->size = 1;
}