#include <stdexcept>
#include "settlement.hpp"

int
guild_of_heroes::Settlement::getSize() const {
	return size;
}

int
guild_of_heroes::Settlement::getMaxSizeInPast() const {
	return maxSizeInPast;
}

void
guild_of_heroes::Settlement::setSize(int size) {
	if (size < 0)
		throw std::invalid_argument("Settlement size can not be negative");
	this->size = size;
	if (size > maxSizeInPast)
		maxSizeInPast = size;
}

bool
guild_of_heroes::Settlement::isAbandoned() const {
	return size == 0;
}

int
guild_of_heroes::Settlement::getRaceWeight(const std::string& race) const
{
	if(racesWeights.empty())
		throw std::logic_error("Settlement state error: races not setupped");
	auto raceWeight = racesWeights.find(race);
	if (raceWeight == racesWeights.end())
		return 0;
	return raceWeight->second;
}

void
guild_of_heroes::Settlement::setRaceWeight(const std::string& race, int weight)
{
	if (weight < 0)
		throw std::invalid_argument("Race weight can not be negative");
	if(race.empty())
		throw std::invalid_argument("Race name can not be empty");

	auto raceWeight = racesWeights.find(race);
	if (weight == 0 && raceExist(raceWeight)) {
		if (existOnlyOneRace())
			throw std::logic_error("Impossible remove last race");
		racesWeights.erase(raceWeight);
	}
	else if(weight != 0)
		racesWeights[race] = weight;
}

bool
guild_of_heroes::Settlement::existOnlyOneRace() {
	return racesWeights.size() == 1;
}

bool
guild_of_heroes::Settlement::raceExist(
	std::map<std::string, int>::iterator& raceWeight
) {
	return raceWeight != racesWeights.end();
}

guild_of_heroes::Settlement*
guild_of_heroes::Settlement::create(const std::string& name) {
	if (name.empty())
		throw std::invalid_argument("name can not be empty");
	return new Settlement(name);
}

guild_of_heroes::Settlement::Settlement(const std::string& name)
	: Nameable(name), size(1), maxSizeInPast(1) {
}