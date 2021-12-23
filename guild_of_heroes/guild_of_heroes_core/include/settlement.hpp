#pragma once
#include <map>
#include "nameable.hpp"

namespace guild_of_heroes {
	class Settlement : public Nameable {
	public:
		Int getSize() const;
		Int getMaxSizeInPast() const;
		void setSize(Int newSize);
		bool isAbandoned() const;
		Int getRaceWeight(const std::string& race) const;
		void setRaceWeight(const std::string& race, Int weight);
		bool existOnlyOneRace();

		static Settlement* create(const std::string& name);

	private:
		Settlement(const std::string& name);
		bool raceExist(std::map<std::string, Int>::iterator& raceWeight);

		Int size;
		Int maxSizeInPast;
		std::map<std::string, Int> racesWeights;
	};
}