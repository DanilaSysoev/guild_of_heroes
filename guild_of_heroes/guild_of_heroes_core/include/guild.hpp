#pragma once
#include <vector>
#include <set>
#include <algorithm>
#include "nameable.hpp"


namespace guild_of_heroes {
	class Hero;

	class Guild : public Nameable {
	public:
		Int getHeroesCount() const;
		std::vector<Hero*> getHeroes() const;
		void addHero(Hero* hero);
		void removeHero(Hero* hero);

		static Guild* create(const std::string& name);


	private:
		Guild(const std::string& name);

		std::vector<Hero*> heroes;
	};
}