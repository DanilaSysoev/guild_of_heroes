#pragma once
#include <map>
#include "nameable.hpp"

namespace guild_of_heroes {
	class Settlement : public Nameable {
	public:
		int getSize() const;
		int getMaxSizeInPast() const;
		void setSize(int size);
		bool isAbandoned() const;
		int getRaceWeight(const std::string& race) const;
		void setRaceWeight(const std::string& race, int weight);

		static Settlement* create(const std::string& name);

	private:
		Settlement(const std::string& name);

		int size;
		int maxSizeInPast;
		std::map<std::string, int> racesWeights;
	};
}