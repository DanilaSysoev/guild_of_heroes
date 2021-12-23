#pragma once
#include <vector>
#include "nameable.hpp"


namespace guild_of_heroes {
	class Hero;

	class Guild : public Nameable {
	public:
		Int getHeroesCount() const;
		std::vector<Hero*> getHeroes() const;

		static Guild* create(const std::string& name);


	private:
		Guild(const std::string& name);

		std::vector<Hero*> heroes;
	};
}